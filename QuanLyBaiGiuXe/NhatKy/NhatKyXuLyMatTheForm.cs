using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhatKyXuLyMatTheForm: Form
    {
        Manager manager = new Manager();
        public NhatKyXuLyMatTheForm()
        {
            InitializeComponent();
        }

        private void NhatKyXuLyMatTheForm_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dtgNhatKy.DataSource = manager.GetAllXuLyVeThang();

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
        }
    }
}
