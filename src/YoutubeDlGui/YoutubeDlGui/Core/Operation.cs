namespace YoutubeDlGui.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="Operation" />
    /// </summary>
    public abstract class Operation : IDisposable, INotifyPropertyChanged
    {
        #region Constants

        /// <summary>
        /// The amount of time to wait for progress updates in milliseconds.
        /// </summary>
        protected const int ProgressDelay = 500;

        protected const int ProgressMax = 100;

        protected const int ProgressMin = 0;

        #endregion Constants

        #region Fields

        private readonly string _progressText = string.Empty;

        private readonly string _progressTextOverride = string.Empty;

        /// <summary>
        /// Store running operations that can be stopped automatically when closing application.
        /// </summary>
        public static List<Operation> Running = new List<Operation>();

        private bool _disposed = false;

        private long _duration;

        private string _eta;

        private long _fileSize;

        private string _link;

        private long _progress;

        private int _progressPercentage = 0;

        private bool _reportsProgress = false;

        private string _speed;

        private OperationStatus _status = OperationStatus.Queued;

        private string _thumbnail;

        private string _title;

        private BackgroundWorker _worker;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        protected Operation()
        {
            _worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_Completed;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when the operation is complete.
        /// </summary>
        public event OperationEventHandler Completed;

        /// <summary>
        /// Defines the ProgressChanged
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Defines the ReportsProgressChanged
        /// </summary>
        public event EventHandler ReportsProgressChanged;

        /// <summary>
        /// Defines the Resumed
        /// </summary>
        public event EventHandler Resumed;

        /// <summary>
        /// Defines the Started
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Defines the StatusChanged
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a value indicating whether CancellationPending
        /// </summary>
        public bool CancellationPending => _worker?.CancellationPending == true;

        /// <summary>
        /// Gets or sets the Duration
        /// </summary>
        public long Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Errors
        /// Gets a human readable list of errors caused by the operation.
        /// </summary>
        public ReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(ErrorsInternal);

        /// <summary>
        /// Gets or sets the Errors1
        /// </summary>
        public List<string> Errors1 { get => ErrorsInternal; set => ErrorsInternal = value; }

        /// <summary>
        /// Gets or sets the ETA
        /// </summary>
        public string ETA
        {
            get { return _eta; }
            set
            {
                _eta = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Exception
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets or sets the FileSize
        /// </summary>
        public long FileSize
        {
            get { return _fileSize; }
            set
            {
                _fileSize = value;
                this.OnPropertyChanged();
                this.OnPropertyChangedExplicit(nameof(ProgressText));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HasStarted
        /// </summary>
        public bool HasStarted { get; set; }

        /// <summary>
        /// Gets or sets the Input
        /// Gets the input file or download url.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets a value indicating whether IsBusy
        /// </summary>
        public bool IsBusy => _worker?.IsBusy == true;

        /// <summary>
        /// Gets a value indicating whether IsCanceled
        /// </summary>
        public bool IsCanceled => this.Status == OperationStatus.Canceled;

        /// <summary>
        /// Gets a value indicating whether IsDone
        /// Returns True if Operation is done, regardless of result.
        /// </summary>
        public bool IsDone
        {
            get
            {
                return this.Status == OperationStatus.Canceled
                    || this.Status == OperationStatus.Failed
                    || this.Status == OperationStatus.Success;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsPaused
        /// </summary>
        public bool IsPaused => this.Status == OperationStatus.Paused;

        /// <summary>
        /// Gets a value indicating whether IsQueued
        /// </summary>
        public bool IsQueued => this.Status == OperationStatus.Queued;

        /// <summary>
        /// Gets a value indicating whether IsSuccessful
        /// </summary>
        public bool IsSuccessful => this.Status == OperationStatus.Success;

        /// <summary>
        /// Gets a value indicating whether IsWorking
        /// </summary>
        public bool IsWorking => this.Status == OperationStatus.Working;

        /// <summary>
        /// Gets or sets the Link
        /// </summary>
        public string Link
        {
            get { return _link; }
            set
            {
                _link = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Output
        /// Gets the output file.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets the Progress
        /// </summary>
        public long Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                this.OnPropertyChanged();
                this.OnPropertyChangedExplicit(nameof(ProgressText));
            }
        }

        /// <summary>
        /// Gets or sets the ProgressPercentage
        /// Gets the operation progress, as a double between 0-100.
        /// </summary>
        public int ProgressPercentage
        {
            get { return _progressPercentage; }
            set
            {
                _progressPercentage = value;
                this.OnPropertyChanged();
                this.OnPropertyChangedExplicit(nameof(ProgressText));
            }
        }

        /// <summary>
        /// Gets or sets the ProgressText
        /// </summary>
        public string ProgressText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ReportsProgress
        /// </summary>
        public bool ReportsProgress
        {
            get { return _reportsProgress; }
            set
            {
                _reportsProgress = value;
                this.OnReportsProgressChanged(EventArgs.Empty);
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Speed
        /// </summary>
        public string Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                this.OnPropertyChanged();
                this.OnPropertyChangedExplicit(nameof(ProgressText));
            }
        }

        /// <summary>
        /// Gets or sets the Status
        /// Gets the operation status.
        /// </summary>
        public OperationStatus Status
        {
            get { return _status; }
            set
            {
                var oldStatus = _status;

                _status = value;

                this.OnStatusChanged(new StatusChangedEventArgs(this, value, oldStatus));
                this.OnPropertyChanged();

                // Send Changed notification to following properties
                foreach (string property in new string[] {
                    nameof(IsCanceled),
                    nameof(IsDone),
                    nameof(IsPaused),
                    nameof(IsSuccessful),
                    nameof(IsWorking) })
                {
                    this.OnPropertyChangedExplicit(property);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Thumbnail
        /// </summary>
        public string Thumbnail
        {
            get { return _thumbnail; }
            set
            {
                _thumbnail = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Operation's arguments.
        /// </summary>
        protected Dictionary<string, object> Arguments { get; set; }

        /// <summary>
        /// Gets or sets the ErrorsInternal
        /// Gets or sets a editable list of errors.
        /// </summary>
        protected List<string> ErrorsInternal { get; set; } = new List<string>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns whether 'Open' method is supported and available at the moment.
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool CanOpen()
        {
            return false;
        }

        /// <summary>
        /// Returns whether 'Pause' method is supported and available at the moment.
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool CanPause()
        {
            return false;
        }

        /// <summary>
        /// Returns whether 'Resume' method is supported and available at the moment.
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool CanResume()
        {
            return false;
        }

        /// <summary>
        /// Returns whether 'Stop' method is supported and available at the moment.
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool CanStop()
        {
            return false;
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
        /// Opens the output file.
        /// </summary>
        /// <returns></returns>
        public virtual bool Open()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Opens the containing folder of the output file(s).
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool OpenContainingFolder()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Pauses the operation if supported &amp; available.
        /// </summary>
        public virtual void Pause()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Queues the operation. Used to pause and put the operation back to being queued.
        /// </summary>
        public virtual void Queue()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Resumes the operation if supported &amp; available.
        /// </summary>
        public void Resume()
        {
            if (!this.HasStarted)
            {
                this.Start();
            }
            else
            {
                this.ResumeInternal();
            }

            this.Resumed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Resumes the operation if supported &amp; available, but does not fire the Resumed event.
        /// </summary>
        public void ResumeQuiet()
        {
            this.ResumeInternal();
        }

        /// <summary>
        /// Starts the operation.
        /// </summary>
        public void Start()
        {
            _worker.RunWorkerAsync(this.Arguments);

            Operation.Running.Add(this);

            this.Status = OperationStatus.Working;
            this.OnStarted(EventArgs.Empty);

            this.HasStarted = true;
        }

        /// <summary>
        /// Stops the operation if supported &amp; available.
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public virtual bool Stop()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The CancelAsync
        /// </summary>
        protected void CancelAsync() => _worker?.CancelAsync();

        /// <summary>
        /// The Complete
        /// </summary>
        protected void Complete()
        {
            this.ReportsProgress = true;

            if (this.Status == OperationStatus.Success)
            {
                this.ProgressPercentage = ProgressMax;
            }

            this.OnPropertyChangedExplicit(nameof(ProgressText));
            OnCompleted(new OperationEventArgs(null, this.Status));
        }

        /// <summary>
        /// The OnCompleted
        /// </summary>
        /// <param name="e">The e<see cref="OperationEventArgs"/></param>
        protected virtual void OnCompleted(OperationEventArgs e) => this.Completed?.Invoke(this, e);

        /// <summary>
        /// The OnProgressChanged
        /// </summary>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/></param>
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e) => this.ProgressChanged?.Invoke(this, e);

        /// <summary>
        /// The OnReportsProgressChanged
        /// </summary>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        protected virtual void OnReportsProgressChanged(EventArgs e) => this.ReportsProgressChanged?.Invoke(this, e);

        /// <summary>
        /// The OnStarted
        /// </summary>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        protected virtual void OnStarted(EventArgs e) => this.Started?.Invoke(this, e);

        /// <summary>
        /// The OnStatusChanged
        /// </summary>
        /// <param name="e">The e<see cref="StatusChangedEventArgs"/></param>
        protected virtual void OnStatusChanged(StatusChangedEventArgs e) => this.StatusChanged?.Invoke(this, e);

        /// <summary>
        /// The ReportProgress
        /// </summary>
        /// <param name="percentProgress">The percentProgress<see cref="int"/></param>
        /// <param name="userState">The userState<see cref="object"/></param>
        protected void ReportProgress(int percentProgress, object userState) => _worker?.ReportProgress(percentProgress, userState);

        /// <summary>
        /// Resumes the operation if supported &amp; available.
        /// </summary>
        protected virtual void ResumeInternal() => throw new NotSupportedException();

        /// <summary>
        /// The WorkerCompleted
        /// </summary>
        /// <param name="e">The e<see cref="RunWorkerCompletedEventArgs"/></param>
        protected abstract void WorkerCompleted(RunWorkerCompletedEventArgs e);

        /// <summary>
        /// The WorkerDoWork
        /// </summary>
        /// <param name="e">The e<see cref="DoWorkEventArgs"/></param>
        protected abstract void WorkerDoWork(DoWorkEventArgs e);

        /// <summary>
        /// The WorkerProgressChanged
        /// </summary>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/></param>
        protected abstract void WorkerProgressChanged(ProgressChangedEventArgs e);

        /// <summary>
        /// The Dispose
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Completed = null;

                if (_worker != null)
                {
                    _worker.Dispose();
                    _worker = null;
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// The Worker_Completed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RunWorkerCompletedEventArgs"/></param>
        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Operation.Running.Remove(this);

            if (e.Error != null)
            {
                this.Exception = e.Error;
                this.Status = OperationStatus.Failed;
            }
            else
            {
                this.Status = (OperationStatus)e.Result;
            }

            this.Complete();
            this.WorkerCompleted(e);
        }

        /// <summary>
        /// The Worker_DoWork
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DoWorkEventArgs"/></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e) => WorkerDoWork(e);

        /// <summary>
        /// The Worker_ProgressChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
            {
                this.ProgressPercentage = e.ProgressPercentage;
            }

            this.WorkerProgressChanged(e);
            this.OnProgressChanged(e);
        }

        #endregion Methods
    }
}