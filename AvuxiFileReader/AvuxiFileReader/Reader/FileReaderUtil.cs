using AvuxiFileReader.Config;
using AvuxiFileReader.Log;
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
    public class FileReaderUtil
    {
        private const char EMPTY_CHARACTER = ' ';

        /// <summary>
        /// Read a File and counter all the characteres of it, then display the distribution
        /// of the letters of the file.
        /// </summary>
        /// <param name="filePath">filePath of the file to process</param>
        public void ReadFile(string filePath, Configuration options)
        {
            if(File.Exists(filePath) == false)
            {
                throw new ArgumentException($"File {filePath} does not exist");
            }

            Dictionary<char, int> fileCharactersCounter = new Dictionary<char, int>();

            Encoding fileEncoding;

            Stopwatch chronometer = new Stopwatch();

            chronometer.Start();

            using (StreamReader streamReader = new StreamReader(filePath, true))
            {
                fileEncoding = streamReader.CurrentEncoding;

                while (!streamReader.EndOfStream)
                {
                    string fileLine = streamReader.ReadLine();

                    char[] lineCharacters = fileLine.ToCharArray();

                    for (int charIndex = 0; charIndex < lineCharacters.Length; charIndex++)
                    {
                        char character = lineCharacters[charIndex];

                        if (options.IgnoreSpaces && character == EMPTY_CHARACTER)
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

            StringBuilder sBuilder = new StringBuilder();

            foreach (KeyValuePair<char, int> charEntry in fileCharactersCounter)
            {
                sBuilder.Append($"{charEntry.Key}: {charEntry.Value}. ");
            }

            chronometer.Stop();

            Console.OutputEncoding = fileEncoding;

            Logger.Log.Info(sBuilder.ToString());

            string fileName = Path.GetFileName(filePath);

            Logger.Debug($"Read File {fileName} with Encoding {fileEncoding.EncodingName} took {chronometer.ElapsedMilliseconds} Milliseconds / {chronometer.ElapsedTicks} Ticks");
        }

    }
}
