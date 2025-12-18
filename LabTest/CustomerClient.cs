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

                Thread receiveThread = new Thread(ReceiveFromServer);
                receiveThread.IsBackground = true;
                receiveThread.Start();
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

        private void butmenu_Click(object sender, EventArgs e)
        {

        }

        void ProcessServerMessage(string message)
        {
            try
            {
                message = message.Trim();

                if (string.IsNullOrWhiteSpace(message))
                {
                    return;
                }

                string[] parts = message.Split('|');

                if (parts.Length < 2)
                {
                    MessageBox.Show(" Lỗi form.");
                    return;
                }

                dataGridView1.Rows.Clear();

                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("IDmonan", "ID món ăn");
                    dataGridView1.Columns.Add("Tenmonan", "Tên món ăn");
                    dataGridView1.Columns.Add("Gia", "Giá");
                    dataGridView1.Columns.Add("Soluong", "Số lượng cho phép");
                }

                string[] items = parts[1].Split(';');
                foreach (var item in items)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        string[] info = item.Split(',');
                        if (info.Length >= 4)
                        {
                            dataGridView1.Rows.Add(info[0], info[1], info[2], info[3]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing message: " + ex.Message);
            }
        }
        private void ReceiveFromServer()
        {
            try
            {
                while (tcpClient != null && tcpClient.Connected)
                {
                    NetworkStream ns = tcpClient.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = ns.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break; 
                    }

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => ProcessServerMessage(message)));
                    }
                    else
                    {
                        ProcessServerMessage(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Receive error: " + ex.Message);
            }
        }

     
        private void butthoat_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected)
                {
                    tcpClient.Close();
                    tcpClient.Dispose();
                }
                butketnot.Enabled = true;
                butthoat.Enabled = false;
                MessageBox.Show("Disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnect error: " + ex.Message);
            }
        }
    }
}
    

