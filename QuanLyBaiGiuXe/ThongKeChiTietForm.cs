using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;

namespace QuanLyBaiGiuXe
{
    public partial class ThongKeChiTietForm: Form
    {
        Manager  manager = new Manager();
        public ThongKeChiTietForm()
        {
            InitializeComponent();
        }

        private void ThongKeChiTietForm_Load(object sender, EventArgs e)
        {
            LoadUI();
        }
        private void LoadUI() {
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

        }
    }
}
