using AvuxiFileReader.Config;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AvuxiFileReader.Reader
{
    /// <summary>
    /// Class on charge of Read a given file, read line by line and
    /// output the distributtion of the letters
    /// </summary>
    public class FileLetterDistributionUtil : IFileReaderUtil
    {
        private const char EMPTY_CHARACTER = ' ';

        private readonly ILogger<FileLetterDistributionUtil> _logger;

        private IConfiguration _options;

        /// <summary>
        /// Main constructor for the FileLetterDistributtionUtil
        /// </summary>
        /// <param name="options">DI Options</param>
        /// <param name="logger">DI ILogger</param>
        public FileLetterDistributionUtil(IConfiguration options, ILogger<FileLetterDistributionUtil> logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Read a File and counter all the characteres of it, then display the distribution
        /// of the letters of the file.
        /// </summary>
        /// <param name="filePath">filePath of the file to process</param>
        public void ReadFile(string filePath)
        {
            //Check if the file Really exists
            if(File.Exists(filePath) == false)
            {
                throw new ArgumentException($"File {filePath} does not exist");
            }

            Dictionary<char, int> fileCharactersCounter = new Dictionary<char, int>();

            Encoding fileEncoding;

            Stopwatch chronometer = new Stopwatch();

            //Start counting the time to perform debug Information
            chronometer.Start();

            //Open the Stream Reader for the given file
            using (StreamReader streamReader = new StreamReader(filePath, true))
            {
                //Keep the encoding
                fileEncoding = streamReader.CurrentEncoding;

                //Read the file until End
                while (!streamReader.EndOfStream)
                {
                    //Read the file Line by Line
                    string fileLine = streamReader.ReadLine();

                    //Get all characters of the file line in an array
                    char[] lineCharacters = fileLine.ToCharArray();

                    //loop the characters in the line to count the distribution
                    for (int charIndex = 0; charIndex < lineCharacters.Length; charIndex++)
                    {
                        char character = lineCharacters[charIndex];

                        //If the IgnoreSpaces option is set and the character is an empty space we avoid the counting
                        //else look up for the character in the Dictionary, if register exists add to the cointer
                        //otherwise initialize it
                        if (_options.IgnoreSpaces && character == EMPTY_CHARACTER)
                        {
                            continue;
                        }
                        else if (fileCharactersCounter.ContainsKey(character))
                        {
                            fileCharactersCounter[character]++;
                        }
                        else
                        {
                            fileCharactersCounter[character] = 1;
                        }
                    }
                }
            }

            string fileName = Path.GetFileName(filePath);

            //Loop all the Characters Dictionary Entries and build the string
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append($"File {fileName} letter distribution: ");

            foreach (KeyValuePair<char, int> charEntry in fileCharactersCounter)
            {
                //If formatOutput option is set add a line break for each entry
                if (_options.FormatOutput)
                {
                    sBuilder.Append(Environment.NewLine);
                }
                sBuilder.Append($"{charEntry.Key}: {charEntry.Value}. ");
            }

            //File Process end so Stop counting the time 
            chronometer.Stop();

            //Set the encoding of the ouput
            Console.OutputEncoding = fileEncoding;

            //Display the output result of the application
            _logger.LogInformation(sBuilder.ToString());

            //If enabledebug option is set display additional information
            if (_options.EnableDebug)
            {
                _logger.LogInformation($"Read File {fileName} with Encoding {fileEncoding.EncodingName} took: "+
                    $"{chronometer.ElapsedMilliseconds} Milliseconds / {chronometer.ElapsedTicks} Ticks");
            }
        }

    }
}
