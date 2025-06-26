using QuanLyBaiGiuXe.Helper;
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
            List<ComboBoxItem> groups = manager.GetDanhSachNhomNhanVien();
            LoadComboBox(cbNhom, groups, includeTatCa: false);
        }

        private void LoadComboBox(ComboBox comboBox, List<ComboBoxItem> data, string suffix = null, bool includeTatCa = true)
        {
            if (includeTatCa)
            {
                data.Insert(0, new ComboBoxItem { Value = -1, Text = "Tất cả" + " " + suffix });
            }

            comboBox.DataSource = null;
            comboBox.DataSource = data;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = 0;
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
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNhapLai.Focus();
                return false;
            }
            return true;
        }

        #region Button Logic
        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTinNhap()) return;
            bool result = false;
            if (option == Them)
            {
                result = ThemNhanVien();
                if (result)
                {
                    ThemSuaThanhCong = true;
                    this.Close();
                }
            }
            else if (option == Sua)
            {
                result = CapNhatNhanVien();
                if (result)
                {
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
        #endregion


        #region Main Function
        bool ThemNhanVien()
        {
            string tenNhom = cbNhom.SelectedItem.ToString().Trim();
            string hoTen = tbHoTen.Text.Trim();
            string maThe = tbMaThe.Text.Trim();
            string tenDangNhap = tbTenDangNhap.Text.Trim();
            string matKhau = tbNhapLai.Text.Trim();
            if (!string.IsNullOrWhiteSpace(matKhau))
            {
                matKhau = HashHelper.ComputeSHA256Hash(matKhau);
            }
            string ghiChu = rtbGhiChu.Text.Trim();
            bool isAdded = manager.ThemNhanVien(tenNhom, hoTen, maThe, tenDangNhap, matKhau, ghiChu, out bool isTonTaiThe, out bool isTonTaiTheChoNhanVien);
            if (isAdded)
            {
                ToastService.Show("Thêm nhân viên thành công!", this);
                return true;
            }
            else
            {
                if (!isTonTaiThe)
                {
                    ToastService.Show("Thẻ không tồn tại!", this);
                    return false;
                }
                else if (isTonTaiTheChoNhanVien)
                {
                    ToastService.Show("Thẻ đã được sử dụng cho nhân viên khác!", this);
                    return false;
                }
                else
                {
                    ToastService.Show("Thêm nhân viên thất bại", this);
                    return false;
                }
            }
        }

        bool CapNhatNhanVien()
        {
            string tenNhom = cbNhom.SelectedItem.ToString();
            string hoTen = tbHoTen.Text.Trim();
            string maThe = tbMaThe.Text.Trim();
            string tenDangNhap = tbTenDangNhap.Text.Trim();
            string matKhau = tbNhapLai.Text.Trim();
            if (!string.IsNullOrWhiteSpace(matKhau))
            {
                matKhau = HashHelper.ComputeSHA256Hash(matKhau);
            }
            string ghiChu = rtbGhiChu.Text.Trim();
            bool isUpdated = manager.CapNhatNhanVien(MaNhanVien, tenNhom, hoTen, maThe, tenDangNhap, matKhau, ghiChu, out bool isTonTaiThe, out bool isTonTaiTheChoNhanVien);
            if (isUpdated)
            {
                ToastService.Show("Cập nhật nhân viên thành công!", this);
                return true;
            }
            else
            {
                if (!isTonTaiThe)
                {
                    ToastService.Show("Thẻ không tồn tại!", this);
                    return false;
                }
                else if (isTonTaiTheChoNhanVien)
                {
                    ToastService.Show("Thẻ đã được sử dụng cho nhân viên khác!", this);
                    return false;
                }
                else
                {
                    ToastService.Show("Cập nhật nhân viên thất bại", this);
                    return false;
                }
            }
        }
        #endregion
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

        private void NhanVienThemSuaNhanVienForm_Load(object sender, EventArgs e)
        {

        }
    }
}
