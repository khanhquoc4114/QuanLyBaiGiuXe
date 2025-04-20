using OfficeOpenXml.LoadFunctions.Params;
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
    public partial class NhatKyXuLyTheThangForm: Form
    {
        Manager Manager = new Manager();
        public NhatKyXuLyTheThangForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dtgXuLyVeThang.DataSource = Manager.GetAllXuLyVeThang();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
    }
}
