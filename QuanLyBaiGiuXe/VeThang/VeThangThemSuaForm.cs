using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangThemSuaForm: Form
    {
        Manager manager = new Manager();
        string option="Sửa";
        string MaVeThang = null;
        public bool ThemSuaThanhCong = false;

        public VeThangThemSuaForm(string option, string MaVeThang=null)
        {
            InitializeComponent();
            LoadUI();
            this.option = option;
            if (option == "Sửa")
            {
                this.Text = "Sửa Vé Tháng";
                gbMain.Text = "Thông tin Vé Tháng";
                this.MaVeThang = MaVeThang;
                LoadData();
                btnDongYTiepTuc.Enabled = false;
            }
        }

        #region UI
        private void btnDongYDong_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTinNhap()) return;
            bool result = false;
            if (option == "Thêm")
            {
                result = ThemVeThang();
                if (result)
                {
                    MessageBox.Show("Thêm vé tháng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    this.Close();
                }
            }
            else if (option == "Sửa")
            {
                result = SuaVeThang();
                if (result)
                {
                    MessageBox.Show("Cập nhật vé tháng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemSuaThanhCong = true;
                    this.Close();
                }
            }
        }

        private void btnDongYTiepTuc_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTinNhap()) return;
            if (option == "Sửa") return;
            bool result = ThemVeThang();
            if (result)
            {
                MessageBox.Show("Thêm vé tháng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ThemSuaThanhCong = true;
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
        #endregion

        private void VeThangThemSuaForm_Load(object sender, EventArgs e)
        {
        }
        #region Main Function

        #endregion
        private bool ThemVeThang()
        {
            string maThe = tbMaThe.Text;
            if (!manager.KiemTraTonTaiThe(maThe))
            {
                MessageBox.Show("Mã thẻ không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string chuXe = tbChuXe.Text;
            string dienThoai = tbDienThoai.Text;
            string diaChi = tbDiaChi.Text;
            string email = tbEmail.Text;
            DateTime ngayKichHoat = dataPickerNgayKichHoat.Value;
            DateTime ngayHetHan = datePickerNgayHetHan.Value;
            string bienSo = tbBienSo.Text;
            string nhanHieu = tbNhanHieu.Text;
            string maloaiXe= cbLoaiXe.SelectedValue?.ToString();
            string maNhom = cbNhom.SelectedValue?.ToString();
            decimal giaVe = updGiaVe.Value;
            string ghiChu = rtbGhiChu.Text;
            if (manager.KiemTraTheTrongVeThang(maThe)) return false;
            MessageBox.Show($"Xe được chọn là {maloaiXe}");
            bool result = manager.ThemVeThang(maNhom, maThe, chuXe, dienThoai, diaChi, email, ngayKichHoat,
                ngayHetHan, bienSo, nhanHieu, maloaiXe, giaVe, ghiChu);
            return result;
        }

        private bool SuaVeThang()
        {
            string maNhom = cbNhom.SelectedValue?.ToString();
            string maThe = tbMaThe.Text;
            string chuXe = tbChuXe.Text;
            string dienThoai = tbDienThoai.Text;
            string diaChi = tbDiaChi.Text;
            string email = tbEmail.Text;
            DateTime ngayKichHoat = dataPickerNgayKichHoat.Value;
            DateTime ngayHetHan = datePickerNgayHetHan.Value;
            string bienSo = tbBienSo.Text;
            string nhanHieu = tbNhanHieu.Text;
            string maLoaiXe = cbLoaiXe.SelectedValue.ToString();
            decimal giaVe = updGiaVe.Value;
            string ghiChu = rtbGhiChu.Text;
            bool result = manager.SuaVeThang( this.MaVeThang, maNhom, maThe, chuXe, dienThoai, diaChi, email, ngayKichHoat,
                ngayHetHan, bienSo, nhanHieu, maLoaiXe, giaVe, ghiChu);
            return result;
        }

        private void LoadUI()
        {
            var groups = manager.GetDanhSachNhom();
            if (groups != null && groups.Count > 0)
            {
                LoadComboBox(cbNhom, groups, "nhóm", false);
            }
            else
            {
                cbNhom.DataSource = null;
                cbNhom.Text = "-- Không có dữ liệu --";
            }

            var danhSachXe = manager.GetDanhSachXe();
            if (danhSachXe != null && danhSachXe.Count > 0)
            {
                LoadComboBox(cbLoaiXe, danhSachXe, "loại xe", false);
            }
            else
            {
                cbLoaiXe.DataSource = null;
                cbLoaiXe.Text = "-- Không có dữ liệu --";
            }
            updGiaVe.Maximum = 100000000;
            updGiaVe.Minimum = 0;
            dataPickerNgayKichHoat.Value = DateTime.Now;
            datePickerNgayHetHan.Value = DateTime.Now.AddMonths(1);
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
            DataTable dt = manager.GetVeThangByMaVeThang(MaVeThang);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                tbMaThe.Text = row["MaThe"].ToString();
                tbChuXe.Text = row["ChuXe"].ToString();
                tbDienThoai.Text = row["DienThoai"].ToString();
                tbDiaChi.Text = row["DiaChi"].ToString();
                tbEmail.Text = row["Email"].ToString();
                dataPickerNgayKichHoat.Value = Convert.ToDateTime(row["NgayKichHoat"]);
                datePickerNgayHetHan.Value = Convert.ToDateTime(row["NgayHetHan"]);
                tbBienSo.Text = row["BienSo"].ToString();
                tbNhanHieu.Text = row["NhanHieu"].ToString();
                cbLoaiXe.SelectedValue = Convert.ToInt32(row["MaLoaiXe"]);
                cbNhom.Text = row["TenNhom"].ToString();
                updGiaVe.Value = Convert.ToDecimal(row["GiaVe"]);
                rtbGhiChu.Text = row["GhiChu"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu.");
            }
        }

        private bool KiemTraThongTinNhap()
        {
            if (string.IsNullOrWhiteSpace(tbMaThe.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Thẻ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbChuXe.Text))
            {
                MessageBox.Show("Vui lòng nhập Chủ Xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập Số Điện Thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbLoaiXe.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Loại Xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbNhom.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Nhóm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbBienSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Biển Số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (updGiaVe.Value <= 0)
            {
                MessageBox.Show("Giá vé phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void Clear()
        {
            tbMaThe.Clear();
            tbChuXe.Clear();
            tbDienThoai.Clear();
            tbDiaChi.Clear();
            tbEmail.Clear();
            dataPickerNgayKichHoat.Value = DateTime.Now;
            datePickerNgayHetHan.Value = DateTime.Now.AddMonths(1);
            tbBienSo.Clear();
            tbNhanHieu.Clear();
            cbLoaiXe.SelectedIndex = -1;
            cbNhom.SelectedIndex = -1;
            updGiaVe.Value = updGiaVe.Minimum;
            rtbGhiChu.Clear();
        }

        private void cbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (option == "Thêm")
            {
                string loaiXe = cbLoaiXe.SelectedValue.ToString();
                updGiaVe.Value = manager.GetGiaTienTheoLoaiXe(loaiXe);
            }
        }
    }
}
