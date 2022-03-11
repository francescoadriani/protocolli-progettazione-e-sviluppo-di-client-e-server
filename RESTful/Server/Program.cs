using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibraryServerRESTful
{
    class Program
    {
        public static string root = "";
        static void Main(string[] args)
        {
            WebServiceHost hostWeb = new WebServiceHost(typeof(AudioLibraryServerRESTful.Service));

            //hostWeb.AddServiceEndpoint(typeof(ICustomerCollection), new WebHttpBinding(), "");
            //hostWeb.Description.Endpoints[0].Behaviors.Add(new WebHttpBehavior { EnableHelp = true });

            ServiceEndpoint ep = hostWeb.AddServiceEndpoint(typeof(AudioLibraryServerRESTful.IService), new WebHttpBinding(), "");
            hostWeb.Description.Endpoints[1].Behaviors.Add(new WebHttpBehavior { HelpEnabled = true });
            ServiceDebugBehavior stp = hostWeb.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = true;
            hostWeb.Open();

            root = hostWeb.BaseAddresses[0].ToString();
            Console.WriteLine("Service Host started @" + hostWeb.BaseAddresses[0]);

            var methods = typeof(AudioLibraryServerRESTful.IService).GetMethods();
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
