using AvuxiFileReader.Config;
using AvuxiFileReader.Reader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace AvuxiFileReader
{
    class Program
    {


        public const string Log4NetFileName = "log4net.config";

        static void Main(string[] args)
        {
            //Register Provider for Special Pages and Languages
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Detect base directory of the application
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //Create a configurator for the configuration file
            var configuratorBuilder = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile("configuration.json", optional: true, reloadOnChange: true);

            var configuration = configuratorBuilder.Build();

            //Register the Provider for Dependency Injection
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<Config.IConfiguration, Configuration>()
            .AddSingleton<IFileReaderUtil, FileLetterDistributionUtil>()
            .BuildServiceProvider();

            //Bind the Options to the configuration class
            var options = serviceProvider.GetService<Config.IConfiguration>();

            configuration.GetSection("Options").Bind(options);

            //Configure Log4Net Provider with the configuration file
            string lof4NetFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Log4NetFileName);

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            loggerFactory.AddLog4Net(lof4NetFileLocation);

            var MainLogger = loggerFactory.CreateLogger<Program>();

            /// Start the application
            if (options.EnableDebug)
            {
                MainLogger.LogInformation("Start application");
            }

            var fileReaderUtil = serviceProvider.GetService<IFileReaderUtil>();

            string filePath = null;

            int exitCode = 0;
            try
            {
                //If no file is provide show an Error Message
                if (args.Length == 0)
                {
                    MainLogger.LogError($"Provide a valid filePath to read The File and count the distributtion of the letters within");
                    exitCode = 1;
                }
                else
                {
                    //Read the file to count the letters distributtion 
                    filePath = args[0];
                    fileReaderUtil.ReadFile(filePath);
                }
            }
            catch (ArgumentException ae)
            {
                MainLogger.LogCritical(ae.Message);
                exitCode = 1;
            }
            catch (IOException ioe)
            {
                MainLogger.LogCritical(ioe, $"Error Reading the File {filePath}");
                exitCode = 1;
            }
            catch (UnauthorizedAccessException uae)
            {
                MainLogger.LogCritical(uae, $"You don't have permission to access the file {filePath}");
                exitCode = 1;
            }
            catch (Exception e)
            {
                MainLogger.LogCritical(e, $"Unhandled Error Reading {filePath}");
                exitCode = 1;
            }
            finally
            {
                //End the application and exit with the status result
                if (options.EnableDebug)
                {
                    MainLogger.LogInformation("Stop application");
                }
                Console.ReadLine();
                Environment.Exit(exitCode);
            }
        }
    }
}
