namespace VideoBrowser.Models
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using VideoBrowser.Helpers;

    /// <summary>
    /// Defines the <see cref="GlobalData" />.
    /// </summary>
    public class GlobalData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        internal GlobalData()
        {
        }

        #endregion Constructors



        #region Properties

        /// <summary>
        /// Gets a value indicating whether IsDebug.
        /// </summary>
        public bool IsDebug => DebugHelper.IsDebug;

        /// <summary>
        /// Gets the OperationModels.
        /// </summary>
        public ObservableCollection<OperationModel> OperationModels { get; } = new ObservableCollection<OperationModel>();

        /// <summary>
        /// Gets the TestCommand.
        /// </summary>
        public ICommand TestCommand { get; }

        #endregion Properties
    }
}