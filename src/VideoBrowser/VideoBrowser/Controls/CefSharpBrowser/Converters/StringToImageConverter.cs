namespace VideoBrowser.Controls.CefSharpBrowser.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using VideoBrowser.Controls.CefSharpBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="StringToImageConverter" />.
    /// </summary>
    public class StringToImageConverter : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static StringToImageConverter Instance { get; } = new StringToImageConverter();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string imageString) || string.IsNullOrEmpty(imageString))
            {
                return null;
            }

            if (imageString.IsImageUrl())
            {
                return imageString;
            }

            var imageSource = imageString.FindIconForFilename(true);
            return imageSource;
        }

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}