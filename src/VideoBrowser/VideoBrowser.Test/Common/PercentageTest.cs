namespace VideoBrowser.Test
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="PercentageTest" />
    /// </summary>
    [TestClass]
    public class PercentageTest
    {
        #region Methods

        /// <summary>
        /// The PercentageTest_Constructor
        /// </summary>
        [TestMethod]
        public void PercentageTest_Constructor()
        {
            var percentage = new Percentage(50);
            percentage.Normalized.Should().Be(0.5);
            percentage.ToString().Should().Be("50%");
        }

        #endregion Methods
    }
}