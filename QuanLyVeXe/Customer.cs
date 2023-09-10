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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabHome;
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab=tabSchedule;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabSearch;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabBill;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabUser;
        }

       
    }
}
