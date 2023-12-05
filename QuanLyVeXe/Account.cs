using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using DataPlayer;
using System.Windows.Interop;

namespace QuanLyVeXe
{
    public partial class Account : Form
    {
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities();

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
            string email = txtEmail.Text;
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Bạn phải điền đầy đủ thông tin");
                return; 
            }

            var uu = db.NHANVIENs.ToList().Find(each => each.EMAIL == email);
            if(uu == null)
            {
                MessageBox.Show("Email không tồn tại bạn cần đăng ký tài khoản mới");
                return;
            }

            var u = db.NHANVIENs.ToList().Find(each => each.EMAIL == email && each.MATKHAU_NV == pass);
            if(u == null)
            {
                MessageBox.Show("Email hoặc mật khẩu đã sai");
                return;
            }

            if (u.QUYEN == "admin")
            {
                Admin a = new Admin();
                a.Show();
                this.Hide();
                a.Logout += F_Logout;
            }
            else 
            {
                Customer c = new Customer();
                c.Show();
                this.Hide();
                c.Logout += F_Logout;
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
