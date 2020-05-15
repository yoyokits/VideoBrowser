namespace AirspaceFixer
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="AirspacePanel" />
    /// </summary>
    public class AirspacePanel : ContentControl
    {
        #region Fields

        public static readonly DependencyProperty FixAirspaceProperty =
            DependencyProperty.Register(nameof(FixAirspace),
                                        typeof(bool),
                                        typeof(AirspacePanel),
                                        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnFixAirspaceChanged)));

        private ContentControl _airspaceContent;

        private Image _airspaceScreenshot;

        private float _scalingFactor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AirspacePanel"/> class.
        /// </summary>
        public AirspacePanel()
        {
            Loaded += (_, __) => GetScalingFactor();
        }

        /// <summary>
        /// Initializes static members of the <see cref="AirspacePanel"/> class.
        /// </summary>
        static AirspacePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirspacePanel), new FrameworkPropertyMetadata(typeof(AirspacePanel)));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether FixAirspace
        /// </summary>
        public bool FixAirspace
        {
            get { return (bool)GetValue(FixAirspaceProperty); }
            set { SetValue(FixAirspaceProperty, value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetImageSourceFromBitmap
        /// </summary>
        /// <param name="bitmap">The bitmap<see cref="System.Drawing.Bitmap"/></param>
        /// <returns>The <see cref="ImageSource"/></returns>
        public ImageSource GetImageSourceFromBitmap(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        /// <summary>
        /// The OnApplyTemplate
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _airspaceContent = GetTemplateChild("PART_AirspaceContent") as ContentControl;
            _airspaceScreenshot = GetTemplateChild("PART_AirspaceScreenshot") as Image;
        }

        // https://stackoverflow.com/questions/5977445/how-to-get-windows-display-settings
        /// <summary>
        /// The GetDeviceCaps
        /// </summary>
        /// <param name="hdc">The hdc<see cref="IntPtr"/></param>
        /// <param name="nIndex">The nIndex<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        [DllImport("gdi32.dll")]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// The OnFixAirspaceChanged
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/></param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/></param>
        private static async void OnFixAirspaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as AirspacePanel;
            if (panel == null || panel.ActualWidth == 0 || panel.ActualHeight == 0 || PresentationSource.FromVisual(panel) == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                panel.CreateScreenshotFromContent();
                panel._airspaceScreenshot.Visibility = Visibility.Visible;
                await Task.Delay(300);
                panel._airspaceContent.Visibility = Visibility.Hidden;
            }
            else
            {
                panel._airspaceContent.Visibility = Visibility.Visible;
                panel._airspaceScreenshot.Visibility = Visibility.Hidden;
                panel._airspaceScreenshot.Source = null;
            }
        }

        /// <summary>
        /// The CreateScreenshotFromContent
        /// </summary>
        private void CreateScreenshotFromContent()
        {
            var source = PresentationSource.FromVisual(this);

            var scalingX = source.CompositionTarget.TransformToDevice.M11;
            var scalingY = source.CompositionTarget.TransformToDevice.M22;

            var upperLeftPoint = _airspaceContent.PointToScreen(new Point(0, 0));
            var bounds = new System.Drawing.Rectangle((int)(upperLeftPoint.X * _scalingFactor),
                                                      (int)(upperLeftPoint.Y * _scalingFactor),
                                                      (int)(_airspaceContent.RenderSize.Width * scalingX),
                                                      (int)(_airspaceContent.RenderSize.Height * scalingY));

            using (var bitmap = new System.Drawing.Bitmap(bounds.Width, bounds.Height))
            {
                using (var g = System.Drawing.Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top),
                                     System.Drawing.Point.Empty,
                                     new System.Drawing.Size(bounds.Width, bounds.Height));
                }

                _airspaceScreenshot.Source = GetImageSourceFromBitmap(bitmap);
            }
        }

        /// <summary>
        /// The GetScalingFactor
        /// </summary>
        private void GetScalingFactor()
        {
            var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            var desktop = g.GetHdc();
            var LogicalScreenHeight = GetDeviceCaps(desktop, 10);
            var PhysicalScreenHeight = GetDeviceCaps(desktop, 117);

            var ScreenScalingFactor = PhysicalScreenHeight / (float)LogicalScreenHeight;

            _scalingFactor = ScreenScalingFactor; // 1.25 = 125%
        }

        #endregion Methods
    }
}