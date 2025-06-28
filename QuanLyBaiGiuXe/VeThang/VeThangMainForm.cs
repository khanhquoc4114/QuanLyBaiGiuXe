using QuanLyBaiGiuXe.Models;
using System;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace QuanLyBaiGiuXe
{
    public partial class VeThangMainForm: Form
    {
        Manager manager = new Manager();
        public VeThangMainForm()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            try
            {
                this.dtgVeThang.DataSource = manager.GetAllVeThang();
                dtgVeThang.Columns["MaVeThang"].Visible = false;
                dtgVeThang.ClearSelection();
                if (dtgVeThang.Rows.Count > 0)
                {
                    dtgVeThang.Rows[0].Selected = true;
                }

                this.dtgNhom.DataSource = manager.GetAllNhomVeThang();
                dtgNhom.Columns["MaNhom"].Visible = false;
                dtgNhom.ClearSelection();
                if (dtgNhom.Rows.Count > 0)
                {
                    dtgNhom.Rows[0].Selected = true;
                }
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
        }
        private void VeThangMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        #region Nhóm
        private void btnThemNhom_Click(object sender, EventArgs e)
        {
            VeThangThemSuaNhom veThangThemForm = new VeThangThemSuaNhom(btnThemNhom.Text);
            veThangThemForm.ShowDialog();
            if(veThangThemForm.ThemSuaThanhCong) LoadData();
        }
        private void btnXoaNhom_Click(object sender, EventArgs e)
        {
            if (dtgNhom.SelectedRows.Count > 0)
            {
                int r = dtgNhom.CurrentCell.RowIndex;
                string MaNhom = dtgNhom.Rows[r].Cells["MaNhom"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá nhóm chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaNhomVeThangByID(MaNhom);

                    if (isDeleted)
                    {
                        new ToastForm("Xóa nhóm thành công!", this).ShowDialog();
                        LoadData();
                    }
                    else
                    {
                        new ToastForm("Xóa nhóm thất bại!", this).ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSuaNhom_Click(object sender, EventArgs e)
        {
            int r = dtgNhom.CurrentCell.RowIndex;
            string MaNhom = dtgNhom.Rows[r].Cells["MaNhom"].Value.ToString();
            VeThangThemSuaNhom veThangThemForm = new VeThangThemSuaNhom(btnSuaNhom.Text,MaNhom);
            veThangThemForm.ShowDialog();
            if (veThangThemForm.ThemSuaThanhCong) LoadData();
        }
        #endregion

        #region Vé Tháng
        private void btnThemVeThang_Click(object sender, EventArgs e)
        {
            VeThangThemSuaForm veThangThemForm = new VeThangThemSuaForm("Thêm");
            veThangThemForm.ShowDialog();
            if (veThangThemForm.ThemSuaThanhCong) LoadData();
        }

        private void btnSuaVeThang_Click(object sender, EventArgs e)
        {
            string option = "Sửa";
            if (dtgVeThang.SelectedRows.Count > 0)
            {
                string MaVeThang = dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["MaVeThang"].Value.ToString();

                VeThangThemSuaForm frmChinhSua = new VeThangThemSuaForm(option, MaVeThang);
                frmChinhSua.ShowDialog();
                if (frmChinhSua.ThemSuaThanhCong) LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vé tháng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaVeThang_Click(object sender, EventArgs e)
        {
            if (dtgVeThang.SelectedRows.Count > 0)
            {
                string MaVeThang = dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["MaVeThang"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá vé này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaVeThang(MaVeThang);

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

        private void btnGiaHanVeThang_Click(object sender, EventArgs e)
        {
            if (dtgVeThang.SelectedRows.Count > 0)
            {
                string MaVeThang = dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["MaVeThang"].Value.ToString();
                VeThangGiaHanForm veThangGiaHanForm = new VeThangGiaHanForm(MaVeThang);
                veThangGiaHanForm.ShowDialog();
                if (veThangGiaHanForm.GiaHanThanhCong) LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để gia hạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                sfd.FileName = $"DuLieuVeThang_{DateTime.Now:ddMMyyyy}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Thiết lập LicenseContext trước khi tạo ExcelPackage
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                            // Header
                            for (int col = 0; col < dtgVeThang.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col + 1].Value = dtgVeThang.Columns[col].HeaderText;
                            }

                            // Dữ liệu
                            for (int row = 0; row < dtgVeThang.Rows.Count; row++)
                            {
                                for (int col = 0; col < dtgVeThang.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dtgVeThang.Rows[row].Cells[col].Value?.ToString();
                                }
                            }

                            // Lưu file
                            File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());
                            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDoiThe_Click(object sender, EventArgs e)
        {
            if (dtgVeThang.SelectedRows.Count > 0)
            {
                string MaVeThang = dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["MaVeThang"].Value.ToString();
                VeThangDoiTheThang veThangDoiTheThang = new VeThangDoiTheThang(MaVeThang);
                veThangDoiTheThang.ShowDialog();
                if (veThangDoiTheThang.DoiTheThanhCong) LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vé tháng để đổi thẻ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string content = tbContent.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                dtgVeThang.DataSource = manager.GetAllVeThang(content);
            } else
            {
                LoadData();
            }
        }
        #endregion

        private void btnKhoiPhucVeThang_Click(object sender, EventArgs e)
        {

        }
    }
}
