namespace VideoBrowser.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using VideoBrowser.Common;
    using VideoBrowser.Extensions;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="YoutubeDl" />.
    /// </summary>
    public static class YoutubeDl
    {
        #region Constants

        private const string ErrorSignIn = "YouTube said: Please sign in to view this video.";

        #endregion Constants

        #region Fields

        public static string YouTubeDlPath = Path.Combine(AppEnvironment.AppBinaryDirectory, "youtube-dl.exe");

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets current youtube-dl version.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetVersion()
        {
            string version = string.Empty;
            ProcessHelper.StartProcess(YouTubeDlPath, Commands.Version,
                delegate (Process process, string line)
                {
                    // Only one line gets printed, so assume any non-empty line is the version
                    if (!string.IsNullOrEmpty(line))
                        version = line.Trim();
                },
                null, null).WaitForExit();

            return version;
        }

        /// <summary>
        /// Returns a <see cref="VideoInfo"/> of the given video.
        /// </summary>
        /// <param name="url">The url to the video.</param>
        /// <param name="authentication">The authentication<see cref="YoutubeAuthentication"/>.</param>
        /// <returns>The <see cref="VideoInfo"/>.</returns>
        public static VideoInfo GetVideoInfo(string url,
                                             YoutubeAuthentication authentication = null)
        {
            string json_dir = AppEnvironment.GetJsonDirectory();
            string json_file = string.Empty;
            string arguments = string.Format(Commands.GetJsonInfo,
                json_dir,
                url,
                authentication == null ? string.Empty : authentication.ToCmdArgument());
            VideoInfo video = new VideoInfo();

            LogHeader(arguments);

            ProcessHelper.StartProcess(YouTubeDlPath, arguments,
                delegate (Process process, string line)
                {
                    line = line.Trim();

                    if (line.StartsWith("[info] Writing video description metadata as JSON to:"))
                    {
                        // Store file path
                        json_file = line.Substring(line.IndexOf(":") + 1).Trim();
                    }
                    else if (line.Contains("Refetching age-gated info webpage"))
                    {
                        video.RequiresAuthentication = true;
                    }
                },
                delegate (Process process, string error)
                {
                    error = error.Trim();

                    if (error.Contains(ErrorSignIn))
                    {
                        video.RequiresAuthentication = true;
                    }
                    else if (error.StartsWith("ERROR:"))
                    {
                        video.Failure = true;
                        video.FailureReason = error.Substring("ERROR: ".Length);
                    }
                }, null)
                .WaitForExit();

            if (!video.Failure && !video.RequiresAuthentication)
            {
                video.DeserializeJson(json_file);
            }

            return video;
        }

        /// <summary>
        /// The GetVideoInfoBatchAsync.
        /// </summary>
        /// <param name="urls">The urls<see cref="ICollection{string}"/>.</param>
        /// <param name="videoReady">The videoReady<see cref="Action{VideoInfo}"/>.</param>
        /// <param name="authentication">The authentication<see cref="YoutubeAuthentication"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task GetVideoInfoBatchAsync(ICollection<string> urls,
                                                        Action<VideoInfo> videoReady,
                                                        YoutubeAuthentication authentication = null)
        {
            string json_dir = AppEnvironment.GetJsonDirectory();
            string arguments = string.Format(Commands.GetJsonInfoBatch, json_dir, string.Join(" ", urls));
            var videos = new OrderedDictionary();
            var jsonFiles = new Dictionary<string, string>();
            var findVideoID = new Regex(@"(?:\]|ERROR:)\s(.{11}):", RegexOptions.Compiled);
            var findVideoIDJson = new Regex(@":\s.*\\(.{11})_", RegexOptions.Compiled);

            LogHeader(arguments);
            await Task.Run(() =>
            {
                ProcessHelper.StartProcess(YouTubeDlPath, arguments,
                        (Process process, string line) =>
                        {
                            line = line.Trim();
                            Match m;
                            string id;
                            VideoInfo video = null;

                            if ((m = findVideoID.Match(line)).Success)
                            {
                                id = findVideoID.Match(line).Groups[1].Value;
                                video = videos.Get<VideoInfo>(id, new VideoInfo() { Id = id });
                            }

                            if (line.StartsWith("[info] Writing video description metadata as JSON to:"))
                            {
                                id = findVideoIDJson.Match(line).Groups[1].Value;
                                var jsonFile = line.Substring(line.IndexOf(":") + 1).Trim();
                                jsonFiles.Put(id, jsonFile);

                                video = videos[id] as VideoInfo;
                                video.DeserializeJson(jsonFile);
                                videoReady(video);
                            }
                            else if (line.Contains("Refetching age-gated info webpage"))
                            {
                                video.RequiresAuthentication = true;
                            }
                        },
                        (Process process, string error) =>
                        {
                            error = error.Trim();
                            var id = findVideoID.Match(error).Groups[1].Value;
                            var video = videos.Get<VideoInfo>(id, new VideoInfo() { Id = id });

                            if (error.Contains(ErrorSignIn))
                            {
                                video.RequiresAuthentication = true;
                            }
                            else if (error.StartsWith("ERROR:"))
                            {
                                video.Failure = true;
                                video.FailureReason = error.Substring("ERROR: ".Length);
                            }
                        }, null)
                    .WaitForExit();
            });
        }

        /// <summary>
        /// Writes log footer to log.
        /// </summary>
        public static void LogFooter()
        {
            // Write log footer to stream.
            // Possibly write elapsed time and/or error in future.
            Logger.Info("-" + Environment.NewLine);
        }

        /// <summary>
        /// Writes log header to log.
        /// </summary>
        /// <param name="arguments">The arguments to log in header.</param>
        /// <param name="caller">The caller<see cref="string"/>.</param>
        public static void LogHeader(string arguments, [CallerMemberName]string caller = "")
        {
            Logger.Info("[" + DateTime.Now + "]");
            Logger.Info("version: " + GetVersion());
            Logger.Info("caller: " + caller);
            Logger.Info("cmd: " + arguments.Trim());
            Logger.Info(string.Empty);
            Logger.Info("OUTPUT");
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <returns>The <see cref="Task{string}"/>.</returns>
        public static async Task<string> Update()
        {
            var returnMsg = string.Empty;
            var versionRegex = new Regex(@"(\d{4}\.\d{2}\.\d{2})");

            await Task.Run(delegate
            {
                ProcessHelper.StartProcess(YouTubeDlPath, Commands.Update,
                    delegate (Process process, string line)
                    {
                        Match m;
                        if ((m = versionRegex.Match(line)).Success)
                            returnMsg = m.Groups[1].Value;
                    },
                    delegate (Process process, string line)
                    {
                        returnMsg = "Failed";
                    }, null)
                    .WaitForExit();
            });

            return returnMsg;
        }

        #endregion Methods
    }
}