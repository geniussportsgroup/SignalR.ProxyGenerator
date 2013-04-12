using CommandLine;
using CommandLine.Text;

namespace SignalRProxyGenerator
{
    public class Options
    {
        [Option('u', "url", Required = true, HelpText = "The URL to the magic signalr address. Include the website address. E.g /MyWebsite/signalr")]
        public string Url { get; set; }

        [Option('p', "path", Required = true, HelpText = "Bin path of Hub dll")]
        public string BinPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output path for generated proxy code")]
        public string OutputPath { get; set; }

        [Option('m', "meta", Required = false, HelpText = "CSV lsit of meta data")]
        public string MetaData { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}