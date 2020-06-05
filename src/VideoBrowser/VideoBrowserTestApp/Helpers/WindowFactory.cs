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
        /// <param name="ownerWindow">The ownerWindow<see cref="Window"/>.</param>
        /// <returns>The <see cref="MetroWindow"/>.</returns>
        internal static MetroWindow Create(FrameworkElement view, Window ownerWindow)
        {
            var window = new MetroWindow { Content = view, MinWidth = 600, MinHeight = 400, SizeToContent = SizeToContent.WidthAndHeight, Owner = ownerWindow };
            return window;
        }

        /// <summary>
        /// The CreateAndShow.
        /// </summary>
        /// <param name="view">The view<see cref="FrameworkElement"/>.</param>
        /// <param name="ownerWindow">The ownerWindow<see cref="Window"/>.</param>
        internal static void CreateAndShow(FrameworkElement view, Window ownerWindow)
        {
            var window = new Window { Content = view, MinWidth = 600, MinHeight = 400, SizeToContent = SizeToContent.WidthAndHeight, Owner = ownerWindow };
            window.Show();
        }

        #endregion Methods
    }
}