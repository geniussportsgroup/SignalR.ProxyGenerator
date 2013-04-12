using System;
using System.IO;

namespace SignalRProxyGenerator.Commands
{
    public class GenerateProxyCommand : ICommand
    {
        public void Execute(Options options)
        {
            string outputPath = Path.GetFullPath(options.OutputPath);
            string binPath = Path.GetFullPath(options.BinPath);
            
            OutputHubs(options.Url, binPath, outputPath, options.MetaData);
        }

        private void OutputHubs(string url, string binPath, string outputPath, string metaData)
        {
            var assemblies = Directory.GetFiles(binPath, "*.dll", SearchOption.AllDirectories);
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempPath);

            // Copy all assemblies to temp
            foreach (var assemblyPath in assemblies)
            {
                Copy(assemblyPath, tempPath);
            }

            Copy(typeof(Program).Assembly.Location, tempPath);

            var setup = new AppDomainSetup
            {
                ApplicationBase = tempPath
            };

            var domain = AppDomain.CreateDomain("hubs", AppDomain.CurrentDomain.Evidence, setup);

            var generator = (JavaScriptGenerator)domain.CreateInstanceAndUnwrap(typeof(Program).Assembly.FullName, typeof(JavaScriptGenerator).FullName);
            var js = generator.GenerateProxy(binPath, url);

            string jsToOutput = TrimStartGunk(js);
            jsToOutput = PrependMetaData(jsToOutput, metaData);

            Generate(outputPath, jsToOutput);
        }

        private string PrependMetaData(string js, string metaData)
        {
            string outputString = String.Empty;
            string[] splitMetaData = metaData.Split(',');
            foreach (var s in splitMetaData)
            {
                outputString += s;
                outputString += Environment.NewLine;
            }

            outputString += Environment.NewLine;
            outputString += js;

            return outputString;
        }

        private string TrimStartGunk(string js)
        {
            int indexOfStartOfJS = js.IndexOf("(function ($, window) {", StringComparison.Ordinal);
            return js.Substring(indexOfStartOfJS, js.Length - indexOfStartOfJS);
        }

        private static void Copy(string sourcePath, string destinationPath)
        {
            string target = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
            File.Copy(sourcePath, target, overwrite: true);
        }

        private static void Generate(string outputPath, string js)
        {
            if(File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            File.WriteAllText(outputPath, js);
        }
    }
}
