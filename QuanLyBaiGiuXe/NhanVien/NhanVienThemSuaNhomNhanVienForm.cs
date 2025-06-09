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
    public partial class NhanVienThemSuaNhomNhanVienForm: Form
    {
        Manager manager = new Manager();
        string maNhomNhanVien = string.Empty;
        string option = "";
        string SuaNhom = "Sửa nhóm";
        public bool ThemSuaThanhCong = false;
        string TenNhomHienTai = null;
        string ThongTinKhacHienTai = null;

        public NhanVienThemSuaNhomNhanVienForm(string option, string maNhomNhanVien=null)
        {
            InitializeComponent();
            this.option = option;
            this.maNhomNhanVien = maNhomNhanVien;
            LoadData();
        }

        void LoadData()
        {
            if (option == SuaNhom)
            {
                btnDongYTiepTuc.Enabled = false;
                DataTable dt = manager.GetNhomNhanVienByID(maNhomNhanVien);
                
                if (dt.Rows.Count > 0)
                {
                    TenNhomHienTai = dt.Rows[0]["TenNhomNhanVien"].ToString();
                    ThongTinKhacHienTai = dt.Rows[0]["ThongTinKhac"].ToString();
                }
                tbTen.Text = TenNhomHienTai;
                tbThongTinKhac.Text = ThongTinKhacHienTai;
            }
        }

        bool checkNull()
        {
            if (!string.IsNullOrEmpty(tbTen.Text))
            {
                return true;
            }
            return false;
        }

        private void Clear()
        {
            tbTen.Clear();
            tbThongTinKhac.Clear();
        }

        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            if (!checkNull())
            {
                MessageBox.Show("Vui lòng nhập tên nhóm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTen.Focus();
                return;
            }
            string TenNhom = tbTen.Text.Trim();
            string ThongTinKhac = tbThongTinKhac.Text.Trim();
            if (option == SuaNhom)
            {
                if (CapNhatNhomNhanVien(TenNhom, ThongTinKhac))
                {
                    this.Close();
                }
            } else
            {
                if (ThemNhomNhanVien(TenNhom, ThongTinKhac))
                {
                    this.Close();
                }
            }
        }

        #region Function Chính
        bool ThemNhomNhanVien(string TenNhom, string ThongTinKhac)
        {
            var result = MessageBox.Show($"Bạn có chắc chắn muốn thêm nhóm nhân viên {TenNhom} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isAdded = manager.ThemNhomNhanVien(TenNhom, ThongTinKhac);

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

        bool CapNhatNhomNhanVien(string TenNhom, string ThongTinKhac)
        {
            var result = MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin của nhóm {TenNhom} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isUpdated = manager.CapNhatNhomNhanVien(maNhomNhanVien, TenNhom, ThongTinKhac);

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

        #endregion

        private void btnDongYTiepTuc_Click(object sender, EventArgs e)
        {
            if (!checkNull()) return;
            if (option == SuaNhom) return;
            string TenNhom = tbTen.Text.Trim();
            string ThongTinKhac = tbThongTinKhac.Text.Trim();
            if (ThemNhomNhanVien(TenNhom, ThongTinKhac))
            {
                Clear();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NhanVienThemSuaNhomNhanVienForm_Load(object sender, EventArgs e)
        {

        }
    }
}
