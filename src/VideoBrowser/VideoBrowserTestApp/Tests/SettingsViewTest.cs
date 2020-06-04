namespace VideoBrowserTestApp.Tests
{
    using VideoBrowser.Models;
    using VideoBrowser.ViewModels;
    using VideoBrowser.Views;
    using VideoBrowserTestApp.Helpers;

    /// <summary>
    /// Defines the <see cref="SettingsViewTest" />.
    /// </summary>
    public class SettingsViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewTest"/> class.
        /// </summary>
        public SettingsViewTest() : base(nameof(SettingsView))
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
            var globalData = new GlobalData();
            var viewModel = new SettingsViewModel(globalData);
            var view = new SettingsView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view);
        }

        #endregion Methods
    }
}