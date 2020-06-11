namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddInButton"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        protected AddInButton(string name = null)
        {
            if (name == null)
            {
                name = this.GetType().Name;
            }

            this.Name = name;
            this.Command = new RelayCommand(this.OnExecute, $"{this.Name} add in is executed");
        }

        #endregion Constructors

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
        public ICommand Command { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get => _icon; set => this.Set(this.PropertyChanged, ref _icon, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsEnabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => this.Set(this.PropertyChanged, ref _isEnabled, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ToolTip.
        /// </summary>
        public string ToolTip { get => _toolTip; set => this.Set(this.PropertyChanged, ref _toolTip, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected abstract void Execute(WebBrowserTabControlViewModel viewModel);

        /// <summary>
        /// The OnExecute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnExecute(object obj)
        {
            var viewModel = obj as WebBrowserTabControlViewModel;
            this.Execute(viewModel);
        }

        #endregion Methods
    }
}