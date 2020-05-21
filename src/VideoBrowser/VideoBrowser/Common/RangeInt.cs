namespace VideoBrowser.Common
{
    using System;
    using VideoBrowser.If;

    /// <summary>
    /// Defines the <see cref="RangeInt" />
    /// </summary>
    public struct RangeInt : IRange<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="start">The start<see cref="int"/></param>
        /// <param name="end">The end<see cref="int"/></param>
        /// <param name="allowInvert">The allowInvert<see cref="bool"/></param>
        public RangeInt(int start, int end, bool allowInvert = false)
        {
            if (!allowInvert && start > end)
            {
                throw new ArgumentException($"{nameof(start)} is equal or greater than {nameof(end)}");
            }

            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the End
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Gets the Start
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="other">The other<see cref="IRange{int}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Equals(IRange<int> other)
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
            if (!(obj is RangeInt))
            {
                return false;
            }

            return this == (RangeInt)obj;
        }

        /// <summary>Equalses the specified other.</summary>
        /// <param name="other">The other.</param>
        public bool Equals(RangeInt other)
        {
            return this.Start == other.Start && this.End == other.End;
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
            return $"Start:{this.Start};End:{this.End};Length:{this.End - this.Start}";
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator ==(RangeInt left, RangeInt right)
        {
            return left.Start == right.Start && left.End == right.End;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator !=(RangeInt left, RangeInt right)
        {
            return !(left == right);
        }
    }
}