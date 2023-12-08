using DataPlayer;
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
    public partial class AddTuyen : Form
    {
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities(); 
        public AddTuyen()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string maTuyen = guna2TextBox1.Text;
            string tuyen = guna2TextBox2.Text;
            string maXe = guna2ComboBox2.Text;
            string maTaiXe = guna2ComboBox1.Text;
            int soGheTrong = Convert.ToInt32(guna2TextBox5.Text);
            TimeSpan gioKhoiHanh = TimeSpan.Parse(guna2TextBox6.Text);
            string diemDi = guna2ComboBox4.Text;
            string diemDen = guna2ComboBox3.Text;
            //decimal giaTien = Convert.ToDecimal(guna2TextBox9.Text);

            TUYENXE newTuyenXe = new TUYENXE
            {
                MATUYENXE = maTuyen,
                TUYEN = tuyen,
                MAXE = maXe,
                MATAIXE = maTaiXe,
                SOGHETRONG = soGheTrong,
                GIOKHOIHANH = gioKhoiHanh,
                DIEMDI = diemDi,
                DIEMDEN = diemDen,
            };

            using (var context = new QL_BANVEXEKHACHEntities())
            {
                context.TUYENXEs.Add(newTuyenXe);
                context.SaveChanges();
                MessageBox.Show("Thêm thành công!");
            }

            this.Close();
        }

        private void AddTuyen_Load(object sender, EventArgs e)
        {
            var lxe = db.XEs.ToList();
            var ltx = db.TAIXEs.ToList();
            var ltt = db.TINHTHANHs.ToList();
            guna2ComboBox2.DataSource = lxe;
            guna2ComboBox2.ValueMember = "MAXE";
            guna2ComboBox1.DataSource = ltx;
            guna2ComboBox1.ValueMember = "MATAIXE";
            guna2ComboBox4.DataSource = ltt;
            guna2ComboBox4.ValueMember = "MA";

        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ltt = db.TINHTHANHs.ToList();
            guna2ComboBox3.DataSource = ltt;
            guna2ComboBox3.ValueMember = "MA";

        }
    }
}
