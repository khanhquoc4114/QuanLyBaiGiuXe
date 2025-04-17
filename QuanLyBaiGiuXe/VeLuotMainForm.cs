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
    public partial class VeLuotMainForm: Form
    {
        Manager manager = new Manager();
        public VeLuotMainForm()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.dtgVeLuot.DataSource = manager.GetAllVeLuot();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"DuLieuVeLuot_{now:ddMMyyyy}.xlsx";

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
    }
}
