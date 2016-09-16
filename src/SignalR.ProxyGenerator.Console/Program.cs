using System;
using System.IO;
using CommandLine;

namespace GeniusSports.SignalR.ProxyGenerator.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var options = new CommandLineOptions();
                if (Parser.Default.ParseArguments(args, options))
                {
                    Run(options);
                    return 0;
                }

                System.Console.WriteLine("Error parsing command line options");
                System.Console.WriteLine(options.GetUsage());
                System.Console.ReadKey();
                return 1;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error generating TypeScript");
                System.Console.WriteLine(e);
                return 1;
            }
        }

        private static void Run(CommandLineOptions commandLineOptions)
        {
            LoadAssemblies(commandLineOptions);

            string url = null;
            string[] metadata = null;
            string outputText;

            if (!string.IsNullOrWhiteSpace(commandLineOptions.Url))
            {
                url = commandLineOptions.Url;
            }

            if (!string.IsNullOrWhiteSpace(commandLineOptions.Metadata))
            {
                metadata = commandLineOptions.Metadata.Split(',');
            }

            var hubJavaScriptGenerator = new SignalRProxyGenerator();
            
            if (url != null && metadata != null)
            {
                outputText = hubJavaScriptGenerator.Generate(signalRUrlPath: url, metaData: metadata);
            } else if (url == null && metadata != null)
            {
                outputText = hubJavaScriptGenerator.Generate(metaData: metadata);
            }
            else if (url != null && metadata == null)
            {
                outputText = hubJavaScriptGenerator.Generate(signalRUrlPath: url);
            }
            else
            {
                outputText = hubJavaScriptGenerator.Generate();
            }
            

            if (string.IsNullOrWhiteSpace(commandLineOptions.OutFile))
            {
                System.Console.WriteLine(outputText);
            }
            else
            {
                File.WriteAllText(commandLineOptions.OutFile, outputText);
            }
        }


        private static void LoadAssemblies(CommandLineOptions commandLineOptions)
        {
            var assemblyLoader = new AssemblyLoader();
            assemblyLoader.LoadAssemblyIntoAppDomain(commandLineOptions.AssemblyPath);
        }
    }
}