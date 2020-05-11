namespace YoutubeDlGui.Core
{
    using CefSharp;
    using CefSharp.WinForms;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="KeyboardHandler" />.
    /// </summary>
    public class CefKeyboardHandler : IKeyboardHandler
    {
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