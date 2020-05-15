namespace YoutubeDlGui.Core
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="VideoInfo" />
    /// </summary>
    public class VideoInfo : INotifyPropertyChanged
    {
        #region Fields

        internal long _duration = 0;

        internal string _thumbnailUrl = string.Empty;

        internal string _title = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoInfo"/> class.
        /// </summary>
        public VideoInfo()
        {
            this.Formats = new List<VideoFormat>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoInfo"/> class.
        /// </summary>
        /// <param name="json_file">The json_file<see cref="string"/></param>
        public VideoInfo(string json_file)
            : this()
        {
            this.DeserializeJson(json_file);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when one of the format's file size has been updated.
        /// </summary>
        public event FileSizeUpdateHandler FileSizeUpdated;

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the Duration
        /// Gets the video duration in seconds.
        /// </summary>
        public long Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Failure
        /// Gets or sets whether there was a failure retrieving video information.
        /// </summary>
        public bool Failure { get; set; }

        /// <summary>
        /// Gets or sets the reason for failure retrieving video information.
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// Gets the Formats
        /// Gets all the available formats.
        /// </summary>
        public List<VideoFormat> Formats { get; private set; }

        /// <summary>
        /// Gets or sets the video Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the playlist index. Default value is -1.
        /// </summary>
        public int PlaylistIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether RequiresAuthentication
        /// Gets or sets whether authentication is required to get video information.
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the ThumbnailUrl
        /// Gets the video thumbnail url.
        /// </summary>
        public string ThumbnailUrl
        {
            get => _thumbnailUrl;
            set
            {
                _thumbnailUrl = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Title
        /// Gets the video title.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the video url.
        /// </summary>
        public string Url { get; private set; }

        /////// <summary>
        /////// Gets the video source (Twitch/YouTube).
        /////// </summary>
        ////public VideoSource VideoSource { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Aborts all the requests for file size for each video format.
        /// </summary>
        public void AbortUpdateFileSizes()
        {
            foreach (VideoFormat format in this.Formats)
            {
                format.AbortUpdateFileSize();
            }
        }

        /// <summary>
        /// The DeserializeJson
        /// </summary>
        /// <param name="json_file">The json_file<see cref="string"/></param>
        public void DeserializeJson(string json_file)
        {
            var raw_json = ReadJSON(json_file);
            var json = JObject.Parse(raw_json);

            this.Duration = json.Value<long>("duration");
            this.Title = json.Value<string>("fulltitle");
            this.Id = json.Value<string>("id");

            var displayId = json.Value<string>("display_id");
            this.ThumbnailUrl = string.Format("https://i.ytimg.com/vi/{0}/mqdefault.jpg", displayId);
            this.Url = json.Value<string>("webpage_url");

            foreach (JToken token in (JArray)json["formats"])
            {
                this.Formats.Add(new VideoFormat(this, token));
            }
        }

        /// <summary>
        /// The OnFileSizeUpdated
        /// </summary>
        /// <param name="videoFormat">The videoFormat<see cref="VideoFormat"/></param>
        internal void OnFileSizeUpdated(VideoFormat videoFormat)
        {
            this.FileSizeUpdated?.Invoke(this, new FileSizeUpdateEventArgs(videoFormat));
        }

        /// <summary>
        /// The ReadJSON
        /// </summary>
        /// <param name="json_file">The json_file<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string ReadJSON(string json_file)
        {
            var json = string.Empty;

            // Should try for about 10 seconds. */
            var attempts = 0;
            var maxAttempts = 20;

            while ((attempts++) <= maxAttempts)
            {
                try
                {
                    json = File.ReadAllText(json_file);
                    break;
                }
                catch (IOException ex)
                {
                    if (ex is FileNotFoundException || ex.Message.EndsWith("because it is being used by another process."))
                    {
                        Console.WriteLine(ex);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return json;
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChangedExplicit(propertyName);
        }

        /// <summary>
        /// The OnPropertyChangedExplicit
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        private void OnPropertyChangedExplicit(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}