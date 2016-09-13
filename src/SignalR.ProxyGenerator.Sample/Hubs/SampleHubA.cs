using Microsoft.AspNet.SignalR;

namespace SignalR.ProxyGenerator.Sample.Hubs
{
    public class SampleHubA : Hub
    {
        public string HelloWorld()
        {
            return "Hello there!";
        }
    }
}