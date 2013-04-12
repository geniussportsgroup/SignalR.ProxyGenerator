using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SignalRProxyGenerator
{
    public class JavaScriptGenerator : MarshalByRefObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Called from non-static.")]
        public string GenerateProxy(string path, string url)
        {
            foreach (var assemblyPath in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
            {
                Assembly.Load(AssemblyName.GetAssemblyName(assemblyPath));
            }

            var signalrAssembly = (from a in AppDomain.CurrentDomain.GetAssemblies()
                                   where a.GetName().Name.Equals("Microsoft.AspNet.SignalR.Core", StringComparison.OrdinalIgnoreCase)
                                   select a).FirstOrDefault();

            if (signalrAssembly == null)
            {
                return null;
            }

            Type resolverType = signalrAssembly.GetType("Microsoft.AspNet.SignalR.DefaultDependencyResolver");
            if (resolverType == null)
            {
                return null;
            }

            Type proxyGeneratorType = signalrAssembly.GetType("Microsoft.AspNet.SignalR.Hubs.DefaultJavaScriptProxyGenerator");
            if (proxyGeneratorType == null)
            {
                return null;
            }

            object resolver = Activator.CreateInstance(resolverType);
            dynamic proxyGenerator = Activator.CreateInstance(proxyGeneratorType, resolver);

            return proxyGenerator.GenerateProxy(url, true);
        }
    }
}