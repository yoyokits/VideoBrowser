namespace VideoBrowser.Helpers
{
    using System.Drawing;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="ImageHelper" />.
    /// </summary>
    public static class ImageHelper
    {
        #region Methods

        /// <summary>
        /// The ToImageSource.
        /// </summary>
        /// <param name="icon">The icon<see cref="Icon"/>.</param>
        /// <returns>The <see cref="ImageSource"/>.</returns>
        public static ImageSource ToImageSource(this Icon icon)
        {
            var imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        #endregion Methods
    }
}