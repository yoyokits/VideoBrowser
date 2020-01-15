namespace YoutubeDlGui.Extensions
{
    /// <summary>
    /// Defines the <see cref="StringExtension" />
    /// </summary>
    public static class StringExtension
    {
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

        #endregion Methods
    }
}