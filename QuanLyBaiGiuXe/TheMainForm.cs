using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuanLyBaiGiuXe
{
    public partial class TheMainForm: Form
    {
        Manager manager = new Manager();

        public TheMainForm()
        {
            InitializeComponent();
        }
        #region TheFunction
        private void btnXoaThe_Click(object sender, EventArgs e)
        {
            if (dtgThe.SelectedRows.Count > 0)
            {
                //int selectedId = Convert.ToInt32(dtgThe.SelectedRows[0].Cells["Id"].Value);
                int r = dtgThe.CurrentCell.RowIndex;
                string MaThe = dtgThe.Rows[r].Cells[0].Value.ToString();

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xoá thẻ {MaThe} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool isDeleted = manager.XoaThe(MaThe);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ThemThe()
        {
            string MaThe = tbMaThe.Text.Trim();
            var result = MessageBox.Show($"Bạn có chắc chắn muốn thêm thẻ {MaThe} chứ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool isAdded = manager.ThemThe(MaThe,"Tháng",DateTime.Now);

                if (isAdded)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        private void TheMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDevice();
        }
        private void LoadData()
        {
            try
            {
                this.dtgThe.DataSource = manager.GetAllThe();
            }
            catch
            {
                MessageBox.Show("Không lấy được nội dung trong table");
            }
        }
        private void LoadDevice()
        {
            //using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity WHERE PNPClass = 'USB'"))
            //{
            //    foreach (ManagementObject device in searcher.Get())
            //    {
            //        Console.WriteLine($"Tên thiết bị: {device["Name"]}");
            //        Console.WriteLine($"ID phần cứng: {device["DeviceID"]}");
            //        Console.WriteLine("===================================");
            //        richTextBox1.Text += $"Tên thiết bị: {device["Name"]} + {device["DeviceID"]} \n";
            //    }
            //}
        }
        private void tbMaThe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThemThe();
                tbMaThe.Clear();
            }
        }

        private void cbSoThe_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoThe.Checked)
            {
                tbMaThe.Enabled = true;
                tbMaThe.ReadOnly = false;
                tbMaThe.Focus();
            }
            else
            {
                tbMaThe.Enabled = false;
                tbMaThe.ReadOnly = true;
            }
        }
    }
}
