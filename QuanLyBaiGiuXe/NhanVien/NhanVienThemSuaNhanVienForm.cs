using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhanVienThemSuaNhanVienForm: Form
    {
        Manager manager = new Manager();
        string option = "";
        string Sua = "Sửa";
        string Them = "Thêm";
        string MaNhanVien = "";
        public bool ThemSuaThanhCong = false;

        public NhanVienThemSuaNhanVienForm(string option, string MaNhanVien = null)
        {
            InitializeComponent();
            this.option = option;
            this.MaNhanVien = MaNhanVien;
            LoadUI();
            LoadData();
        }

        private void LoadUI()
        {
            List<string> groups = manager.GetDanhSachNhomNhanVien();
            cbNhom.DataSource = groups;
        }

        private void LoadData()
        {
            if (option == Sua)
            {
                btnDongYTiepTuc.Enabled = false;
                DataTable dt = manager.GetNhanVienByID(MaNhanVien);

                if (dt.Rows.Count > 0)
                {
                    cbNhom.SelectedItem = dt.Rows[0]["TenNhomNhanVien"].ToString();
                    tbHoTen.Text = dt.Rows[0]["HoTen"].ToString();
                    tbMaThe.Text = dt.Rows[0]["MaThe"].ToString();
                    tbTenDangNhap.Text = dt.Rows[0]["TenDangNhap"].ToString();
                    tbMatKhau.Text = dt.Rows[0]["MatKhau"].ToString();
                    tbNhapLai.Text = dt.Rows[0]["MatKhau"].ToString();
                    rtbGhiChu.Text = dt.Rows[0]["GhiChu"].ToString();
                }
            }
        }

        private bool KiemTraThongTinNhap()
        {
            if (cbNhom.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhóm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNhom.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ Tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbHoTen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Đăng Nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTenDangNhap.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật Khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMatKhau.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbNhapLai.Text) || tbNhapLai.Text != tbMatKhau.Text)
            {
                MessageBox.Show("Vui lòng nhập lại mật Khẩu đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMatKhau.Focus();
                return false;
            }
            return true;
        }

        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTinNhap()) return;
            bool result = false;
            if (option == Them)
            {
                result = ThemNhanVien();
                if (result)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    this.Close();
                }
            }
            else if (option == Sua)
            {
                result = CapNhatNhanVien();
                if (result)
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    this.Close();
                }
            }
        }

        private void btnDongYTiepTuc_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTinNhap()) return;
            if (option == "Sửa") return;
            ThemNhanVien();
            Clear();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Clear()
        {
            cbNhom.SelectedIndex = -1;
            tbHoTen.Clear();
            tbMaThe.Clear();
            tbTenDangNhap.Clear();
            tbMatKhau.Clear();
            tbNhapLai.Clear();
            rtbGhiChu.Clear();
        }

        #region Main Function
        bool ThemNhanVien()
        {
            string tenNhom = cbNhom.SelectedItem.ToString().Trim();
            string hoTen = tbHoTen.Text.Trim();
            string maThe = tbMaThe.Text.Trim();
            string tenDangNhap = tbTenDangNhap.Text.Trim();
            string matKhau = tbMatKhau.Text.Trim();
            string ghiChu = rtbGhiChu.Text.Trim();
            return manager.ThemNhanVien(tenNhom, hoTen, maThe, tenDangNhap, matKhau, ghiChu);
        }
        bool CapNhatNhanVien()
        {
            string tenNhom = cbNhom.SelectedItem.ToString();
            string hoTen = tbHoTen.Text.Trim();
            string maThe = tbMaThe.Text.Trim();
            string tenDangNhap = tbTenDangNhap.Text.Trim();
            string matKhau = tbMatKhau.Text.Trim();
            string ghiChu = rtbGhiChu.Text.Trim();
            return manager.CapNhatNhanVien(MaNhanVien, tenNhom, hoTen, maThe, tenDangNhap, matKhau, ghiChu);
        }
        #endregion

        private void NhanVienThemSuaNhanVienForm_Load(object sender, EventArgs e)
        {

        }
    }
}
