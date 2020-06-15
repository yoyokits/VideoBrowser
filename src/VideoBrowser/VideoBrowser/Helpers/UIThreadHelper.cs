namespace VideoBrowser.Helpers
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Defines the <see cref="UIThreadHelper" />.
    /// </summary>
    public static class UIThreadHelper
    {
        #region Methods

        /// <summary>
        /// The DelayedInvokeAsync.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        /// <param name="milliSecondsDelay">The milliSecondsDelay<see cref="int"/>.</param>
        public static void DelayedInvokeAsync(Action action, int milliSecondsDelay)
        {
            Task.Run(async () =>
            {
                await Task.Delay(milliSecondsDelay);
                InvokeAsync(action);
            });
        }

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
        /// <param name="dispatcherObject">The dispatcherObject<see cref="DispatcherObject"/>.</param>
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

        /// <summary>
        /// The InvokeAsync.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public static void InvokeAsync(Action action)
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
                dispatcher.InvokeAsync(() => action?.Invoke());
            }
        }

        #endregion Methods
    }
}