namespace AvuxiFileReader.Config
{
    /// <summary>
    /// Class to bind the configuration file
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// activate to display additional info debug info
        /// </summary>
        public bool EnableDebug { get; set; } = false;
        /// <summary>
        /// activate to ignore the spaces distribution inside the file
        /// </summary>
        public bool IgnoreSpaces { get; set; } = true;
    }
}
