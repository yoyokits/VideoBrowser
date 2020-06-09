namespace VideoBrowser.Models
{
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Resources;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="SettingsButton" />.
    /// </summary>
    internal class SettingsButton : CreateTabAddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsButton"/> class.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="SettingsViewModel"/>.</param>
        internal SettingsButton(SettingsViewModel viewModel) : base("Settings", Icons.Settings)
        {
            this.SettingsViewModel = viewModel;
            this.ToolTip = "Adjust the Settings";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the SettingsViewModel.
        /// </summary>
        public SettingsViewModel SettingsViewModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateView.
        /// </summary>
        /// <returns>The <see cref="UIElement"/>.</returns>
        protected override UIElement CreateView()
        {
            var view = new SettingsView { DataContext = this.SettingsViewModel };
            return view;
        }

        #endregion Methods
    }
}