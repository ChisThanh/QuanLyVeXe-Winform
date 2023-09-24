using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVeXe
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public bool isExit = true;
        public event EventHandler Logout;
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(isExit)
            {
                Application.Exit();
            }
        }
    }
}
