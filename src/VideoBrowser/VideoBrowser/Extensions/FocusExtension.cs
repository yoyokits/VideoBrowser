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

        public static readonly DependencyProperty TrackFocusProperty =
            DependencyProperty.RegisterAttached(nameof(TrackFocusProperty).Name(), typeof(bool), typeof(FocusExtension), new PropertyMetadata(false, OnTrackFocusChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetIsFocused.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsFocused(DependencyObject obj) => (bool)obj.GetValue(IsFocusedProperty);

        /// <summary>
        /// The GetTrackFocus.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetTrackFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackFocusProperty);
        }

        /// <summary>
        /// The SetIsFocused.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetIsFocused(DependencyObject obj, bool value) => obj.SetValue(IsFocusedProperty, value);

        /// <summary>
        /// The SetTrackFocus.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetTrackFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackFocusProperty, value);
        }

        /// <summary>
        /// The OnGotFocus.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            if (!GetIsFocused(element))
            {
                SetIsFocused(element, true);
            }
        }

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

        /// <summary>
        /// The OnLostFocus.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            if (GetIsFocused(element))
            {
                SetIsFocused(element, false);
            }
        }

        /// <summary>
        /// The OnTrackFocusChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnTrackFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement element))
            {
                return;
            }

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (!oldValue && newValue) // If changed from false to true
            {
                element.GotFocus += OnGotFocus;
                element.LostFocus += OnLostFocus;
            }
            else if (oldValue && !newValue) // If changed from true to false
            {
                element.GotFocus -= OnGotFocus;
                element.LostFocus -= OnLostFocus;
            }
        }

        #endregion Methods
    }
}