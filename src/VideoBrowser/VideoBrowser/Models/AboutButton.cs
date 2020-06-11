namespace VideoBrowser.Models
{
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Resources;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="AboutButton" />.
    /// </summary>
    internal class AboutButton : CreateTabAddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutButton"/> class.
        /// </summary>
        internal AboutButton() : base("About", Icons.Info)
        {
            this.ToolTip = "Information about Cekli Browser";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the AboutViewModel.
        /// </summary>
        public AboutViewModel AboutViewModel { get; } = new AboutViewModel();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateView.
        /// This method is already in UI Thread.
        /// </summary>
        /// <returns>The <see cref="UIElement"/>.</returns>
        protected override UIElement CreateView()
        {
            var aboutView = new AboutView { DataContext = this.AboutViewModel };
            return aboutView;
        }

        #endregion Methods
    }
}