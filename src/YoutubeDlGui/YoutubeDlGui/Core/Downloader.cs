namespace YoutubeDlGui.Core
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using WrapYoutubeDl;
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="Downloader" />
    /// </summary>
    public class Downloader : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Downloader"/> class.
        /// </summary>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="outputName">The outputName<see cref="string"/></param>
        /// <param name="outputfolder">The outputfolder<see cref="string"/></param>
        public Downloader(string url, string outputName, string outputfolder)
        {
            this.Started = false;
            this.Finished = false;
            this.Percentage = 0;

            DestinationFolder = outputfolder;
            Url = url;

            // make sure filename ends with an mp3 extension
            OutputName = outputName;
            if (!OutputName.ToLower().EndsWith(".mp3"))
            {
                OutputName += ".mp3";
            }

            // this is the path where you keep the binaries (ffmpeg, youtube-dl etc)
            var binaryPath = ConfigurationManager.AppSettings["binaryfolder"];
            if (string.IsNullOrEmpty(binaryPath))
            {
                throw new Exception("Cannot read 'binaryfolder' variable from app.config / web.config.");
            }

            // if the destination file exists, exit
            var destinationPath = System.IO.Path.Combine(outputfolder, OutputName);
            if (System.IO.File.Exists(destinationPath))
            {
                throw new Exception(destinationPath + " exists");
            }
            var arguments = string.Format(@"--continue  --no-overwrites --restrict-filenames --extract-audio --audio-format mp3 {0} -o ""{1}""", url, destinationPath);  //--ignore-errors

            // setup the process that will fire youtube-dl
            Process = new Process();
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.StartInfo.FileName = System.IO.Path.Combine(binaryPath, "youtube-dl.exe");
            Process.StartInfo.Arguments = arguments;
            Process.StartInfo.CreateNoWindow = true;
            Process.EnableRaisingEvents = true;

            Process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            Process.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// The ErrorEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ProgressEventArgs"/></param>
        public delegate void ErrorEventHandler(object sender, ProgressEventArgs e);

        /// <summary>
        /// The FinishedDownloadEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DownloadEventArgs"/></param>
        public delegate void FinishedDownloadEventHandler(object sender, DownloadEventArgs e);

        /// <summary>
        /// The ProgressEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ProgressEventArgs"/></param>
        public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);

        /// <summary>
        /// The StartedDownloadEventHandler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DownloadEventArgs"/></param>
        public delegate void StartedDownloadEventHandler(object sender, DownloadEventArgs e);

        #endregion Delegates

        #region Events

        /// <summary>
        /// Defines the ErrorDownload
        /// </summary>
        public event ErrorEventHandler ErrorDownload;

        /// <summary>
        /// Defines the FinishedDownload
        /// </summary>
        public event FinishedDownloadEventHandler FinishedDownload;

        /// <summary>
        /// Defines the ProgressDownload
        /// </summary>
        public event ProgressEventHandler ProgressDownload;

        /// <summary>
        /// Defines the StartedDownload
        /// </summary>
        public event StartedDownloadEventHandler StartedDownload;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the ConsoleLog
        /// </summary>
        public string ConsoleLog { get; set; }

        /// <summary>
        /// Gets or sets the DestinationFolder
        /// </summary>
        public string DestinationFolder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Finished
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// Gets or sets the OutputName
        /// </summary>
        public string OutputName { get; set; }

        /// <summary>
        /// Gets or sets the Percentage
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the Process
        /// </summary>
        public Process Process { get; set; }

        /// <summary>
        /// Gets or sets the ProcessObject
        /// </summary>
        public Object ProcessObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Started
        /// </summary>
        public bool Started { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Download
        /// </summary>
        public void Download()
        {
            Console.WriteLine("Downloading {0}", Url);
            Process.Exited += Process_Exited;
            Process.Start();
            Process.BeginOutputReadLine();
            this.OnDownloadStarted(new DownloadEventArgs() { ProcessObject = this.ProcessObject });
            while (this.Finished == false)
            {
                System.Threading.Thread.Sleep(100);                   // wait while process exits;
            }
        }

        /// <summary>
        /// The ErrorDataReceived
        /// </summary>
        /// <param name="sendingprocess">The sendingprocess<see cref="object"/></param>
        /// <param name="error">The error<see cref="DataReceivedEventArgs"/></param>
        public void ErrorDataReceived(object sendingprocess, DataReceivedEventArgs error)
        {
            if (!String.IsNullOrEmpty(error.Data))
            {
                this.OnDownloadError(new ProgressEventArgs() { Error = error.Data, ProcessObject = this.ProcessObject });
            }
        }

        /// <summary>
        /// The OutputHandler
        /// </summary>
        /// <param name="sendingProcess">The sendingProcess<see cref="object"/></param>
        /// <param name="outLine">The outLine<see cref="DataReceivedEventArgs"/></param>
        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            // extract the percentage from process output
            if (String.IsNullOrEmpty(outLine.Data) || Finished)
            {
                return;
            }

            this.ConsoleLog += outLine.Data;

            if (outLine.Data.Contains("ERROR"))
            {
                this.OnDownloadError(new ProgressEventArgs() { Error = outLine.Data, ProcessObject = this.ProcessObject });
                return;
            }

            if (!outLine.Data.Contains("[download]"))
            {
                return;
            }

            var pattern = new Regex(@"\b\d+([\.,]\d+)?", RegexOptions.None);
            if (!pattern.IsMatch(outLine.Data))
            {
                return;
            }

            // fire the process event
            var perc = Convert.ToDecimal(Regex.Match(outLine.Data, @"\b\d+([\.,]\d+)?").Value);
            if (perc > 100 || perc < 0)
            {
                Console.WriteLine("weird perc {0}", perc);
                return;
            }

            this.Percentage = perc;
            this.OnProgress(new ProgressEventArgs() { ProcessObject = this.ProcessObject, Percentage = perc });

            // is it finished?
            if (perc < 100)
            {
                return;
            }

            if (perc == 100 && !Finished)
            {
                OnDownloadFinished(new DownloadEventArgs() { ProcessObject = this.ProcessObject });
            }
        }

        /// <summary>
        /// The Process_Exited
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        internal void Process_Exited(object sender, EventArgs e)
        {
            OnDownloadFinished(new DownloadEventArgs() { ProcessObject = this.ProcessObject });
        }

        /// <summary>
        /// The OnDownloadError
        /// </summary>
        /// <param name="e">The e<see cref="ProgressEventArgs"/></param>
        protected virtual void OnDownloadError(ProgressEventArgs e)
        {
            ErrorDownload?.Invoke(this, e);
        }

        /// <summary>
        /// The OnDownloadFinished
        /// </summary>
        /// <param name="e">The e<see cref="DownloadEventArgs"/></param>
        protected virtual void OnDownloadFinished(DownloadEventArgs e)
        {
            if (Finished == false)
            {
                Finished = true;
                FinishedDownload?.Invoke(this, e);
            }
        }

        /// <summary>
        /// The OnDownloadStarted
        /// </summary>
        /// <param name="e">The e<see cref="DownloadEventArgs"/></param>
        protected virtual void OnDownloadStarted(DownloadEventArgs e)
        {
            StartedDownload?.Invoke(this, e);
        }

        /// <summary>
        /// The OnProgress
        /// </summary>
        /// <param name="e">The e<see cref="ProgressEventArgs"/></param>
        protected virtual void OnProgress(ProgressEventArgs e)
        {
            this.ProgressDownload?.Invoke(this, e);
        }

        #endregion Methods
    }
}