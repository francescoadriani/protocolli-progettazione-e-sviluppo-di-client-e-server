using DocumentFormat.OpenXml.Presentation;
using AudioLibraryServerRESTful.Discography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AudioLibraryServerRESTful
{
    [ServiceContract]
    public interface IService
    {
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Bare,
        //     UriTemplate = "/random/?min={minString}&max={maxString}")]
        //[return: MessageParameter(Name = "RandomValueExtract")]
        //[Description("Get random value between min and max")]
        //RandomResult ReadRandom(String minString, String maxString);


        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Bare,
        //     UriTemplate = "/")]
        //[return: MessageParameter(Name = "Data")]
        //[System.ComponentModel.Description("Comando semplice di base")]
        //object READ(); //READ cioè il nome del metodo è il nome della risorsa restituita



        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/tracks/")]
        [System.ComponentModel.Description("Fornisce la lista delle tracce")]
        [return: MessageParameter(Name = "tracks")]
        List<Track> ReadTracks();

        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/tracks/{TrackID}")]
        [System.ComponentModel.Description("Fornisce i dettagli della traccia")]
        [return: MessageParameter(Name = "track")]
        Track ReadTrackByID(String TrackID);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/tracks/{TrackID}",
            BodyStyle = WebMessageBodyStyle.Bare)]
        [System.ComponentModel.Description("Cancella una traccia")]
        long DeleteTrackByID(String TrackID);

        [OperationContract]
        [Description("Crea una traccia")]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/tracks/")]
        Track AddTrack(Stream contents);

        [OperationContract]
        [Description("Modifica una traccia")]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/tracks/{TrackID}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Track UpdateTrack(String TrackID, Stream contents);



        [OperationContract]
        [Description("Modifica una traccia senza id esplicito")]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/tracks/")]
        Track UpdateTrackWithoutExplicitID(Stream contents);







        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/albums/")]
        [System.ComponentModel.Description("Fornisce la lista degli album")]
        [return: MessageParameter(Name = "albums")]
        List<Album> ReadAlbums();


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/albums/{AlbumID}")]
        [System.ComponentModel.Description("Fornisce i dettagli di un album")]
        [return: MessageParameter(Name = "album")]
        Album ReadAlbumByID(String AlbumID);


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/artists/")]
        [System.ComponentModel.Description("Fornisce la lista degli artisti")]
        [return: MessageParameter(Name = "artists")]
        List<Artist> ReadArtists();


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/artists/{ArtistID}")]
        [System.ComponentModel.Description("Fornisce i dettagli di un artista")]
        [return: MessageParameter(Name = "artist")]
        Artist ReadArtistByID(String ArtistID);


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/genres/")]
        [System.ComponentModel.Description("Fornisce la lista dei generi musicali")]
        [return: MessageParameter(Name = "genres")]
        List<Genre> ReadGenres();


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/genres/{GenreID}")]
        [System.ComponentModel.Description("Fornisce i dettagli di un genere musicali")]
        [return: MessageParameter(Name = "genre")]
        Genre ReadGenreByID(String GenreID);


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/media-types/")]
        [System.ComponentModel.Description("Fornisce la lista dei formati")]
        [return: MessageParameter(Name = "media-types")]
        List<MediaType> ReadMediaTypes();


        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/media-types/{MediaTypeID}")]
        [System.ComponentModel.Description("Fornisce i dettagli di un formato")]
        [return: MessageParameter(Name = "media-type")]
        MediaType ReadMediaTypeByID(String MediaTypeID);





        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Bare,
        //     UriTemplate = "/Tutorial/{Tutorialid}")]
        //String ReadTutorialbyID(String Tutorialid);



        //[OperationContract]
        //[WebInvoke(Method = "DELETE",
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/Tutorial/{Tutorialid}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare)]
        //[System.ComponentModel.Description("Comando semplice di base")]
        //void DeleteTutorial(String Tutorialid);



        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    //RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/Tutorial/",
        //    //ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare)]
        //void CreateTutorial(string par);



        //[OperationContract]
        //[Description("Submits the user entered data, and returns the stream")]
        //[WebInvoke(Method = "POST", UriTemplate = "/SubmitData/{fileName}")]
        //Stream Submit(string fileName, Stream contents);



        //[OperationContract]
        //[WebInvoke(Method = "PUT",
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/Tutorial/{Tutorialid}",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare)]
        //void UpdateTutorial(String Tutorialid);

    }

}
