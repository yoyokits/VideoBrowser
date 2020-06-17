// Copyright © 2014 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

namespace VideoBrowser.Controls.CefSharpBrowser.Helpers
{
    /// <summary>
    /// Defines the <see cref="InternetProxyInfo" />.
    /// </summary>
    public struct InternetProxyInfo
    {
        #region Fields

        public InternetOpenType AccessType;

        public string ProxyAddress;

        public string ProxyBypass;

        #endregion Fields
    }
}