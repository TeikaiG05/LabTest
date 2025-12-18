using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LabTest
{
    public partial class CustomerClient : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private readonly Dictionary<int, MenuItem> _menu = new Dictionary<int, MenuItem>();

        public CustomerClient()
        {
            InitializeComponent();

            //btnConnect.Click += btnConnect_Click;
            //btnMenu.Click += btnMenu_Click;
            //btnDat.Click += btnDat_Click;
            //btnThoat.Click += btnThoat_Click;

            cboMon.SelectedIndexChanged += cboMon_SelectedIndexChanged;
            this.FormClosing += CustomerClient_FormClosing;
        }

        private void CustomerClient_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text)) txtIP.Text = "127.0.0.1";
            if (string.IsNullOrWhiteSpace(txtPort.Text)) txtPort.Text = "8080";

            SetUiDisconnected();
            Log("READY");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            

            string host = txtIP.Text.Trim();
            if (!int.TryParse(txtPort.Text.Trim(), out int port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Port không hợp lệ!");
                return;
            }

            try
            {
                client = new TcpClient();
                client.Connect(host, port);

                var ns = client.GetStream();
                reader = new StreamReader(ns, Encoding.UTF8);
                writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };

                SetUiConnected();
                Log($"CONNECTED to {host}:{port}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại: " + ex.Message);
                Disconnect();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { MessageBox.Show("Chưa kết nối server!"); return; }

            try
            {
                writer.WriteLine("MENU");

                string header = reader.ReadLine(); 
                if (header == null) throw new Exception("Server closed.");

                if (!header.StartsWith("OK"))
                {
                    Log("MENU << " + header);
                    MessageBox.Show("Lỗi MENU: " + header);
                    return;
                }

                var parts = header.Split(' ');
                int count = (parts.Length >= 2 && int.TryParse(parts[1], out int c)) ? c : 0;

                _menu.Clear();
                cboMon.Items.Clear();
                rtbView.Clear();

                for (int i = 0; i < count; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null) throw new Exception("Server closed while reading menu.");

                    var p = line.Split(';');
                    if (p.Length != 3) continue;

                    if (!int.TryParse(p[0].Trim(), out int id)) continue;
                    string name = p[1].Trim();
                    if (!int.TryParse(p[2].Trim(), out int price)) continue;

                    var item = new MenuItem { Id = id, Name = name, Price = price };
                    _menu[id] = item;
                    cboMon.Items.Add(item);

                    rtbView.AppendText($"{id,-3} | {name,-20} | {price} VND\n");
                }

                if (cboMon.Items.Count > 0) cboMon.SelectedIndex = 0;

                Log($"MENU loaded: {count} items");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi MENU: " + ex.Message);
                Disconnect();
            }
        }

        private void btnDat_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { MessageBox.Show("Chưa kết nối server!"); return; }

            if (!int.TryParse(txtBan.Text.Trim(), out int table) || table <= 0)
            {
                MessageBox.Show("Số bàn không hợp lệ!");
                return;
            }

            var selected = cboMon.SelectedItem as MenuItem;
            if (selected == null)
            {
                MessageBox.Show("Chưa chọn món!");
                return;
            }


            if (!int.TryParse(txtSoMon.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Số món (số lượng) không hợp lệ!");
                return;
            }

            try
            {
                writer.WriteLine($"ORDER {table}");
                writer.WriteLine($"{selected.Id} {qty}");

                string resp = reader.ReadLine();
                if (resp == null) throw new Exception("Server closed.");

                if (resp.StartsWith("OK"))
                {
                    Log($"ORDER OK: table={table}, item={selected.Id}, qty={qty} | {resp}");
                    MessageBox.Show("Đặt món thành công! " + resp);
                }
                else
                {
                    Log("ORDER << " + resp);
                    MessageBox.Show("Đặt món thất bại: " + resp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ORDER: " + ex.Message);
                Disconnect();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSoMon.Text)) return;
            txtSoMon.Text = "1";
        }

        private void CustomerClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (IsConnected())
                {
                    writer.WriteLine("QUIT");
                    reader.ReadLine();
                }
            }
            catch { }

            Disconnect();
        }
        private bool IsConnected()
        {
            try
            {
                return client != null && client.Connected;
            }
            catch { return false; }
        }

        private void Disconnect()
        {
            try { reader?.Dispose(); } catch { }
            try { writer?.Dispose(); } catch { }
            try { client?.Close(); } catch { }

            reader = null;
            writer = null;
            client = null;

            SetUiDisconnected();
            Log("DISCONNECTED");
        }

        private void SetUiConnected()
        {
            btnConnect.Text = "Ngắt";
            btnMenu.Enabled = true;
            btnDat.Enabled = true;
        }

        private void SetUiDisconnected()
        {
            btnConnect.Text = "Kết nối";
            btnMenu.Enabled = false;
            btnDat.Enabled = false;
        }

        private void Log(string msg)
        {
            if (rtbView == null) return;
            rtbView.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
        }

        private class MenuItem
        {
            public int Id;
            public string Name;
            public int Price;

            public override string ToString()
            {
                return $"{Id} - {Name} ({Price}đ)";
            }
        }
    }
}
