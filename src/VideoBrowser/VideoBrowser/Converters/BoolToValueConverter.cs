namespace VideoBrowser.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="BoolToBoolConverter" />
    /// </summary>
    public class BoolToBoolConverter : BoolToValueConverter<bool>
    {
        #region Fields

        private static readonly Lazy<ValueConverter> _instance = new Lazy<ValueConverter>(() => new BoolToBoolConverter());

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToBoolConverter"/> class.
        /// </summary>
        public BoolToBoolConverter()
        {
            TrueValue = false;
            FalseValue = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Instance
        /// </summary>
        public static ValueConverter Instance
        {
            get { return _instance.Value; }
        }

        #endregion Properties
    }

    /// <summary>
    /// Defines the <see cref="BoolToBrushConverter" />
    /// </summary>
    public class BoolToBrushConverter : BoolToValueConverter<Brush>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToByteConverter" />
    /// </summary>
    public class BoolToByteConverter : BoolToValueConverter<Byte>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToColorConverter" />
    /// </summary>
    public class BoolToColorConverter : BoolToValueConverter<Color>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToDecimalConverter" />
    /// </summary>
    public class BoolToDecimalConverter : BoolToValueConverter<Decimal>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToDoubleConverter" />
    /// </summary>
    public class BoolToDoubleConverter : BoolToValueConverter<Double>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToHorizontalAlignmentConverter" />
    /// </summary>
    public class BoolToHorizontalAlignmentConverter : BoolToValueConverter<HorizontalAlignment>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToInt16Converter" />
    /// </summary>
    public class BoolToInt16Converter : BoolToValueConverter<Int16>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToInt32Converter" />
    /// </summary>
    public class BoolToInt32Converter : BoolToValueConverter<Int32>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToInt64Converter" />
    /// </summary>
    public class BoolToInt64Converter : BoolToValueConverter<Int64>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToObjectConverter" />
    /// </summary>
    public class BoolToObjectConverter : BoolToValueConverter<Object>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToPointConverter" />
    /// </summary>
    public class BoolToPointConverter : BoolToValueConverter<Point>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToRectConverter" />
    /// </summary>
    public class BoolToRectConverter : BoolToValueConverter<Rect>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToSByteConverter" />
    /// </summary>
    public class BoolToSByteConverter : BoolToValueConverter<SByte>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToSingleConverter" />
    /// </summary>
    public class BoolToSingleConverter : BoolToValueConverter<Single>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToStringConverter" />
    /// </summary>
    public class BoolToStringConverter : BoolToValueConverter<String>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToStyleConverter" />
    /// </summary>
    public class BoolToStyleConverter : BoolToValueConverter<Style>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToThicknessConverter" />
    /// </summary>
    public class BoolToThicknessConverter : BoolToValueConverter<Thickness>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToUInt16Converter" />
    /// </summary>
    public class BoolToUInt16Converter : BoolToValueConverter<UInt16>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToUInt32Converter" />
    /// </summary>
    public class BoolToUInt32Converter : BoolToValueConverter<UInt32>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToUInt64Converter" />
    /// </summary>
    public class BoolToUInt64Converter : BoolToValueConverter<UInt64>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToValueConverter{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoolToValueConverter<T> : ValueConverter
    {
        #region Properties

        /// <summary>
        /// Gets or sets the FalseValue
        /// </summary>
        public T FalseValue { get; set; }

        /// <summary>
        /// Gets or sets the TrueValue
        /// </summary>
        public T TrueValue { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return this.FalseValue;
            else
                return (bool)value ? this.TrueValue : this.FalseValue;
        }

        /// <summary>
        /// The ConvertBack
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? value.Equals(this.TrueValue) : false;
        }

        #endregion Methods
    }

    /// <summary>
    /// Defines the <see cref="BoolToVerticalAlignmentConverter" />
    /// </summary>
    public class BoolToVerticalAlignmentConverter : BoolToValueConverter<VerticalAlignment>
    {
    }

    /// <summary>
    /// Defines the <see cref="BoolToVisibilityConverter" />
    /// </summary>
    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
        #region Fields

        private static readonly Lazy<ValueConverter> _trueIsVisibleConverter = new Lazy<ValueConverter>(() => new BoolToVisibilityConverter() { TrueValue = Visibility.Visible, FalseValue = Visibility.Collapsed });

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the TrueIsVisibleConverter
        /// </summary>
        public static ValueConverter TrueIsVisibleConverter
        {
            get { return _trueIsVisibleConverter.Value; }
        }

        #endregion Properties
    }

    /// <summary>
    /// Defines the <see cref="EqualsToBoolConverter" />
    /// </summary>
    public class EqualsToBoolConverter : BoolToValueConverter<String>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CompareValue
        /// </summary>
        public string CompareValue { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return this.FalseValue;
            else if (CompareValue != null)
                return value.ToString() == CompareValue ? this.TrueValue : this.FalseValue;
            return System.Convert.ToBoolean(value) ? this.TrueValue : this.FalseValue;
        }

        #endregion Methods
    }

    /// <summary>
    /// Defines the <see cref="EqualsToBrushConverter" />
    /// </summary>
    public class EqualsToBrushConverter : BoolToValueConverter<Brush>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CompareValue
        /// </summary>
        public string CompareValue { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return this.FalseValue;
            else if (CompareValue != null)
                return String.Equals(value.ToString(), CompareValue, StringComparison.CurrentCultureIgnoreCase) ? this.TrueValue : this.FalseValue;
            return System.Convert.ToBoolean(value) ? this.TrueValue : this.FalseValue;
        }

        #endregion Methods
    }

    /// <summary>
    /// Defines the <see cref="EqualsToVisibilityConverter" />
    /// </summary>
    public class EqualsToVisibilityConverter : BoolToValueConverter<Visibility>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CompareValue
        /// </summary>
        public string CompareValue { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert
        /// </summary>
        /// <param name="value">The value<see cref="object"/></param>
        /// <param name="targetType">The targetType<see cref="Type"/></param>
        /// <param name="parameter">The parameter<see cref="object"/></param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/></param>
        /// <returns>The <see cref="object"/></returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return this.FalseValue;
            else if (CompareValue != null)
                return value.ToString() == CompareValue ? this.TrueValue : this.FalseValue;
            return System.Convert.ToBoolean(value) ? this.TrueValue : this.FalseValue;
        }

        #endregion Methods
    }
}