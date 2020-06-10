namespace VideoBrowser.Controls.CefSharpBrowser
{
    using Dragablz;
    using System;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Controls.CefSharpBrowser.Views;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="InterTabClient" />.
    /// </summary>
    public class InterTabClient : IInterTabClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InterTabClient"/> class.
        /// </summary>
        /// <param name="data">The data<see cref="GlobalBrowserData"/>.</param>
        internal InterTabClient(GlobalBrowserData data)
        {
            this.GlobalBrowserData = data;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the CreateWindow.
        /// </summary>
        public Func<(Window, TabablzControl)> CreateWindow { get; set; }

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        public GlobalBrowserData GlobalBrowserData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetNewHost.
        /// </summary>
        /// <param name="interTabClient">The interTabClient<see cref="IInterTabClient"/>.</param>
        /// <param name="partition">The partition<see cref="object"/>.</param>
        /// <param name="source">The source<see cref="TabablzControl"/>.</param>
        /// <returns>The <see cref="INewTabHost{Window}"/>.</returns>
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            NewTabHost<Window> host = null;
            UIThreadHelper.Invoke(() =>
            {
                var (window, tabControl) = this.CreateWindow != null ? this.CreateWindow() : this.CreateDefaultWindow();
                host = new NewTabHost<Window>(window, tabControl);
            });

            return host;
        }

        /// <summary>
        /// The TabEmptiedHandler.
        /// </summary>
        /// <param name="tabControl">The tabControl<see cref="TabablzControl"/>.</param>
        /// <param name="window">The window<see cref="Window"/>.</param>
        /// <returns>The <see cref="TabEmptiedResponse"/>.</returns>
        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }

        /// <summary>
        /// The CreateDefaultWindow.
        /// </summary>
        /// <returns>The <see cref="(Window, TabablzControl)"/>.</returns>
        internal (Window, TabablzControl) CreateDefaultWindow()
        {
            var viewModel = new DefaultTabHostViewModel(this.GlobalBrowserData);
            var window = new DefaultTabHostWindow { DataContext = viewModel };
            var tabControl = window.WebBrowserTabControlView.InitialTabablzControl;
            return (window, tabControl);
        }

        #endregion Methods
    }
}