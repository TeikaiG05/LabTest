using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabTest
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Server_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Show();
        }

        private void CustomerClient_Click(object sender, EventArgs e)
        {
            CustomerClient client = new CustomerClient();
            client.Show();
        }

        private void StaffClient_Click(object sender, EventArgs e)
        {
            StaffClient staff = new StaffClient();
            staff.Show();
        }
    }
}
