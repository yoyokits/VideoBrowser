namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Extensions;

    /// <summary>
    /// Defines the <see cref="AddInButton" />.
    /// </summary>
    public abstract class AddInButton : INotifyPropertyChanged
    {
        #region Fields

        private Geometry _icon;

        private bool _isEnabled = true;

        private string _toolTip;

        #endregion Fields

        #region Events

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the Command.
        /// </summary>
        public ICommand Command { get; protected set; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get => _icon; set => this.Set(this.PropertyChanged, ref _icon, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsEnabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => this.Set(this.PropertyChanged, ref _isEnabled, value); }

        /// <summary>
        /// Gets or sets the ToolTip.
        /// </summary>
        public string ToolTip { get => _toolTip; set => this.Set(this.PropertyChanged, ref _toolTip, value); }

        #endregion Properties
    }
}