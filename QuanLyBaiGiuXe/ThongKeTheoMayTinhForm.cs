using OfficeOpenXml.LoadFunctions.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Models;
using OfficeOpenXml;
using System.IO;

namespace QuanLyBaiGiuXe
{
    public partial class ThongKeTheoMayTinhForm: Form
    {
        Manager manager = new Manager();
        public ThongKeTheoMayTinhForm()
        {
            InitializeComponent();
        }

        private void ThongKeTheoMayTinhForm_Load(object sender, EventArgs e)
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
            List<string> groupsXe = manager.GetDanhSachXe();
            groupsXe.Insert(0, "Tất cả xe");
            cbLoaiXe.DataSource = groupsXe;
            List<string> groupsVe = new List<string> { "Tất cả loại vé", "Vé tháng", "Vé lượt" };
            cbLoaiVe.DataSource = groupsVe;
        }
        private void LoadData()
        {
            dtgThongKe.DataSource = manager.GetThongKeTheoMayTinh();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpTu.Value > dtpDen.Value)
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
            if (cbLoaiVe.SelectedIndex == 0) {
                loaive = "";
            }
            if(cbLoaiXe.SelectedIndex == 0)
            {
                loaixe = "";
            }
            dtgThongKe.DataSource = manager.GetThongKeTheoMayTinhByTimKiem(loaive, loaixe, dtpTu.Value, dtpDen.Value);
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
    }
}
