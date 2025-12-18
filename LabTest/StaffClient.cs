using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LabTest
{
    public partial class StaffClient : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        private const string DEFAULT_HOST = "192.168.219.96";
        private const int DEFAULT_PORT = 8080;

        public StaffClient()
        {
            InitializeComponent();

            //btnTinhTien.Click += btnTinhTien_Click;
            this.Load += StaffClient_Load;
            this.FormClosing += StaffClient_FormClosing;
        }

        private void StaffClient_Load(object sender, EventArgs e)
        {
            InitBillGrid();
        }

        private void InitBillGrid()
        {
            dgvBill.AutoGenerateColumns = false;
            dgvBill.Columns.Clear();

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Width = 60
            });

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên món",
                Width = 200
            });

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQty",
                HeaderText = "SL",
                Width = 60
            });

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Giá",
                Width = 90
            });

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "Thành tiền",
                Width = 110
            });

            dgvBill.ReadOnly = true;
            dgvBill.AllowUserToAddRows = false;
            dgvBill.AllowUserToDeleteRows = false;
            dgvBill.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBill.MultiSelect = false;
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBan.Text.Trim(), out int table) || table <= 0)
            {
                MessageBox.Show("Số bàn không hợp lệ!");
                return;
            }

            try
            {
                EnsureConnected(DEFAULT_HOST, DEFAULT_PORT);

                dgvBill.Rows.Clear();

                writer.WriteLine("PAY " + table);

                string header = reader.ReadLine();
                if (header == null) throw new Exception("Server closed.");

                if (!header.StartsWith("OK"))
                {
                    MessageBox.Show("Lỗi: " + header);
                    return;
                }

                long total = 0;
                int count = 0;
                var parts = header.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2) long.TryParse(parts[1], out total);
                if (parts.Length >= 3) int.TryParse(parts[2], out count);

                for (int i = 0; i < count; i++)
                {
                    string line =  reader.ReadLine();
                    if (line == null) throw new Exception("Server closed while reading bill.");

                    var p = line.Split(';');
                    if (p.Length == 6)
                    {
                        string id = p[1];
                        string name = p[2];
                        string qty = p[3];
                        string price = p[4];
                        string amount = p[5];

                        dgvBill.Rows.Add(id, name, qty, price, amount);
                    }
                    else
                    {
                        dgvBill.Rows.Add("?", line, "", "", "");
                    }
                }

                this.Text = "StaffClient - Total: " + total + " VND";
                MessageBox.Show("Tổng tiền: " + total + " VND");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tiền: " + ex.Message);
                Disconnect();
            }
        }

        private void EnsureConnected(string host, int port)
        {
            if (client != null && client.Connected) return;

            client = new TcpClient();
            client.Connect(host, port);

            var ns = client.GetStream();
            reader = new StreamReader(ns, Encoding.UTF8);
            writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true };
        }

        private void Disconnect()
        {
            try { reader?.Dispose(); } catch { }
            try { writer?.Dispose(); } catch { }
            try { client?.Close(); } catch { }

            reader = null;
            writer = null;
            client = null;
        }

        private void StaffClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    writer.WriteLine("QUIT");
                    reader.ReadLine();
                }
            }
            catch { }
            Disconnect();
        }
    }
}
