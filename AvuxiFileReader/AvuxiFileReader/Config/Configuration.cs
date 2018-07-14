using System;

namespace AvuxiFileReader.Config
{
    /// <summary>
    /// Class to bind the configuration file
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// activate to display additional info debug info
        /// </summary>
        public bool EnableDebug { get; set; } = false;
        /// <summary>
        /// activate to ignore the spaces distribution inside the file
        /// </summary>
        public bool IgnoreSpaces { get; set; } = true;
        /// <summary>
        /// Format the output for easy List the contents of the file
        /// </summary>
        public bool FormatOutput { get; set; } = false;

        /// <summary>
        /// Check if the application is Running in Development environment
        /// This is indicated via the AVUXI_ENV Environment Variable
        /// </summary>
        /// <returns>true if this is the Development Environment</returns>
        public bool isDevelopment()
        {
            string avuxiEnvironmentValue = Environment.GetEnvironmentVariable(AvuxiEnvironment.ENVIRONMENT_VARIABLE_NAME);
            if (string.IsNullOrEmpty(avuxiEnvironmentValue))
            {
                return false;
            }

            return avuxiEnvironmentValue.ToUpper() == AvuxiEnvironment.DEVELOPMENT;
        }

        /// <summary>
        /// Check if the application is Running in Production environment
        /// This is indicated via the AVUXI_ENV Environment Variable
        /// If AVUXI_ENV is not set then the default Environment is Production
        /// </summary>
        /// <returns>true if this is the Production Environment</returns>
        public bool isProduction()
        {
            string avuxiEnvironmentValue = Environment.GetEnvironmentVariable(AvuxiEnvironment.ENVIRONMENT_VARIABLE_NAME);
            if (string.IsNullOrEmpty(avuxiEnvironmentValue))
            {
                return true;
            }

            return avuxiEnvironmentValue.ToUpper() == AvuxiEnvironment.PRODUCTION;
        }

    }
}
