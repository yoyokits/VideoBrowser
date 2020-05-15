namespace YoutubeDlGui.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="MathExtensions" />
    /// </summary>
    internal static class MathExtensions
    {
        #region Methods

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.Decimal.MinValue"/> ≤ value ≤ <see cref="F:System.Decimal.MaxValue"/>.</param>
        /// <returns>A <see cref="T:System.Decimal"/>, x, such that 0 ≤ x ≤ <see cref="F:System.Decimal.MaxValue"/>.</returns>
        public static decimal Abs(this decimal value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.double.MinValue"/> ≤ value ≤ <see cref="F:System.double.MaxValue"/>.</param>
        /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.double.MaxValue"/>.</returns>
        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.Single.MinValue"/> ≤ value ≤ <see cref="F:System.Single.MaxValue"/>.</param>
        /// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.Single.MaxValue"/>.</returns>
        public static float Abs(this float value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.Int32.MinValue"/> &lt; value ≤ <see cref="F:System.Int32.MaxValue"/>.</param>
        /// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int32.MaxValue"/>.</returns>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.Int64.MinValue"/> &lt; value ≤ <see cref="F:System.Int64.MaxValue"/>.</param>
        /// <returns>A 64-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int64.MaxValue"/>.</returns>
        public static long Abs(this long value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.SByte.MinValue"/> &lt; value ≤ <see cref="F:System.SByte.MaxValue"/>.</param>
        /// <returns>An 8-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.SByte.MaxValue"/>.</returns>
        public static sbyte Abs(this sbyte value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Abs
        /// </summary>
        /// <param name="value">A number in the range <see cref="F:System.Int16.MinValue"/> &lt; value ≤ <see cref="F:System.Int16.MaxValue"/>.</param>
        /// <returns>A 16-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int16.MaxValue"/>.</returns>
        public static short Abs(this short value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The Acos
        /// </summary>
        /// <param name="value">A number representing a cosine, where -1 ≤ value ≤ 1.</param>
        /// <returns>An angle, θ, measured in radians, such that 0 ≤ θ ≤ π -or- <see cref="F:System.double.NaN"/> if value &lt; -1 or value &gt; 1.</returns>
        public static double Acos(this double value)
        {
            return Math.Acos(value);
        }

        /// <summary>
        /// The Asin
        /// </summary>
        /// <param name="value">A number representing a sine, where -1 ≤ value ≤ 1.</param>
        /// <returns>An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2 -or- <see cref="F:System.double.NaN"/> if value &lt; -1 or value &gt; 1.</returns>
        public static double Asin(this double value)
        {
            return Math.Asin(value);
        }

        /// <summary>
        /// The Atan
        /// </summary>
        /// <param name="value">A number representing a tangent.</param>
        /// <returns>An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2 -or- <see cref="F:System.double.NaN"/> if value equals <see cref="F:System.double.NaN"/>, -π/2 rounded to double precision (-1.5707963267949) if value equals <see cref="F:System.double.NegativeInfinity"/>, or π/2 rounded to double precision (1.5707963267949) if value equals <see cref="F:System.double.PositiveInfinity"/>.</returns>
        public static double Atan(this double value)
        {
            return Math.Atan(value);
        }

        /// <summary>
        /// The Atan2
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        /// <returns>An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = y / x, where (x, y) is a point in the Cartesian plane. See <see cref="Math.Atan2"/> for details.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Atan2(this double y, double x)
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte AtLeast(this byte a, byte b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal AtLeast(this decimal a, decimal b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger. If a, b, or both a and b are equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double AtLeast(this double a, double b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger. If a, or b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float AtLeast(this float a, float b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int AtLeast(this int a, int b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long AtLeast(this long a, long b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte AtLeast(this sbyte a, sbyte b) => Math.Max(a, b);

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short AtLeast(this short a, short b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint AtLeast(this uint a, uint b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong AtLeast(this ulong a, ulong b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtLeast
        /// </summary>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort AtLeast(this ushort a, ushort b)
        {
            return Math.Max(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte AtMost(this byte a, byte b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal AtMost(this decimal a, decimal b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller. If a, b, or both a and b are equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double AtMost(this double a, double b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller. If a, b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float AtMost(this float a, float b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int AtMost(this int a, int b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long AtMost(this long a, long b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 8-bit signed integers to compare.</param>
        /// <param name="b">The second of two 8-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte AtMost(this sbyte a, sbyte b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short AtMost(this short a, short b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint AtMost(this uint a, uint b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong AtMost(this ulong a, ulong b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The AtMost
        /// </summary>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort AtMost(this ushort a, ushort b)
        {
            return Math.Min(a, b);
        }

        /// <summary>
        /// The BigMul
        /// </summary>
        /// <param name="a">The first <see cref="T:System.Int32"/> to multiply.</param>
        /// <param name="b">The second <see cref="T:System.Int32"/> to multiply.</param>
        /// <returns>The <see cref="T:System.Int64"/> containing the product of the specified numbers.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long BigMul(this int a, int b)
        {
            return Math.BigMul(a, b);
        }

        /// <summary>
        /// The Ceiling
        /// </summary>
        /// <param name="value">A decimal number.</param>
        /// <returns>The smallest integer greater than or equal to value.</returns>
        public static decimal Ceiling(this decimal value)
        {
            return Math.Ceiling(value);
        }

        /// <summary>
        /// The Ceiling
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>The smallest integer greater than or equal to value. If value is equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NegativeInfinity"/>, or <see cref="F:System.double.PositiveInfinity"/>, that value is returned.</returns>
        public static double Ceiling(this double value)
        {
            return Math.Ceiling(value);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte Clamp(this byte value, byte a, byte b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal Clamp(this decimal value, decimal a, decimal b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>A value between a and b inclusively. If a, b, or both a and b are equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Clamp(this double value, double a, double b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        /// <returns>A value between a and b inclusively. If a, b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float Clamp(this float value, float a, float b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int Clamp(this int value, int a, int b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long Clamp(this long value, long a, long b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 8-bit signed integers to compare.</param>
        /// <param name="b">The second of two 8-bit signed integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte Clamp(this sbyte value, sbyte a, sbyte b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short Clamp(this short value, short a, short b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint Clamp(this uint value, uint a, uint b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong Clamp(this ulong value, ulong a, ulong b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Clamp
        /// </summary>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        /// <returns>A value between a and b inclusively.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort Clamp(this ushort value, ushort a, ushort b)
        {
            return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b);
        }

        /// <summary>
        /// The Cos
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The cosine of angle.</returns>
        public static double Cos(this double angle)
        {
            return Math.Cos(angle);
        }

        /// <summary>
        /// The Cosh
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cosine of angle. If angle is equal to <see cref="F:System.double.NegativeInfinity"/> or <see cref="F:System.double.PositiveInfinity"/>, <see cref="F:System.double.PositiveInfinity"/> is returned. If angle is equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NaN"/> is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Cosh(this double angle)
        {
            return Math.Cosh(angle);
        }

        /// <summary>
        /// The DivRem
        /// </summary>
        /// <param name="a">The <see cref="T:System.Int32"/> that contains the dividend.</param>
        /// <param name="b">The <see cref="T:System.Int32"/> that contains the divisor.</param>
        /// <param name="result">The <see cref="T:System.Int32"/> that receives the remainder.</param>
        /// <returns>The <see cref="T:System.Int32"/> containing the quotient of the specified numbers.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int DivRem(this int a, int b, out int result)
        {
            return Math.DivRem(a, b, out result);
        }

        /// <summary>
        /// The DivRem
        /// </summary>
        /// <param name="a">The <see cref="T:System.Int64"/> that contains the dividend.</param>
        /// <param name="b">The <see cref="T:System.Int64"/> that contains the divisor.</param>
        /// <param name="result">The <see cref="T:System.Int64"/> that receives the remainder.</param>
        /// <returns>The <see cref="T:System.Int64"/> containing the quotient of the specified numbers.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long DivRem(this long a, long b, out long result)
        {
            return Math.DivRem(a, b, out result);
        }

        /// <summary>
        /// The Exp
        /// </summary>
        /// <param name="value">A number specifying a power.</param>
        /// <returns>The number e raised to the power value. If value equals <see cref="F:System.double.NaN"/> or <see cref="F:System.double.PositiveInfinity"/>, that value is returned. If value equals <see cref="F:System.double.NegativeInfinity"/>, 0 is returned.</returns>
        public static double Exp(this double value)
        {
            return Math.Exp(value);
        }

        /// <summary>
        /// The Floor
        /// </summary>
        /// <param name="value">A decimal number.</param>
        /// <returns>The largest integer less than or equal to value.</returns>
        public static decimal Floor(this decimal value)
        {
            return Math.Floor(value);
        }

        /// <summary>
        /// The Floor
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>The largest integer less than or equal to value. If value is equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NegativeInfinity"/>, or <see cref="F:System.double.PositiveInfinity"/>, that value is returned.</returns>
        public static double Floor(this double value)
        {
            return Math.Floor(value);
        }

        /// <summary>
        /// The Format the double value with 0.###.
        /// </summary>
        /// <param name="value">The value<see cref="double"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string Format(this double value)
        {
            var message = $"{value:0.###}";
            return message;
        }

        /// <summary>
        /// The GetValueOrDefault
        /// </summary>
        /// <param name="value">A double-precision floating-point number. </param>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.double.NaN"/>; otherwise, 0.0.</returns>
        public static double GetValueOrDefault(this double value)
        {
            return double.IsNaN(value) ? 0.0 : value;
        }

        /// <summary>
        /// The GetValueOrDefault
        /// </summary>
        /// <param name="value">A double-precision floating-point number. </param>
        /// <param name="defaultValue">The value to return if <see cref="IsNaN"/> returns true.</param>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.double.NaN"/>; otherwise, the <paramref name="defaultValue"/> parameter.</returns>
        public static double GetValueOrDefault(this double value, double defaultValue)
        {
            return double.IsNaN(value) ? defaultValue : value;
        }

        /// <summary>
        /// The GetValueOrDefault
        /// </summary>
        /// <param name="value">A single-precision floating-point number. </param>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Single.NaN"/>; otherwise, 0.0.</returns>
        public static float GetValueOrDefault(this float value)
        {
            return Single.IsNaN(value) ? 0.0f : value;
        }

        /// <summary>
        /// The GetValueOrDefault
        /// </summary>
        /// <param name="value">A single-precision floating-point number. </param>
        /// <param name="defaultValue">The value to return if <see cref="IsNaN"/> returns true.</param>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Single.NaN"/>; otherwise, the <paramref name="defaultValue"/> parameter.</returns>
        public static float GetValueOrDefault(this float value, float defaultValue)
        {
            return Single.IsNaN(value) ? defaultValue : value;
        }

        /// <summary>
        /// The IEEERemainder
        /// </summary>
        /// <param name="x">A dividend.</param>
        /// <param name="y">A divisor.</param>
        /// <returns>A number equal to x - (y Q), where Q is the quotient of x / y rounded to the nearest integer (if x / y falls halfway between two integers, the even integer is returned).If x - (y Q) is zero, the value +0 is returned if x is positive, or -0 if x is negative. If y = 0, <see cref="F:System.double.NaN"/> (Not-A-Number) is returned.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double IEEERemainder(this double x, double y)
        {
            return Math.IEEERemainder(x, y);
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">An 8-bit unsigned integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this byte value, byte a, byte b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A decimal value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this decimal value, decimal a, decimal b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A double-precision floating-point value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this double value, double a, double b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A double-precision floating-point value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this float value, float a, float b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A 32-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this int value, int a, int b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A 64-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this long value, long a, long b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// The IsBetween
        /// </summary>
        /// <param name="value">A 16-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this short value, short a, short b)
        {
            return a < b ? a <= value && value <= b : b <= value && value <= a;
        }

        /// <summary>
        /// Determines whether [is equal to] [the specified b].
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsEqualTo(this double a, double b, double tolerance = Epsilon.Default)
        {
            var isEqual = ((a - b).Abs() < tolerance) ? true : false;
            return isEqual;
        }

        /// <summary>
        /// The IsNaN
        /// </summary>
        /// <param name="value">A double-precision floating-point number. </param>
        /// <returns>true if value evaluates to <see cref="F:System.double.NaN"/>; otherwise, false.</returns>
        public static bool IsNaN(this double value)
        {
            return double.IsNaN(value);
        }

        /// <summary>
        /// The IsNaN
        /// </summary>
        /// <param name="value">A single-precision floating-point number. </param>
        /// <returns>true if value evaluates to not a number (<see cref="F:System.Single.NaN"/>); otherwise, false.</returns>
        public static bool IsNaN(this float value)
        {
            return Single.IsNaN(value);
        }

        /// <summary>
        /// The IsZero
        /// </summary>
        /// <param name="value">The <see cref="double"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsZero(this double value)
        {
            return value.Abs() < Epsilon.Default;
        }

        /// <summary>
        /// The Log
        /// </summary>
        /// <param name="value">A number whose logarithm is to be found.</param>
        /// <returns>See <see cref="Math.Log"/> for details.</returns>
        public static double Log(this double value)
        {
            return Math.Log(value);
        }

        /// <summary>
        /// The Log
        /// </summary>
        /// <param name="value">A number whose logarithm is to be found.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <returns>See <see cref="Math.Log"/> for details.</returns>
        public static double Log(this double value, double newBase)
        {
            return Math.Log(value, newBase);
        }

        /// <summary>
        /// The Log10
        /// </summary>
        /// <param name="value">A number whose logarithm is to be found.</param>
        /// <returns>See <see cref="Math.Log10"/> for details.</returns>
        public static double Log10(this double value)
        {
            return Math.Log10(value);
        }

        /// <summary>
        /// The Pow
        /// </summary>
        /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="y">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The number x raised to the power y. See <see cref="Math.Pow"/> for details.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Pow(this double x, double y)
        {
            return Math.Pow(x, y);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <returns>The integer nearest parameter value. If value is halfway between two integers, one of which is even and the other odd, then the even number is returned.</returns>
        public static decimal Round(this decimal value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of significant decimal places  (precision) in the return value.</param>
        /// <returns>The number nearest value with a precision equal to decimals. If value is halfway between two numbers, one of which is even and the other odd, then the even number is returned. If the precision of value is less than decimals, then value is returned unchanged.</returns>
        public static decimal Round(this decimal value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of significant decimal places  (precision) in the return value.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The number nearest value with a precision equal to decimals. If value is halfway between two numbers, one of which is even and the other odd, then mode determines which of the two numbers is returned. If the precision of value is less than decimals, then value is returned unchanged.</returns>
        public static decimal Round(this decimal value, int decimals, MidpointRounding mode)
        {
            return Math.Round(value, decimals, mode);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The integer nearest value. If value is halfway between two numbers, one of which is even and the other odd, then mode determines which of the two is returned.</returns>
        public static decimal Round(this decimal value, MidpointRounding mode)
        {
            return Math.Round(value, mode);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <returns>The integer nearest value. If value is halfway between two integers, one of which is even and the other odd, then the even number is returned.</returns>
        public static double Round(this double value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of significant digits (precision) in the return value.</param>
        /// <returns>The number nearest value with a precision equal to digits. If value is halfway between two numbers, one of which is even and the other odd, then the even number is returned. If the precision of value is less than digits, then value is returned unchanged.</returns>
        public static double Round(this double value, int digits)
        {
            return Math.Round(value, digits);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of significant digits (precision) in the return value.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The number nearest value with a precision equal to digits. If value is halfway between two numbers, one of which is even and the other odd, then the mode parameter determines which number is returned. If the precision of value is less than digits, then value is returned unchanged.</returns>
        public static double Round(this double value, int digits, MidpointRounding mode)
        {
            return Math.Round(value, digits, mode);
        }

        /// <summary>
        /// The Round
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <returns>The integer nearest value. If value is halfway between two integers, one of which is even and the other odd, then mode determines which of the two is returned.</returns>
        public static double Round(this double value, MidpointRounding mode)
        {
            return Math.Round(value, mode);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">An 8-bit unsigned integer to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static byte RoundDown(this byte value)
        {
            return RoundDown(value, (byte)1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">An 8-bit unsigned integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static byte RoundDown(this byte value, byte factor)
        {
            var d = (value % factor); return (byte)(value - d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static decimal RoundDown(this decimal value)
        {
            return RoundDown(value, 1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static decimal RoundDown(this decimal value, decimal factor)
        {
            var d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static double RoundDown(this double value)
        {
            return RoundDown(value, 1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static double RoundDown(this double value, double factor)
        {
            var d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static float RoundDown(this float value)
        {
            return RoundDown(value, 1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static float RoundDown(this float value, float factor)
        {
            var d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static int RoundDown(this int value)
        {
            return RoundDown(value, 1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static int RoundDown(this int value, int factor)
        {
            var d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static long RoundDown(this long value)
        {
            return RoundDown(value, 1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static long RoundDown(this long value, long factor)
        {
            var d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        public static short RoundDown(this short value)
        {
            return RoundDown(value, (short)1);
        }

        /// <summary>
        /// The RoundDown
        /// </summary>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        public static short RoundDown(this short value, short factor)
        {
            var d = (value % factor); return (short)(d == 0 ? value : value > 0 ? value - d : value - (factor + d));
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 8-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static byte RoundUp(this byte value)
        {
            return RoundUp(value, (byte)1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 8-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static byte RoundUp(this byte value, byte factor)
        {
            var d = (value % factor); return (byte)(d == 0 ? value : value + (factor - d));
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static decimal RoundUp(this decimal value)
        {
            return RoundUp(value, 1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static decimal RoundUp(this decimal value, decimal factor)
        {
            var d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static double RoundUp(this double value)
        {
            return RoundUp(value, 1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static double RoundUp(this double value, double factor)
        {
            var d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static float RoundUp(this float value)
        {
            return RoundUp(value, 1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static float RoundUp(this float value, float factor)
        {
            var d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static int RoundUp(this int value)
        {
            return RoundUp(value, 1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static int RoundUp(this int value, int factor)
        {
            var d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static long RoundUp(this long value)
        {
            return RoundUp(value, 1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static long RoundUp(this long value, long factor)
        {
            var d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static short RoundUp(this short value)
        {
            return RoundUp(value, (short)1);
        }

        /// <summary>
        /// The RoundUp
        /// </summary>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static short RoundUp(this short value, short factor)
        {
            var d = (value % factor); return (short)(d == 0 ? value : value < 0 ? value - d : value + (factor - d));
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed <see cref="T:System.Decimal"/> number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this decimal value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this double value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this float value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this int value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this long value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this sbyte value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sign
        /// </summary>
        /// <param name="value">A signed number.</param>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        public static int Sign(this short value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// The Sin
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The sine of angle. If angle is equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NegativeInfinity"/>, or <see cref="F:System.double.PositiveInfinity"/>, this method returns <see cref="F:System.double.NaN"/>.</returns>
        public static double Sin(this double angle)
        {
            return Math.Sin(angle);
        }

        /// <summary>
        /// The Sinh
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The hyperbolic sine of angle. If angle is equal to <see cref="F:System.double.NegativeInfinity"/>, <see cref="F:System.double.PositiveInfinity"/>, or <see cref="F:System.double.NaN"/>, this method returns a <see cref="T:System.Double"/> equal to angle.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Sinh(this double angle)
        {
            return Math.Sinh(angle);
        }

        /// <summary>
        /// The Sqrt
        /// </summary>
        /// <param name="value">A number.</param>
        /// <returns><see cref="Math.Sqrt"/> for details.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Sqrt(this double value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// The Tan
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The tangent of angle. If angle is equal to <see cref="F:System.double.NaN"/>, <see cref="F:System.double.NegativeInfinity"/>, or <see cref="F:System.double.PositiveInfinity"/>, this method returns <see cref="F:System.double.NaN"/>.</returns>
        public static double Tan(this double angle)
        {
            return Math.Tan(angle);
        }

        /// <summary>
        /// The Tanh
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The hyperbolic tangent of angle. If angle is equal to <see cref="F:System.double.NegativeInfinity"/>, this method returns -1. If angle is equal to <see cref="F:System.double.PositiveInfinity"/>, this method returns 1. If angle is equal to <see cref="F:System.double.NaN"/>, this method returns <see cref="F:System.double.NaN"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Tanh(this double angle)
        {
            return Math.Tanh(angle);
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<decimal> To(this decimal start, decimal bound)
        {
            var step = start <= bound ? 1 : (decimal)-1; for (decimal i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<decimal> To(this decimal start, decimal bound, decimal step)
        {
            for (decimal i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<double> To(this double start, double bound)
        {
            var step = start <= bound ? 1 : (double)-1; for (double i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<double> To(this double start, double bound, double step)
        {
            for (double i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<float> To(this float start, float bound)
        {
            var step = start <= bound ? 1 : (float)-1; for (float i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<float> To(this float start, float bound, float step)
        {
            for (float i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<int> To(this int start, int bound)
        {
            var step = start <= bound ? 1 : -1; for (int i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<int> To(this int start, int bound, int step)
        {
            for (int i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<long> To(this long start, long bound)
        {
            var step = start <= bound ? 1 : (long)-1; for (long i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<long> To(this long start, long bound, long step)
        {
            for (long i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        public static IEnumerable<short> To(this short start, short bound)
        {
            var step = start <= bound ? (short)1 : (short)-1; for (short i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The To
        /// </summary>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        /// <param name="step"> The increment step. </param>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        public static IEnumerable<short> To(this short start, short bound, short step)
        {
            for (short i = start; i < bound; i += step) yield return i;
        }

        /// <summary>
        /// The Truncate
        /// </summary>
        /// <param name="value">A number to truncate.</param>
        /// <returns>The integral part of value; that is, the number that remains after any fractional digits have been discarded.</returns>
        public static decimal Truncate(this decimal value)
        {
            return Math.Truncate(value);
        }

        /// <summary>
        /// The Truncate
        /// </summary>
        /// <param name="value">A number to truncate.</param>
        /// <returns>The integral part of value; that is, the number that remains after any fractional digits have been discarded.</returns>
        public static double Truncate(this double value)
        {
            return Math.Truncate(value);
        }

        #endregion Methods
    }
}