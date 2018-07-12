using AvuxiFileReader.Config;
using AvuxiFileReader.Log;
using AvuxiFileReader.Reader;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace AvuxiFileReader
{
    class Program
    {

        public static Configuration Options = new Configuration();

        static void Main(string[] args)
        {
            var configuratorBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("configuration.json", optional: true, reloadOnChange: true);

            var configuration = configuratorBuilder.Build();

            configuration.GetSection("Options").Bind(Options);

            Logger.Configure(Options);

            Logger.Debug("Start application");

            FileReaderUtil fileReaderUil = new FileReaderUtil();

            string filePath = null;

            int exitCode = 0;
            try
            {
                if (args.Length == 0)
                {
                    Logger.Log.Error($"Provide a valid filePath to read The File and count the distributtion of the letters within");
                    exitCode = 1;
                }
                else
                {
                    filePath = args[0];
                    fileReaderUil.ReadFile(filePath, Options);
                }
            }
            catch (ArgumentException ae)
            {
                Logger.Log.Fatal(ae.Message);
                exitCode = 1;
            }
            catch (IOException ioe)
            {
                Logger.Log.Fatal($"Error Reading the File {filePath}", ioe);
                exitCode = 1;
            }
            catch (UnauthorizedAccessException uae)
            {
                Logger.Log.Fatal($"You don't have permission to access the file {filePath}", uae);
                exitCode = 1;
            }
            catch (Exception e)
            {
                Logger.Log.Fatal($"Unhandled Error Reading {filePath}", e);
                exitCode = 1;
            }
            finally
            {
                Logger.Debug("Stop application");
                Environment.Exit(exitCode);
            }
        }
    }
}
