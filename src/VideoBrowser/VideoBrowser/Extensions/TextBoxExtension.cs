namespace VideoBrowser.Extensions
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="TextBoxExtension" />.
    /// </summary>
    public static class TextBoxExtension
    {
        #region Fields

        public static readonly DependencyProperty CaretIndexProperty =
            DependencyProperty.RegisterAttached(nameof(CaretIndexProperty).Name(), typeof(int), typeof(TextBoxExtension), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCaretIndexChanged));

        public static readonly DependencyProperty IsClickToSelectAllProperty =
            DependencyProperty.RegisterAttached(nameof(IsClickToSelectAllProperty).Name(), typeof(bool), typeof(TextBoxExtension), new PropertyMetadata(false, OnIsClickToSelectAllChanged));

        public static readonly DependencyProperty SelectionLengthProperty =
            DependencyProperty.RegisterAttached(nameof(SelectionLengthProperty).Name(), typeof(int), typeof(TextBoxExtension), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionLengthChanged));

        public static readonly DependencyProperty SelectionStartProperty =
            DependencyProperty.RegisterAttached(nameof(SelectionStartProperty).Name(), typeof(int), typeof(TextBoxExtension), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionStartChanged));

        public static readonly DependencyProperty TrackCaretIndexProperty =
            DependencyProperty.RegisterAttached(nameof(TrackCaretIndexProperty).Name(), typeof(bool), typeof(TextBoxExtension), new PropertyMetadata(false, OnTrackCaretIndexChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetCaretIndex.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetCaretIndex(DependencyObject obj) => (int)obj.GetValue(CaretIndexProperty);

        /// <summary>
        /// The GetIsClickToSelectAll.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsClickToSelectAll(DependencyObject obj) => (bool)obj.GetValue(IsClickToSelectAllProperty);

        /// <summary>
        /// The GetSelectionLength.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetSelectionLength(DependencyObject obj) => (int)obj.GetValue(SelectionLengthProperty);

        /// <summary>
        /// The GetSelectionStart.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetSelectionStart(DependencyObject obj) => (int)obj.GetValue(SelectionStartProperty);

        /// <summary>
        /// The GetTrackCaretIndex.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetTrackCaretIndex(DependencyObject obj) => (bool)obj.GetValue(TrackCaretIndexProperty);

        /// <summary>
        /// The SetCaretIndex.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        public static void SetCaretIndex(DependencyObject obj, int value) => obj.SetValue(CaretIndexProperty, value);

        /// <summary>
        /// The SetIsClickToSelectAll.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetIsClickToSelectAll(DependencyObject obj, bool value) => obj.SetValue(IsClickToSelectAllProperty, value);

        /// <summary>
        /// The SetSelectionLength.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        public static void SetSelectionLength(DependencyObject obj, int value) => obj.SetValue(SelectionLengthProperty, value);

        /// <summary>
        /// The SetSelectionStart.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        public static void SetSelectionStart(DependencyObject obj, int value) => obj.SetValue(SelectionStartProperty, value);

        /// <summary>
        /// The SetTrackCaretIndex.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetTrackCaretIndex(DependencyObject obj, bool value) => obj.SetValue(TrackCaretIndexProperty, value);

        /// <summary>
        /// The OnCaretIndexChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnCaretIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textbox))
            {
                return;
            }

            var caretIndex = (int)e.NewValue;
            if (textbox.CaretIndex != caretIndex)
            {
                textbox.CaretIndex = caretIndex;
            }
        }

        /// <summary>
        /// The OnIsClickToSelectAllChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnIsClickToSelectAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textbox))
            {
                return;
            }

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (!oldValue && newValue) // If changed from false to true
            {
                textbox.PreviewMouseLeftButtonDown += OnTextbox_PreviewMouseLeftButtonDown;
            }
            else if (oldValue && !newValue) // If changed from true to false
            {
                textbox.PreviewMouseLeftButtonDown -= OnTextbox_PreviewMouseLeftButtonDown;
            }
        }

        /// <summary>
        /// The OnSelectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private static void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                SetCaretIndex(textbox, textbox.CaretIndex);
                SetSelectionStart(textbox, textbox.SelectionStart);
                SetSelectionLength(textbox, textbox.SelectionLength);
            }
        }

        /// <summary>
        /// The OnSelectionLengthChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnSelectionLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textbox))
            {
                return;
            }

            var selectionLength = (int)e.NewValue;
            if (textbox.SelectionLength != selectionLength)
            {
                textbox.SelectionLength = selectionLength;
            }
        }

        /// <summary>
        /// The OnSelectionStartChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnSelectionStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textbox))
            {
                return;
            }

            var selectionStart = (int)e.NewValue;
            if (textbox.SelectionStart != selectionStart)
            {
                textbox.SelectionStart = selectionStart;
            }
        }

        /// <summary>
        /// The OnTextbox_PreviewMouseLeftButtonDown.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="MouseButtonEventArgs"/>.</param>
        private static void OnTextbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBox textbox))
            {
                return;
            }

            if (!textbox.IsFocused)
            {
                UIThreadHelper.DelayedInvokeAsync(() =>
                {
                    textbox.Focus();
                    textbox.SelectAll();
                }, 500);
            }
        }

        /// <summary>
        /// The OnTrackCaretIndexChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnTrackCaretIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textbox))
            {
                return;
            }

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if (!oldValue && newValue) // If changed from false to true
            {
                textbox.SelectionChanged += OnSelectionChanged;
            }
            else if (oldValue && !newValue) // If changed from true to false
            {
                textbox.SelectionChanged -= OnSelectionChanged;
            }
        }

        #endregion Methods
    }
}