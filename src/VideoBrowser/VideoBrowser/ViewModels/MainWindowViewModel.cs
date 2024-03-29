﻿namespace VideoBrowser.ViewModels
{
    using Dragablz;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Core;
    using VideoBrowser.Models;
    using VideoBrowser.Views;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        /// <param name="globalBrowserData">The globalBrowserData<see cref="GlobalBrowserData"/>.</param>
        public MainWindowViewModel(GlobalBrowserData globalBrowserData)
        {
            this.ClosingCommand = new RelayCommand(this.OnClosing);
            this.LoadedCommand = new RelayCommand(this.OnLoaded);
            this.PressEscCommand = new RelayCommand(this.OnPressEsc);
            this.PressF2Command = new RelayCommand(this.OnPressF2);
            this.About = new AboutViewModel();
            var downloadItemModels = globalBrowserData.DownloadItemModels;
            this.DownloadQueueViewModel = new DownloadQueueViewModel(downloadItemModels) { ShowMessageAsync = this.ShowMessageAsync };
            this.DownloadFlyoutViewModel = new DownloadFlyoutViewModel(downloadItemModels) { ShowDownloadTabAction = this.ShowDownloadTabAction };
            this.WebBrowserTabControlViewModel = new WebBrowserTabControlViewModel(globalBrowserData)
            {
                CreateBrowserFunc = this.CreateBrowser
            };

            this.Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the About.
        /// </summary>
        public AboutViewModel About { get; }

        /// <summary>
        /// Gets the CefWindowData.
        /// </summary>
        public CefWindowData CefWindowData => this.WebBrowserTabControlViewModel.CefWindowData;

        /// <summary>
        /// Gets the ClosingCommand.
        /// </summary>
        public RelayCommand ClosingCommand { get; }

        /// <summary>
        /// Gets the DownloadFlyoutViewModel.
        /// </summary>
        public DownloadFlyoutViewModel DownloadFlyoutViewModel { get; }

        /// <summary>
        /// Gets the DownloadQueueViewModel.
        /// </summary>
        public DownloadQueueViewModel DownloadQueueViewModel { get; }

        /// <summary>
        /// Gets the GlobalBrowserData.
        /// </summary>
        public GlobalBrowserData GlobalBrowserData => this.WebBrowserTabControlViewModel.GlobalBrowserData;

        /// <summary>
        /// Gets the LoadedCommand.
        /// </summary>
        public ICommand LoadedCommand { get; }

        /// <summary>
        /// Gets the PressEscCommand.
        /// </summary>
        public ICommand PressEscCommand { get; }

        /// <summary>
        /// Gets the PressF2Command.
        /// </summary>
        public ICommand PressF2Command { get; }

        /// <summary>
        /// Gets the Settings.
        /// </summary>
        public SettingsViewModel Settings => this.GlobalBrowserData.Settings;

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title => $"{AppEnvironment.Name}";

        /// <summary>
        /// Gets the WebBrowserTabControlViewModel.
        /// </summary>
        public WebBrowserTabControlViewModel WebBrowserTabControlViewModel { get; }

        /// <summary>
        /// Gets the DownloadAction.
        /// </summary>
        private Action<Operation> DownloadAction => this.DownloadQueueViewModel.Download;

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateBrowser.
        /// </summary>
        /// <returns>The <see cref="HeaderedItemViewModel"/>.</returns>
        internal TabItem CreateBrowser()
        {
            var model = new WebBrowserHeaderedItemViewModel(this.GlobalBrowserData, this.CefWindowData, this.DownloadAction);
            return model;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        private void Dispose()
        {
            this.WebBrowserTabControlViewModel.Dispose();
            var viewModels = this.GlobalBrowserData.WindowViewModels;
            if (viewModels.Contains(this))
            {
                viewModels.Remove(this);
            }

            if (!viewModels.Any())
            {
                this.GlobalBrowserData.Dispose();
            }
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize()
        {
            var args = AppEnvironment.Arguments;
            var url = Properties.Settings.Default.LastUrl;
            if (args != null && args.Any())
            {
                url = args.First();
            }

            if (this.WebBrowserTabControlViewModel.TabItems.Any())
            {
                var browser = this.WebBrowserTabControlViewModel.TabItems.First();
                if (browser is WebBrowserHeaderedItemViewModel model)
                {
                    model.VideoBrowserViewModel.NavigateUrl = url;
                }
            }
        }

        /// <summary>
        /// The OnClosing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClosing(object obj)
        {
            var (_, _, commandParameter) = (ValueTuple<object, EventArgs, object>)obj;
            var window = (Window)commandParameter;
            var settings = Properties.Settings.Default;
            settings.WindowPosition = new Point(window.Left, window.Top);
            settings.WindowWidth = window.ActualWidth;
            settings.WindowHeight = window.Height;
            settings.WindowState = window.WindowState;
            settings.Save();
            DownloadQueueHandler.Stop();
            this.Dispose();
        }

        /// <summary>
        /// The OnLoaded.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLoaded(object obj)
        {
            var (_, _, commandParameter) = (ValueTuple<object, EventArgs, object>)obj;
            var settings = Properties.Settings.Default;
            var window = (MainWindow)commandParameter;
            window.Left = settings.WindowPosition.X;
            window.Top = settings.WindowPosition.Y;
            window.Width = settings.WindowWidth;
            window.Height = settings.WindowHeight;
            window.WindowState = settings.WindowState;
            this.CefWindowData.MainWindow = window;
            DownloadQueueHandler.LimitDownloads = settings.ShowMaxSimDownloads;
            DownloadQueueHandler.StartWatching(settings.MaxSimDownloads);
        }

        /// <summary>
        /// The OnPressEsc.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPressEsc(object obj)
        {
            // Leave full screen.
            if (this.CefWindowData.IsFullScreen)
            {
                this.CefWindowData.IsFullScreenCommand.Execute(false);
            }
        }

        /// <summary>
        /// The OnPressF2.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnPressF2(object obj)
        {
            var window = this.CefWindowData.MainWindow;
            window.Width = 1022;
            window.Height = 900;
        }

        /// <summary>
        /// The ShowDownloadTabAction.
        /// </summary>
        private void ShowDownloadTabAction()
        {
            var button = this.GlobalBrowserData.GetAddInButton(typeof(DownloadQueueButton));
            button?.Command.Execute(this.WebBrowserTabControlViewModel);
        }

        /// <summary>
        /// The ShowMessageAsync.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        private void ShowMessageAsync(string title, string message)
        {
            this.CefWindowData.ShowMessageAsync(title, message);
        }

        #endregion Methods
    }
}