namespace VideoBrowser.Controls.CefSharpBrowser
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using VideoBrowser.Controls.CefSharpBrowser.ViewModels;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="CreateTabAddInButton" />.
    /// </summary>
    public abstract class CreateTabAddInButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTabAddInButton"/> class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="icon">The icon<see cref="Geometry"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public CreateTabAddInButton(string title, Geometry icon, string name = null) : base(name)
        {
            this.Title = title;
            this.Icon = icon;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Guid.
        /// </summary>
        public Guid Guid { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateView.
        /// This method is already in UI Thread.
        /// </summary>
        /// <returns>The <see cref="UIElement"/>.</returns>
        protected abstract UIElement CreateView();

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="WebBrowserTabControlViewModel"/>.</param>
        protected override void Execute(WebBrowserTabControlViewModel viewModel)
        {
            if (viewModel.IsTabItemExist(this.Guid))
            {
                viewModel.SetActiveTab(this.Guid);
                return;
            }

            UIThreadHelper.Invoke(() =>
            {
                var view = this.CreateView();
                var tab = new TabItem(this.Guid)
                {
                    Content = view,
                    Icon = this.Icon,
                    Title = this.Title
                };

                viewModel.AddTab(tab);
            });
        }

        #endregion Methods
    }
}