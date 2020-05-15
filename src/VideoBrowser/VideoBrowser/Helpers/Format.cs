namespace VideoBrowser.Helpers
{
    using System;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="FormatString" />
    /// </summary>
    public static class FormatString
    {
        #region Fields

        private static readonly string[] TimeUnitsNames = { "Milli", "Sec", "Min", "Hour", "Day", "Month", "Year", "Decade", "Century" };

        private static int[] TimeUnitsValue = { 1000, 60, 60, 24, 30, 12, 10, 10 };// Reference unit is milli

        #endregion Fields

        #region Methods

        /// <summary>
        /// Returns a formatted string of the given file size.
        /// </summary>
        /// <param name="size">The file size as long to format.</param>
        /// <returns>The <see cref="string"/></returns>
        public static string FormatFileSize(long size)
        {
            return string.Format(new ByteFormatProvider(), "{0:fs}", size);
        }

        /// <summary>
        /// The FormatLeftTime
        /// </summary>
        /// <param name="millis">The millis<see cref="long"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string FormatLeftTime(long millis)
        {
            var format = "";
            for (var i = 0; i < TimeUnitsValue.Length; i++)
            {
                var y = millis % TimeUnitsValue[i];
                millis = millis / TimeUnitsValue[i];

                if (y == 0)
                {
                    continue;
                }

                format = y + " " + TimeUnitsNames[i] + " , " + format;
            }

            format = format.Trim(',', ' ');
            return format == "" ? "0 Sec" : format;
        }

        /// <summary>
        /// Returns a formatted string of the video length.
        /// </summary>
        /// <param name="duration">The video duration as long.</param>
        /// <returns>The <see cref="string"/></returns>
        public static string FormatVideoLength(this long duration)
        {
            return FormatVideoLength(TimeSpan.FromSeconds(duration));
        }

        /// <summary>
        /// Returns a formatted string of the video length.
        /// </summary>
        /// <param name="duration">The video duration as TimeSpan.</param>
        /// <returns>The <see cref="string"/></returns>
        public static string FormatVideoLength(TimeSpan duration)
        {
            return duration.Hours > 0
                ? string.Format("{0}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds)
                : string.Format("{0}:{1:00}", duration.Minutes, duration.Seconds);
        }

        /// <summary>
        /// The GetDirectorySizeFormatted
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetDirectorySizeFormatted(string directory)
        {
            return FormatFileSize(FileHelper.GetDirectorySize(directory));
        }

        #endregion Methods
    }
}