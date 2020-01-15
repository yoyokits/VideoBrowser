namespace YoutubeDlGui.If
{
    using System;

    /// <summary>
    /// Defines the <see cref="IRange{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRange<T> : IEquatable<IRange<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Gets the End
        /// </summary>
        T End { get; }

        /// <summary>
        /// Gets the Start
        /// </summary>
        T Start { get; }
    }
}