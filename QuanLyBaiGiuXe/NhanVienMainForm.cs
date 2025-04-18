using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhanVienMainForm: Form
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
                this.dtgNhanVien.DataSource = manager.GetAllNhanVien();
                this.dtgNhomNhanVien.DataSource = manager.GetAllNhomNhanVien();
                dtgNhomNhanVien.Columns["MaNhomNhanVien"].Visible = false;
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
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
            int rowIndex = dtgNhomNhanVien.CurrentCell.RowIndex;
            var maNhom = dtgNhomNhanVien.Rows[rowIndex].Cells["MaNhomNhanVien"].Value.ToString();
            NhanVienThemSuaNhomNhanVienForm nhanVienThemSuaNhomNhanVienForm = new NhanVienThemSuaNhomNhanVienForm("Sửa nhóm", maNhom);
            nhanVienThemSuaNhomNhanVienForm.ShowDialog();
            if (nhanVienThemSuaNhomNhanVienForm.ThemSuaThanhCong)
            {
                LoadData();
            }
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
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

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
                string maNhanVien = dtgNhanVien.Rows[r].Cells["MaNhanVien"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá nhân viên này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaNhanVien(maNhanVien);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var maNhanVien = dtgNhanVien.Rows[rowIndex].Cells["MaNhanVien"].Value.ToString();

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
                dtgNhanVien.DataSource = manager.TimKiemNhanVien(content);
            }
            else
            {
                LoadData();
            }
        }
    }
}
