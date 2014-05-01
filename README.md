SignalRProxyGenerator
=====================

A command line tool to generate SignalR hub proxies so that you can minify your Hub proxy and avoid using the dynamically generated code. The tool also accepts a CSV list of Meta Data to prepend to the file. It does this so if you are using a tool like Cassette you do not need to manually edit the file after generation.

###Options

[Option('u', "url", Required = true, HelpText = "The URL to the magic signalr address excluding the host name. Include the website address. E.g /MyWebsite/signalr. This website would be accessible at http://HOSTNAME.DOMAIN/MyWebsite/signalr")]
[Option('p', "path", Required = true, HelpText = "Bin path of Hub dll")]
[Option('o', "output", Required = true, HelpText = "Output path for generated proxy code")]
[Option('m', "meta", Required = false, HelpText = "CSV lsit of meta data")]

### Example Usage

SignalRProxyGenerator.exe -u "/TestApp/signalr" -p C:\Temp\MyAssemblies -o C:\Temp\SampleOutput.js -m "// @reference ~/jquery,// @reference ~/SignalR"
