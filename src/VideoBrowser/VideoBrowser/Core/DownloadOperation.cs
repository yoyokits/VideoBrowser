namespace VideoBrowser.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;
    using VideoBrowser.Common;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="DownloadOperation" />
    /// </summary>
    public class DownloadOperation : Operation
    {
        #region Constants

        private const string RegexAddToFilename = @"^(\w:.*\\.*)(\..*)$";

        #endregion Constants

        #region Fields

        private readonly bool _combine;

        private bool _downloadSuccessful;

        private bool _processing;

        private FileDownloader downloader;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadOperation"/> class.
        /// </summary>
        /// <param name="format">The format<see cref="VideoFormat"/></param>
        /// <param name="output">The output<see cref="string"/></param>
        public DownloadOperation(VideoFormat format,
                                 string output)
            : this(format)
        {
            this.Input = format.DownloadUrl;
            this.Output = output;
            this.Title = Path.GetFileName(this.Output);

            downloader.Files.Add(new FileDownload(this.Output, this.Input));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadOperation"/> class.
        /// </summary>
        /// <param name="video">The video<see cref="VideoFormat"/></param>
        /// <param name="audio">The audio<see cref="VideoFormat"/></param>
        /// <param name="output">The output<see cref="string"/></param>
        public DownloadOperation(VideoFormat video,
                                 VideoFormat audio,
                                 string output)
            : this(video)
        {
            _combine = true;
            this.Input = $"{audio.DownloadUrl}|{video.DownloadUrl}";
            this.Output = output;
            this.FileSize += audio.FileSize;
            this.Title = Path.GetFileName(this.Output);

            this.Formats.Add(audio);

            var regex = new Regex(RegexAddToFilename);

            downloader.Files.Add(new FileDownload(regex.Replace(this.Output, "$1_audio$2"),
                                                  audio.DownloadUrl,
                                                  true));
            downloader.Files.Add(new FileDownload(regex.Replace(this.Output, "$1_video$2"),
                                                  video.DownloadUrl,
                                                  true));

            // Delete _audio and _video files in case they exists from a previous attempt
            FileHelper.DeleteFiles(downloader.Files[0].Path,
                               downloader.Files[1].Path);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DownloadOperation"/> class from being created.
        /// </summary>
        private DownloadOperation()
        {
            downloader = new FileDownloader();
            // Attach events
            downloader.Canceled += downloader_Canceled;
            downloader.Completed += downloader_Completed;
            downloader.FileDownloadFailed += downloader_FileDownloadFailed;
            downloader.CalculatedTotalFileSize += downloader_CalculatedTotalFileSize;
            downloader.ProgressChanged += downloader_ProgressChanged;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DownloadOperation"/> class from being created.
        /// </summary>
        /// <param name="format">The format<see cref="VideoFormat"/></param>
        private DownloadOperation(VideoFormat format)
            : this()
        {
            this.ReportsProgress = true;
            this.Duration = format.VideoInfo.Duration;
            this.FileSize = format.FileSize;
            this.Link = format.VideoInfo.Url;
            this.Thumbnail = format.VideoInfo.ThumbnailUrl;

            this.Formats.Add(format);
            this.Video = format.VideoInfo;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Formats
        /// </summary>
        public List<VideoFormat> Formats { get; } = new List<VideoFormat>();

        /// <summary>
        /// Gets the Video
        /// </summary>
        public VideoInfo Video { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CanOpen
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool CanOpen() => this.IsSuccessful;

        /// <summary>
        /// The CanPause
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool CanPause()
        {
            // Only downloader can pause.
            return downloader?.CanPause == true && this.IsWorking;
        }

        /// <summary>
        /// The CanResume
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool CanResume()
        {
            // Only downloader can resume.
            return downloader?.CanResume == true && (this.IsPaused || this.IsQueued);
        }

        /// <summary>
        /// The CanStop
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool CanStop() => this.IsPaused || this.IsWorking || this.IsQueued;

        /// <summary>
        /// The Dispose
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            downloader?.Dispose();
            downloader = null;
        }

        /// <summary>
        /// The Open
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Open()
        {
            try
            {
                Controls.CefSharpBrowser.Helpers.ProcessHelper.Start(this.Output);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The OpenContainingFolder
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool OpenContainingFolder()
        {
            try
            {
                Controls.CefSharpBrowser.Helpers.ProcessHelper.OpenFolder(this.Output);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The Pause
        /// </summary>
        public override void Pause()
        {
            downloader.Pause();
            this.Status = OperationStatus.Paused;
        }

        /// <summary>
        /// The Queue
        /// </summary>
        public override void Queue()
        {
            downloader.Pause();
            this.Status = OperationStatus.Queued;
        }

        /// <summary>
        /// The Stop
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Stop()
        {
            // Stop downloader if still running.
            if (downloader?.CanStop == true)
            {
                downloader.Stop();
            }

            // Don't set status to canceled if already successful.
            if (!this.IsSuccessful)
            {
                this.Status = OperationStatus.Canceled;
            }

            return true;
        }

        /// <summary>
        /// The ResumeInternal
        /// </summary>
        protected override void ResumeInternal()
        {
            downloader.Resume();
            this.Status = OperationStatus.Working;
        }

        /// <summary>
        /// The WorkerCompleted
        /// </summary>
        /// <param name="e">The e<see cref="RunWorkerCompletedEventArgs"/></param>
        protected override void WorkerCompleted(RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        /// The WorkerDoWork
        /// </summary>
        /// <param name="e">The e<see cref="DoWorkEventArgs"/></param>
        protected override void WorkerDoWork(DoWorkEventArgs e)
        {
            downloader.Start();
            while (downloader?.IsBusy == true)
            {
                Thread.Sleep(200);
            }

            if (_combine && _downloadSuccessful)
            {
                var audio = downloader.Files[0].Path;
                var video = downloader.Files[1].Path;

                this.ReportProgress(-1, new Dictionary<string, object>()
                {
                    { nameof(Progress), 0 }
                });
                this.ReportProgress(ProgressMax, null);

                try
                {
                    FFMpegResult<bool> result;

                    this.ReportProgress(-1, new Dictionary<string, object>()
                    {
                        { nameof(ProgressText), "Combining..." }
                    });

                    result = FFMpeg.Combine(video, audio, this.Output, delegate (int percentage)
                    {
                        // Combine progress
                        this.ReportProgress(percentage, null);
                    });

                    if (result.Value)
                    {
                        e.Result = OperationStatus.Success;
                    }
                    else
                    {
                        e.Result = OperationStatus.Failed;
                        this.ErrorsInternal.AddRange(result.Errors);
                    }

                    // Cleanup the separate audio and video files
                    FileHelper.DeleteFiles(audio, video);
                }
                catch (Exception ex)
                {
                    Logger.WriteException(ex);
                    e.Result = OperationStatus.Failed;
                }
            }
            else
            {
                e.Result = this.Status;
            }
        }

        /// <summary>
        /// The WorkerProgressChanged
        /// </summary>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/></param>
        protected override void WorkerProgressChanged(ProgressChangedEventArgs e)
        {
            if (e.UserState == null)
            {
                return;
            }

            // Used to set multiple properties
            if (e.UserState is Dictionary<string, object>)
            {
                foreach (var pair in (e.UserState as Dictionary<string, object>))
                {
                    this.GetType().GetProperty(pair.Key).SetValue(this, pair.Value);
                }
            }
        }

        /// <summary>
        /// The downloader_CalculatedTotalFileSize
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void downloader_CalculatedTotalFileSize(object sender, EventArgs e)
        {
            this.FileSize = downloader.TotalSize;
        }

        /// <summary>
        /// The downloader_Canceled
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void downloader_Canceled(object sender, EventArgs e)
        {
            if (this.Status == OperationStatus.Failed)
            {
                this.Status = OperationStatus.Canceled;
            }
        }

        /// <summary>
        /// The downloader_Completed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void downloader_Completed(object sender, EventArgs e)
        {
            // Set status to successful if no download(s) failed.
            if (this.Status != OperationStatus.Failed)
            {
                if (_combine)
                {
                    _downloadSuccessful = true;
                }
                else
                {
                    this.Status = OperationStatus.Success;
                }
            }
        }

        /// <summary>
        /// The downloader_FileDownloadFailed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FileDownloadFailedEventArgs"/></param>
        private void downloader_FileDownloadFailed(object sender, FileDownloadFailedEventArgs e)
        {
            // If one or more files fail, whole operation failed. Might handle it more
            // elegantly in the future.
            this.Status = OperationStatus.Failed;
            downloader.Stop();
        }

        /// <summary>
        /// The downloader_ProgressChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void downloader_ProgressChanged(object sender, EventArgs e)
        {
            if (_processing)
            {
                return;
            }

            try
            {
                _processing = true;

                var speed = string.Format(new ByteFormatProvider(), "{0:s}", downloader.Speed);
                var longETA = FileHelper.GetETA(downloader.Speed, downloader.TotalSize, downloader.TotalProgress);
                var ETA = longETA == 0 ? "" : "  " + TimeSpan.FromMilliseconds(longETA * 1000).ToString(@"hh\:mm\:ss");

                this.ETA = ETA;
                this.Speed = speed;
                this.Progress = downloader.TotalProgress;
                this.ReportProgress((int)downloader.TotalPercentage(), null);
            }
            catch { }
            finally
            {
                _processing = false;
            }
        }

        #endregion Methods
    }
}