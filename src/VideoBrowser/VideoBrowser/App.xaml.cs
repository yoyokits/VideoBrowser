namespace VideoBrowser
{
    using CefSharp;
    using System.Windows;
    using VideoBrowser.Common;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        /// <summary>
        /// The OnApplication_Startup.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="StartupEventArgs"/>.</param>
        private void OnApplication_Startup(object sender, StartupEventArgs e)
        {
            AppEnvironment.Arguments = e.Args;
        }

        #endregion Methods

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}