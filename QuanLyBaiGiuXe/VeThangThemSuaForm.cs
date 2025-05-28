using BitMiracle.LibTiff.Classic;
using QuanLyBaiGiuXe.DataAccess;
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
            ThemVeThang();
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

        private bool ThemVeThang()
        {
            string maThe = tbMaThe.Text;
            if (!manager.KiemTraThe(maThe))
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
            string loaiXe = cbLoaiXe.SelectedItem?.ToString();
            string tenNhom = cbNhom.SelectedItem?.ToString();
            decimal giaVe = updGiaVe.Value;
            string ghiChu = rtbGhiChu.Text;
            bool result = manager.ThemVeThang(tenNhom, maThe, chuXe, dienThoai, diaChi, email, ngayKichHoat,
                ngayHetHan, bienSo, nhanHieu, loaiXe, giaVe, ghiChu);
            return result;
        }

        private bool SuaVeThang()
        {
            string tenNhom = cbNhom.SelectedItem?.ToString();
            string maThe = tbMaThe.Text;
            string chuXe = tbChuXe.Text;
            string dienThoai = tbDienThoai.Text;
            string diaChi = tbDiaChi.Text;
            string email = tbEmail.Text;
            DateTime ngayKichHoat = dataPickerNgayKichHoat.Value;
            DateTime ngayHetHan = datePickerNgayHetHan.Value;
            string bienSo = tbBienSo.Text;
            string nhanHieu = tbNhanHieu.Text;
            string loaiXe = cbLoaiXe.SelectedItem?.ToString();
            decimal giaVe = updGiaVe.Value;
            string ghiChu = rtbGhiChu.Text;

            bool result = manager.SuaVeThang( this.MaVeThang, tenNhom, maThe, chuXe, dienThoai, diaChi, email, ngayKichHoat,
                ngayHetHan, bienSo, nhanHieu, loaiXe, giaVe, ghiChu);
            return result;
        }

        private void LoadUI()
        {
            List<string> groups = manager.GetDanhSachNhom();
            cbNhom.DataSource = groups;
            List<string> groupsXe = manager.GetDanhSachXe();
            cbLoaiXe.DataSource = groupsXe;
            cbNhom.SelectedIndex = 0;
            cbLoaiXe.SelectedIndex = 0;
            updGiaVe.Maximum = 10000000;
            updGiaVe.Minimum = 0;
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

                cbLoaiXe.SelectedItem = row["LoaiXe"].ToString();
                cbNhom.SelectedItem = row["TenNhom"].ToString();

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
            if (string.IsNullOrWhiteSpace(tbDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa Chỉ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrWhiteSpace(tbNhanHieu.Text))
            {
                MessageBox.Show("Vui lòng nhập Nhãn Hiệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            datePickerNgayHetHan.Value = DateTime.Now;
            tbBienSo.Clear();
            tbNhanHieu.Clear();
            cbLoaiXe.SelectedIndex = -1;
            cbNhom.SelectedIndex = -1;
            updGiaVe.Value = updGiaVe.Minimum; // Đặt giá trị nhỏ nhất
            rtbGhiChu.Clear();
        }
    }
}
