using OfficeOpenXml;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class NhatKyVeLuotForm: Form
    {
        Manager manager = new Manager();
        public NhatKyVeLuotForm()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            List<string> hanhdonglist = new List<string> { "Tất cả hành động", "Xử lý mất thẻ", "Thêm vé lượt", "Sửa vé lượt", "Cho xe ra (Miễn phí)", "Cho xe ra (Tính phí)" };
            cbHanhDong.DataSource = hanhdonglist;

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";

            btnTimKiem.PerformClick();
        }

        private void NhatKyVeLuotForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"ThongKeXuLyVeLuot_{now:ddMMyyyy}.xlsx";

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Xuất tiêu đề cột
                        for (int col = 0; col < dtgXuLyVeLuot.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgXuLyVeLuot.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgXuLyVeLuot.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgXuLyVeLuot.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgXuLyVeLuot.Rows[row].Cells[col].Value?.ToString();
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
            string hanhdong = cbHanhDong.Text;
            this.dtgXuLyVeLuot.DataSource = manager.GetXuLyVeLuot(hanhdong, tgTu, tgDen);
        }

        private void cbHanhDong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
