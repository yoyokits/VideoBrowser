using YoutubeDlGui.Common;

namespace YoutubeDlGui.Extensions
{
    /// <summary>
    /// Defines the <see cref="StringExtension" />
    /// </summary>
    public static class StringExtension
    {
        private static ByteFormatProvider ByteSizeFormatProvider { get; } = new ByteFormatProvider();

        #region Methods

        /// <summary>
        /// Replace dependency property name to property name.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string Name(this string propertyName)
        {
            var name = propertyName.Replace("Property", string.Empty);
            return name;
        }

        public static string ToFormatedSpeed(int speed)
        {
            var formated = string.Format(ByteSizeFormatProvider, "{0:s}", speed);
            return formated;
        }

        public static string ToFormatedByte(int byteSize)
        {
            var formated = string.Format(ByteSizeFormatProvider, "{0:fs}", byteSize);
            return formated;
        }

        #endregion Methods
    }
}