using DataPlayer;
using Guna.UI2.WinForms;
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
    public partial class QlVE : Form
    {
        List<VEXE> lVeXe = (new QL_BANVEXEKHACHEntities()).VEXEs.ToList();

        public QlVE()
        {
            InitializeComponent();
        }
        public void loadVx()
        {
            var vx = lVeXe.OrderBy(x => x.MAVEXE).ToList();
            foreach (var i in vx)
            {
                    int rowIndex = guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = i.MAVEXE;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = i.MAKHACHHANG;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = i.MANHANVIEN;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = i.MATUYENXE;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = i.TINHTRANG;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = i.THANHTIEN;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column7"].Value = i.VITRIGHE;
                    guna2DataGridView1.Rows[rowIndex].Cells["Column8"].Value = Image.FromFile(@"..\..\images\Close.png");
            }
        }

        private void QlVE_Load(object sender, EventArgs e)
        {
            loadVx();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns.Count - 1 && e.RowIndex >= 0)
            {
                string tuyenValue = guna2DataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Vé: " + tuyenValue + "?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string maVeXe = guna2DataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
                    VEXE veXeToDelete = lVeXe.FirstOrDefault(v => v.MAVEXE == maVeXe);
                    if (veXeToDelete != null)
                    {
                        lVeXe.Remove(veXeToDelete);
                    }
                    using (var context = new QL_BANVEXEKHACHEntities())
                    {
                        var veXeToDeleteDB = context.VEXEs.FirstOrDefault(v => v.MAVEXE == maVeXe);
                        if (veXeToDeleteDB != null)
                        {
                            context.VEXEs.Remove(veXeToDeleteDB);
                            context.SaveChanges();
                        }
                    }
                    MessageBox.Show("Đã xóa Vé: " + tuyenValue + " thành công!");
                }
            }
            //string tuyenValue = guna2DataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();

            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Vé: " + tuyenValue + "?", "Xác nhận", MessageBoxButtons.YesNo);

            //if (result == DialogResult.Yes)
            //{
            //    string maVeXe = guna2DataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();

            //    guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
            //    var veXeToDelete = lVeXe.FirstOrDefault(v => v.MAVEXE == maVeXe);
            //    if (veXeToDelete != null)
            //    {
            //        lVeXe.Remove(veXeToDelete);
            //    }
            //    using (var context = new QL_BANVEXEKHACHEntities())
            //    {
            //        var veXedel = context.VEXEs.FirstOrDefault(v => v.MAVEXE == maVeXe);
            //        if (veXedel != null)
            //        {
            //            context.VEXEs.Remove(veXedel);
            //            context.SaveChanges();
            //        }
            //    }

            //    MessageBox.Show("Đã xóa Vé: " + tuyenValue + " thành công!");
            //}
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = guna2DataGridView1.CurrentRow.Index;

                // Lấy thông tin từ các ô trong dòng hiện tại
                string maVeXe = guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value.ToString();
                string maKhachHang = guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value.ToString();
                string maNhanVien = guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value.ToString();
                string maTuyenXe = guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value.ToString();
                string tinhTrang = guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value.ToString();
                decimal thanhTien = Convert.ToDecimal(guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value.ToString());
                string viTriGhe = guna2DataGridView1.Rows[rowIndex].Cells["Column7"].Value.ToString();

                // Hiển thị các điều kiện sửa trực tiếp tại đây (ví dụ, sử dụng TextBox, ComboBox, ...)

                // Sau khi người dùng sửa xong, cập nhật lại dữ liệu trong DataGridView
                guna2DataGridView1.Rows[rowIndex].Cells["Column1"].Value = maVeXe;
                guna2DataGridView1.Rows[rowIndex].Cells["Column2"].Value = maKhachHang;
                guna2DataGridView1.Rows[rowIndex].Cells["Column3"].Value = maNhanVien;
                guna2DataGridView1.Rows[rowIndex].Cells["Column4"].Value = maTuyenXe;
                guna2DataGridView1.Rows[rowIndex].Cells["Column5"].Value = tinhTrang;
                guna2DataGridView1.Rows[rowIndex].Cells["Column6"].Value = thanhTien;
                guna2DataGridView1.Rows[rowIndex].Cells["Column7"].Value = viTriGhe;

                using (var context = new QL_BANVEXEKHACHEntities())
                {
                    var veXeToUpdate = context.VEXEs.FirstOrDefault(v => v.MAVEXE == maVeXe);
                    if (veXeToUpdate != null)
                    {
                        veXeToUpdate.MAKHACHHANG = maKhachHang;
                        veXeToUpdate.MANHANVIEN = maNhanVien;
                        veXeToUpdate.MATUYENXE = maTuyenXe;
                        veXeToUpdate.TINHTRANG = tinhTrang;
                        veXeToUpdate.THANHTIEN = thanhTien;
                        veXeToUpdate.VITRIGHE = Convert.ToInt32(viTriGhe);

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Đã cập nhật thông tin vé xe thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa thông tin vé xe.");
            }
        }
    }
}
