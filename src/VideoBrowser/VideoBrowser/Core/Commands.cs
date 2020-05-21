namespace VideoBrowser.Core
{
    /// <summary>
    /// Defines the <see cref="Commands" />
    /// </summary>
    public static class Commands
    {
        #region Constants

        public const string Authentication = " -u {0} -p {1}";

        public const string Download = " -o \"{0}\" --hls-prefer-native -f {1} {2}";

        public const string GetJsonInfo = " -o \"{0}\\%(title)s\" --no-playlist --skip-download --restrict-filenames --write-info-json \"{1}\"{2}";

        public const string GetJsonInfoBatch = " -o \"{0}\\%(id)s_%(title)s\" --no-playlist --skip-download --restrict-filenames --write-info-json {1}";

        public const string TwoFactor = " -2 {0}";

        public const string Update = " -U";

        public const string Version = " --version";

        #endregion Constants
    }
}