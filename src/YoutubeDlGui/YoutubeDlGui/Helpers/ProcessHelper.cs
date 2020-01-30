namespace YoutubeDlGui.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using YoutubeDlGui.Common;

    /// <summary>
    /// Defines the <see cref="ProcessHelper" />
    /// </summary>
    public static class ProcessHelper
    {
        #region Methods

        /// <summary>
        /// Creates a Process with the given arguments, then returns it.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/></param>
        /// <param name="arguments">The arguments<see cref="string"/></param>
        /// <param name="output">The output<see cref="Action{Process, string}"/></param>
        /// <param name="error">The error<see cref="Action{Process, string}"/></param>
        /// <param name="environmentVariables">The environmentVariables<see cref="Dictionary{string, string}"/></param>
        /// <returns>The <see cref="Process"/></returns>
        public static Process StartProcess(string fileName,
                                           string arguments,
                                           Action<Process, string> output,
                                           Action<Process, string> error,
                                           Dictionary<string, string> environmentVariables)
        {
            var psi = new ProcessStartInfo(fileName, arguments)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = AppEnvironment.GetLogsDirectory()
            };

            if (environmentVariables != null)
            {
                foreach (KeyValuePair<string, string> pair in environmentVariables)
                    psi.EnvironmentVariables.Add(pair.Key, pair.Value);
            }

            var process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = psi
            };

            process.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrEmpty(e.Data))
                {
                    return;
                }

                Logger.Info(e.Data);
                output?.Invoke(process, e.Data);
            };
            process.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                if (string.IsNullOrEmpty(e.Data))
                {
                    return;
                }

                Logger.Info(e.Data);
                error?.Invoke(process, e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return process;
        }

        #endregion Methods
    }
}