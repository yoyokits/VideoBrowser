namespace VideoBrowser.Helpers
{
    using System;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="UIThreadHelper" />
    /// </summary>
    public static class UIThreadHelper
    {
        #region Methods

        /// <summary>
        /// The Invoke
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        public static void Invoke(Action action)
        {
            var dispatcher = Application.Current.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                action?.Invoke();
            }
            else
            {
                dispatcher.Invoke(() => action?.Invoke());
            }
        }

        #endregion Methods
    }
}