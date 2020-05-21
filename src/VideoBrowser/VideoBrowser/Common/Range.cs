namespace VideoBrowser.Common
{
    using System;
    using VideoBrowser.If;

    /// <summary>
    /// Defines the <see cref="Range{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Range<T> : IRange<T> where T : IComparable<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public Range(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the End
        /// </summary>
        public T End { get; }

        /// <summary>
        /// Gets the Start
        /// </summary>
        public T Start { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="other">The other<see cref="IRange{T}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Equals(IRange<T> other)
        {
            return this.Start.Equals(other.Start) && this.End.Equals(other.End);
        }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="obj">The <see cref="object"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Equals(object obj)
        {
            return this == (Range<T>)obj;
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
            return $"Start:{this.Start};End:{this.End}";
        }

        #endregion Methods

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator ==(Range<T> left, Range<T> right)
        {
            return Equals(left.Start, right.Start) && Equals(left.End, right.End);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns> The result of the operator. </returns>
        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !(left == right);
        }
    }
}