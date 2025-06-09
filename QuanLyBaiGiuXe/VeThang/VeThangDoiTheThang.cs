using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangDoiTheThang: Form
    {
        string MaVeThang = null;
        public bool DoiTheThanhCong = false;
        Manager manager = new Manager();

        public VeThangDoiTheThang(string MaVeThang)
        {
            InitializeComponent();
            this.MaVeThang = MaVeThang;
        }

        private bool DoiThe(string BienSo)
        {
            return manager.DoiTheThang(MaVeThang, BienSo);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            string BienSo = tbMaThe.Text;
            if (string.IsNullOrEmpty(BienSo)) return;
            if (DoiThe(BienSo))
            {
                MessageBox.Show("Đổi thẻ thành công.");
                DoiTheThanhCong = true;
                this.Close();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
