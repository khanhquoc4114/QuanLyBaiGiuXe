using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangMainForm: Form
    {
        Manager manager = new Manager();
        public VeThangMainForm()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            try
            {
                this.dtgVeThang.DataSource = manager.GetAllVeThang();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
        }

        private void VeThangMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
