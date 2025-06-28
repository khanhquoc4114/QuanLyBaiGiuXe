using OfficeOpenXml;
using QuanLyBaiGiuXe.Helper;
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
    public partial class ThongKeTheoKhoangThoiGianForm: Form
    {
        Manager manager = new Manager();
        public ThongKeTheoKhoangThoiGianForm()
        {
            InitializeComponent();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"ThongKeTheoKhoangThoiGian_{now:ddMMyyyy}.xlsx";

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

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpTu.Value >= dtpDen.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbLoaiXe.SelectedItem == null || cbLoaiVe.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại xe và loại vé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string loaixe = cbLoaiXe.SelectedValue.ToString();
            string loaive = cbLoaiVe.Text.ToString().Trim();
            string nhanvien = cbNhanVien.SelectedValue.ToString();
            string khoangThoiGian = cbKhoangThoiGian.SelectedItem.ToString();
            var tokens = khoangThoiGian.Trim().ToLower().Split(' ');
            var lastWord = tokens.Last();
            dtgThongKe.DataSource = manager.GetThongKeTheoKhoangThoiGian(lastWord, dtpTu.Value, dtpDen.Value, loaixe, loaive, nhanvien);
        }

        private void ThongKeTheoKhoangThoiGianForm_Load(object sender, EventArgs e)
        {
            LoadUI();
            LoadData();
        }
        private void LoadUI()
        {
            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";

            List<ComboBoxItem> listXe = manager.GetDanhSachXe();
            LoadComboBox(cbLoaiXe, listXe, "loại xe");

            List<string> groupsVe = new List<string> { "Tất cả loại vé", "Vé tháng", "Vé lượt" };
            cbLoaiVe.DataSource = groupsVe;
            List<string> groupsTruyVan = new List<string> { "Tất cả xe", "Đã ra", "Chưa ra" };
            cbKhoangThoiGian.DataSource = groupsTruyVan;
            List<string> groupsKhoang = new List<string> { "Theo ngày", "Theo tuần", "Theo tháng", "Theo năm"};
            cbKhoangThoiGian.DataSource = groupsKhoang;

            List<ComboBoxItem> groupsNhanVien = manager.GetDanhSachNhanVien();
            LoadComboBox(cbNhanVien, groupsNhanVien, "nhân viên");
        }

        private void LoadData() {
            btnThongKe.PerformClick();
        }

        private void LoadComboBox(ComboBox comboBox, List<ComboBoxItem> data, string suffix = null,  bool includeTatCa = true)
        {
            if (includeTatCa)
            {
                data.Insert(0, new ComboBoxItem { Value = -1, Text = "Tất cả" + " " +suffix });
            }

            comboBox.DataSource = null;
            comboBox.DataSource = data;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = 0;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
