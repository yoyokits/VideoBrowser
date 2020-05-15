namespace YoutubeDlGui.Extensions
{
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="StringExtension" />
    /// </summary>
    public static class StringExtension
    {
        #region Properties

        /// <summary>
        /// Gets the ByteSizeFormatProvider
        /// </summary>
        private static ByteFormatProvider ByteSizeFormatProvider { get; } = new ByteFormatProvider();

        #endregion Properties

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

        /// <summary>
        /// The ToFormatedByte
        /// </summary>
        /// <param name="byteSize">The byteSize<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToFormatedByte(int byteSize)
        {
            var formated = string.Format(ByteSizeFormatProvider, "{0:fs}", byteSize);
            return formated;
        }

        /// <summary>
        /// The ToFormatedSpeed
        /// </summary>
        /// <param name="speed">The speed<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToFormatedSpeed(int speed)
        {
            var formated = string.Format(ByteSizeFormatProvider, "{0:s}", speed);
            return formated;
        }

        #endregion Methods
    }
}