namespace VideoBrowser.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ListExtension" />.
    /// </summary>
    public static class ListExtension
    {
        #region Methods

        /// <summary>
        /// The ClearAndDispose.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="ICollection{T}"/>.</param>
        public static void ClearAndDispose<T>(this ICollection<T> list) where T : IDisposable
        {
            foreach (var item in list)
            {
                item?.Dispose();
            }

            list.Clear();
        }

        #endregion Methods
    }
}