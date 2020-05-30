namespace VideoBrowser.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="MultiplyConverter" />.
    /// </summary>
    public class MultiplyConverter : IMultiValueConverter
    {
        #region Methods

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="values">The values<see cref="object[]"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var result = 1.0;
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] is double)
                {
                    result *= (double)values[i];
                }
            }

            return result;
        }

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetTypes">The targetTypes<see cref="Type[]"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }

        #endregion Methods
    }
}