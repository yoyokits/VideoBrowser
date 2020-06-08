namespace VideoBrowser.Helpers
{
    using Dragablz;
    using System;
    using System.Windows;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Models;

    /// <summary>
    /// Defines the <see cref="WebBrowserViewModelFactory" />.
    /// It is Freezable because using DependencyObject will not work.
    /// </summary>
    public class WebBrowserViewModelFactory : Freezable
    {
        #region Fields

        public static readonly DependencyProperty GlobalDataProperty =
            DependencyProperty.Register(nameof(GlobalData), typeof(GlobalData), typeof(WebBrowserViewModelFactory), new PropertyMetadata(null));

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the HeaderedItemViewModel factory....
        /// </summary>
        public Func<HeaderedItemViewModel> Create => this.CreateWebBrowserHeaderedItem;

        /// <summary>
        /// Gets or sets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get => (GlobalData)this.GetValue(GlobalDataProperty); set => this.SetValue(GlobalDataProperty, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateInstanceCore.
        /// </summary>
        /// <returns>The <see cref="Freezable"/>.</returns>
        protected override Freezable CreateInstanceCore() => new WebBrowserViewModelFactory();

        /// <summary>
        /// The CreateWebBrowserHeaderedItem.
        /// </summary>
        /// <returns>The <see cref="HeaderedItemViewModel"/>.</returns>
        private HeaderedItemViewModel CreateWebBrowserHeaderedItem() => new WebBrowserHeaderedItemViewModel(this.GlobalData);

        #endregion Methods
    }
}