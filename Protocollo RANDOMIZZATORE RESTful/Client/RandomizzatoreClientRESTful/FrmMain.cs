using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomizzatoreClient
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnGetInt_Click(object sender, EventArgs e)
        {
            try
            {
                double min = double.Parse(txtMin.Text);
                double max = double.Parse(txtMax.Text) + 1.0;
                timer1.Enabled = true;
                lblRandom.Visible = false;
                txtRandom.Visible = false;
                picDadi.Visible = true;
                Application.DoEvents();
                double number = getNumberFromServer(min, max);
                txtRandom.Text = ((int)Math.Truncate(number)).ToString("0");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Sono stati inseriti dei numeri non riconosciuti!");
            }
        }


        //String json = JsonConvert.SerializeObject(new RandomResult() { ReadRandomResult = 9.45 });

        private double getNumberFromServer(double min, double max)
        {
            double num = -1.0;
            try
            {
                string responseBody = null;
                String minString = (min.ToString("N", new CultureInfo("it-IT", false).NumberFormat));
                String maxString = (max.ToString("N", new CultureInfo("it-IT", false).NumberFormat));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/random?min=" + minString + "&max=" + maxString);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream != null)
                        responseBody = new StreamReader(responseStream).ReadToEnd();
                }
                response.Close();

                ResultContainer result = JsonConvert.DeserializeObject<ResultContainer>(responseBody, 
                    new JsonSerializerSettings() {Culture = System.Globalization.CultureInfo.GetCultureInfo("it-IT")});
                
                RandomResult randomResult = result.RandomValueExtract;
                num = randomResult.Random;
            }
            catch (Exception ex)
            {

            }
            return num;
        }

        private void btnGetReal_Click(object sender, EventArgs e)
        {
            try
            {
                double min = double.Parse(txtMin.Text, new CultureInfo("it-IT", false).NumberFormat);
                double max = double.Parse(txtMax.Text, new CultureInfo("it-IT", false).NumberFormat);
                timer1.Enabled = true;
                lblRandom.Visible = false;
                txtRandom.Visible = false;
                picDadi.Visible = true;
                Application.DoEvents();
                double number = getNumberFromServer(min, max);
                txtRandom.Text = number.ToString("0.00");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Sono stati inseriti dei numeri non riconosciuti!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblRandom.Visible = true;
            txtRandom.Visible = true;
            picDadi.Visible = false;
            timer1.Enabled = false;
        }
    }
}
