namespace YoutubeDlGui.Common
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="NotifyPropertyChanged" />
    /// Standard INotifyPropertyChanged implementation.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changed handler
        /// </summary>
        protected PropertyChangedEventHandler PropertyChangedHandler;

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/></param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}