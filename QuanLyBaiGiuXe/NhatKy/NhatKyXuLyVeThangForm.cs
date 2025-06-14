using OfficeOpenXml;
using QuanLyBaiGiuXe.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhatKyXuLyVeThangForm: Form
    {
        Manager manager = new Manager();
        public NhatKyXuLyVeThangForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dtgXuLyVeThang.DataSource = manager.GetAllXuLyVeThang();

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime tgTu = dtpTu.Value;
            DateTime tgDen = dtpDen.Value;

            this.dtgXuLyVeThang.DataSource = manager.GetAllXuLyVeThang(tgTu, tgDen);
        }

        private void NhatKyXuLyVeThangForm_Load(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
