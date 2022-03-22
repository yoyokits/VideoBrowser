namespace VideoBrowser
{
    using CefSharp;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows;
    using VideoBrowser.Common;
    using VideoBrowser.Core;
    using VideoBrowser.Helpers;

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
            this.UpdateYoutubeDl();
        }

        #endregion Methods

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Cef.Shutdown();
        }

        private void UpdateYoutubeDl()
        {
            Task.Run(() =>
            {
                var path = YoutubeDl.YouTubeDlPath;
                ProcessHelper.StartProcess(path, "--update --no-check-certificate", this.UpdateOutput, null, null);
            });
        }

        private void UpdateOutput(Process process, string output)
        {
            Logger.Info($"Youtube-dl message:{output}");
        }
    }
}