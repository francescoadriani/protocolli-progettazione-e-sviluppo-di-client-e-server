using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private double getNumberFromServer(double min, double max)
        {
            double num = 0.0;
            Socket client = null;
            byte[] bytes = new byte[1024];

            IPAddress ipAddress = new IPAddress(new byte[4] { 127, 0, 0, 1 });
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 10108);
            client = new Socket(ipAddress.AddressFamily,
                SocketType.Dgram, ProtocolType.Udp);

            try
            {
                client.SendTo(Encoding.ASCII.GetBytes("GET[" + min.ToString("N", new CultureInfo("it-IT", false).NumberFormat) 
                    + ";" + max.ToString("N", new CultureInfo("it-IT", false).NumberFormat) + "]"), remoteEP);
                int bytesRec = client.Receive(bytes);
                String rnd = Encoding.ASCII.GetString(bytes, 0, bytesRec).Replace("\n", "").Replace("\r", "").Replace(".", ",");
                num = double.Parse(rnd, new CultureInfo("it-IT", false).NumberFormat);
            }
            catch (Exception ex) { }
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
