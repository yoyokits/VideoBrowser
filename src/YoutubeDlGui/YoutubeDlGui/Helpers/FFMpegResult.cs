namespace YoutubeDlGui.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FFMpegResult{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FFMpegResult<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FFMpegResult{T}"/> class.
        /// </summary>
        /// <param name="exitCode">The exitCode<see cref="int"/></param>
        /// <param name="errors">The errors<see cref="IEnumerable{string}"/></param>
        public FFMpegResult(int exitCode, IEnumerable<string> errors)
        {
            this.Value = default(T);
            this.ExitCode = exitCode;
            this.Errors = new List<string>(errors);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FFMpegResult{T}"/> class.
        /// </summary>
        /// <param name="result">The result<see cref="T"/></param>
        public FFMpegResult(T result)
        {
            this.Value = result;
            this.ExitCode = 0;
            this.Errors = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FFMpegResult{T}"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="T"/></param>
        /// <param name="exitCode">The exitCode<see cref="int"/></param>
        /// <param name="errors">The errors<see cref="IEnumerable{string}"/></param>
        public FFMpegResult(T value, int exitCode, IEnumerable<string> errors)
        {
            this.Value = value;
            this.ExitCode = exitCode;
            this.Errors = new List<string>(errors);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Errors
        /// Gets a list of errors from running FFmpeg. Returns null if there wasn't any errors.
        /// </summary>
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Gets or sets the ExitCode
        /// Gets the FFmpeg exit code.
        /// </summary>
        public int ExitCode { get; set; }

        /// <summary>
        /// Gets the result value.
        /// </summary>
        public T Value { get; private set; }

        #endregion Properties
    }
}