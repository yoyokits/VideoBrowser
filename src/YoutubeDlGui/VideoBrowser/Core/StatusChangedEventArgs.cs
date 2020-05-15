namespace YoutubeDlGui.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="StatusChangedEventArgs" />
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/></param>
        /// <param name="newStatus">The newStatus<see cref="OperationStatus"/></param>
        /// <param name="oldStatus">The oldStatus<see cref="OperationStatus"/></param>
        public StatusChangedEventArgs(Operation operation, OperationStatus newStatus, OperationStatus oldStatus)
        {
            this.Operation = operation;
            this.NewStatus = newStatus;
            this.OldStatus = oldStatus;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the NewStatus
        /// </summary>
        public OperationStatus NewStatus { get; private set; }

        /// <summary>
        /// Gets the OldStatus
        /// </summary>
        public OperationStatus OldStatus { get; private set; }

        /// <summary>
        /// Gets the Operation
        /// </summary>
        public Operation Operation { get; private set; }

        #endregion Properties
    }
}