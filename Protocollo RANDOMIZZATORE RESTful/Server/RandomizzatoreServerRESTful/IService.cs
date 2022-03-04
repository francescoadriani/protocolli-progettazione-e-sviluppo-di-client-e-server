using RandomizzatoreClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RandomizzatoreServerRESTful
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "/")]
        [return: MessageParameter(Name = "Data")]
        object READ(); //READ cioè il nome del metodo è il nome della risorsa restituita



        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "/random/?min={minString}&max={maxString}")]
        [return: MessageParameter(Name = "RandomValueExtract")]
        RandomResult ReadRandom(String minString,String maxString);


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "/Tutorial/{Tutorialid}")]
        String ReadTutorialbyID(String Tutorialid);



        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/Tutorial/{Tutorialid}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteTutorial(String Tutorialid);



        [OperationContract]
        [WebInvoke(Method = "POST",
            //RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/Tutorial/",
            //ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void CreateTutorial(string par);



        [OperationContract]
        [Description("Submits the user entered data, and returns the stream")]
        [WebInvoke(Method = "POST", UriTemplate = "/SubmitData/{fileName}")]
        Stream Submit(string fileName, Stream contents);



        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/Tutorial/{Tutorialid}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void UpdateTutorial(String Tutorialid);

    }

}
