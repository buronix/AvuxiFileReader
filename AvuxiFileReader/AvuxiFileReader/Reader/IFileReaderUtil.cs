using System;
using System.Collections.Generic;
using System.Text;

namespace AvuxiFileReader.Reader
{
    /// <summary>
    /// Basic Interface to implement a File Reader Util
    /// </summary>
    public interface IFileReaderUtil
    {
        /// <summary>
        /// Method to read a file and perform Operations
        /// </summary>
        /// <param name="filePath">the file to perform operations</param>
        void ReadFile(string filePath);
    }
}
