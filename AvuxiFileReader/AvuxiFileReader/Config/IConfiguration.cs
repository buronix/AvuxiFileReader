using System;
using System.Collections.Generic;
using System.Text;

namespace AvuxiFileReader.Config
{
    /// <summary>
    /// Configuration Interface
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// activate to display additional info debug info
        /// </summary>
        bool EnableDebug { get; set; }
        /// <summary>
        /// activate to ignore the spaces distribution inside the file
        /// </summary>
        bool IgnoreSpaces { get; set; }
        /// <summary>
        /// Format the output for easy List the contents of the file
        /// </summary>
        bool FormatOutput { get; set; }
    }
}
