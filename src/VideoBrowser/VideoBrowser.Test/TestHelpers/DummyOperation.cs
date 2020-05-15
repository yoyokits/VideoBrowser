namespace YoutubeDlGui.Test.TestHelpers
{
    using System.ComponentModel;
    using YoutubeDlGui.Core;

    /// <summary>
    /// Defines the <see cref="DummyOperation" />.
    /// </summary>
    public class DummyOperation : Operation
    {
        #region Methods

        /// <summary>
        /// The WorkerCompleted.
        /// </summary>
        /// <param name="e">The e<see cref="RunWorkerCompletedEventArgs"/>.</param>
        protected override void WorkerCompleted(RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        /// The WorkerDoWork.
        /// </summary>
        /// <param name="e">The e<see cref="DoWorkEventArgs"/>.</param>
        protected override void WorkerDoWork(DoWorkEventArgs e)
        {
        }

        /// <summary>
        /// The WorkerProgressChanged.
        /// </summary>
        /// <param name="e">The e<see cref="ProgressChangedEventArgs"/>.</param>
        protected override void WorkerProgressChanged(ProgressChangedEventArgs e)
        {
        }

        #endregion Methods
    }
}