namespace VideoBrowser.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DownloadQueueHandler" />.
    /// </summary>
    public static class DownloadQueueHandler
    {
        #region Fields

        private static bool _stop;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the DownloadingCount.
        /// </summary>
        private static int DownloadingCount => Queue.Count(o => IsDownloaderType(o) && o.CanPause());

        /// <summary>
        /// Gets or sets a value indicating whether LimitDownloads.
        /// </summary>
        public static bool LimitDownloads { get; set; }

        /// <summary>
        /// Gets or sets the MaxDownloads.
        /// </summary>
        public static int MaxDownloads { get; set; }

        /// <summary>
        /// Gets or sets the Queue.
        /// </summary>
        public static List<Operation> Queue { get; } = new List<Operation>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        public static void Add(Operation operation)
        {
            operation.Completed += Operation_Completed;
            operation.Resumed += Operation_Resumed;
            operation.StatusChanged += Operation_StatusChanged;

            Queue.Add(operation);
        }

        /// <summary>
        /// The GetQueued.
        /// </summary>
        /// <returns>The <see cref="Operation[]"/>.</returns>
        public static Operation[] GetQueued()
        {
            return Queue.Where(o => IsDownloaderType(o) && o.Status == OperationStatus.Queued).ToArray();
        }

        /// <summary>
        /// The GetWorking.
        /// </summary>
        /// <returns>The <see cref="Operation[]"/>.</returns>
        public static Operation[] GetWorking()
        {
            return Queue.Where(o => IsDownloaderType(o) && o.Status == OperationStatus.Working).ToArray();
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        public static void Remove(Operation operation)
        {
            operation.Completed -= Operation_Completed;
            operation.Resumed -= Operation_Resumed;
            operation.StatusChanged -= Operation_StatusChanged;

            Queue.Remove(operation);
        }

        /// <summary>
        /// The StartWatching.
        /// </summary>
        /// <param name="maxDownloads">The maxDownloads<see cref="int"/>.</param>
        public static void StartWatching(int maxDownloads)
        {
            MaxDownloads = maxDownloads;
            MainLoop();
        }

        /// <summary>
        /// The Stop.
        /// </summary>
        public static void Stop()
        {
            _stop = true;
        }

        /// <summary>
        /// The IsDownloaderType.
        /// </summary>
        /// <param name="operation">The operation<see cref="Operation"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool IsDownloaderType(Operation operation)
        {
            return operation is DownloadOperation;
        }

        /// <summary>
        /// The MainLoop.
        /// </summary>
        private static async void MainLoop()
        {
            while (!_stop)
            {
                await Task.Delay(1000);
                var queued = GetQueued();

                // If downloads isn't limited, start all queued operations
                if (!LimitDownloads)
                {
                    if (queued.Length == 0)
                    {
                        continue;
                    }

                    foreach (var operation in queued)
                    {
                        if (operation.HasStarted)
                        {
                            operation.ResumeQuiet();
                        }
                        else
                        {
                            operation.Start();
                        }
                    }
                }
                else if (DownloadingCount < MaxDownloads)
                {
                    // Number of operations to start
                    var count = Math.Min(MaxDownloads - DownloadingCount, queued.Length);

                    for (var i = 0; i < count; i++)
                    {
                        if (queued[i].HasStarted)
                        {
                            queued[i].ResumeQuiet();
                        }
                        else
                        {
                            queued[i].Start();
                        }
                    }
                }
                else if (DownloadingCount > MaxDownloads)
                {
                    // Number of operations to pause
                    var count = DownloadingCount - MaxDownloads;
                    var working = GetWorking();

                    for (var i = DownloadingCount - 1; i > (MaxDownloads - 1); i--)
                    {
                        working[i].Queue();
                    }
                }
            }
        }

        /// <summary>
        /// The Operation_Completed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="OperationEventArgs"/>.</param>
        private static void Operation_Completed(object sender, OperationEventArgs e)
        {
            Remove((sender as Operation));
        }

        /// <summary>
        /// The Operation_Resumed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private static void Operation_Resumed(object sender, EventArgs e)
        {
            // User resumed operation, prioritize this operation over other queued
            var operation = sender as Operation;

            // Move operation to top of queue, since pausing happens from the bottom.
            // I.E. this operation will only paused if absolutely necessary.
            Queue.Remove(operation);
            Queue.Insert(0, operation);
        }

        /// <summary>
        /// The Operation_StatusChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="StatusChangedEventArgs"/>.</param>
        private static void Operation_StatusChanged(object sender, StatusChangedEventArgs e)
        {
        }

        #endregion Methods
    }
}