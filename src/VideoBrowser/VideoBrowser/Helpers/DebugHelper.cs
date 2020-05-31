namespace VideoBrowser.Helpers
{
    /// <summary>
    /// Defines the <see cref="DebugHelper" />.
    /// </summary>
    public class DebugHelper
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether IsDebug.
        /// </summary>
        internal static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        #endregion Properties
    }
}