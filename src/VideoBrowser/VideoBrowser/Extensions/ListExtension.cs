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
        /// The AddRange.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="ICollection{T}"/>.</param>
        /// <param name="otherList">The otherList<see cref="IEnumerable{T}"/>.</param>
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> otherList)
        {
            foreach (var item in otherList)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// The ClearAndDispose.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="list">The list<see cref="ICollection{T}"/>.</param>
        public static void ClearAndDispose<T>(this ICollection<T> list)
        {
            foreach (var item in list)
            {
                var disposableItem = item as IDisposable;
                disposableItem?.Dispose();
            }

            list.Clear();
        }

        #endregion Methods
    }
}