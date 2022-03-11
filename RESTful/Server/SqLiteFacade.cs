using AudioLibraryServerRESTful;
using AudioLibraryServerRESTful.Discography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibraryServerRESTful
{
    public class SqLiteFacade
    {
        public static void updateTrack(String trackId, Track track)
        {
            getDatatableFromQuery("UPDATE tracks " +
                "SET Name = '" + track.Name +"',"+
                "AlbumId = " + track.Album.resource + ", "+
                "MediaTypeId = " + track.MediaType.resource + ", "+
                "GenreId = "+ track.Genre.resource +", "+
                "Composer = '" + track.Composer + "', "+
                "Milliseconds = " + track.Milliseconds + ", "+
                "Bytes = " + track.Bytes + ", "+
                "UnitPrice = " + track.UnitPrice + " " +
                "WHERE TrackId = " + trackId);
        }
        public static void insertTrack(Track track)
        {
            getDatatableFromQuery("INSERT INTO tracks" +
                "(Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice)" +
                "VALUES('" + track.Name + "', " + track.Album.resource + ",  + " + track.MediaType.resource + ", " + track.Genre.resource + ", '" + track.Composer + "',  + " + track.Milliseconds + ", " + track.Bytes + ", " + track.UnitPrice + ")");
        }
        public static Track trackFromRow(DataRow row)
        {
            Track t = new Track()
            {
                Album = new Link<long>() { resource = (long)row["AlbumId"], href = Program.root + "albums/" + (long)row["AlbumId"] + "/" },
                Bytes = (long)row["Bytes"],
                Composer = (row["Composer"] == DBNull.Value) ? string.Empty : row["Composer"].ToString(),
                Genre = new Link<long>() { resource = (long)row["GenreId"], href = Program.root + "genres/" + (long)row["GenreId"] + "/" },
                MediaType = new Link<long>() { resource = (long)row["MediaTypeId"], href = Program.root + "mediatypes/" + (long)row["MediaTypeId"] + "/" },
                Milliseconds = (long)row["Milliseconds"],
                Name = (row["Name"] == DBNull.Value) ? string.Empty : row["Name"].ToString(),
                ID = new Link<long>() { resource = (long)row["TrackId"], href = Program.root + "tracks/" + (long)row["TrackId"] + "/" },
                UnitPrice = (decimal)row["UnitPrice"]
            };
            return t;
        }
        public static MediaType mediaTypeFromRow(DataRow row)
        {
            MediaType a = new MediaType()
            {
                ID = new Link<long>() { resource = (long)row["MediaTypeId"], href = Program.root + "media-types/" + (long)row["MediaTypeId"] + "/" },//(long)row["MediaTypeId"],
                Name = (row["Name"] == DBNull.Value) ? string.Empty : row["Name"].ToString()
            };
            return a;
        }
        public static Genre genreFromRow(DataRow row)
        {
            Genre a = new Genre()
            {
                ID = new Link<long>() { resource = (long)row["GenreId"], href = Program.root + "genres/" + (long)row["GenreId"] + "/" },//(long)row["GenreId"],
                Name = (row["Name"] == DBNull.Value) ? string.Empty : row["Name"].ToString()
            };
            return a;
        }
        public static Artist artistFromRow(DataRow row)
        {
            Artist a = new Artist()
            {
                ID = new Link<long>() { resource = (long)row["ArtistId"], href = Program.root + "artists/" + (long)row["ArtistId"] + "/" },//(long)row["ArtistId"],
                Name = (row["Name"] == DBNull.Value) ? string.Empty : row["Name"].ToString()
            };
            return a;
        }

        public static Album albumFromRow(DataRow row)
        {
            Album a = new Album()
            {
                Artist = new Link<long>() { resource = (long)row["ArtistId"], href = Program.root + "artist/" + (long)row["ArtistId"] + "/" },
                ID = new Link<long>() { resource = (long)row["AlbumId"], href = Program.root + "albums/" + (long)row["AlbumId"] + "/" },//(long)row["AlbumId"],
                Title = (row["Title"] == DBNull.Value) ? string.Empty : row["Title"].ToString()
            };
            return a;
        }
        public static DataTable getDatatableFromQuery(String s)
        {
            string cs = @"URI=file:.\chinook.db";
            System.Data.SQLite.SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(con);

            try
            {
                SQLiteDataAdapter db = new SQLiteDataAdapter(s, con);

                // Create a dataset
                DataSet ds = new DataSet();

                // Fill dataset
                db.Fill(ds);

                // Create a datatable
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                // Close connection
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
