namespace VideoBrowser.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Threading;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="FileDownloader" />
    /// </summary>
    public class FileDownloader : IDisposable
    {
        #region Fields

        private readonly bool _disposed = false;

        private BackgroundWorker _downloader;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDownloader"/> class.
        /// </summary>
        public FileDownloader()
        {
            _downloader = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _downloader.DoWork += downloader_DoWork;
            _downloader.ProgressChanged += downloader_ProgressChanged;
            _downloader.RunWorkerCompleted += downloader_RunWorkerCompleted;

            this.DeleteUnfinishedFilesOnCancel = true;
            this.Files = new List<FileDownload>();
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// The FileDownloadEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FileDownloadEventArgs"/></param>
        public delegate void FileDownloadEventHandler(object sender, FileDownloadEventArgs e);

        /// <summary>
        /// The FileDownloadFailedEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FileDownloadFailedEventArgs"/></param>
        public delegate void FileDownloadFailedEventHandler(object sender, FileDownloadFailedEventArgs e);

        #endregion Delegates

        #region Events

        /// <summary>
        /// Defines the CalculatedTotalFileSize
        /// </summary>
        public event EventHandler CalculatedTotalFileSize;

        /// <summary>
        /// Defines the Canceled
        /// </summary>
        public event EventHandler Canceled;

        /// <summary>
        /// Defines the Completed
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Defines the FileDownloadComplete
        /// </summary>
        public event FileDownloadEventHandler FileDownloadComplete;

        /// <summary>
        /// Defines the FileDownloadFailed
        /// </summary>
        public event FileDownloadFailedEventHandler FileDownloadFailed;

        /// <summary>
        /// Defines the FileDownloadSucceeded
        /// </summary>
        public event FileDownloadEventHandler FileDownloadSucceeded;

        /// <summary>
        /// Defines the Paused
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Defines the ProgressChanged
        /// </summary>
        public event EventHandler ProgressChanged;

        /// <summary>
        /// Defines the Resumed
        /// </summary>
        public event EventHandler Resumed;

        /// <summary>
        /// Defines the Started
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Defines the Stopped
        /// </summary>
        public event EventHandler Stopped;

        #endregion Events

        #region Enums

        /// <summary>
        /// Defines the BackgroundEvents
        /// </summary>
        private enum BackgroundEvents
        {
            /// <summary>
            /// Defines the CalculatedTotalFileSize
            /// </summary>
            CalculatedTotalFileSize,

            /// <summary>
            /// Defines the FileDownloadComplete
            /// </summary>
            FileDownloadComplete,

            /// <summary>
            /// Defines the FileDownloadSucceeded
            /// </summary>
            FileDownloadSucceeded,

            /// <summary>
            /// Defines the ProgressChanged
            /// </summary>
            ProgressChanged
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// Gets a value indicating whether CanPause
        /// </summary>
        public bool CanPause => this.IsBusy && !this.IsPaused && !_downloader.CancellationPending;

        /// <summary>
        /// Gets a value indicating whether CanResume
        /// </summary>
        public bool CanResume => this.IsBusy && this.IsPaused && !_downloader.CancellationPending;

        /// <summary>
        /// Gets a value indicating whether CanStart
        /// </summary>
        public bool CanStart => !this.IsBusy;

        /// <summary>
        /// Gets a value indicating whether CanStop
        /// </summary>
        public bool CanStop => this.IsBusy && !_downloader.CancellationPending;

        /// <summary>
        /// Gets the CurrentFile
        /// </summary>
        public FileDownload CurrentFile { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether DeleteUnfinishedFilesOnCancel
        /// </summary>
        public bool DeleteUnfinishedFilesOnCancel { get; set; }

        /// <summary>
        /// Gets or sets the Files
        /// </summary>
        public List<FileDownload> Files { get; set; }

        /// <summary>
        /// Gets a value indicating whether IsBusy
        /// </summary>
        public bool IsBusy { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsPaused
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Gets or sets the PackageSize
        /// </summary>
        public int PackageSize { get; set; } = 4096;

        /// <summary>
        /// Gets or sets the Speed
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the TotalProgress
        /// </summary>
        public long TotalProgress { get; set; }

        /// <summary>
        /// Gets the TotalSize
        /// </summary>
        public long TotalSize { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether WasCanceled
        /// </summary>
        public bool WasCanceled { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The Pause
        /// </summary>
        public void Pause()
        {
            if (!this.IsBusy || this.IsPaused)
            {
                return;
            }

            this.IsPaused = true;
            this.OnPaused();
        }

        /// <summary>
        /// The Resume
        /// </summary>
        public void Resume()
        {
            if (!this.IsBusy || !this.IsPaused)
            {
                return;
            }

            this.IsPaused = false;
            this.OnResumed();
        }

        /// <summary>
        /// The Start
        /// </summary>
        public void Start()
        {
            this.IsBusy = true;
            this.WasCanceled = false;
            this.TotalProgress = 0;

            _downloader.RunWorkerAsync();

            this.OnStarted();
        }

        /// <summary>
        /// The Stop
        /// </summary>
        public void Stop()
        {
            this.IsBusy = false;
            this.IsPaused = false;
            this.WasCanceled = true;

            _downloader.CancelAsync();

            this.OnCanceled();
        }

        /// <summary>
        /// The TotalPercentage
        /// </summary>
        /// <returns>The <see cref="double"/></returns>
        public double TotalPercentage()
        {
            return Math.Round((double)this.TotalProgress / this.TotalSize * 100, 2);
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects)
                    _downloader.Dispose();
                }

                // Free your own state (unmanaged objects)
                // Set large fields to null
                this.Files = null;
            }
        }

        /// <summary>
        /// The OnCalculatedTotalFileSize
        /// </summary>
        protected void OnCalculatedTotalFileSize() => this.CalculatedTotalFileSize?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnCanceled
        /// </summary>
        protected void OnCanceled() => this.Canceled?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnCompleted
        /// </summary>
        protected void OnCompleted() => this.Completed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnFileDownloadComplete
        /// </summary>
        /// <param name="e">The e<see cref="FileDownloadEventArgs"/></param>
        protected void OnFileDownloadComplete(FileDownloadEventArgs e) => this.FileDownloadComplete?.Invoke(this, e);

        /// <summary>
        /// The OnFileDownloadFailed
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/></param>
        /// <param name="fileDownload">The fileDownload<see cref="FileDownload"/></param>
        protected void OnFileDownloadFailed(Exception exception, FileDownload fileDownload) => this.FileDownloadFailed?.Invoke(this, new FileDownloadFailedEventArgs(exception, fileDownload));

        /// <summary>
        /// The OnFileDownloadSucceeded
        /// </summary>
        /// <param name="e">The e<see cref="FileDownloadEventArgs"/></param>
        protected void OnFileDownloadSucceeded(FileDownloadEventArgs e) => this.FileDownloadSucceeded?.Invoke(this, e);

        /// <summary>
        /// The OnPaused
        /// </summary>
        protected void OnPaused() => this.Paused?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnProgressChanged
        /// </summary>
        protected void OnProgressChanged() => this.ProgressChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnResumed
        /// </summary>
        protected void OnResumed() => this.Resumed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnStarted
        /// </summary>
        protected void OnStarted() => this.Started?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The OnStopped
        /// </summary>
        protected void OnStopped() => this.Stopped?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The CalculateTotalFileSize
        /// </summary>
        private void CalculateTotalFileSize()
        {
            this.TotalSize = 0;

            foreach (var file in this.Files)
            {
                try
                {
                    var webReq = WebRequest.Create(file.Url);
                    var webResp = webReq.GetResponse();

                    this.TotalSize += webResp.ContentLength;

                    webResp.Close();
                }
                catch (Exception) { }
            }

            _downloader.ReportProgress(-1, BackgroundEvents.CalculatedTotalFileSize);
        }

        /// <summary>
        /// The CleanupFiles
        /// </summary>
        private void CleanupFiles()
        {
            if (this.Files == null)
            {
                Logger.Info("Clean up files is canceled, Files property is null.");
                return;
            }

            var files = this.Files;
            new Thread(delegate ()
            {
                var dict = new Dictionary<string, int>();
                var keys = new List<string>();
                foreach (var file in files)
                {
                    if (file.AlwaysCleanupOnCancel || !file.IsFinished)
                    {
                        dict.Add(file.Path, 0);
                        keys.Add(file.Path);
                    }
                }

                while (dict.Count > 0)
                {
                    foreach (string key in keys)
                    {
                        try
                        {
                            if (File.Exists(key))
                            {
                                File.Delete(key);
                            }

                            // Remove file from dictionary since it either got deleted
                            // or it doesn't exist anymore.
                            dict.Remove(key);
                        }
                        catch
                        {
                            if (dict[key] == 10)
                            {
                                dict.Remove(key);
                            }
                            else
                            {
                                dict[key]++;
                            }
                        }
                    }

                    Thread.Sleep(2000);
                }
            }).Start();
        }

        /// <summary>
        /// The downloader_DoWork
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DoWorkEventArgs"/></param>
        private void downloader_DoWork(object sender, DoWorkEventArgs e)
        {
            this.CalculateTotalFileSize();
            foreach (var file in this.Files)
            {
                this.CurrentFile = file;
                if (!Directory.Exists(file.Directory))
                {
                    Directory.CreateDirectory(file.Directory);
                }

                this.DownloadFile();

                this.RaiseEventFromBackground(BackgroundEvents.FileDownloadComplete,
                        new FileDownloadEventArgs(file));

                if (_downloader.CancellationPending)
                {
                    this.CleanupFiles();
                }
            }
        }

        /// <summary>
        /// The downloader_ProgressChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/></param>
        private void downloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Exception)
            {
                this.CurrentFile.Exception = e.UserState as Exception;
                this.OnFileDownloadFailed(e.UserState as Exception, this.CurrentFile);
            }
            else if (e.UserState is object[])
            {
                var obj = (object[])e.UserState;
                if (obj[0] is BackgroundEvents)
                {
                    this.RaiseEvent((BackgroundEvents)obj[0], (EventArgs)obj[1]);
                }
            }
        }

        /// <summary>
        /// The downloader_RunWorkerCompleted
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RunWorkerCompletedEventArgs"/></param>
        private void downloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.IsBusy = this.IsPaused = false;
            if (!this.WasCanceled)
            {
                this.OnCompleted();
            }
            else
            {
                this.OnCanceled();
            }

            this.OnStopped();
        }

        /// <summary>
        /// The DownloadFile
        /// </summary>
        private void DownloadFile()
        {
            long totalSize = 0;

            var readBytes = new byte[this.PackageSize];
            int currentPackageSize;
            var speedTimer = new Stopwatch();
            Exception exception = null;

            long existLen = 0;

            if (File.Exists(this.CurrentFile.Path))
            {
                existLen = new FileInfo(this.CurrentFile.Path).Length;
            }

            FileStream writer = existLen > 0
                ? new FileStream(this.CurrentFile.Path, FileMode.Append, FileAccess.Write)
                : new FileStream(this.CurrentFile.Path, FileMode.Create, FileAccess.Write);
            HttpWebRequest webReq;
            HttpWebResponse webResp = null;

            try
            {
                webReq = (HttpWebRequest)WebRequest.Create(this.CurrentFile.Url);
                webReq.Method = "HEAD";
                webResp = (HttpWebResponse)webReq.GetResponse();

                if (webResp.ContentLength == existLen)
                {
                    totalSize = existLen;
                }
                else
                {
                    webResp.Close();
                    webReq = (HttpWebRequest)WebRequest.Create(this.CurrentFile.Url);
                    webReq.AddRange(existLen);
                    webResp = (HttpWebResponse)webReq.GetResponse();
                    totalSize = existLen + webResp.ContentLength;
                }
            }
            catch (Exception ex)
            {
                webResp?.Close();
                webResp?.Dispose();
                exception = ex;
            }

            this.CurrentFile.TotalFileSize = totalSize;

            if (exception != null)
            {
                _downloader.ReportProgress(0, exception);
            }
            else
            {
                this.CurrentFile.Progress = existLen;
                this.TotalProgress += existLen;

                long prevSize = 0;
                var speedInterval = 100;
                var stream = webResp.GetResponseStream();

                while (this.CurrentFile.Progress < totalSize && !_downloader.CancellationPending)
                {
                    while (this.IsPaused)
                    {
                        Thread.Sleep(100);
                    }

                    speedTimer.Start();
                    currentPackageSize = stream.Read(readBytes, 0, this.PackageSize);

                    this.CurrentFile.Progress += currentPackageSize;
                    this.TotalProgress += currentPackageSize;

                    // Raise ProgressChanged event
                    this.RaiseEventFromBackground(BackgroundEvents.ProgressChanged, EventArgs.Empty);

                    writer.Write(readBytes, 0, currentPackageSize);

                    if (speedTimer.Elapsed.TotalMilliseconds >= speedInterval)
                    {
                        var downloadedBytes = writer.Length - prevSize;
                        prevSize = writer.Length;

                        this.Speed = (int)downloadedBytes * (speedInterval == 100 ? 10 : 1);

                        // Only update speed once a second after initial update
                        speedInterval = 1000;

                        speedTimer.Reset();
                    }
                }

                speedTimer.Stop();
                stream.Close();
                writer.Close();
                webResp.Close();
                webResp.Dispose();

                if (!_downloader.CancellationPending)
                {
                    this.CurrentFile.IsFinished = true;
                    this.RaiseEventFromBackground(BackgroundEvents.FileDownloadSucceeded,
                        new FileDownloadEventArgs(this.CurrentFile));
                }
            }
        }

        /// <summary>
        /// The RaiseEvent
        /// </summary>
        /// <param name="evt">The evt<see cref="BackgroundEvents"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void RaiseEvent(BackgroundEvents evt, EventArgs e)
        {
            switch (evt)
            {
                case BackgroundEvents.CalculatedTotalFileSize:
                    this.OnCalculatedTotalFileSize();
                    break;

                case BackgroundEvents.FileDownloadComplete:
                    this.OnFileDownloadComplete((FileDownloadEventArgs)e);
                    break;

                case BackgroundEvents.FileDownloadSucceeded:
                    this.OnFileDownloadSucceeded((FileDownloadEventArgs)e);
                    break;

                case BackgroundEvents.ProgressChanged:
                    this.OnProgressChanged();
                    break;
            }
        }

        /// <summary>
        /// The RaiseEventFromBackground
        /// </summary>
        /// <param name="evt">The evt<see cref="BackgroundEvents"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void RaiseEventFromBackground(BackgroundEvents evt, EventArgs e) => _downloader.ReportProgress(-1, new object[] { evt, e });

        #endregion Methods
    }
}