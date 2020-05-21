namespace VideoBrowser.Common
{
    using System;
    using VideoBrowser.Extensions;
    using VideoBrowser.If;

    /// <summary>
    /// Defines the <see cref="RangeDouble" />
    /// </summary>
    public struct RangeDouble : IRange<double>
    {
        private const double PRECISION = 1E-6;

        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="start">The start<see cref="double"/></param>
        /// <param name="end">The end<see cref="double"/></param>
        /// <param name="allowInvert">The allowInvert<see cref="bool"/></param>
        public RangeDouble(double start, double end, bool allowInvert = false)
        {
            if (!allowInvert && start >= end)
            {
                throw new ArgumentException($"{nameof(start)} is equal or greater than {nameof(end)}");
            }

            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the Empty
        /// </summary>
        public static RangeDouble Empty { get; } = new RangeDouble();

        /// <summary>
        /// Gets the End
        /// </summary>
        public double End { get; }

        /// <summary>
        /// Gets the Start
        /// </summary>
        public double Start { get; }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="other">The other<see cref="IRange{double}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Equals(IRange<double> other)
        {
            return Equals(this.Start, other.Start) && Equals(this.End, other.End);
        }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="obj">The <see cref="object"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is RangeDouble))
            {
                return false;
            }

            return this == (RangeDouble)obj;
        }

        /// <summary>Equalses the specified other.</summary>
        /// <param name="other">The other.</param>
        public bool Equals(RangeDouble other)
        {
            return this.Start.IsEqualTo(other.Start, PRECISION) && this.End.IsEqualTo(other.End, PRECISION);
        }

        /// <summary>
        /// The GetHashCode
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public override int GetHashCode()
        {
            var startHash = this.Start.GetHashCode();
            var endHash = this.End.GetHashCode();
            return startHash ^ (startHash << 8) ^ endHash ^ (endHash << 4);
        }

        /// <summary>
        /// The ToString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public override string ToString()
        {
            return $"Start:{this.Start.Format()};End:{this.End.Format()};Length:{this.Length().Format()}";
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator ==(RangeDouble left, RangeDouble right)
        {
            return left.Start.IsEqualTo(right.Start, PRECISION) && left.End.IsEqualTo(right.End, PRECISION);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator !=(RangeDouble left, RangeDouble right)
        {
            return !(left == right);
        }
    }
}