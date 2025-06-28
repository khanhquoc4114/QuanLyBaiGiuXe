using NetTopologySuite.GeometriesGraph;
using OfficeOpenXml;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    public partial class TraCuuXeVaoRaForm: Form
    {
        Manager manager = new Manager();
        public TraCuuXeVaoRaForm()
        {
            InitializeComponent();
        }

        private void TraCuuXeVaoRaForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<ComboBoxItem> dsNhanVien = manager.GetDanhSachNhanVien();
            LoadComboBox(cbNhanVienVao, new List<ComboBoxItem>(dsNhanVien), "nhân viên");
            LoadComboBox(cbNhanVienRa, new List<ComboBoxItem>(dsNhanVien), "nhân viên");

            List<ComboBoxItem> dsLoaiXe = manager.GetDanhSachXe();
            LoadComboBox(cbLoaiXe, dsLoaiXe, "loại xe");

            List<string> dsLoaiVe = new List<string> { "Tất cả loại vé", "Vé tháng","Vé lượt"};
            cbLoaiVe.DataSource = dsLoaiVe;
            cbLoaiVe.SelectedIndex = 0;

            List<string> dsTruyVan = new List<string> { "Tất cả xe", "Xe chưa ra", "Xe đã ra", "Xe mất thẻ"};
            cbTruyVan.DataSource = dsTruyVan;
            cbTruyVan.SelectedIndex = 0;

            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTu.Value = DateTime.Now.AddDays(-7);
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy HH:mm";

            dtgThongKe.DataSource = manager.TraCuuRaVao();
            if (dtgThongKe.Rows.Count > 0)
            {
                dtgThongKe.Columns["MaVeLuot"].Visible = false;
                dtgThongKe.ClearSelection();
                foreach (DataGridViewColumn col in dtgThongKe.Columns)
                {
                    if (col.Visible)
                    {
                        dtgThongKe.Rows[0].Selected = true;
                        dtgThongKe.CurrentCell = dtgThongKe.Rows[0].Cells[col.Index];
                        break;
                    }
                }
            }
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string loaiTruyVan = cbTruyVan.Text.Trim();
            loaiTruyVan = loaiTruyVan.StartsWith("Xe ") ? loaiTruyVan.Substring(3).Trim() : loaiTruyVan;
            if (!string.IsNullOrEmpty(loaiTruyVan))
            {
                loaiTruyVan = char.ToUpper(loaiTruyVan[0]) + loaiTruyVan.Substring(1);
            }
            string loaiVe = cbLoaiVe.Text.Trim();
            string maLoaiXe = cbLoaiXe.SelectedValue?.ToString();
            DateTime tgTu = dtpTu.Value.Date;
            DateTime tgDen = dtpDen.Value.Date;
            string soThe = tbSoThe.Text.Trim();
            string bienSo = tbBienSo.Text.Trim();
            string maThe = tbMaThe.Text.Trim();
            string maNhanVienVao = cbNhanVienVao.SelectedValue?.ToString();
            string maNhanVienRa = cbNhanVienRa.SelectedValue?.ToString();
            dtgThongKe.DataSource = manager.TraCuuRaVao(loaiTruyVan, loaiVe, maLoaiXe, tgTu, tgDen, soThe, bienSo, maThe, maNhanVienVao, maNhanVienRa);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                DateTime now = DateTime.Now;
                sfd.FileName = $"ThongKeXeRaVao_{now:ddMMyyyy}.xlsx";

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

        private void btnXuLyMatThe_Click(object sender, EventArgs e)
        {
            if (dtgThongKe.CurrentRow != null)
            {
                var maVeLuot = dtgThongKe.CurrentRow.Cells["MaVeLuot"].Value?.ToString();
                var form = new XuLyMatThe(maVeLuot);
                form.ShowDialog();
                if (form.isThanhCong)
                {
                    LoadData();
                }
            }
        }

        private void btnDieuChinhGiaVe_Click(object sender, EventArgs e)
        {
            if (dtgThongKe.CurrentRow != null)
            {
                var maVeLuot = dtgThongKe.CurrentRow.Cells["MaVeLuot"].Value?.ToString();
                var sotien = dtgThongKe.CurrentRow.Cells["Tổng tiền"].Value?.ToString();
                var form = new DieuChinhGiaVeForm(maVeLuot, Convert.ToInt32(sotien));
                form.ShowDialog();
                if (form.isThanhCong)
                {
                    LoadData();
                }
            }
        }

        private void dtgThongKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnDieuChinhGiaVe.Enabled = true;
                string pathAnhVao = dtgThongKe.Rows[e.RowIndex].Cells["Ảnh vào"].Value?.ToString();
                string pathAnhRa = dtgThongKe.Rows[e.RowIndex].Cells["Ảnh ra"].Value?.ToString();
                LoadImageToPictureBox(pbVao, pathAnhVao);
                LoadImageToPictureBox(pbRa, pathAnhRa);
            }
        }

        private void LoadImageToPictureBox(PictureBox picBox, string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picBox.Image?.Dispose();

                    using (var ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                    {
                        picBox.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                picBox.Image = null;
            }
        }
    }
}
