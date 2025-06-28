using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhanVienMainForm : Form
    {
        Manager manager = new Manager();

        public NhanVienMainForm()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                var dtNhanVien = manager.GetAllNhanVien();
                dtgNhanVien.DataSource = dtNhanVien;
                if (dtNhanVien != null && dtNhanVien.Columns.Contains("MaNhanVien"))
                {
                    dtgNhanVien.Columns["MaNhanVien"].Visible = false;
                }

                var dtNhomNhanVien = manager.GetAllNhomNhanVien();
                dtgNhomNhanVien.DataSource = dtNhomNhanVien;
                if (dtNhomNhanVien != null && dtNhomNhanVien.Columns.Contains("MaNhomNhanVien"))
                {
                    dtgNhomNhanVien.Columns["MaNhomNhanVien"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được nội dung trong table: " + ex.Message);
            }
        }

        private void NhanVienMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        #region Nhóm Nhân Viên

        private void btnThemNhom_Click(object sender, EventArgs e)
        {
            NhanVienThemSuaNhomNhanVienForm nhanVienThemSuaNhomNhanVienForm = new NhanVienThemSuaNhomNhanVienForm("Thêm nhóm");
            nhanVienThemSuaNhomNhanVienForm.ShowDialog();
            if (nhanVienThemSuaNhomNhanVienForm.ThemSuaThanhCong)
            {
                LoadData();
            }
        }

        private void btnSuaNhom_Click(object sender, EventArgs e)
        {
            string maNhom = null;

            if (dtgNhomNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgNhomNhanVien.SelectedRows[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    maNhom = selectedRow.Cells[0].Value.ToString();
                }
            }
            NhanVienThemSuaNhomNhanVienForm nhanVienThemSuaNhomNhanVienForm = new NhanVienThemSuaNhomNhanVienForm("Sửa nhóm", maNhom);
            nhanVienThemSuaNhomNhanVienForm.ShowDialog();
            if (nhanVienThemSuaNhomNhanVienForm.ThemSuaThanhCong) LoadData();
        }

        private void btnXoaNhom_Click(object sender, EventArgs e)
        {
            if (dtgNhomNhanVien.SelectedRows.Count > 0)
            {
                int r = dtgNhomNhanVien.CurrentCell.RowIndex;
                string TenNhomNhanVien = dtgNhomNhanVien.Rows[r].Cells["MaNhomNhanVien"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá nhóm {TenNhomNhanVien} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaNhomNhanVien(TenNhomNhanVien);

                    if (isDeleted)
                    {
                        ToastService.Show("Xoá nhóm thành công!", this);
                        LoadData();
                    }
                    else
                    {
                        ToastService.Show("Xoá nhóm thất bại!", this);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Nhân Viên
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            NhanVienThemSuaNhanVienForm nhanVienThemSuaForm = new NhanVienThemSuaNhanVienForm("Thêm");
            nhanVienThemSuaForm.ShowDialog();
            if (nhanVienThemSuaForm.ThemSuaThanhCong)
            {
                LoadData();
            }
        }

        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            if (dtgNhanVien.SelectedRows.Count > 0)
            {
                int r = dtgNhanVien.CurrentCell.RowIndex;
                string maNhanVien = dtgNhanVien.Rows[r].Cells[0].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá nhân viên này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaNhanVien(maNhanVien);

                    if (isDeleted)
                    {
                        ToastService.Show("Xoá nhân viên thành công!", this);
                        LoadData();
                    }
                    else
                    {
                        ToastService.Show("Xoá nhân viên thất bại!", this);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSuaNhanVien_Click(object sender, EventArgs e)
        {
            string option = "Sửa";
            if (dtgNhanVien.SelectedRows.Count > 0)
            {
                int rowIndex = dtgNhanVien.CurrentCell.RowIndex;
                var maNhanVien = dtgNhanVien.Rows[rowIndex].Cells[0].Value.ToString();

                NhanVienThemSuaNhanVienForm formThemSua = new NhanVienThemSuaNhanVienForm(option, maNhanVien);
                formThemSua.ShowDialog();
                if (formThemSua.ThemSuaThanhCong) LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string content = tbContent.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                dtgNhanVien.DataSource = manager.GetAllNhanVien(content);
            }
            else
            {
                LoadData();
            }
        }

        private void dtgNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgNhanVien.CurrentRow != null)
            {
                var row = dtgNhanVien.CurrentRow;
                string trangThai = Convert.ToString(row.Cells["Trạng Thái"].Value);

                if (trangThai == "Sử dụng")
                {
                    btnKhoiPhuc.Text = "Khoá";
                    btnKhoiPhuc.BackColor = Color.LightCoral;
                }
                else if (trangThai == "Khoá")
                {
                    btnKhoiPhuc.Text = "Khôi phục";
                    btnKhoiPhuc.BackColor = Color.LightGreen;
                }
            }
        }
        #endregion

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            string TrangThai = btnKhoiPhuc.Text == "Khoá" ? "Khoá" : "Sử dụng";
            if (dtgNhanVien.SelectedRows.Count > 0)
            {
                int rowIndex = dtgNhanVien.CurrentCell.RowIndex;
                var maNhanVien = dtgNhanVien.Rows[rowIndex].Cells[0].Value.ToString();
                bool isUpdated = false;
                if (TrangThai == "Khoá")
                {
                    isUpdated = manager.CapNhatTrangThaiNhanVien(maNhanVien, "Khoá");
                }
                else if (TrangThai == "Sử dụng")
                {
                    isUpdated = manager.CapNhatTrangThaiNhanVien(maNhanVien, "Sử dụng");
                }
                else
                {
                    ToastService.Show("Vui lòng chọn một nhân viên để cập nhật!", this);
                }
                if (isUpdated)
                {
                    ToastService.Show($"Cập nhật trạng thái nhân viên thành công!", this);
                    LoadData();
                }
                else
                {
                    ToastService.Show($"Cập nhật trạng thái nhân viên thất bại!", this);
                }
            }
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {

        }
    }
}
