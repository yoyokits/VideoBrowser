namespace YoutubeDlGui.Common
{
    using System;

    /// <summary>
    /// Defines the <see cref="FileSizeFormatProvider" />
    /// </summary>
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        #region Constants

        private const string FileSizeFormat = "fs";

        private const decimal OneGigaByte = OneMegaByte * 1024M;

        private const decimal OneKiloByte = 1024M;

        private const decimal OneMegaByte = OneKiloByte * 1024M;

        #endregion Constants

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
            if (format == null || !format.StartsWith(FileSizeFormat))
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
                suffix = "kB";
            }
            else
            {
                suffix = " B";
            }

            var precision = format.Substring(2);
            if (String.IsNullOrEmpty(precision))
            {
                precision = "2";
            }

            var formatString = "{0:N" + precision + "}{1}";
            return string.Format(formatString, size, suffix);
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