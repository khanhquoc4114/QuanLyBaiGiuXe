using OfficeOpenXml;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class VeLuotMainForm: Form
    {
        Manager manager = new Manager();
        public VeLuotMainForm()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                this.dtgVeLuot.DataSource = manager.GetAllVeLuot();
                this.dtgTong.DataSource = manager.GetTongVeLuot();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime tgTu = dtpTu.Value;
            DateTime tgDen = dtpDen.Value;
            if (tgTu > tgDen)
            {
                ToastService.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc.", this);
                return;
            }
            try
            {
                this.dtgVeLuot.DataSource = manager.GetAllVeLuot(tgTu, tgDen);
                dtgTong.DataSource = manager.GetTongVeLuot(tgTu, tgDen);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"DuLieuVeLuot_{now:ddMMyyyy}.xlsx";

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Xuất tiêu đề cột
                        for (int col = 0; col < dtgVeLuot.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col + 1].Value = dtgVeLuot.Columns[col].HeaderText;
                        }

                        // Xuất dữ liệu từ DataGridView
                        for (int row = 0; row < dtgVeLuot.Rows.Count; row++)
                        {
                            for (int col = 0; col < dtgVeLuot.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dtgVeLuot.Rows[row].Cells[col].Value?.ToString();
                            }
                        }

                        File.WriteAllBytes(sfd.FileName, package.GetAsByteArray());

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void VeLuotMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
