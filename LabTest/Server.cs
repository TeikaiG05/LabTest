using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LabTest
{
    public partial class Server : Form
    {
        private TcpListener listener;
        private Thread acceptThread;
        private volatile bool running = false;
        private readonly Dictionary<int, MenuItem> menu = new Dictionary<int, MenuItem>();
        private readonly Dictionary<int, List<OrderLine>> orders = new Dictionary<int, List<OrderLine>>();
        private readonly object Lock = new object();

        public Server()
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            this.Load += Server_Load;
            this.FormClosing += Server_FormClosing;
        }

        private void Server_Load(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            txtIP.Text = GetLocalIPv4();
            if (string.IsNullOrWhiteSpace(txtPort.Text)) txtPort.Text = "8080";

            LoadMenuFromFile();
            Log("SERVER READY");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (running) return;

            int port;
            if (!int.TryParse(txtPort.Text.Trim(), out port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Port không hợp lệ!");
                return;
            }

            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                running = true;

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                txtPort.Enabled = false;

                Log($"SERVER START: {txtIP.Text}:{port}");

                acceptThread = new Thread(AcceptLoop);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            running = false;

            try {listener?.Stop(); } catch { }

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            txtPort.Enabled = true;

            Log("SERVER STOP");
        }

        private void AcceptLoop()
        {
            while (running)
            {
                try
                {
                    var client = listener.AcceptTcpClient();

                    var t = new Thread(() => HandleClient(client));
                    t.IsBackground = true;
                    t.Start();
                }
                catch
                {
                    if (running) Log("ACCEPT ERROR");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            string endpoint = "unknown";
            try { endpoint = client.Client.RemoteEndPoint?.ToString() ?? "unknown"; } catch { }

            Log($"CONNECT {endpoint}");

            try
            {
                using (client)
                using (var ns = client.GetStream())
                using (var reader = new StreamReader(ns, Encoding.UTF8))
                using (var writer = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true })
                {
                    while (running && client.Connected)
                    {
                        string line = reader.ReadLine();
                        if (line == null) break;

                        line = line.Trim();
                        if (line.Length == 0) continue;

                        // QUIT
                        if (line.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
                        {
                            writer.WriteLine("BYE");
                            Log($"{endpoint} >> QUIT | << BYE");
                            break;
                        }

                        // MENU
                        if (line.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                        {
                            var items = menu    .Values.OrderBy(x => x.Id).ToList();
                            writer.WriteLine($"OK {items.Count}");
                            foreach (var m in items)
                                writer.WriteLine($"{m.Id};{m.Name};{m.Price}");

                            Log($"{endpoint} >> MENU | << OK {items.Count}");
                            continue;
                        }

                        if (line.StartsWith("ORDER", StringComparison.OrdinalIgnoreCase))
                        {
                            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            int table, itemId, qty;

                            if (parts.Length == 2)
                            {
                                // ORDER
                                if (!int.TryParse(parts[1], out table)) { writer.WriteLine("ERR BAD_TABLE"); continue; }

                                var next = reader.ReadLine();
                                if (next == null) { writer.WriteLine("ERR MISSING_ITEMLINE"); continue; }
                                next = next.Trim();

                                var p2 = next.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (p2.Length != 2) { writer.WriteLine("ERR BAD_ITEMLINE"); continue; }

                                if (!int.TryParse(p2[0], out itemId) || !int.TryParse(p2[1], out qty))
                                { writer.WriteLine("ERR BAD_ITEMLINE"); continue; }
                            }
                            else if (parts.Length == 4)
                            {
                                if (!int.TryParse(parts[1], out table) ||
                                    !int.TryParse(parts[2], out itemId) ||
                                    !int.TryParse(parts[3], out qty))
                                { writer.WriteLine("ERR BAD_ORDER"); continue; }
                            }
                            else
                            {
                                writer.WriteLine("ERR BAD_ORDER");
                                continue;
                            }

                            if (qty <= 0) { writer.WriteLine("ERR QTY"); continue; }
                            if (!menu.TryGetValue(itemId, out var mi)) { writer.WriteLine("ERR ITEM_NOT_FOUND"); continue; }

                            lock (Lock)
                            {
                                if (!orders.TryGetValue(table, out var list))
                                {
                                    list = new List<OrderLine>();
                                    orders[table] = list;
                                }

                                var existed = list.FirstOrDefault(x => x.ItemId == itemId);
                                if (existed == null) list.Add(new OrderLine { ItemId = itemId, Qty = qty });
                                else existed.Qty += qty;
                            }

                            long amount = (long)mi.Price * qty;
                            writer.WriteLine($"OK {amount}");

                            Log($"{endpoint} >> ORDER {table} | {itemId}x{qty} | << OK {amount}");
                            continue;
                        }

                        if (line.Equals("GET_ORDERS", StringComparison.OrdinalIgnoreCase))
                        {
                            var rows = new List<string>();

                            lock (Lock)
                            {
                                foreach (var kv in orders)
                                {
                                    int table = kv.Key;
                                    foreach (var ol in kv.Value)
                                    {
                                        var mi = menu[ol.ItemId];
                                        long amount = (long)mi.Price * ol.Qty;
                                        rows.Add($"{table};{mi.Id};{mi.Name};{ol.Qty};{mi.Price};{amount}");
                                    }
                                }
                            }

                            writer.WriteLine($"OK {rows.Count}");
                            foreach (var r in rows) writer.WriteLine(r);

                            Log($"{endpoint} >> GET_ORDERS | << OK {rows.Count}");
                            continue;
                        }

                        if (line.StartsWith("PAY", StringComparison.OrdinalIgnoreCase))
                        {
                            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length != 2) { writer.WriteLine("ERR BAD_PAY"); continue; }

                            if (!int.TryParse(parts[1], out int tablePay))
                            { writer.WriteLine("ERR BAD_TABLE"); continue; }

                            long total = 0;
                            var bill = new List<string>();

                            lock (Lock)
                            {
                                if (!orders.TryGetValue(tablePay, out var list) || list.Count == 0)
                                { writer.WriteLine("ERR EMPTY"); continue; }

                                foreach (var ol in list)
                                {
                                    var mi = menu[ol.ItemId];
                                    long amount = (long)mi.Price * ol.Qty;
                                    total += amount;
                                    bill.Add($"{tablePay};{mi.Id};{mi.Name};{ol.Qty};{mi.Price};{amount}");
                                }

                                orders.Remove(tablePay);
                            }

                            writer.WriteLine($"OK {total} {bill.Count}");
                            foreach (var b in bill) writer.WriteLine(b);

                            Log($"{endpoint} >> PAY {tablePay} | << OK total={total} lines={bill.Count}");
                            continue;
                        }

                        writer.WriteLine("ERR UNKNOWN_CMD");
                        Log($"{endpoint} >> {line} | << ERR UNKNOWN_CMD");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"{endpoint} ERROR: {ex.Message}");
            }

            Log($"DISCONNECT {endpoint}");
        }

        private void LoadMenuFromFile()
        {
            menu.Clear();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "menu.txt");

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                var p = line.Split(';');
                if (p.Length != 3) continue;

                if (!int.TryParse(p[0].Trim(), out int id)) continue;
                string name = p[1].Trim();
                if (!int.TryParse(p[2].Trim(), out int price)) continue;

                menu[id] = new MenuItem { Id = id, Name = name, Price = price };
            }

            Log($"MENU LOADED: {menu.Count} items");
        }

        private string GetLocalIPv4()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                var ip = host.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
                return ip?.ToString() ?? "0.0.0.0";
            }
            catch { return "0.0.0.0"; }
        }

        private void Log(string msg)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(new Action(() => Log(msg)));
                return;
            }
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private class MenuItem
        {
            public int Id;
            public string Name;
            public int Price;
        }

        private class OrderLine
        {
            public int ItemId;
            public int Qty;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
