namespace VideoBrowserTestApp.ViewModels
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using VideoBrowser.Common;
    using VideoBrowserTestApp.Helpers;
    using VideoBrowserTestApp.Tests;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        internal MainWindowViewModel()
        {
            this.Tests = ReflectionHelper.GetInstances<TestBase>();
            this.LoadedCommand = new RelayCommand(this.OnLoaded);
            this.ClosedCommand = new RelayCommand(this.OnClosed);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the ClosedCommand.
        /// </summary>
        public ICommand ClosedCommand { get; }

        /// <summary>
        /// Gets the LoadedCommand.
        /// </summary>
        public ICommand LoadedCommand { get; }

        /// <summary>
        /// Gets the Tests.
        /// </summary>
        public IList<TestBase> Tests { get; }

        /// <summary>
        /// Gets or sets the MainWindow.
        /// </summary>
        private Window MainWindow { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnClosed.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClosed(object obj)
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// The OnLoaded.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLoaded(object obj)
        {
            this.MainWindow = obj as Window;
            var lastTest = Properties.Settings.Default.LastTest;
            if (!string.IsNullOrEmpty(lastTest))
            {
                foreach (var test in this.Tests)
                {
                    if (test.Name == lastTest)
                    {
                        test.TestCommand.Execute(this.MainWindow);
                        break;
                    }
                }
            }
        }

        #endregion Methods
    }
}