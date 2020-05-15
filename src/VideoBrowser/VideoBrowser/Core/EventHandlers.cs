namespace VideoBrowser.Core
{
    #region Delegates

    /// <summary>
    /// The OperationEventHandler
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="OperationEventArgs"/></param>
    public delegate void OperationEventHandler(object sender, OperationEventArgs e);

    /// <summary>
    /// The StatusChangedEventHandler
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="StatusChangedEventArgs"/></param>
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    #endregion Delegates
}