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
            VeThangThemSuaForm veThangThemForm = new VeThangThemSuaForm();
            veThangThemForm.ShowDialog();
        }

        private void btnXoaVeThang_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xoá vé tháng?",
                "Xác vé tháng?",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Xóa nhóm
            }
            else if (result == DialogResult.No)
            {
                // Không làm gì cả
            }
            else
            {
                // Đóng dialog
            }
        }

        private void btnGiaHanVeThang_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                sfd.FileName = "DataGridView_Export.xlsx";

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

        #endregion

    }
}
