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
                sfd.FileName = $"ThongKeDangNhap_{now:ddMMyyyy}.xlsx";

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
            string loaixe = cbLoaiXe.SelectedItem.ToString();
            string loaive = cbLoaiVe.SelectedItem.ToString();
            string nhanvien = cbNhanVien.SelectedItem.ToString();
            if (cbLoaiVe.SelectedIndex == 0)
            {
                loaive = null;
            }
            if (cbLoaiXe.SelectedIndex == 0)
            {
                loaixe = "";
            }
            if (cbNhanVien.SelectedIndex == 0)
            {
                nhanvien = "";
            }
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
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
            List<LoaiXeItem> listXe = manager.GetDanhSachXe();
            listXe.Insert(0, new LoaiXeItem { MaLoaiXe = 0, TenLoaiXe = "Tất cả xe" });
            cbLoaiXe.DataSource = listXe;
            cbLoaiXe.DisplayMember = "TenLoaiXe";  // hiển thị tên
            cbLoaiXe.ValueMember = "MaLoaiXe";
            List<string> groupsVe = new List<string> { "Tất cả loại vé", "Vé tháng", "Vé lượt" };
            cbLoaiVe.DataSource = groupsVe;
            List<string> groupsTruyVan = new List<string> { "Tất cả xe", "Đã ra", "Chưa ra" };
            cbKhoangThoiGian.DataSource = groupsTruyVan;
            List<string> groupsKhoang = new List<string> { "Theo ngày", "Theo tuần", "Theo tháng", "Theo năm"};
            cbKhoangThoiGian.DataSource = groupsKhoang;
            List<string> groupsNhanVien = manager.GetDanhSachNhomNhanVien();
            groupsNhanVien.Insert(0, "Tất cả nhân viên");
            cbNhanVien.DataSource = groupsNhanVien;
        }
        private void LoadData() { 
            dtgThongKe.DataSource = manager.GetThongKeTheoKhoangThoiGian();
        }
    }
}
