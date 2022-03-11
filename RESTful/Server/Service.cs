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

namespace AudioLibraryServerRESTful
{
    public class Service : IService
    {
        //Random rnd = new Random();
        //public RandomResult ReadRandom(String minString, String maxString)
        //{
        //    double res = -1;
        //    double minTemp = 0;
        //    double maxTemp = 0;
        //    if (double.TryParse(minString, NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out minTemp))
        //        if (double.TryParse(maxString, NumberStyles.Float, new CultureInfo("it-IT", false).NumberFormat, out maxTemp))
        //            res = minTemp + rnd.NextDouble() * (maxTemp - minTemp);
        //    return new RandomResult { Random = res, Max = maxTemp, Min = minTemp };
        //}
        
        public Track ReadTrackByID(String TrackID)
        {
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE TrackID = " + TrackID);
            foreach (DataRow row in dt.Rows)
            {
                Track t = SqLiteFacade.trackFromRow(row);
                return t;
            }
            return null;
        }

        [return: MessageParameter(Name = "tracks")]
        public List<Track> ReadTracks()
        {
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
            List<Album> albumsList = new List<Album>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums");
            foreach (DataRow row in dt.Rows)
            {
                Album t = SqLiteFacade.albumFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE AlbumId=" + t.ID.resource);
                //foreach (DataRow row2 in dt2.Rows)
                //    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID + "/" });
                albumsList.Add(t);
            }
            return albumsList;
        }

        [return: MessageParameter(Name = "album")]
        public Album ReadAlbumByID(string AlbumID)
        {
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE AlbumID = " + AlbumID);
            foreach (DataRow row in dt.Rows)
            {
                Album t = SqLiteFacade.albumFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE AlbumId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource + "/" });
                return t;
            }
            return null;
        }

        [return: MessageParameter(Name = "artists")]
        public List<Artist> ReadArtists()
        {
            List<Artist> artistsList = new List<Artist>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Artists");
            foreach (DataRow row in dt.Rows)
            {
                Artist t = SqLiteFacade.artistFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE ArtistId=" + t.ID.resource);
                //foreach (DataRow row2 in dt2.Rows)
                //    t.AlbumsList.Add(new Link<long>() { resource = SqLiteFacade.albumFromRow(row2).ID, href = Program.root + "albums/" + SqLiteFacade.albumFromRow(row2).ID + "/" });
                artistsList.Add(t);
            }
            return artistsList;
        }

        [return: MessageParameter(Name = "artist")]
        public Artist ReadArtistByID(string ArtistID)
        {
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Artists WHERE ArtistID = " + ArtistID);
            foreach (DataRow row in dt.Rows)
            {
                Artist t = SqLiteFacade.artistFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Albums WHERE ArtistId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.AlbumsList.Add(new Link<long>() { resource = SqLiteFacade.albumFromRow(row2).ID.resource, href = Program.root + "albums/" + SqLiteFacade.albumFromRow(row2).ID.resource + "/" });
                return (t);
            }
            return null;
        }

        [return: MessageParameter(Name = "genres")]
        public List<Genre> ReadGenres()
        {
            List<Genre> genreList = new List<Genre>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM genres");
            foreach (DataRow row in dt.Rows)
            {
                Genre t = SqLiteFacade.genreFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE GenreId=" + t.ID.resource);
                //foreach (DataRow row2 in dt2.Rows)
                //    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID + "/" });
                genreList.Add(t);
        }
            return genreList;
        }

        [return: MessageParameter(Name = "genre")]
        public Genre ReadGenreByID(string GenreID)
        {
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Genres WHERE GenreId = " + GenreID);
            foreach (DataRow row in dt.Rows)
            {
                Genre t = SqLiteFacade.genreFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE GenreId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource + "/" });
                return t;
            }
            return null;
        }

        [return: MessageParameter(Name = "media-types")]
        public List<MediaType> ReadMediaTypes()
        {
            List<MediaType> mediaTypeList = new List<MediaType>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM media_types");
            foreach (DataRow row in dt.Rows)
            {
                MediaType t = SqLiteFacade.mediaTypeFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE MediaTypeId=" + t.ID.resource);
                //foreach (DataRow row2 in dt2.Rows)
                //    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID + "/" });
                mediaTypeList.Add(t);
            }
            return mediaTypeList;
        }

        [return: MessageParameter(Name = "media-type")]
        public MediaType ReadMediaTypeByID(string MediaTypeID)
        {
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM media_types WHERE MediaTypeId=" + MediaTypeID);
            foreach (DataRow row in dt.Rows)
            {
                MediaType t = SqLiteFacade.mediaTypeFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE MediaTypeId=" + t.ID.resource);
                foreach (DataRow row2 in dt2.Rows)
                    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID.resource, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID.resource + "/" });
                return t;
            }
            return null;
        }

        public void DeleteTrackByID(string TrackID)
        {
            SqLiteFacade.getDatatableFromQuery("DELETE FROM tracks where TrackId=" + TrackID);
        }

        public Track AddTrack(Stream contents)
        {
            string input = new StreamReader(contents).ReadToEnd();
            Track t = JsonConvert.DeserializeObject<Track>(input);
            SqLiteFacade.insertTrack(t);
            return t;
        }

        public Track UpdateTrack(string TrackID, Stream contents)
        {
            string input = new StreamReader(contents).ReadToEnd();
            Track t = JsonConvert.DeserializeObject<Track>(input);
            SqLiteFacade.updateTrack(TrackID, t);
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
