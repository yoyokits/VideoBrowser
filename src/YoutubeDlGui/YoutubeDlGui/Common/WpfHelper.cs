namespace YoutubeDlGui.Common
{
    using System;
    using System.Windows.Threading;

    /// <summary>
    /// Defines the <see cref="WpfHelper" />.
    /// </summary>
    public static class WpfHelper
    {
        #region Methods

        /// <summary>
        /// The InvokeUIThread.
        /// </summary>
        /// <param name="element">The element<see cref="DispatcherObject"/>.</param>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public static void InvokeUIThread(this DispatcherObject element, Action action)
        {
            if (element == null || action == null)
            {
                return;
            }

            var dispatcher = element.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                dispatcher.Invoke(action);
            }
        }

        #endregion Methods
    }
}