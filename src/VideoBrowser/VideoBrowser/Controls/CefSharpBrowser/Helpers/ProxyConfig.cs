// Copyright © 2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines the <see cref="ProxyConfig" />.
    /// </summary>
    public class ProxyConfig
    {
        #region Constants

        private const uint InternetOptionProxy = 38;

        #endregion Constants

        #region Methods

        /// <summary>
        /// The GetProxyInformation.
        /// </summary>
        /// <returns>The <see cref="InternetProxyInfo"/>.</returns>
        public static InternetProxyInfo GetProxyInformation()
        {
            var bufferLength = 0;
            InternetQueryOption(IntPtr.Zero, InternetOptionProxy, IntPtr.Zero, ref bufferLength);
            var buffer = IntPtr.Zero;

            try
            {
                buffer = Marshal.AllocHGlobal(bufferLength);

                if (InternetQueryOption(IntPtr.Zero, InternetOptionProxy, buffer, ref bufferLength))
                {
                    var ipi = (InternetProxyInfo)Marshal.PtrToStructure(buffer, typeof(InternetProxyInfo));
                    return ipi;
                }

                throw new Win32Exception();
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
        }

        /// <summary>
        /// The InternetQueryOption.
        /// </summary>
        /// <param name="hInternet">The hInternet<see cref="IntPtr"/>.</param>
        /// <param name="dwOption">The dwOption<see cref="uint"/>.</param>
        /// <param name="lpBuffer">The lpBuffer<see cref="IntPtr"/>.</param>
        /// <param name="lpdwBufferLength">The lpdwBufferLength<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetQueryOption(IntPtr hInternet, uint dwOption, IntPtr lpBuffer, ref int lpdwBufferLength);

        #endregion Methods
    }
}