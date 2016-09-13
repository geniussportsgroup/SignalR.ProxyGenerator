SignalR.ProxyGenerator
=====================

Small wrapper over the SignalR Proxy Generator to allow you to create the proxies at build time. This will generate the normal proxy for any assmblies loaded into the app domain. All the comments are stripped out from the top. You an then prepend any text lines into the top of the proxy using the metadata array, this allows you to specify your own dependency path comments for things like jQuery.

https://www.nuget.org/packages/SignalR.ProxyGenerator

### Usage

    /// <summary>
    /// Returns the JavaScript proxy for any hubs found in any assemblies loaded into the AppDomain
    /// </summary>
    /// <param name="signalRUrlPath">The service URL used by the connection. Defaults to "/signalr". When created dynamically this would generate at the running address, for example if you had an IIS app called "MyApp" it would generate "http://localhost:port/MyApp/signalr".</param>
    /// <param name="metaData">An array of lines to prepend into the file. This can be useful if you want to specify a path in your build system to a dependency such as jQuery</param>
    /// <returns>JavaScript proxy code</returns>
    string Generate(string signalRUrlPath = "/signalr", params string[] metaData);