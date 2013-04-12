using System;
using CommandLine;
using SignalRProxyGenerator.Commands;

namespace SignalRProxyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            Parser parser = new Parser();
            
            if (!parser.ParseArguments(args, options))
            {
                Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            try
            {
                ICommand command = new GenerateProxyCommand();
                command.Execute(options);
            }
            catch (Exception ex)
            {
                ExitWithError(ex.ToString());
            }
        }

        private static void ExitWithError(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(1);
        }
    }
}
