namespace YoutubeDlGui.Common
{
    using System;

    /// <summary>
    /// Defines the <see cref="ByteFormatProvider" />
    /// </summary>
    public class ByteFormatProvider : IFormatProvider, ICustomFormatter
    {
        #region Constants

        private const string FileSizeFormat = "fs";

        private const decimal OneGigaByte = OneMegaByte * 1024M;

        private const decimal OneKiloByte = 1024M;

        private const decimal OneMegaByte = OneKiloByte * 1024M;

        private const string SpeedFormat = "s";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets the Lock
        /// </summary>
        private object Lock { get; } = new object();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Format
        /// </summary>
        /// <param name="format">The format<see cref="string"/></param>
        /// <param name="arg">The arg<see cref="object"/></param>
        /// <param name="formatProvider">The formatProvider<see cref="IFormatProvider"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            lock (this.Lock)
            {
                if (format == null || (!format.StartsWith(FileSizeFormat) && !format.StartsWith(SpeedFormat)))
                {
                    return DefaultFormat(format, arg, formatProvider);
                }

                if (arg is string)
                {
                    return DefaultFormat(format, arg, formatProvider);
                }

                decimal size;

                try
                {
                    size = Convert.ToDecimal(arg);
                }
                catch (InvalidCastException)
                {
                    return DefaultFormat(format, arg, formatProvider);
                }

                string suffix;

                if (size > OneGigaByte)
                {
                    size /= OneGigaByte;
                    suffix = "GB";
                }
                else if (size > OneMegaByte)
                {
                    size /= OneMegaByte;
                    suffix = "MB";
                }
                else if (size > OneKiloByte)
                {
                    size /= OneKiloByte;
                    suffix = "KB";
                }
                else
                {
                    suffix = "Bytes";
                }

                if (format.StartsWith(SpeedFormat))
                {
                    suffix += "/s";
                }

                var postion = format.StartsWith(SpeedFormat) ? SpeedFormat.Length : FileSizeFormat.Length;
                var precision = format.Substring(postion);

                if (string.IsNullOrEmpty(precision))
                {
                    precision = "2";
                }

                return string.Format("{0:N" + precision + "}{1}", size, " " + suffix);
            }
        }

        /// <summary>
        /// The GetFormat
        /// </summary>
        /// <param name="formatType">The formatType<see cref="Type"/></param>
        /// <returns>The <see cref="object"/></returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? (this) : null;
        }

        /// <summary>
        /// The DefaultFormat
        /// </summary>
        /// <param name="format">The format<see cref="string"/></param>
        /// <param name="arg">The arg<see cref="object"/></param>
        /// <param name="formatProvider">The formatProvider<see cref="IFormatProvider"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is IFormattable formattableArg)
            {
                return formattableArg.ToString(format, formatProvider);
            }

            return arg.ToString();
        }

        #endregion Methods
    }
}