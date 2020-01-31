namespace YoutubeDlGui.Common
{
    using System;
    using YoutubeDlGui.Extensions;

    /// <summary>
    /// Defines the <see cref="Percentage" />
    /// </summary>
    internal struct Percentage : IEquatable<Percentage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="percent">The percent<see cref="double"/></param>
        public Percentage(double percent)
        {
            this.Percent = percent;
        }

        /// <summary>
        /// Gets the Empty
        /// </summary>
        public static Percentage Empty { get; } = new Percentage(double.NaN);

        /// <summary>
        /// Gets a value indicating whether IsEmpty
        /// </summary>
        public bool IsEmpty => double.IsNaN(this.Percent);

        /// <summary>
        /// Gets the Normalized
        /// </summary>
        public double Normalized => this.Percent * 0.01;

        /// <summary>
        /// Gets the Percent
        /// </summary>
        public double Percent { get; }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="other">The other<see cref="Percentage"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Equals(Percentage other)
        {
            var equals = this.Percent.IsEqualTo(other.Percent);
            return equals;
        }

        /// <summary>
        /// The GetHashCode
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public override int GetHashCode()
        {
            return this.Percent.GetHashCode();
        }

        /// <summary>
        /// The ToString
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public override string ToString()
        {
            var message = $"{this.Percent.Format()}%";
            return base.ToString();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            var equals = left.Percent.IsEqualTo(right.Percent);
            return equals;
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            var equals = left.Percent.IsEqualTo(right.Percent);
            return !equals;
        }
    }
}