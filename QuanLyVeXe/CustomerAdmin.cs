using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVeXe
{
    public partial class CustomerAdmin : Form
    {

        SqlConnection cnn = new SqlConnection("Data Source=ChisThanh\\CHISTHANH; Initial Catalog = QL_BANVEXEKHACH; Integrated Security = True");
        DataSet ds_QLBV = new DataSet();
        SqlDataAdapter da_ct;

        void loadDuLieuQuanLy()
        {
            string strsel = "SELECT * FROM KHACHHANG";
            da_ct = new SqlDataAdapter(strsel, cnn);
            da_ct.Fill(ds_QLBV, "KHACHHANG");
            loadComBoBox();
            guna2DataGridView1.DataSource = ds_QLBV.Tables["KHACHHANG"];
            cbb_GioiTinh.DataSource = ds_QLBV.Tables["KHACHHANG"];
            cbb_GioiTinh.DisplayMember = "GIOITINH";
            DataColumn[] keycolum = new DataColumn[2];
            keycolum[0] = ds_QLBV.Tables["KHACHHANG"].Columns["MAKHACHHANG"];
            ds_QLBV.Tables["KHACHHANG"].PrimaryKey = keycolum;
        }

        private void loadComBoBox()
        {
            DataTable dtTinhThanh = new DataTable();
            string load_tt = "SELECT * FROM TINHTHANH";

            using (SqlDataAdapter da_tt = new SqlDataAdapter(load_tt, cnn))
            {
                da_tt.Fill(dtTinhThanh);
            }
            cbb_DiaChi.DataSource = dtTinhThanh.DefaultView.ToTable(true, "TEN");
            cbb_DiaChi.DisplayMember = "TEN";
        }
        public CustomerAdmin()
        {
            InitializeComponent();
        }

        private void CustomerAdmin_Load(object sender, EventArgs e)
        {
            this.kHACHHANGTableAdapter.Fill(this.qL_BANVEXEKHACHDataSet.KHACHHANG);
            loadDuLieuQuanLy();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedMAKH = txt_MaKH.Text;

                if (string.IsNullOrWhiteSpace(selectedMAKH))
                {
                    MessageBox.Show("Vui lòng nhập Mã khách hàng (MAKH) trước khi cập nhật thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DataRow existingRow = ds_QLBV.Tables["KHACHHANG"].Rows.Find(selectedMAKH);

                if (existingRow == null)
                {
                    MessageBox.Show("Thông tin KHACHHANG không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                txt_MaKH.Enabled = false;


                existingRow["TENKHACHHANG"] = txt_TenKH.Text;
                existingRow["DIACHI"] = cbb_DiaChi.Text;
                existingRow["SDT"] = txt_sdt.Text;
                existingRow["EMAIL"] = txt_Email.Text;
                existingRow["MATKHAU_KH"] = txt_pass.Text;


                if (!string.IsNullOrWhiteSpace(txt_NamSinh.Text))
                {
                    if (int.TryParse(txt_NamSinh.Text, out int namsinh))
                    {
                        existingRow["NAMSINH"] = namsinh;
                    }
                    else
                    {
                        MessageBox.Show("Năm sinh không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                existingRow["GIOITINH"] = cbb_GioiTinh.Text;
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_ct);
                da_ct.Update(ds_QLBV, "KHACHHANG");
                ds_QLBV.AcceptChanges();

                MessageBox.Show("Thông tin KHACHHANG đã được cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        private void bnt_Xoa_Click(object sender, EventArgs e)
        {

            try
            {
                string selectedMAKH = txt_MaKH.Text;

                if (string.IsNullOrWhiteSpace(selectedMAKH))
                {
                    MessageBox.Show("Vui lòng chọn Mã khách hàng (MAKH) trước khi xóa thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin của khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (cnn.State != ConnectionState.Open)
                    {
                        cnn.Open();
                    }

                    // Kiểm tra và xóa dữ liệu liên quan từ bảng VEXE
                    using (SqlCommand cmdXoaVEXE = new SqlCommand($"DELETE FROM VEXE WHERE MAKHACHHANG = '{selectedMAKH}'", cnn))
                    {
                        cmdXoaVEXE.ExecuteNonQuery();
                    }

                    // Xóa dữ liệu từ bảng KHACHHANG
                    DataRow existingRow = ds_QLBV.Tables["KHACHHANG"].Rows.Find(selectedMAKH);

                    if (existingRow == null)
                    {
                        MessageBox.Show("Thông tin KHACHHANG không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    existingRow.Delete();

                    SqlCommandBuilder cmb = new SqlCommandBuilder(da_ct);
                    da_ct.Update(ds_QLBV, "KHACHHANG");
                    ds_QLBV.AcceptChanges();

                    MessageBox.Show("Thông tin KHACHHANG đã được xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }
    }
}
