namespace VideoBrowserTestApp.Tests
{
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;
    using VideoBrowserTestApp.Helpers;

    /// <summary>
    /// Defines the <see cref="AboutViewTest" />.
    /// </summary>
    public class AboutViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewTest"/> class.
        /// </summary>
        public AboutViewTest() : base(nameof(AboutView))
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Test(object obj)
        {
            var viewModel = new AboutViewModel();
            var view = new AboutView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}