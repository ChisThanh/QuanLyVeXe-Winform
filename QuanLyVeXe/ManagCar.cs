using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataPlayer;
namespace QuanLyVeXe
{
    public partial class ManagCar : Form
    {
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities();
        List<XE> _l = (new QL_BANVEXEKHACHEntities()).XEs.ToList();
        public ManagCar()
        {
            InitializeComponent();
        }
       
        private void ManageTickets_Load(object sender, EventArgs e)
        {
            load();
            guna2DataGridView1.ReadOnly = true;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string t = guna2TextBox1.Text;
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            var s = _l.FindAll(each => each.MAXE.ToLower().Contains(t.ToLower()) == true);

            if(s.Count() == 0)
            {
                MessageBox.Show("Không tìm thấy");
                return;
            }
      
            foreach (var item in s)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Col1"].Value = item.MAXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col2"].Value = item.SOLUONGGHE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col4"].Value = Image.FromFile(@"..\..\images\Close.png");
            }
        }
        private void load()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            foreach (var item in _l)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Col1"].Value = item.MAXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col2"].Value = item.SOLUONGGHE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col4"].Value = Image.FromFile(@"..\..\images\Close.png");
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

            if (e.ColumnIndex == 2)
            {
                if (MessageBox.Show("Bạn có muốn xóa", "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                object cellValue = selectedRow.Cells[0].Value;

                if (cellValue != null)
                {
                    try
                    {
                        db.XEs.Remove(db.XEs.ToList().Find(each => each.MAXE == cellValue.ToString()));
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xe này không thể xóa. vì đang hoạt động");
                        return;
                    }

                    guna2DataGridView1.Rows.Remove(selectedRow);
                }
            }

            
        }
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            _l = (new QL_BANVEXEKHACHEntities()).XEs.ToList();
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.Refresh();
            foreach (var item in _l)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Col1"].Value = item.MAXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col2"].Value = item.SOLUONGGHE;
                guna2DataGridView1.Rows[rowIndex].Cells["Col4"].Value = Image.FromFile(@"..\..\images\Close.png");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddManageCar f = new AddManageCar();
            f.Show();
        }
    }
}
