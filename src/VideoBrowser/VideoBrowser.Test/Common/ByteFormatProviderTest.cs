namespace VideoBrowser.Test.Common
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="ByteFormatProviderTest" />
    /// </summary>
    [TestClass]
    public class ByteFormatProviderTest
    {
        #region Methods

        /// <summary>
        /// The FileSizeFormatProvider_With_String
        /// </summary>
        [TestMethod]
        public void ByteSizeFormatProvider_With_String()
        {
            var provider = new ByteFormatProvider();
            var size = 1000000.0;
            var formatedSize = string.Format(new ByteFormatProvider(), "{0:fs}", size);
            formatedSize.Should().Be("976.56 KB");
            var speed = 1000000000.0;
            var formatedSpeed = string.Format(new ByteFormatProvider(), "{0:s}", speed);
            formatedSpeed.Should().Be("953.67 MB/s");
        }

        #endregion Methods
    }
}