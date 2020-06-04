namespace VideoBrowserTestApp.Tests
{
    using System.Windows.Input;
    using VideoBrowser.Common;

    /// <summary>
    /// Defines the <see cref="TestBase" />.
    /// </summary>
    public abstract class TestBase : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        protected TestBase(string name)
        {
            this.Name = name;
            this.TestCommand = new RelayCommand(this.Test, nameof(this.TestCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the TestCommand.
        /// </summary>
        public ICommand TestCommand { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected abstract void Test(object obj);

        #endregion Methods
    }
}