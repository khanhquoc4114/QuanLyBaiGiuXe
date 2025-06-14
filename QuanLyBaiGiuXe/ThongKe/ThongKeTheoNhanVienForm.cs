using OfficeOpenXml;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class ThongKeTheoNhanVienForm: Form
    {
        Manager manager= new Manager();
        public ThongKeTheoNhanVienForm()
        {
            InitializeComponent();
        }

        private void ThongKeTheoNhanVienForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            List<ComboBoxItem> dsLoaiXe = manager.GetDanhSachXe();
            LoadComboBox(cbLoaiXe, dsLoaiXe, "loại xe");

            List<string> dsLoaiVe = new List<string> { "Tất cả loại vé", "Vé tháng", "Vé lượt" };
            cbLoaiVe.DataSource = dsLoaiVe;
            cbLoaiVe.SelectedIndex = 0;

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
            btnThongKe.PerformClick();
        }

        private void LoadComboBox(ComboBox comboBox, List<ComboBoxItem> data, string suffix = null, bool includeTatCa = true)
        {
            if (includeTatCa)
            {
                data.Insert(0, new ComboBoxItem { Value = -1, Text = "Tất cả" + " " + suffix });
            }

            comboBox.DataSource = null;
            comboBox.DataSource = data;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = 0;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string Loaive = cbLoaiVe.SelectedItem.ToString();
            string MaLoaiXe = cbLoaiXe.SelectedValue.ToString();
            dtgThongKe.DataSource = manager.GetThongKeTheoNhanVien(
                Loaive,
                MaLoaiXe,
                dtpTu.Value,
                dtpDen.Value
            );
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"ThongKeTheoNhanVien_{now:ddMMyyyy}.xlsx";

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Xuất tiêu đề cột
                        for (int col = 0; col < dtgThongKe.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgThongKe.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgThongKe.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgThongKe.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgThongKe.Rows[row].Cells[col].Value?.ToString();
                            }
                        }

                        File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
