using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabTest
{
    public partial class CustomerClient : Form
    {
        private TcpClient tcpClient;
        public CustomerClient()
        {
            InitializeComponent();
        }

        private void butketnot_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = textip.Text.Trim();
                int port = int.Parse(textport.Text.Trim());
                tcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                tcpClient.Connect(ipEndPoint);
                MessageBox.Show("Connected");
                butketnot.Enabled = false;
                butthoat.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect error: " + ex.Message);
            }
        }

        private void butdatmon_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    MessageBox.Show("Not connected to server.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(textsomon.Text))
                {
                    MessageBox.Show("Please enter a message to send.");
                    return;
                }
                string message = combomon.Text + ";" + combsoban.Text + ";" + textsomon.Text + "\n";
                NetworkStream ns = tcpClient.GetStream();
                byte[] data = Encoding.ASCII.GetBytes(message);
                ns.Write(data, 0, data.Length);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send error: " + ex.Message);
            }
        }

        private void butthoat_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                }
                MessageBox.Show("QUIT");
                butketnot.Enabled = true;
                butthoat.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("QUIT error: " + ex.Message);
            }
        }

    }
}
    

