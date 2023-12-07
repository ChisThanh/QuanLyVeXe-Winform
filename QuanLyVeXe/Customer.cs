using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using DataPlayer;
using QuanLyVeXe.Models;
namespace QuanLyVeXe
{
    public partial class Customer : Form
    {
        public bool isExit = true;
        public event EventHandler Logout;
        AutoCompleteStringCollection collfrom = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collto = new AutoCompleteStringCollection();
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities();
        List<TUYENXE> _list = (new QL_BANVEXEKHACHEntities()).TUYENXEs.ToList();
        public Customer()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }
        private void Customer_Load(object sender, EventArgs e)
        {
            var l = db.TINHTHANHs.ToList();
            foreach (var item in l)
            {
                collfrom.Add(item.TEN);
                collto.Add(item.TEN);
            }
            from.AutoCompleteMode = AutoCompleteMode.Suggest;
            from.AutoCompleteSource = AutoCompleteSource.CustomSource;
            from.AutoCompleteCustomSource = collfrom;

            to.AutoCompleteMode = AutoCompleteMode.Suggest;
            to.AutoCompleteSource = AutoCompleteSource.CustomSource;
            to.AutoCompleteCustomSource = collto;
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabHome;
        }
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab=tabSchedule;

            var ltmp = _list.OrderBy(each => each.DIEMDI).ToList();
            foreach (var ii in ltmp)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Col1"].Value = ii.TUYEN;
                guna2DataGridView1.Rows[rowIndex].Cells["Col2"].Value = ii.GIOKHOIHANH;
                guna2DataGridView1.Rows[rowIndex].Cells["Col3"].Value = Image.FromFile(@"..\..\images\ShoppingCart.png");
            }
            
        }
        private void btnBill_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabBill;
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabUser;
            KHACHHANG user = GlobalVariables.cus;

            txt_HoTen.Text = user.TENKHACHHANG;
            txt_TenDN.Text = user.MAKHACHHANG;
            txtDC.Text = user.DIACHI;
            txt_sdt.Text = user.SDT;
            txt_Email.Text = user.EMAIL;
            txt_pass.Text = user.MATKHAU_KH;
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isExit)
            {
                Application.Exit();
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string strFrom = from.Text;
            string strTo = to.Text;

            var vF = db.TINHTHANHs.FirstOrDefault(item => item.TEN == strFrom);
            var vTo = db.TINHTHANHs.FirstOrDefault(item => item.TEN == strTo);

            if(vF == null || vTo == null )
            {
                MessageBox.Show("Tỉnh thành không tồn tại");
                return;
            }

            var tx = _list.FindAll(item => item.DIEMDI == vF.MA && item.DIEMDEN == vTo.MA).ToList();
            if (tx == null || tx.Count() == 0)
            {
                MessageBox.Show("Hiện tại chưa có tuyến xe này!. Vui lòng tìm kiếm tuyến xe khác giúp");
                return;
            }

           
            DataGirdView(tx);
            return;
        }
        public void DataGirdView(List<TUYENXE> list)
        {
            gvhome.Rows.Clear();
            gvhome.Refresh();
            foreach (var ii in list)
            {
                int rowIndex = gvhome.Rows.Add();
                gvhome.Rows[rowIndex].Cells["C1"].Value = ii.MATUYENXE;
                gvhome.Rows[rowIndex].Cells["C2"].Value = ii.TUYEN;
                gvhome.Rows[rowIndex].Cells["C3"].Value = ii.MAXE;
                gvhome.Rows[rowIndex].Cells["C4"].Value = ii.MATAIXE;
                gvhome.Rows[rowIndex].Cells["C5"].Value = ii.SOGHETRONG;
                gvhome.Rows[rowIndex].Cells["C6"].Value = ii.GIOKHOIHANH;
                gvhome.Rows[rowIndex].Cells["C7"].Value = Image.FromFile(@"..\..\images\ShoppingCart.png");
            }
        }

        private void gvhome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                object cellValue = gvhome.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                MessageBox.Show($"Đã nhấp vào ô có giá trị: {cellValue}");
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                object cellValue = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                MessageBox.Show($"Đã nhấp vào ô có giá trị: {cellValue}");
            }
        }

    }
}
