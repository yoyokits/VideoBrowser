namespace VideoBrowser.Helpers
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="DelayCall" />.
    /// </summary>
    public class DelayCall
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayCall"/> class.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        /// <param name="milliSecondDelay">The milliSecondDelay<see cref="int"/>.</param>
        /// <param name="callInUIThread">The callInUIThread<see cref="bool"/>.</param>
        public DelayCall(Action action, int milliSecondDelay, bool callInUIThread = false)
        {
            this.Action = action ?? throw new ArgumentNullException(nameof(action));
            this.MilliSecondDelay = milliSecondDelay;
            this.IsCallInUIThread = callInUIThread;
            this.Timer = new Timer();
            this.Timer.Tick += this.OnTimer_Tick;
            this.Timer.Interval = this.MilliSecondDelay;
            this.Timer.Stop();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Action.
        /// </summary>
        public Action Action { get; }

        /// <summary>
        /// Gets a value indicating whether IsCallInUIThread.
        /// </summary>
        public bool IsCallInUIThread { get; }

        /// <summary>
        /// Gets or sets the MilliSecondDelay.
        /// </summary>
        public int MilliSecondDelay { get; set; }

        /// <summary>
        /// Gets the Timer.
        /// </summary>
        public Timer Timer { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Call.
        /// </summary>
        public void Call()
        {
            this.Restart();
        }

        /// <summary>
        /// The OnThick.
        /// </summary>
        /// <param name="state">The state<see cref="object"/>.</param>
        private void OnThick(object state)
        {
            this.Timer.Stop();
            if (this.IsCallInUIThread)
            {
                UIThreadHelper.Invoke(this.Action);
            }
            else
            {
                this.Action();
            }
        }

        /// <summary>
        /// The OnTimer_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void OnTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Restart.
        /// </summary>
        private void Restart()
        {
            this.Timer.Stop();
            this.Timer.Start();
        }

        #endregion Methods
    }
}