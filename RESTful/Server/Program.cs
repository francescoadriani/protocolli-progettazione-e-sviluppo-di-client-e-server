using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RandomizzatoreServerRESTful
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost hostWeb = new WebServiceHost(typeof(RandomizzatoreServerRESTful.Service));
            ServiceEndpoint ep = hostWeb.AddServiceEndpoint(typeof(RandomizzatoreServerRESTful.IService), new WebHttpBinding(), "");
            ServiceDebugBehavior stp = hostWeb.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            hostWeb.Open();

            Console.WriteLine("Service Host started @" + hostWeb.BaseAddresses[0]);

            var methods = typeof(RandomizzatoreServerRESTful.IService).GetMethods();
            IEnumerable<string> actions = methods.Where(
                m => m.GetCustomAttributes(typeof(WebInvokeAttribute), true).Count() > 0)
                .Select(m =>
                    ("(" +
                    ((WebInvokeAttribute)m.GetCustomAttributes(typeof(WebInvokeAttribute), true).First()).Method +
                    ")").PadRight(8) + "\t" +
                    hostWeb.BaseAddresses[0] + "" +
                    ((WebInvokeAttribute)m.GetCustomAttributes(typeof(WebInvokeAttribute), true).First()).UriTemplate.Substring(1)
                    );
            Console.WriteLine(string.Join("\r\n", actions.ToArray()));

            Console.Read();
        }
    }
}
