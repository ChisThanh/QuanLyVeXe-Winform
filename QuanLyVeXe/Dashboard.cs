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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyVeXe
{
    public partial class Dashboard : Form
    {
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            var total = db.Database.SqlQuery<double>("SELECT dbo.DoanhThu()").FirstOrDefault();
            var emp = db.Database.SqlQuery<double>("SELECT dbo.NhannVien()").FirstOrDefault();
            var cus = db.Database.SqlQuery<double>("SELECT dbo.SLKhachHang()").FirstOrDefault();
            var ticket = db.Database.SqlQuery<double>("SELECT dbo.SLVE()").FirstOrDefault();
            label1.Text = total.ToString() + " VNĐ";
            label2.Text = emp.ToString();
            label3.Text = cus.ToString();
            label4.Text = ticket.ToString();
            chart1.Series["Series1"].Points.Clear();
            var l = db.DoanhThuTheoThang();
            chart1.Series["Series1"].IsValueShownAsLabel = true;

            foreach (var item in l)
            {
                chart1.Series["Series1"].Points.AddXY("Tháng " + item.Month.ToString(), item.TotalRevenue);
            }    
        }
    }
}
