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
using QuanLyVeXe.Models;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
        private void btnGetCode_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtefp.Text))
            {
                MessageBox.Show("Bạn chưa điền email", "Thông báo");
                return;
            }

            MessageBox.Show("Mã sẽ được gửi về email bạn vừa nhập!!", "Thông báo");

            string code = Helper.GenerateRandomString(5);
            GlobalVariables.code = code;
            Helper.SendEmail(txtefp.Text, "Mã code đăng ký", "Mã đăng ký của bạn là: " + code);
        }
        private void btnGetCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtE.Text))
            {
                MessageBox.Show("Bạn chưa điền email", "Thông báo");
                return;
            }

            MessageBox.Show("Mã sẽ được gửi về email bạn vừa nhập!!", "Thông báo");

            string code = Helper.GenerateRandomString(5);
            GlobalVariables.code = code;
            Helper.SendEmail(txtE.Text, "Mã code ", "Mã xác nhận đổi mật khẩu của bạn là: " + code);

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
            var kk = db.KHACHHANGs.ToList().Find(each => each.EMAIL == email);

            if (uu == null && kk == null)
            {
                MessageBox.Show("Email không tồn tại bạn cần đăng ký tài khoản mới");
                return;
            }


            var u = db.NHANVIENs.ToList().Find(each => each.EMAIL == email && each.MATKHAU_NV == pass);
            var kh = db.KHACHHANGs.ToList().Find(each => each.EMAIL == email && each.MATKHAU_KH == pass);

            if (u == null && kh == null)
            {
                MessageBox.Show("Email hoặc mật khẩu đã sai");
                return;
            }


            if (u != null)
            {
                GlobalVariables.emp = u;
                Admin a = new Admin();
                a.Show();
                this.Hide();
                a.Logout += F_Logout;
            }
            if (kh != null)
            {
                GlobalVariables.cus = kh;
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

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if(
                string.IsNullOrEmpty(txtT.Text) ||
                string.IsNullOrEmpty(txtP.Text) ||
                string.IsNullOrEmpty(txtGt.Text) ||
                string.IsNullOrEmpty(txtD.Text) ||
                string.IsNullOrEmpty(txtE.Text) ||
                string.IsNullOrEmpty(txtDT.Text) ||
                string.IsNullOrEmpty(txtCode.Text)
                )
            {
                MessageBox.Show("Bạn phải điền đầy đủ thông tin");
                return;
            }
            string code = GlobalVariables.code;
            if (code != txtCode.Text)
            {
                MessageBox.Show("Mã không đúng!");
                return;
            }
            GlobalVariables.code = "";
            var last = db.KHACHHANGs
                         .OrderByDescending(each => each.MAKHACHHANG)
                         .FirstOrDefault();
            string newId;

            if (last != null)
            {
                string Strlast = last.MAKHACHHANG;
                newId = Helper.GetNextCode("KH", Strlast.Trim());
            }
            else
            {
                newId = "KH001";
            }
            KHACHHANG kh = new KHACHHANG() { 
                MAKHACHHANG = newId,
                TENKHACHHANG = txtT.Text,
                EMAIL = txtE.Text,
                GIOITINH = txtGt.Text,
                SDT = txtDT.Text,
                NAMSINH = int.Parse(txtD.Text),
                MATKHAU_KH = txtP.Text,
            };

            db.KHACHHANGs.Add(kh);
            db.SaveChanges();

            MessageBox.Show("Đăng ký thành công mời bạn qua đăng nhập");

            foreach (var item in tabSignUp.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    textBox.Clear();
                }
            }
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            Guna.UI2.WinForms.Guna2TextBox  textBox = (Guna.UI2.WinForms.Guna2TextBox)sender;
            if (textBox.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string email = txtefp.Text;

            var uu = db.NHANVIENs.ToList().Find(each => each.EMAIL == email);
            var kk = db.KHACHHANGs.ToList().Find(each => each.EMAIL == email);

            if (uu == null && kk == null)
            {
                MessageBox.Show("Email không tồn tại bạn cần đăng ký tài khoản mới");
                return;
            }

            string code = GlobalVariables.code;
            if (code != txtcodefp.Text)
            {
                MessageBox.Show("Mã không đúng!");
                return;
            }
            GlobalVariables.code = "";

            if(kk != null)
            {
                kk.MATKHAU_KH = txtpfp.Text;
            }
            if (uu != null)
            {
                uu.MATKHAU_NV = txtpfp.Text;
            }
            db.SaveChanges();
            MessageBox.Show("Đổi mật khẩu thành công!");
        }
    }
}
