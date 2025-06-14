using OfficeOpenXml;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhatKyDieuChinhGiaVeForm: Form
    {
        Manager manager = new Manager();
        public NhatKyDieuChinhGiaVeForm()
        {
            InitializeComponent();
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"ThongKeDangNhap_{now:ddMMyyyy}.xlsx";

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Xuất tiêu đề cột
                        for (int col = 0; col < dtgNhatKy.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgNhatKy.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgNhatKy.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgNhatKy.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgNhatKy.Rows[row].Cells[col].Value?.ToString();
                            }
                        }

                        File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void NhatKyDieuChinhGiaVeForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadUI();
        }
        private void LoadData()
        {
            dtgNhatKy.DataSource = manager.GetNhatKyDieuChinhGiaVe();
        }

        private void LoadUI()
        {
            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
        }
    }
}
