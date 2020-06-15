namespace VideoBrowser.Extensions
{
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="FocusExtension" />.
    /// </summary>
    public static class FocusExtension
    {
        #region Fields

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(nameof(IsFocusedProperty).Name(), typeof(bool), typeof(FocusExtension), new PropertyMetadata(false, OnIsFocusedChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetIsFocused.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsFocused(DependencyObject obj) => (bool)obj.GetValue(IsFocusedProperty);

        /// <summary>
        /// The SetIsFocused.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetIsFocused(DependencyObject obj, bool value) => obj.SetValue(IsFocusedProperty, value);

        /// <summary>
        /// The OnIsFocusedChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;
            var isFocused = (bool)e.NewValue;
            if (isFocused)
            {
                element.Focus();
            }
        }

        #endregion Methods
    }
}