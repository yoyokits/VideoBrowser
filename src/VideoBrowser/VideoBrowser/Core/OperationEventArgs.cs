namespace VideoBrowser.Core
{
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="OperationEventArgs" />
    /// </summary>
    public class OperationEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="ListViewItem"/></param>
        /// <param name="status">The status<see cref="OperationStatus"/></param>
        public OperationEventArgs(ListViewItem item, OperationStatus status)
        {
            this.Item = item;
            this.Status = status;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Item
        /// </summary>
        public ListViewItem Item { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public OperationStatus Status { get; set; }

        #endregion Properties
    }
}