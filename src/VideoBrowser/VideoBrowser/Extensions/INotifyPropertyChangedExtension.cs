namespace VideoBrowser.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="INotifyPropertyChangedExtension" />
    /// </summary>
    public static class INotifyPropertyChangedExtension
    {
        #region Delegates

        /// <summary>
        /// The PropertyChangedInvokerEventHandler
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/></param>
        public delegate void PropertyChangedInvokerEventHandler(string propertyName);

        #endregion Delegates

        #region Methods

        /// <summary>
        /// Invokes the properties changed.
        /// </summary>
        /// <param name="instance">The INotifyPropertyChanged implementation.</param>
        /// <param name="invoker">The invoker.</param>
        /// <param name="propertyNames">The property name list.</param>
        public static void InvokePropertiesChanged(this INotifyPropertyChanged instance, PropertyChangedInvokerEventHandler invoker, params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                invoker?.Invoke(propertyName);
            }
        }

        /// <summary>
        /// The IsMatch
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/></param>
        /// <param name="propertyNames">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsMatch(this PropertyChangedEventArgs e, params string[] propertyNames)
        {
            var propertyName = e.PropertyName;
            foreach (var name in propertyNames)
            {
                if (propertyName == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the specified invoker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="invoker">The invoker.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyNames">The other optional property names to notify.</param>
        /// <returns> True if old value and new value are different. </returns>
        public static bool Set<T>(this INotifyPropertyChanged instance, PropertyChangedInvokerEventHandler invoker, ref T field, T value, [CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            invoker?.Invoke(propertyName);
            return true;
        }

        /// <summary>
        /// The Set
        /// </summary>
        /// <param name="sender">The sender<see cref="INotifyPropertyChanged"/></param>
        /// <param name="propertyChanged">The propertyChanged<see cref="PropertyChangedEventHandler"/></param>
        /// <param name="field">The field<see cref="double"/></param>
        /// <param name="value">The value<see cref="double"/></param>
        /// <param name="range">The range<see cref="RangeDouble"/></param>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        /// <param name="propertyNames">The propertyNames<see cref="string[]"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool Set(this INotifyPropertyChanged sender, PropertyChangedEventHandler propertyChanged, ref double field, double value, RangeDouble range, [CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            if (field.IsEqualTo(value))
            {
                return false;
            }

            if (value.IsInRange(range.Start, range.End))
            {
                field = value;
            }

            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        /// <summary>
        /// The Set
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyChanged">The property changed.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns> True if old value and new value are different. </returns>
        public static bool Set<T>(this INotifyPropertyChanged sender, PropertyChangedEventHandler propertyChanged, ref T field, T value, [CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        #endregion Methods
    }
}