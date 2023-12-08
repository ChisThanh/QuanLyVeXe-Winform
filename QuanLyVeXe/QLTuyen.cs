using DataPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVeXe
{
    public partial class QLTuyen : Form
    {
        List<TUYENXE> ListTX = (new QL_BANVEXEKHACHEntities()).TUYENXEs.ToList();
        private QL_BANVEXEKHACHEntities context;
        public QLTuyen()
        {
            context = new QL_BANVEXEKHACHEntities();
            InitializeComponent();
        }

        private void QLTuyen_Load(object sender, EventArgs e)
        {
            load();
        }
        private void load()
        {
            var vx = ListTX.OrderBy(x => x.MATUYENXE).ToList();
            foreach (var i in vx)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = i.MATUYENXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = i.TUYEN;
                guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = i.MAXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = i.MATAIXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = i.SOGHETRONG;
                guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = i.GIOKHOIHANH;
                guna2DataGridView1.Rows[rowIndex].Cells["Column7"].Value = i.DIEMDI;
                guna2DataGridView1.Rows[rowIndex].Cells["Column8"].Value = i.DIEMDEN;
            }
        }
        private void RefreshData()
        {
            // Clear existing rows
            guna2DataGridView1.Rows.Clear();

            // Reload data
            var vx = context.TUYENXEs.OrderBy(x => x.MATUYENXE).ToList();
            foreach (var i in vx)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = i.MATUYENXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = i.TUYEN;
                guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = i.MAXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = i.MATAIXE;
                guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = i.SOGHETRONG;
                guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = i.GIOKHOIHANH;
                guna2DataGridView1.Rows[rowIndex].Cells["Column7"].Value = i.DIEMDI;
                guna2DataGridView1.Rows[rowIndex].Cells["Column8"].Value = i.DIEMDEN;
            }
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddTuyen addTuyenForm = new AddTuyen();
            addTuyenForm.ShowDialog();
            RefreshData();
            //load();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string maTuyenXe = row.Cells["Column1"].Value.ToString();
                    string tuyen = row.Cells["Column2"].Value.ToString();
                    string maXe = row.Cells["Column3"].Value.ToString();
                    string maTaiXe = row.Cells["Column4"].Value.ToString();
                    int gheTrong = Convert.ToInt32(row.Cells["Column5"].Value.ToString());
                    TimeSpan gioKH = TimeSpan.Parse(row.Cells["Column6"].Value.ToString());
                    string diemDI = row.Cells["Column7"].Value.ToString();
                    string diemDen = row.Cells["Column8"].Value.ToString();
                    var tuyenXeToUpdate = context.TUYENXEs.FirstOrDefault(t => t.MATUYENXE == maTuyenXe);
                    if (tuyenXeToUpdate != null)
                    {
                        tuyenXeToUpdate.TUYEN = tuyen;
                        tuyenXeToUpdate.MAXE = maXe;
                        tuyenXeToUpdate.MATAIXE = maTaiXe;
                        tuyenXeToUpdate.SOGHETRONG = gheTrong;
                        tuyenXeToUpdate.GIOKHOIHANH = gioKH;
                        tuyenXeToUpdate.DIEMDI = diemDI;
                        tuyenXeToUpdate.DIEMDEN = diemDen;


                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Đã lưu thông tin tuyến xe thành công!");
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
