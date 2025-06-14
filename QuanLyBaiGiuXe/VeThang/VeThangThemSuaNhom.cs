using QuanLyBaiGiuXe.Models;
using System;
using System.Data;
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
        Manager manager = new Manager();

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
                lbTitle.Text = "Chỉnh sửa nhóm vé tháng";
                this.Text = "Chỉnh sửa nhóm vé tháng";
                btnDongYTiepTuc.Enabled = false;
                DataTable dt = manager.GetNhomVeThangByID(MaNhom);

                if (dt.Rows.Count > 0)
                {
                    TenNhomHienTai = dt.Rows[0]["TenNhom"].ToString();
                    ThongTinKhacHienTai = dt.Rows[0]["ThongTinKhac"].ToString();
                }
                tbTen.Text = TenNhomHienTai;
                tbThongTinKhac.Text = ThongTinKhacHienTai;
            }
        }


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

        #region Hàm chính Nhóm vé tháng
        private bool CapNhatNhom(string TenNhomMoi, string ThongTinKhacMoi)
        {
            var result = MessageBox.Show($"Bạn có chắc chắn muốn sửa nhóm {TenNhomHienTai} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isUpdated = manager.CapNhatNhomVeThang(MaNhom, TenNhomMoi, ThongTinKhacMoi);
                if (isUpdated)
                {
                    new ToastForm("Cập nhật nhóm thành công!", this).ShowDialog();
                    ThemSuaThanhCong = true;
                    return true;
                }
                else
                {
                    new ToastForm("Cập nhật nhóm thất bại!", this).ShowDialog();
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
                bool isAdded = manager.ThemNhomVeThang(TenNhom,ThongTinKhac);

                if (isAdded)
                {
                    new ToastForm("Thêm nhóm thành công!", this).ShowDialog();
                    ThemSuaThanhCong = true;
                    return true;
                }
                else
                {
                    new ToastForm("Thêm nhóm thất bại!", this).ShowDialog();
                    return false;
                }
            }
            return false;
        }
        #endregion

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
            Clear();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VeThangThemSuaNhom_Load(object sender, EventArgs e)
        {

        }
    }
}
