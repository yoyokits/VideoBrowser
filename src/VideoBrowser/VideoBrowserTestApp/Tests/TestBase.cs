namespace VideoBrowserTestApp.Tests
{
    using System.Windows;
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
            this.TestCommand = new RelayCommand(this.InternalTest, nameof(this.TestCommand));
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
        /// <param name="testWindow">The testWindow<see cref="Window"/>.</param>
        protected abstract void Test(Window testWindow);

        /// <summary>
        /// The InternalTest.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void InternalTest(object obj)
        {
            Properties.Settings.Default.LastTest = this.Name;
            this.Test(obj as Window);
        }

        #endregion Methods
    }
}