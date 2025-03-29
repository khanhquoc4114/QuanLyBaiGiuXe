using QuanLyBaiGiuXe.Models;
using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangDoiTheThang: Form
    {
        int MaVeThang = -1;
        public bool DoiTheThanhCong = false;
        Manager manager = new Manager();

        public VeThangDoiTheThang(string BienSo)
        {
            InitializeComponent();
            this.MaVeThang = manager.GetMaVeThangByBienSo(BienSo);
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
