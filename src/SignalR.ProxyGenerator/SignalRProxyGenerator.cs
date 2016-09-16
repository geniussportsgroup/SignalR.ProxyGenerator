using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GeniusSports.SignalR.ProxyGenerator
{
    public interface ISignalRProxyGenerator
    {
        /// <summary>
        /// Returns the JavaScript proxy for any hubs found in any assemblies loaded into the AppDomain
        /// </summary>
        /// <param name="signalRUrlPath">The service URL used by the connection. Defaults to "/signalr". When created dynamically this would generate at the running address, for example if you had an IIS app called "MyApp" it would generate "http://localhost:port/MyApp/signalr".</param>
        /// <param name="metaData">An array of lines to prepend into the file. This can be useful if you want to specify a path in your build system to a dependency such as jQuery</param>
        /// <returns>JavaScript proxy code</returns>
        string Generate(string signalRUrlPath = "/signalr", params string[] metaData);
    }

    public class SignalRProxyGenerator : ISignalRProxyGenerator
    {
        /// <summary>
        /// Returns the JavaScript proxy for any hubs found in any assemblies loaded into the AppDomain
        /// </summary>
        /// <param name="signalRUrlPath">The service URL used by the connection. Defaults to "/signalr". When created dynamically this would generate at the running address, for example if you had an IIS app called "MyApp" it would generate "http://localhost:port/MyApp/signalr".</param>
        /// <param name="metaData">An array of lines to prepend into the file. This can be useful if you want to specify a path in your build system to a dependency such as jQuery</param>
        /// <returns>JavaScript proxy code</returns>
        public string Generate(string signalRUrlPath = "/signalr", params string[] metaData)
        {
            var generator = new DefaultJavaScriptProxyGenerator(new DefaultDependencyResolver());
            var js = generator.GenerateProxy(signalRUrlPath, true);
            var jsToOutput = TrimComments(js);
            jsToOutput = PrependMetaData(jsToOutput, metaData);
            return jsToOutput;
        }

        private static string PrependMetaData(string js, IEnumerable<string> metaData)
        {
            var outputString = string.Empty;
            foreach (var s in metaData)
            {
                outputString += s;
                outputString += Environment.NewLine;
            }

            outputString += Environment.NewLine;
            outputString += js;

            return outputString;
        }

        private static string TrimComments(string js)
        {
            var lines = js.Split(Environment.NewLine.ToCharArray());
            var linesToKeep = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("///") || line.StartsWith("/") || line.StartsWith("*") || line.StartsWith(" *")) continue;
                linesToKeep.Add(line);
            }

            var usefulLines = linesToKeep.Where(s => !string.IsNullOrWhiteSpace(s));
            return string.Join(Environment.NewLine, usefulLines);
        }
    }
}