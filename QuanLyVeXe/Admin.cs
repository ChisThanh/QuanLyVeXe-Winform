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
        private Form activeForm = null;
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
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            openChildForm(new User());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerAdmin()); 
        }
 
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Dashboard());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            openChildForm(new ManagCar());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            openChildForm(new QlVE());
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            openChildForm(new User());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openChildForm(new QLTuyen());
        }
    }
}
