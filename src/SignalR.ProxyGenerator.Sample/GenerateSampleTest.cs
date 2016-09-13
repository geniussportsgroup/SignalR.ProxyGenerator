using System;
using NUnit.Framework;

namespace SignalR.ProxyGenerator.Sample
{
    [TestFixture]
    public class GenerateSampleTest
    {
        [Test]
        public void GenerateSample()
        {
            var signalRProxyGenerator = new SignalRProxyGenerator();
            var signalRUrlPath = "/signalr";
            var metaData = new[]
            {
                "// Some metadata I want",
            };
            var javaScript = signalRProxyGenerator.Generate(signalRUrlPath, metaData);
            Console.WriteLine(javaScript);
        }
    }
}