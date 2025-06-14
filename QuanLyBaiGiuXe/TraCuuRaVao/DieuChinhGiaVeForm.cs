using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class DieuChinhGiaVeForm: Form
    {
        Manager manager = new Manager();
        public bool isThanhCong = false;
        string maveluot = null;
        public DieuChinhGiaVeForm(string maveluot, int sotien = 0)
        {
            InitializeComponent();
            this.maveluot = maveluot;
            nupCu.Value = sotien;
        }

        private void DieuChinhGiaVeForm_Load(object sender, EventArgs e)
        {

        }

        private void DongY_Click(object sender, EventArgs e)
        {
            int sotien = nupMoi.Value > 0 ? (int)nupMoi.Value : 0;
            bool result = manager.SetGiaVeLuotByID(this.maveluot, sotien);
            if (result)
            {
                new ToastForm("Đã cập nhật giá vé thành công!", this);
                isThanhCong = true;
                this.Close();
            } else
            {
                new ToastForm("Cập nhật giá vé thất bại!", this);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
