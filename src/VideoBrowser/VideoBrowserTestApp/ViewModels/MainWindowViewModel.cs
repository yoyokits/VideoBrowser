namespace VideoBrowserTestApp.ViewModels
{
    using System.Collections.Generic;
    using VideoBrowserTestApp.Helpers;
    using VideoBrowserTestApp.Tests;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        internal MainWindowViewModel()
        {
            this.Tests = ReflectionHelper.GetInstances<TestBase>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Tests.
        /// </summary>
        public IList<TestBase> Tests { get; }

        #endregion Properties
    }
}