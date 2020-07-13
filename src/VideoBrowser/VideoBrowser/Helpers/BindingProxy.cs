namespace VideoBrowser.Helpers
{
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="BindingProxy" />.
    /// </summary>
    public class BindingProxy : Freezable
    {
        #region Fields

        /// <summary>
        /// The binding data context property.
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(DataContext), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the DataContext.
        /// </summary>
        public object DataContext { get => GetValue(DataProperty); set => SetValue(DataProperty, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CreateInstanceCore.
        /// </summary>
        /// <returns>The <see cref="Freezable"/>.</returns>
        protected override Freezable CreateInstanceCore() => new BindingProxy();

        #endregion Methods
    }
}