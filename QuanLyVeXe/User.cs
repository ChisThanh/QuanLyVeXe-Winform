using DataPlayer;
using QuanLyVeXe.Models;
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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            NHANVIEN user = GlobalVariables.emp;

            txt_HoTen.Text = user.TENNHANVIEN;
            txt_TenDN.Text = user.MANHANVIEN;
            txtDC.Text = user.QUYEN;
            txt_sdt.Text = user.SDT;
            txt_Email.Text = user.EMAIL;
            txt_pass.Text = user.MATKHAU_NV;
            txt_NgaySinh.Text = user.NAMSINH.ToString();
            if (user.GIOITINH == "Nam")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }
    }
}
