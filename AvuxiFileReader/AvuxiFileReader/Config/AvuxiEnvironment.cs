using System;
using System.Collections.Generic;
using System.Text;

namespace AvuxiFileReader.Config
{
    /// <summary>
    /// Class to keep the different availabe Environment Values for the
    /// AVUXI_ENV Environment variable
    /// </summary>
    public static class AvuxiEnvironment
    {
        /// <summary>
        /// The name of the Environment Variable for this application
        /// </summary>
        public const string ENVIRONMENT_VARIABLE_NAME = "AVUXI_ENV";
        /// <summary>
        /// Development Environment
        /// </summary>
        public const string DEVELOPMENT = "DEVELOPMENT";
        /// <summary>
        /// Release Environment
        /// </summary>
        public const string PRODUCTION = "PRODUCTION";

    }
}
