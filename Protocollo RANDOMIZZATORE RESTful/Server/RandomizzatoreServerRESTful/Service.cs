using RandomizzatoreClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.ServiceModel.Web;
using System.Text;

namespace RandomizzatoreServerRESTful
{
    public class Service : IService
    {
        Random rnd = new Random();
        public RandomResult ReadRandom(String minString, String maxString)
        {
            double res = -1;
            double minTemp = 0;
            double maxTemp = 0;
            if (double.TryParse(minString, NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out minTemp))
                if (double.TryParse(maxString, NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out maxTemp))
                    res = minTemp + rnd.NextDouble() * (maxTemp - minTemp);
            return new RandomResult { Random = res, Max = maxTemp, Min = minTemp };
        }

        public object READ()
        {
            try
            {
                return "nessuna risorsa richiesta";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String ReadTutorialbyID(String Tutorialid)
        {
            int pid;
            Int32.TryParse(Tutorialid, out pid);
            return Tutorialid;
        }

        public void DeleteTutorial(String Tutorialid)
        {
            int pid;
            Int32.TryParse(Tutorialid, out pid);
            //1st.RemoveAt(pid);
        }
        public void CreateTutorial(string par)
        {
            int pid;
            Int32.TryParse(par, out pid);
            //1st.RemoveAt(pid);
        }

        public Stream Submit(string fileName, Stream contents)
        {
            string input = new StreamReader(contents).ReadToEnd();
            Console.WriteLine("In service, input = {0}", input);

            string response = "{\n\t\"ok\": true\n}";
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";// "text/plain";
            return new MemoryStream(Encoding.UTF8.GetBytes(response));
        }
        
        public void UpdateTutorial(String Tutorialid)
        {
            int pid;
            Int32.TryParse(Tutorialid, out pid);
            //1st.RemoveAt(pid);
        }
    }
}
