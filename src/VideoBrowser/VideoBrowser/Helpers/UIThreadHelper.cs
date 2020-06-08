namespace VideoBrowser.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Defines the <see cref="UIThreadHelper" />.
    /// </summary>
    public static class UIThreadHelper
    {
        #region Methods

        /// <summary>
        /// The Invoke.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public static void Invoke(Action action)
        {
            if (Application.Current == null)
            {
                return;
            }

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

        /// <summary>
        /// The Invoke.
        /// </summary>
        /// <param name="dispatcher">The dispatcher<see cref="Dispatcher"/>.</param>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public static void Invoke(this DispatcherObject dispatcherObject, Action action)
        {
            var dispatcher = dispatcherObject.Dispatcher;
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