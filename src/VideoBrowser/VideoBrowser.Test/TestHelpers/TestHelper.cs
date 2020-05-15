namespace VideoBrowser.Test.TestHelpers
{
    using System;

    /// <summary>
    /// Defines the <see cref="TestHelper" />.
    /// </summary>
    internal static class TestHelper
    {
        #region Properties

        /// <summary>
        /// Gets the Random.
        /// </summary>
        private static Random Random { get; } = new Random();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetRandomInt.
        /// </summary>
        /// <param name="start">The start<see cref="int"/>.</param>
        /// <param name="end">The end<see cref="int"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        internal static int GetRandomInt(int start, int end)
        {
            var random = start + Random.NextDouble() * (end - start);
            return (int)random;
        }

        /// <summary>
        /// The GetRandomLong.
        /// </summary>
        /// <param name="start">The start<see cref="long"/>.</param>
        /// <param name="end">The end<see cref="long"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        internal static long GetRandomLong(long start, long end)
        {
            var random = start + Random.NextDouble() * (end - start);
            return (long)random;
        }

        #endregion Methods
    }
}