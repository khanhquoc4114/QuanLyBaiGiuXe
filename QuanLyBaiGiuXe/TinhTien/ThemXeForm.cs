using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class ThemXeForm: Form
    {
        Manager manager = new Manager();
        public bool isSuccess = false; 
        public ThemXeForm()
        {
            InitializeComponent();
        }

        private void ThemXeForm_Load(object sender, EventArgs e)
        {

        }

        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            string tenloaixe = tbTenLoaiXe.Text.Trim();
            if (string.IsNullOrEmpty(tenloaixe))
            {
                new ToastForm("Vui lòng nhập tên loại xe", this).Show();
            }
            if (manager.ThemLoaiXe(tenloaixe, out string maLoaiXe))
            {
                new ToastForm("Thêm loại xe thành công", this).Show();
                isSuccess = true;
                manager.UpsertTinhTienLuyTien(
                    maLoaiXe,
                    0, 0, 0, 0, 0, 0, 0
                );

                manager.UpsertTinhTienCongVan(
                    maLoaiXe,
                    false, 0, 0, 0, 0, 0, 0, 0, 0, 0
                );
                manager.UpsertGiaVeThang(
                    maLoaiXe,
                    "0", "0"
                );
                this.Close();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
