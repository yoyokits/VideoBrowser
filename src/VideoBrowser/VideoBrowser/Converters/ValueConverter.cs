namespace YoutubeDlGui.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// Defines the <see cref="ValueConverter" />
    /// </summary>
    public class ValueConverter : MarkupExtension, IValueConverter, IMultiValueConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverter"/> class.
        /// </summary>
        protected ValueConverter()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Convert(object value)
        {
            return this.Convert(value, null);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public object Convert(object value, object parameter)
        {
            return this.Convert(value, null, parameter, null);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the <see cref="Binding" /> source.</param>
        /// <param name="targetType">The type of the <see cref="Binding" /> target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The <see cref="object"/></returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object ConvertBack(object value)
        {
            return this.ConvertBack(value, null);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public object ConvertBack(object value, object parameter)
        {
            return this.ConvertBack(value, null, parameter, null);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the <see cref="Binding" /> target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The <see cref="object"/></returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a set of values to a single value.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public object MultiConvert(object[] values)
        {
            return this.MultiConvert(values, null);
        }

        /// <summary>
        /// Converts a set of values to a single value.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public object MultiConvert(object[] values, object parameter)
        {
            return this.MultiConvert(values, null, parameter, null);
        }

        /// <summary>
        /// Converts source values to a value for the <see cref="Binding" /> target. The data binding engine calls this method when it propagates the values from source bindings to the <see cref="Binding" /> target.
        /// </summary>
        /// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding"/> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the <see cref="Binding" /> target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The <see cref="object"/></returns>
        public virtual object MultiConvert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a set of values to a single value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object[] MultiConvertBack(object value)
        {
            return this.MultiConvertBack(value, null);
        }

        /// <summary>
        /// Converts a set of values to a single value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public object[] MultiConvertBack(object value, object parameter)
        {
            return this.MultiConvertBack(value, null, parameter, null);
        }

        /// <summary>
        /// Converts a <see cref="Binding" /> target value to the source binding values.
        /// </summary>
        /// <param name="value">The value that the <see cref="Binding" /> target produces.</param>
        /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The <see cref="object[]"/></returns>
        public virtual object[] MultiConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns this instace.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>The <see cref="object"/></returns>
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="values">The values<see cref="object[]"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return this.MultiConvert(values, targetType, parameter, culture);
        }

        /// <summary>
        /// The ConvertBack
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(value, targetType, parameter, culture);
        }

        /// <summary>
        /// The ConvertBack
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetTypes">The targetTypes<see cref="Type[]"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="CultureInfo"/></param>
        /// <returns>The <see cref="object[]"/></returns>
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return this.MultiConvertBack(value, targetTypes, parameter, culture);
        }

        #endregion Methods
    }
}