namespace VideoBrowser.Core
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="VideoFormat" />
    /// </summary>
    public class VideoFormat : INotifyPropertyChanged
    {
        #region Fields

        internal long _fileSize;

        private WebRequest request;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoFormat"/> class.
        /// </summary>
        /// <param name="videoInfo">The videoInfo<see cref="VideoInfo"/></param>
        /// <param name="token">The token<see cref="JToken"/></param>
        public VideoFormat(VideoInfo videoInfo, JToken token)
        {
            this.AudioBitRate = -1;
            this.VideoInfo = videoInfo;
            this.DeserializeJson(token);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the ACodec
        /// </summary>
        public string ACodec { get; private set; }

        /// <summary>
        /// Gets the audio bit rate. Returns -1 if not defined.
        /// </summary>
        public int AudioBitRate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether AudioOnly
        /// Gets whether the format is audio only.
        /// </summary>
        public bool AudioOnly { get; private set; }

        /// <summary>
        /// Gets a value indicating whether DASH
        /// Gets whether format is a DASH format.
        /// </summary>
        public bool DASH { get; private set; }

        /// <summary>
        /// Gets the download url.
        /// </summary>
        public string DownloadUrl { get; private set; }

        /// <summary>
        /// Gets the file extension, excluding the period.
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the file size as bytes count.
        /// </summary>
        public long FileSize
        {
            get { return _fileSize; }
            private set
            {
                _fileSize = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the format text.
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Gets the format ID.
        /// </summary>
        public string FormatID { get; private set; }

        /// <summary>
        /// Gets the frames per second. Null if not defined.
        /// </summary>
        public string FPS { get; private set; }

        /// <summary>
        /// Gets a value indicating whether HasAudioAndVideo
        /// </summary>
        public bool HasAudioAndVideo => !this.AudioOnly && !this.VideoOnly;

        /// <summary>
        /// Gets the format title, displaying some basic information.
        /// </summary>
        public string Title => this.ToString();

        /// <summary>
        /// Gets the VCodec
        /// </summary>
        public string VCodec { get; private set; }

        /// <summary>
        /// Gets the associated VideoInfo.
        /// </summary>
        public VideoInfo VideoInfo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether VideoOnly
        /// </summary>
        public bool VideoOnly { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Aborts request for file size.
        /// </summary>
        public void AbortUpdateFileSize()
        {
            if (request != null)
            {
                request.Abort();
            }
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.OnPropertyChangedExplicit(propertyName);
        }

        /// <summary>
        /// The OnPropertyChangedExplicit
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        public void OnPropertyChangedExplicit(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The ToString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public override string ToString()
        {
            var text = string.Empty;
            if (this.AudioOnly)
            {
                text = this.AudioBitRate > -1
                    ? string.Format("Audio Only - {0} kbps (.{1})", this.AudioBitRate, this.Extension)
                    : string.Format("Audio Only (.{0})", this.Extension);
            }
            else
            {
                var fps = !string.IsNullOrEmpty(this.FPS) ? $" - {this.FPS}fps" : string.Empty;
                text = string.Format("{0}{1} (.{2})", this.Format, fps, this.Extension);
            }

            return text;
        }

        /// <summary>
        /// Starts a WebRequest to update the file size.
        /// </summary>
        public async void UpdateFileSizeAsync()
        {
            if (this.FileSize > 0)
            {
                // Probably already got the file size from .json file.
                this.VideoInfo.OnFileSizeUpdated(this);
                return;
            }

            WebResponse response = null;
            try
            {
                request = WebRequest.Create(this.DownloadUrl);
                request.Method = "HEAD";
                response = await request.GetResponseAsync();

                var bytes = response.ContentLength;
                this.FileSize = bytes;
                this.VideoInfo.OnFileSizeUpdated(this);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Canceled update file size");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Update file size error");
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        /// <summary>
        /// The DeserializeJson
        /// </summary>
        /// <param name="token">The token<see cref="JToken"/></param>
        private void DeserializeJson(JToken token)
        {
            this.ACodec = token["acodec"]?.ToString();
            this.VCodec = token["vcodec"]?.ToString();

            this.DownloadUrl = token["url"].ToString();

            var formatnote = token.SelectToken("format_note");
            if (formatnote != null)
            {
                this.DASH = token["format_note"].ToString().ToLower().Contains("dash");
            }

            this.Extension = token["ext"].ToString();
            this.Format = Regex.Match(token["format"].ToString(), @".*-\s([^\(\n]*)").Groups[1].Value.Trim();
            this.FormatID = token["format_id"].ToString();

            // Check if format is audio only or video only
            this.AudioOnly = this.Format.Contains("audio only");
            this.VideoOnly = this.ACodec == "none";

            // Check for abr token (audio bit rate?)
            var abr = token.SelectToken("abr");
            if (abr != null)
            {
                this.AudioBitRate = int.Parse(abr.ToString());
            }

            // Check for filesize token
            var filesize = token.SelectToken("filesize");
            if (filesize != null && !string.IsNullOrEmpty(filesize.ToString()))
            {
                this.FileSize = long.Parse(filesize.ToString());
            }

            // Check for 60fps videos. If there is no 'fps' token, default to 30fps.
            var fps = token.SelectToken("fps", false);

            this.FPS = fps == null || fps.ToString() == "null" ? string.Empty : fps.ToString();
            this.UpdateFileSizeAsync();
        }

        #endregion Methods
    }
}