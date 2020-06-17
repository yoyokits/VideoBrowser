namespace VideoBrowser.Controls.CefSharpBrowser
{
    using CefSharp;
    using CefSharp.WinForms;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="KeyboardHandler" />.
    /// </summary>
    public class CefKeyboardHandler : IKeyboardHandler
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CefKeyboardHandler"/> class.
        /// </summary>
        /// <param name="host">The host<see cref="WebBrowser"/>.</param>
        internal CefKeyboardHandler(FrameworkElement host)
        {
            this.WebBrowser = host;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the WebBrowser.
        /// </summary>
        public FrameworkElement WebBrowser { get; }

        /// <summary>
        /// Gets or sets a value indicating whether IsFullScreen.
        /// </summary>
        private bool IsFullScreen { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnKeyEvent.
        /// </summary>
        /// <param name="chromiumWebBrowser">The chromiumWebBrowser<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="type">The type<see cref="KeyType"/>.</param>
        /// <param name="windowsKeyCode">The windowsKeyCode<see cref="int"/>.</param>
        /// <param name="nativeKeyCode">The nativeKeyCode<see cref="int"/>.</param>
        /// <param name="modifiers">The modifiers<see cref="CefEventFlags"/>.</param>
        /// <param name="isSystemKey">The isSystemKey<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return false;

            return this.WebBrowser.Dispatcher.Invoke(() =>
            {
                var window = Window.GetWindow(this.WebBrowser);
                var routedEvent = UIElement.KeyDownEvent;
                var kb = Keyboard.PrimaryDevice;
                var ps = PresentationSource.FromDependencyObject(this.WebBrowser);
                var ts = 0;
                var key = KeyInterop.KeyFromVirtualKey(windowsKeyCode);

                var e = new System.Windows.Input.KeyEventArgs(kb, ps, ts, key)
                {
                    RoutedEvent = routedEvent
                };

                // WPF gets modifiers from PrimaryKeyboard only
                System.Diagnostics.Debug.WriteLine("Raising {0} {1}+{{{2}}}", routedEvent, key, Keyboard.Modifiers);
                this.WebBrowser.RaiseEvent(e);
                return e.Handled;
            });
        }

        /// <summary>
        /// The OnPreKeyEvent.
        /// </summary>
        /// <param name="browserControl">The browserControl<see cref="IWebBrowser"/>.</param>
        /// <param name="browser">The browser<see cref="IBrowser"/>.</param>
        /// <param name="type">The type<see cref="KeyType"/>.</param>
        /// <param name="windowsKeyCode">The windowsKeyCode<see cref="int"/>.</param>
        /// <param name="nativeKeyCode">The nativeKeyCode<see cref="int"/>.</param>
        /// <param name="modifiers">The modifiers<see cref="CefEventFlags"/>.</param>
        /// <param name="isSystemKey">The isSystemKey<see cref="bool"/>.</param>
        /// <param name="isKeyboardShortcut">The isKeyboardShortcut<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            if ((Keys)windowsKeyCode == Keys.Escape)
            {
                chromiumWebBrowser.Invoke((MethodInvoker)delegate
                {
                    var screenSize = Screen.FromControl(chromiumWebBrowser).Bounds.Size;
                    bool fullScreen = screenSize == chromiumWebBrowser.Size;
                    if (fullScreen)
                    {
                        chromiumWebBrowser.DisplayHandler.OnFullscreenModeChange(browserControl, browser, false);
                    }
                });
            }

            return false;
        }

        #endregion Methods
    }
}