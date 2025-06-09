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
    public partial class ThemXeForm: Form
    {
        Manager manager = new Manager();
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
            if (manager.ThemLoaiXe(tenloaixe))
            {
                new ToastForm("Thêm loại xe thành công", this).Show();
                this.Close();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
