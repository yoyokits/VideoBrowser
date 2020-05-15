namespace YoutubeDlGui.Extensions
{
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="RangeExtension" />
    /// </summary>
    public static class RangeExtension
    {
        #region Methods

        /// <summary>
        /// The Center
        /// </summary>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <returns>The <see cref="double"/></returns>
        public static double Center(this RangeDouble range)
        {
            var center = range.Start + range.Length() * 0.5;
            return center;
        }

        /// <summary>
        /// The Contains
        /// </summary>
        /// <param name="container">The container<see cref="RangeDouble"/></param>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool Contains(this RangeDouble container, RangeDouble range)
        {
            var contains = range.Start.IsInRange(container.Start, container.End) && range.End.IsInRange(container.Start, container.End);
            return contains;
        }

        /// <summary>
        /// The Contains
        /// </summary>
        /// <param name="container">The container<see cref="RangeInt"/></param>
        /// <param name="range">The range<see cref="RangeInt"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool Contains(this RangeInt container, RangeInt range)
        {
            var contains = range.Start.IsInRange(container.Start, container.End) && range.End.IsInRange(container.Start, container.End);
            return contains;
        }

        /// <summary>
        /// The IsEmpty
        /// </summary>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsEmpty(this RangeDouble range)
        {
            var isEmpty = range.Start.IsZero() && range.End.IsZero();
            return isEmpty;
        }

        /// <summary>
        /// Determines whether [is in range] [the specified minimum].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsInRange(this double value, double min, double max)
        {
            return (min < max) ? value >= min && value <= max : value >= max && value <= min;
        }

        /// <summary>
        /// The IsInRange
        /// </summary>
        /// <param name="value">The value<see cref="int"/></param>
        /// <param name="min">The min<see cref="int"/></param>
        /// <param name="max">The max<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsInRange(this int value, int min, int max)
        {
            return (min < max) ? value >= min && value <= max : value >= max && value <= min;
        }

        /// <summary>
        /// Lengthes the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>The length in double.</returns>
        public static double Length(this RangeDouble range)
        {
            return range.End - range.Start;
        }

        /// <summary>
        /// The Length
        /// </summary>
        /// <param name="range">The range<see cref="RangeInt"/></param>
        /// <returns>The <see cref="int"/></returns>
        public static int Length(this RangeInt range)
        {
            return range.End - range.Start;
        }

        /// <summary>
        /// The MoveInsideContainer
        /// </summary>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <param name="container">The container<see cref="RangeDouble"/></param>
        /// <returns>The <see cref="RangeDouble"/></returns>
        public static RangeDouble MoveInsideContainer(this RangeDouble range, RangeDouble container)
        {
            if (container.Contains(range))
            {
                return range;
            }

            var length = range.Length();
            if (length > container.Length())
            {
                length = container.Length();
            }

            var rangeInside = range.Start < container.Start ? new RangeDouble(container.Start, container.Start + length) : new RangeDouble(container.End - length, container.End);
            return rangeInside;
        }

        /// <summary>
        /// The Offset the range to the specified distance.
        /// </summary>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <param name="distance">The distance<see cref="double"/></param>
        /// <param name="extentRange">The extentRange<see cref="RangeDouble"/></param>
        /// <returns>The <see cref="RangeDouble"/></returns>
        public static RangeDouble Offset(this RangeDouble range, double distance, RangeDouble extentRange)
        {
            var length = range.Length();
            if (extentRange.Length() < length)
            {
                length = extentRange.Length();
            }

            var start = range.Start + distance;
            var end = start + length;
            var newRange = new RangeDouble(start, end);
            if (extentRange.Contains(newRange))
            {
                return newRange;
            }

            return start < extentRange.Start
                ? new RangeDouble(extentRange.Start, extentRange.Start + length)
                : new RangeDouble(extentRange.End - length, extentRange.End);
        }

        #endregion Methods
    }
}