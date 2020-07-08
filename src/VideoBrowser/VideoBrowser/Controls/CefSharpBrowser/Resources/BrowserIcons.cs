namespace VideoBrowser.Controls.CefSharpBrowser.Resources
{
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="BrowserIcons" />.
    /// </summary>
    public static class BrowserIcons
    {
        #region Properties

        /// <summary>
        /// Gets the Lock.
        /// </summary>
        public static Geometry Lock { get; } = Geometry.Parse("M2,16L2,30 22,30 22,16z M12,2C8.14,2,5,5.141,5,9L5,14 19,14 19,9C19,5.14,15.86,2,12,2z M12,0C16.962,0,21,4.037,21,9L21,14 24,14 24,32 0,32 0,14 3,14 3,9C3,4,7,0,12,0z");

        /// <summary>
        /// Gets the Star.
        /// </summary>
        public static Geometry Star { get; } = Geometry.Parse("M16,0L21,10.533997 32,12.223022 24,20.421997 25.889008,32 16,26.533997 6.1109924,32 8,20.421997 0,12.223022 11.057007,10.533997z");

        /// <summary>
        /// Gets the StarWF.
        /// </summary>
        public static Geometry StarWF { get; } = Geometry.Parse("M16,5.6780396L12.681,12.743042 5.2630005,13.877014 10.631989,19.378052 9.3639984,27.147034 16,23.48 22.634995,27.147034 21.366,19.378052 26.735,13.877014 19.317,12.743042z M16,0L20.942993,10.533997 32,12.223022 24,20.421997 25.886993,32 16,26.533997 6.111,32 8,20.421997 0,12.223 11,10.533997z");

        #endregion Properties
    }
}