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
    public partial class AddManageTickets : Form
    {
        QL_BANVEXEKHACHEntities db = new QL_BANVEXEKHACHEntities();

        public AddManageTickets()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string ma = guna2TextBox1.Text;
            string sl = guna2TextBox2.Text;
            XE x = new XE() { 
                MAXE = ma,
                SOLUONGGHE = int.Parse(sl)
            };
            db.XEs.Add(x);
            db.SaveChanges();
            this.Close();
        }
    }
}
