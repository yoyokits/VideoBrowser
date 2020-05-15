namespace YoutubeDlGui.Test.Common
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ManualTestAttribute" />.
    /// </summary>
    public class ManualTestAttribute : TestCategoryBaseAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualTestAttribute"/> class.
        /// </summary>
        public ManualTestAttribute()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the TestCategories.
        /// </summary>
        public override IList<string> TestCategories => new List<string> { "ManualTest" };

        #endregion Properties
    }
}