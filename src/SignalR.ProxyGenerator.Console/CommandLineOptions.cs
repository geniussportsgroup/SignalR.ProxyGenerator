using CommandLine;
using CommandLine.Text;

namespace GeniusSports.SignalR.ProxyGenerator.Console
{
    public class CommandLineOptions
    {
        [Option('a', "assembly", HelpText = "The path to the assembly (.dll/.exe)", Required = true)]
        public string AssemblyPath { get; set; }

        [Option('u', "url", HelpText = "The service URL used by the connection. Defaults to '/ signalr'. When created dynamically this would generate at the running address, for example if you had an IIS app called 'MyApp' it would generate 'http://localhost:port/MyApp/signalr'.")]
        public string Url { get; set; }

        [Option('m', "metadata", HelpText = "An array of lines to prepend into the file. This can be useful if you want to specify a path in your build system to a dependency such as jQuery.")]
        public string Metadata { get; set; }

        [Option('o', "outfile", HelpText = "The path to the file to generate. If this is empty, the output is written to stdout.")]
        public string OutFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}