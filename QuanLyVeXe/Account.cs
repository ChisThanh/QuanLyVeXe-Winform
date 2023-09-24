using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace QuanLyVeXe
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }
      

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mã sẽ được gửi về email bạn vừa nhập!!", "Thông báo");
        }

        private void btnClickSignUp_Click(object sender, EventArgs e)
        {
            Tab_Main.SelectedTab = tabNone;
            Tab_Main.SelectedTab = tabSignUp;

            tabSignIn.Width = 0;
            tabSignIn.Height = 0;
            tabSignIn.BackColor = Color.White;
        }

        private void btnClickSignIn_Click(object sender, EventArgs e)
        {
            Tab_Main.SelectedTab = tabNone;
            Tab_Main.SelectedTab = tabSignIn;

            tabSignUp.Width = 0;
            tabSignUp.Height = 0;
            tabSignUp.BackColor = Color.White;
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {

            Tab_Main.SelectedTab = tabNone;
            Tab_Main.SelectedTab = tabForgotPass;

            tabSignUp.Width = 0;
            tabSignUp.Height = 0;
            tabSignUp.BackColor = Color.White;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "c")
            {
                Customer c = new Customer();
                c.Show();
                this.Hide();
                c.Logout += F_Logout;
            }
            else 
            {
                Admin a = new Admin();
                a.Show();
                this.Hide();
                a.Logout += F_Logout;
            }
        }

        private void F_Logout(object sender, EventArgs e)
        {
            if(sender is Customer)
            {
                (sender as Customer).isExit = false;
                (sender as Customer).Close();
                this.Show();
                return;
            }

            (sender as Admin).isExit = false;
            (sender as Admin).Close();
            this.Show();
        }

       
    }
}
