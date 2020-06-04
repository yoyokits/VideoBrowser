namespace VideoBrowserTestApp.Helpers
{
    using MahApps.Metro.Controls;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="WindowFactory" />.
    /// </summary>
    internal static class WindowFactory
    {
        #region Methods

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="view">The view<see cref="FrameworkElement"/>.</param>
        /// <returns>The <see cref="MetroWindow"/>.</returns>
        internal static MetroWindow Create(FrameworkElement view)
        {
            var window = new MetroWindow { Content = view, MinWidth = 600, MinHeight = 400, SizeToContent = SizeToContent.WidthAndHeight };
            return window;
        }

        /// <summary>
        /// The CreateAndShow.
        /// </summary>
        /// <param name="view">The view<see cref="FrameworkElement"/>.</param>
        internal static void CreateAndShow(FrameworkElement view)
        {
            var window = new Window { Content = view, MinWidth = 600, MinHeight = 400, SizeToContent = SizeToContent.WidthAndHeight };
            window.ShowDialog();
        }

        #endregion Methods
    }
}