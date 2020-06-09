namespace VideoBrowser.Models
{
    using VideoBrowser.Common;
    using VideoBrowser.Controls.CefSharpBrowser;
    using VideoBrowser.Resources;

    /// <summary>
    /// Defines the <see cref="TestButton" />.
    /// </summary>
    public class TestButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestButton"/> class.
        /// Test new implemented code here.
        /// </summary>
        public TestButton()
        {
            this.Command = new RelayCommand(this.OnExecuteTest, nameof(TestButton));
            this.Icon = Icons.Test;
            this.ToolTip = "Test a code";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The OnExecuteTest.
        /// Write the code to test.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnExecuteTest(object obj)
        {
        }

        #endregion Methods
    }
}