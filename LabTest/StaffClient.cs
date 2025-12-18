using System;
using System.Collections.Generic;
using System.Data;
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
        private DataTable orderTable;

        public StaffClient()
        {
            InitializeComponent();
            InitializeOrderTable();
            ConnectToServer();
        }

        // Khởi tạo cấu trúc bảng hiển thị
        private void InitializeOrderTable()
        {
            orderTable = new DataTable();
            orderTable.Columns.Add("Số bàn");
            orderTable.Columns.Add("Tên món");
            orderTable.Columns.Add("Số lượng");
            orderTable.Columns.Add("Thành tiền");
            dataGridView1.DataSource = orderTable;
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream) { AutoFlush = true };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối Server: " + ex.Message);
            }
        }

        // Xử lý nút "Tính tiền"
        private async void btnPay_Click(object sender, EventArgs e)
        {
            string tableId = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(tableId)) return;

            try
            {
                // 1. Gửi lệnh PAY đến server (Định dạng: PAY|TableID)
                await writer.WriteLineAsync($"PAY|{tableId}");

                // 2. Nhận phản hồi tổng tiền từ Server
                string response = await reader.ReadLineAsync();
                label2.Text = $"Tổng tiền: {response} VNĐ";

                // 3. Xuất hóa đơn ra file text
                ExportBill(tableId, response);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tiền: " + ex.Message);
            }
        }

        private void ExportBill(string tableId, string total)
        {
            try
            {
                string fileName = $"bill_Ban{tableId}.txt";
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("--- HÓA ĐƠN THANH TOÁN ---");
                    sw.WriteLine($"Số bàn: {tableId}");
                    sw.WriteLine($"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    sw.WriteLine("--------------------------");

                    // Lặp qua DataGridView để ghi chi tiết món
                    foreach (DataRow row in orderTable.Rows)
                    {
                        if (row["Số bàn"].ToString() == tableId)
                        {
                            sw.WriteLine($"{row["Tên món"]} | SL: {row["Số lượng"]} | T.Tiền: {row["Thành tiền"]}");
                        }
                    }

                    sw.WriteLine("--------------------------");
                    sw.WriteLine($"TỔNG CỘNG: {total} VNĐ");
                    sw.WriteLine("Cảm ơn quý khách!");
                }
                MessageBox.Show($"Đã xuất hóa đơn: {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message);
            }
        }

        // Mock dữ liệu khi nhận được Order mới từ Server (Event-driven)
        public void AddOrderToGrid(string table, string dish, int qty, double price)
        {
            orderTable.Rows.Add(table, dish, qty, price);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
