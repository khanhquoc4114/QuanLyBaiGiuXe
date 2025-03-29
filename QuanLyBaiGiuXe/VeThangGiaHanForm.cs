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
    public partial class VeThangGiaHanForm: Form
    {
        int MaVeThang = -1;
        Manager manager = new Manager();
        DateTime NgayHetHanCu = DateTime.Now;
        public bool GiaHanThanhCong = false;

        public VeThangGiaHanForm(string BienSo)
        {
            InitializeComponent();
            this.MaVeThang = manager.GetMaVeThangByBienSo(BienSo);
            LoadData();
        }

        private void VeThangGiaHanForm_Load(object sender, EventArgs e)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
        }

        private void LoadData()
        {
            DataTable dt = manager.GetVeThangByMaVeThang(MaVeThang);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                NgayHetHanCu = dtp.Value = Convert.ToDateTime(row["NgayHetHan"]);
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu.");
            }
        }

        private bool GiaHan(DateTime NgayHetHanMoi)
        {
            DateTime ngayHetHan = dtp.Value;
            bool result = manager.GiaHanVeThang(MaVeThang, NgayHetHanMoi);
            return result;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            DateTime ngayHetHanMoi = dtp.Value;
            if (ngayHetHanMoi < DateTime.Now)
            {
                MessageBox.Show("Ngày hết hạn không hợp lệ.");
                return;
            }
            if (GiaHan(ngayHetHanMoi))
            {
                MessageBox.Show("Gia hạn thành công.");
                GiaHanThanhCong = true;
                this.Close();
            }
        }

        private void btnGiaHan1Thang_Click(object sender, EventArgs e)
        {
            DateTime NgayHetHanMoi = NgayHetHanCu.AddMonths(1);
            dtp.Value = NgayHetHanMoi;
            if (NgayHetHanMoi < DateTime.Now)
            {
                MessageBox.Show("Ngày hết hạn không hợp lệ.");
                return;
            }
            if (GiaHan(NgayHetHanMoi))
            {
                MessageBox.Show("Gia hạn thêm 1 tháng thành công.");
                GiaHanThanhCong = true;
                this.Close();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
