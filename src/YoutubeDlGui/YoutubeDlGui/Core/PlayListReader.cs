namespace YoutubeDlGui.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;
    using YoutubeDlGui.Common;
    using YoutubeDlGui.Helpers;

    namespace YouTube_Downloader_DLL.Classes
    {
        /// <summary>
        /// Defines the <see cref="PlaylistReader" />
        /// </summary>
        public class PlaylistReader
        {
            #region Constants

            public const string CmdPlaylistInfo = " -i -o \"{0}\\playlist-{1}\\%(playlist_index)s-%(title)s\" --restrict-filenames --skip-download --write-info-json{2}{3} \"{4}\"";

            public const string CmdPlaylistRange = " --playlist-items {0}";

            public const string CmdPlaylistReverse = " --playlist-reverse";

            #endregion Constants

            #region Fields

            private readonly string _arguments;

            private readonly string _playlist_id;

            private readonly string _url;

            private CancellationTokenSource _cts = new CancellationTokenSource();

            private string _currentVideoID;

            private int _currentVideoPlaylistIndex = -1;

            private int _index = 0;

            private List<string> _jsonPaths = new List<string>();

            private bool _processFinished = false;

            private Regex _regexPlaylistIndex = new Regex(@"\[download\]\s\w*\s\w*\s(\d+)", RegexOptions.Compiled);

            private Regex _regexPlaylistInfo = new Regex(@"^\[youtube:playlist\] playlist (.*):.*Downloading\s+(\d+)\s+.*$", RegexOptions.Compiled);

            private Regex _regexVideoID = new Regex(@"\[youtube\]\s(.*):", RegexOptions.Compiled);

            private Regex _regexVideoJson = new Regex(@"^\[info\].*JSON.*:\s(.*)$", RegexOptions.Compiled);

            private Process _youtubeDl;

            #endregion Fields

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="PlaylistReader"/> class.
            /// </summary>
            /// <param name="url">The url<see cref="string"/></param>
            /// <param name="videos">The videos<see cref="int[]"/></param>
            /// <param name="reverse">The reverse<see cref="bool"/></param>
            public PlaylistReader(string url, int[] videos, bool reverse)
            {
                var json_dir = AppEnvironment.GetJsonDirectory();
                _playlist_id = YoutubeHelper.GetPlaylistId(url);
                var range = string.Empty;

                if (videos != null && videos.Length > 0)
                {
                    // Make sure the video indexes is sorted, otherwise reversing wont do anything
                    Array.Sort(videos);
                    range = string.Format(CmdPlaylistRange, string.Join(",", videos));
                }

                var reverseS = reverse ? CmdPlaylistReverse : string.Empty;
                _arguments = string.Format(CmdPlaylistInfo, json_dir, _playlist_id, range, reverseS, url);
                _url = url;

                YoutubeDl.LogHeader(_arguments);

                _youtubeDl = ProcessHelper.StartProcess(YoutubeDl.YouTubeDlPath,
                    _arguments,
                    OutputReadLine,
                    ErrorReadLine,
                    null);
                _youtubeDl.Exited += delegate
                {
                    _processFinished = true;
                    YoutubeDl.LogFooter();
                };
            }

            #endregion Constructors

            #region Properties

            /// <summary>
            /// Gets or sets a value indicating whether Canceled
            /// </summary>
            public bool Canceled { get; set; } = false;

            /// <summary>
            /// Gets or sets the PlayList
            /// </summary>
            public PlayList PlayList { get; set; } = null;

            #endregion Properties

            #region Methods

            /// <summary>
            /// The ErrorReadLine
            /// </summary>
            /// <param name="process">The process<see cref="Process"/></param>
            /// <param name="line">The line<see cref="string"/></param>
            public void ErrorReadLine(Process process, string line)
            {
                _jsonPaths.Add($"[ERROR:{_currentVideoID}] {line}");
            }

            /// <summary>
            /// The Next
            /// </summary>
            /// <returns>The <see cref="VideoInfo"/></returns>
            public VideoInfo Next()
            {
                var attempts = 0;
                string jsonPath = null;
                VideoInfo video = null;

                while (!_processFinished)
                {
                    if (_jsonPaths.Count > _index)
                    {
                        jsonPath = _jsonPaths[_index];
                        _index++;
                        break;
                    }
                }

                // If it's the end of the stream finish up the process.
                if (jsonPath == null)
                {
                    if (!_youtubeDl.HasExited)
                    {
                        _youtubeDl.WaitForExit();
                    }

                    return null;
                }

                // Sometimes youtube-dl is slower to create the json file, try a couple times
                while (attempts < 10)
                {
                    if (_cts.IsCancellationRequested)
                    {
                        return null;
                    }

                    if (jsonPath.StartsWith("[ERROR"))
                    {
                        var match = new Regex(@"\[ERROR:(.*)]\s(.*)").Match(jsonPath);
                        video = new VideoInfo
                        {
                            Id = match.Groups[1].Value,
                            Failure = true,
                            FailureReason = match.Groups[2].Value
                        };
                        break;
                    }
                    else
                    {
                        try
                        {
                            video = new VideoInfo(jsonPath)
                            {
                                PlaylistIndex = _currentVideoPlaylistIndex
                            };

                            break;
                        }
                        catch (IOException)
                        {
                            attempts++;
                            Thread.Sleep(100);
                        }
                    }
                }

                if (video == null)
                {
                    throw new FileNotFoundException("File not found.", jsonPath);
                }

                this.PlayList.Videos.Add(video);
                return video;
            }

            /// <summary>
            /// The OutputReadLine
            /// </summary>
            /// <param name="process">The process<see cref="Process"/></param>
            /// <param name="line">The line<see cref="string"/></param>
            public void OutputReadLine(Process process, string line)
            {
                Match m;

                if (line.StartsWith("[youtube:playlist]"))
                {
                    if ((m = _regexPlaylistInfo.Match(line)).Success)
                    {
                        // Get the playlist info
                        var name = m.Groups[1].Value;
                        var onlineCount = int.Parse(m.Groups[2].Value);

                        this.PlayList = new PlayList(_playlist_id, name, onlineCount);
                    }
                }
                else if (line.StartsWith("[info]"))
                {
                    // New json found, break & create a VideoInfo instance
                    if ((m = _regexVideoJson.Match(line)).Success)
                    {
                        _jsonPaths.Add(m.Groups[1].Value.Trim());
                    }
                }
                else if (line.StartsWith("[download]"))
                {
                    if ((m = _regexPlaylistIndex.Match(line)).Success)
                    {
                        var i = -1;
                        if (int.TryParse(m.Groups[1].Value, out i))
                        {
                            _currentVideoPlaylistIndex = i;
                        }
                        else
                        {
                            throw new Exception($"PlaylistReader: Couldn't parse '{m.Groups[1].Value}' to integer for '{nameof(_currentVideoPlaylistIndex)}'");
                        }
                    }
                }
                else if (line.StartsWith("[youtube]"))
                {
                    if ((m = _regexVideoID.Match(line)).Success)
                    {
                        _currentVideoID = m.Groups[1].Value;
                    }
                }
            }

            /// <summary>
            /// The Stop
            /// </summary>
            public void Stop()
            {
                _youtubeDl.Kill();
                _cts.Cancel();

                this.Canceled = true;
            }

            /// <summary>
            /// The WaitForPlaylist
            /// </summary>
            /// <param name="timeoutMS">The timeoutMS<see cref="int"/></param>
            /// <returns>The <see cref="PlayList"/></returns>
            public PlayList WaitForPlaylist(int timeoutMS = 30000)
            {
                var sw = new Stopwatch();
                Exception exception = null;

                sw.Start();

                while (this.PlayList == null)
                {
                    if (_cts.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    Thread.Sleep(50);

                    if (sw.ElapsedMilliseconds > timeoutMS)
                    {
                        exception = new TimeoutException("Couldn't get Play list information.");
                        break;
                    }
                }

                if (exception != null)
                    throw exception;

                return this.PlayList;
            }

            #endregion Methods
        }
    }
}