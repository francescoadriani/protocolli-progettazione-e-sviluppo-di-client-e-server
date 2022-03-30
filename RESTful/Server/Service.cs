using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SQLite;
using AudioLibraryServerRESTful.Discography;
using System.Data;
using System.ServiceModel;
using AudioLibraryServerRESTful;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.ServiceModel.Channels;

namespace AudioLibraryServerRESTful
{
    public class Service : IService
    {
        [return: MessageParameter(Name = "track")]
        public Track ReadTrackByID(String TrackID)
        {
            Console.WriteLine("Richiesta traccia: " + TrackID);
            String root = OperationContext.Current.Host.BaseAddresses[0].AbsoluteUri;
            RemoteEndpointMessageProperty clientEndpoint =
             OperationContext.Current.IncomingMessageProperties[
             RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            String client = String.Format("{0}:{1}", clientEndpoint.Address, clientEndpoint.Port);

            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE TrackID = " + TrackID);
            foreach (DataRow row in dt.Rows)
            {
                Track t = SqLiteFacade.trackFromRow(row);
                return t;
            }
            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        [return: MessageParameter(Name = "tracks")]
        public List<Track> ReadTracks()
        {
            Console.WriteLine("Richieste tracce");
            List<Track> tracksList = new List<Track>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks");
            foreach (DataRow row in dt.Rows)
            {
                Track t = SqLiteFacade.trackFromRow(row);
                tracksList.Add(t);
            }
            return tracksList;
        }

        [return: MessageParameter(Name = "albums")]
        public List<Album> ReadAlbums()
        {
            Console.WriteLine("Richiesti album");
            List<Album> albumsList = new List<Album>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums");
            foreach (DataRow row in dt.Rows)
            {
                Album t = SqLiteFacade.albumFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE AlbumId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource });
                albumsList.Add(t);
            }
            return albumsList;
        }

        [return: MessageParameter(Name = "album")]
        public Album ReadAlbumByID(string AlbumID)
        {
            Console.WriteLine("Richiesto album: " + AlbumID);
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE AlbumID = " + AlbumID);
            foreach (DataRow row in dt.Rows)
            {
                Album t = SqLiteFacade.albumFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE AlbumId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource });
                return t;
            }
            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        [return: MessageParameter(Name = "artists")]
        public List<Artist> ReadArtists()
        {
            Console.WriteLine("Richiesti artisti");
            List<Artist> artistsList = new List<Artist>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Artists");
            foreach (DataRow row in dt.Rows)
            {
                Artist t = SqLiteFacade.artistFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE ArtistId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.AlbumsList.Add(new Link<long>() { resource = SqLiteFacade.albumFromRow(row2).ID.resource, href = Program.baseAddress + "albums/" + SqLiteFacade.albumFromRow(row2).ID.resource });
                artistsList.Add(t);
            }
            return artistsList;
        }

        [return: MessageParameter(Name = "artist")]
        public Artist ReadArtistByID(string ArtistID)
        {
            Console.WriteLine("Richiesto artista: " + ArtistID);
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Artists WHERE ArtistID = " + ArtistID);
            foreach (DataRow row in dt.Rows)
            {
                Artist t = SqLiteFacade.artistFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE ArtistId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.AlbumsList.Add(new Link<long>() { resource = SqLiteFacade.albumFromRow(row2).ID.resource, href = Program.baseAddress + "albums/" + SqLiteFacade.albumFromRow(row2).ID.resource });
                return (t);
            }
            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        [return: MessageParameter(Name = "genres")]
        public List<Genre> ReadGenres()
        {
            Console.WriteLine("Richiesti generi");
            List<Genre> genreList = new List<Genre>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM genres");
            foreach (DataRow row in dt.Rows)
            {
                Genre t = SqLiteFacade.genreFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE GenreId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource });
                genreList.Add(t);
        }
            return genreList;
        }

        [return: MessageParameter(Name = "genre")]
        public Genre ReadGenreByID(string GenreID)
        {
            Console.WriteLine("Richiesto genere: " + GenreID);
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Genres WHERE GenreId = " + GenreID);
            foreach (DataRow row in dt.Rows)
            {
                Genre t = SqLiteFacade.genreFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE GenreId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource });
                return t;
            }
            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        [return: MessageParameter(Name = "media-types")]
        public List<MediaType> ReadMediaTypes()
        {
            Console.WriteLine("Richiesti mediatype");
            List<MediaType> mediaTypeList = new List<MediaType>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM media_types");
            foreach (DataRow row in dt.Rows)
            {
                MediaType t = SqLiteFacade.mediaTypeFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE MediaTypeId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource});
                mediaTypeList.Add(t);
            }
            return mediaTypeList;
        }

        [return: MessageParameter(Name = "media-type")]
        public MediaType ReadMediaTypeByID(string MediaTypeID)
        {
            Console.WriteLine("Richiesto mediatype: " + MediaTypeID);
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM media_types WHERE MediaTypeId=" + MediaTypeID);
            foreach (DataRow row in dt.Rows)
            {
                MediaType t = SqLiteFacade.mediaTypeFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE MediaTypeId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.baseAddress + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource });
                return t;
            }
            throw new WebFaultException(HttpStatusCode.NotFound);
        }

        public long DeleteTrackByID(string TrackID)
        {
            Console.WriteLine("Cancellata traccia: " + TrackID);
            long res = SqLiteFacade.executeQueryAndGetLastId("DELETE FROM tracks where TrackId=" + TrackID);
            if (res<0)
                throw new WebFaultException(HttpStatusCode.NotModified);
            return res;
        }

        //String json = JsonConvert.SerializeObject(new RandomResult() { ReadRandomResult = 9.45 });
        public Track AddTrack(Stream contents)
        {
            HttpResponseMessage response = null;
            string input = new StreamReader(contents).ReadToEnd();
            Console.WriteLine("Aggiunta traccia: " + input);
            Track t = null;
            try
            {
                t = JsonConvert.DeserializeObject<Track>(input);
                long id = SqLiteFacade.insertTrack(t);
                if (id > 0)
                {
                    t.ID = new Link<long>() { href = Program.baseAddress + "tracks/" + id, resource = id };
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(t));
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return t;
        }

        public Track UpdateTrackWithoutExplicitID(Stream contents)
        {
            HttpResponseMessage response = null;
            string input = new StreamReader(contents).ReadToEnd();
            Console.WriteLine("Aggiornata traccia senza id esplicito: " + input);
            Track t = null;
            try
            {
                t = JsonConvert.DeserializeObject<Track>(input);
                long id = SqLiteFacade.updateTrack(t.ID.resource.ToString("0"), t);
                if (id == 0)
                {
                    t.ID = new Link<long>() { href = Program.baseAddress + "tracks/" + t.ID.resource, resource = t.ID.resource };
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(t));
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return t;
        }

        public Track UpdateTrack(string TrackID, Stream contents)
        {
            HttpResponseMessage response = null;
            string input = new StreamReader(contents).ReadToEnd();
            Console.WriteLine("Aggiornata traccia " + TrackID + ": " + input);
            Track t = null;
            try
            {
                t = JsonConvert.DeserializeObject<Track>(input);
                t.ID.resource = int.Parse(TrackID);
                long id = SqLiteFacade.updateTrack(TrackID, t);
                if (id == 0)
                {
                    t.ID = new Link<long>() { href = Program.baseAddress + "tracks/" + TrackID, resource = int.Parse(TrackID) };
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(t));
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
            return t;
        }


        //public object READ()
        //{
        //    string cs = @"URI=file:.\chinook.db";

        //    SQLiteConnection con = new SQLiteConnection(cs);
        //    con.Open();

        //    SQLiteCommand cmd = new SQLiteCommand(con);

        //    cmd.CommandText = "SELECT * FROM tracks";
        //    cmd.ExecuteNonQuery();

        //    try
        //    {
        //        return "nessuna risorsa richiesta";
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public String ReadTutorialbyID(String Tutorialid)
        //{
        //    int pid;
        //    Int32.TryParse(Tutorialid, out pid);
        //    return Tutorialid;
        //}

        //public void DeleteTutorial(String Tutorialid)
        //{
        //    int pid;
        //    Int32.TryParse(Tutorialid, out pid);
        //    //1st.RemoveAt(pid);
        //}
        //public void CreateTutorial(string par)
        //{
        //    int pid;
        //    Int32.TryParse(par, out pid);
        //    //1st.RemoveAt(pid);
        //}

        //public Stream Submit(string fileName, Stream contents)
        //{
        //    string input = new StreamReader(contents).ReadToEnd();
        //    Console.WriteLine("In service, input = {0}", input);

        //    string response = "{\n\t\"ok\": true\n}";
        //    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";// "text/plain";
        //    return new MemoryStream(Encoding.UTF8.GetBytes(response));
        //}

        //public void UpdateTutorial(String Tutorialid)
        //{
        //    int pid;
        //    Int32.TryParse(Tutorialid, out pid);
        //    //1st.RemoveAt(pid);
        //}
    }
}
