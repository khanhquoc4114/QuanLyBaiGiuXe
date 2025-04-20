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

namespace QuanLyBaiGiuXe
{
    public partial class VeThangThemSuaNhom: Form
    {
        string option = "Thêm nhóm";
        public bool ThemSuaThanhCong = false;
        string MaNhom = null;
        string TenNhomHienTai = null;
        string ThongTinKhacHienTai = null;
        public VeThangThemSuaNhom(string option, string MaNhom=null)
        {
            InitializeComponent();
            this.option = option;
            this.MaNhom = MaNhom;
            LoadData();
        }

        void LoadData()
        {
            if (option == "Sửa nhóm" )
            {
                btnDongYTiepTuc.Enabled = false;
                DataTable dt = manager.GetNhomByID(MaNhom);

                if (dt.Rows.Count > 0)
                {
                    TenNhomHienTai = dt.Rows[0]["TenNhom"].ToString();
                    ThongTinKhacHienTai = dt.Rows[0]["ThongTinKhac"].ToString();
                }
                tbTen.Text = TenNhomHienTai;
                tbThongTinKhac.Text = ThongTinKhacHienTai;
            }
        }

        Manager manager = new Manager();

        private bool checkNull()
        {
            if (!string.IsNullOrEmpty(tbTen.Text)) { 
                return true; 
            }
            return false;
        }
        private void Clear()
        {
            tbTen.Clear();
            tbThongTinKhac.Clear();   
        }

        private bool CapNhatNhom(string TenNhomMoi, string ThongTinKhacMoi)
        {
            var result = MessageBox.Show($"Bạn có chắc chắn muốn sửa nhóm {TenNhomHienTai} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isUpdated = manager.CapNhatNhom(MaNhom, TenNhomMoi, ThongTinKhacMoi);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        private bool ThemNhom(string TenNhom, string ThongTinKhac)
        {
            var result = MessageBox.Show($"Bạn có chắc chắn muốn thêm nhóm {TenNhom} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isAdded = manager.ThemNhom(TenNhom,ThongTinKhac);

                if (isAdded)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            if (!checkNull()) return;
            string TenNhom = tbTen.Text.Trim();
            string ThongTinKhac = tbThongTinKhac.Text.Trim();
            if (option == "Thêm nhóm")
            {
                if (ThemNhom(TenNhom, ThongTinKhac))
                {
                    Clear();
                    this.Close();
                }
            } else
            {
                if (CapNhatNhom(TenNhom, ThongTinKhac))
                {
                    Clear();
                    this.Close();
                }
            }
        }

        private void btnDongYTiepTuc_Click(object sender, EventArgs e)
        {
            if (!checkNull()) return;
            if (option =="Sửa nhóm") return;
            string TenNhom = tbTen.Text.Trim();
            string ThongTinKhac = tbThongTinKhac.Text.Trim();
            if (ThemNhom(TenNhom, ThongTinKhac))
            {
                Clear();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
