namespace YoutubeDlGui.Test.Extensions
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using YoutubeDlGui.Extensions;

    /// <summary>
    /// Defines the <see cref="StringExtensionTest" />
    /// </summary>
    [TestClass]
    public class StringExtensionTest
    {
        #region Methods

        /// <summary>
        /// The ToFormatedByte
        /// </summary>
        [TestMethod]
        public void ToFormatedByte()
        {
            var formatedByte = StringExtension.ToFormatedByte(10000);
            formatedByte.Should().Be("9.77 KB");
            formatedByte = StringExtension.ToFormatedByte(20000000);
            formatedByte.Should().Be("19.07 MB");
            formatedByte = StringExtension.ToFormatedByte(2000000000);
            formatedByte.Should().Be("1.86 GB");
        }

        [TestMethod]
        public void ToFormatedSpeed()
        {
            var formatedSpeed = StringExtension.ToFormatedSpeed(10000);
            formatedSpeed.Should().Be("9.77 KB/s");
            formatedSpeed = StringExtension.ToFormatedSpeed(20000000);
            formatedSpeed.Should().Be("19.07 MB/s");
            formatedSpeed = StringExtension.ToFormatedSpeed(2000000000);
            formatedSpeed.Should().Be("1.86 GB/s");
        }

        #endregion Methods
    }
}