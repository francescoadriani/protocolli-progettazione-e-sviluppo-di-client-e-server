using RandomizzatoreClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SQLite;
using restservice.Discography;
using System.Data;

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
        
        public Track ReadTrackByID(String TrackID)
        {
            int val = -1;
            if (int.TryParse(TrackID, out val))
            {
                return ReadTrackByID(val);
            }
            return null;
        }
        public Track ReadTrackByID(int TrackID)
        {
            string cs = @"URI=file:.\chinook.db";
            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(con);
            
            try
            {
                SQLiteDataAdapter db = new SQLiteDataAdapter("SELECT * FROM Tracks WHERE TrackID=" + TrackID, con);

                // Create a dataset
                DataSet ds = new DataSet();

                // Fill dataset
                db.Fill(ds);

                // Create a datatable
                DataTable dt = new DataTable("Names");
                dt = ds.Tables[0];

                // Close connection
                con.Close();
                
                // Print table
                foreach (DataRow row in dt.Rows)
                {
                    Track t = new Track()
                    {
                        Album = new Link<long>() { resource = (long)row["AlbumId"], href = Program.root + "albums/" + (long)row["AlbumId"] + "/" },
                        Bytes = (long)row["Bytes"],
                        Composer = (String)row["Composer"],
                        Genre = new Link<long>() { resource = (long)row["GenreId"], href = Program.root + "genres/" + (long)row["GenreId"] + "/" },
                        MediaType = new Link<long>() { resource = (long)row["MediaTypeId"], href = Program.root + "mediatypes/" + (long)row["MediaTypeId"] + "/" },
                        Milliseconds = (long)row["Milliseconds"],
                        Name = (String)row["Name"],
                        TrackId = (long)row["TrackId"],
                        UnitPrice = (decimal)row["UnitPrice"]
                    };
                    Console.WriteLine(string.Format("{0} {1}", row["TrackId"], row["Composer"]));
                    return t;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public object READ()
        {
            string cs = @"URI=file:.\chinook.db";

            SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand(con);

            cmd.CommandText = "SELECT * FROM tracks";
            cmd.ExecuteNonQuery();

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
