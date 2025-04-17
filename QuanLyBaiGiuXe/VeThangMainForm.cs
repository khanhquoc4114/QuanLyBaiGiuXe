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
                this.dtgNhom.DataSource = manager.GetAllNhom();
                dtgNhom.Columns["TenNhom"].HeaderText = "Tên nhóm";
                dtgNhom.Columns["ThongTinKhac"].HeaderText = "Thông Tin Khác";
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
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
                string TenNhom = dtgNhom.Rows[r].Cells[0].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá nhóm {TenNhom} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaNhom(TenNhom);

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

        private void btnSuaNhom_Click(object sender, EventArgs e)
        {
            int r = dtgNhom.CurrentCell.RowIndex;
            string TenNhom = dtgNhom.Rows[r].Cells[0].Value.ToString();
            string ThongTinKhac = dtgNhom.Rows[r].Cells[1].Value.ToString();
            VeThangThemSuaNhom veThangThemForm = new VeThangThemSuaNhom(btnSuaNhom.Text,TenNhom);
            veThangThemForm.ShowDialog();
            if (veThangThemForm.ThemSuaThanhCong) LoadData();
        }

        #endregion

        #region Vé Tháng
        private void VeThangMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

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
                DataGridViewRow row = dtgVeThang.SelectedRows[0];

                string bienSo = row.Cells["BienSo"].Value.ToString();

                VeThangThemSuaForm frmChinhSua = new VeThangThemSuaForm(option, bienSo);
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
                int r = dtgVeThang.CurrentCell.RowIndex;
                string BienSo = dtgVeThang.Rows[r].Cells["BienSo"].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá vé này chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaVeThang(BienSo);

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
            VeThangGiaHanForm veThangGiaHanForm = new VeThangGiaHanForm(dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["BienSo"].Value.ToString());
            veThangGiaHanForm.ShowDialog();
            if (veThangGiaHanForm.GiaHanThanhCong) LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"DuLieuVeThang_{now:ddMMyyyy}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Xuất tiêu đề cột
                        for (int col = 0; col < dtgVeThang.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgVeThang.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgVeThang.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgVeThang.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgVeThang.Rows[row].Cells[col].Value?.ToString();
                            }
                        }

                        File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnDoiThe_Click(object sender, EventArgs e)
        {
            string BienSo = dtgVeThang.Rows[dtgVeThang.CurrentCell.RowIndex].Cells["BienSo"].Value.ToString();
            VeThangDoiTheThang veThangDoiTheThang = new VeThangDoiTheThang(BienSo);
            veThangDoiTheThang.ShowDialog();
            if (veThangDoiTheThang.DoiTheThanhCong) LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string content = tbContent.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                dtgVeThang.DataSource = manager.TimKiemVeThang(content);
            } else
            {
                LoadData();
            }
        }
        #endregion
    }
}
