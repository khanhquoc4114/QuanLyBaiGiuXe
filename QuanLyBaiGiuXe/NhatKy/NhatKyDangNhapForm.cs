using OfficeOpenXml;
using QuanLyBaiGiuXe.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhatKyDangNhapForm: Form
    {
        Manager manager = new Manager();

        public NhatKyDangNhapForm()
        {
            InitializeComponent();
        }

        private void NhatKyDangNhapForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadUI();
        }

        private void LoadUI()
        {
            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void LoadData() {
            try
            {
                this.dtgNhatKyDangNhap.DataSource = manager.GetNhatKyDangNhap();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
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
                        for (int col = 0; col < dtgNhatKyDangNhap.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgNhatKyDangNhap.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgNhatKyDangNhap.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgNhatKyDangNhap.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgNhatKyDangNhap.Rows[row].Cells[col].Value?.ToString();
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
            DateTime tgTu = dtpTu.Value;
            DateTime tgDen = dtpDen.Value;

            this.dtgNhatKyDangNhap.DataSource = manager.TimKiemNhatKyDangNhap(tgTu, tgDen);
        }
    }
}
