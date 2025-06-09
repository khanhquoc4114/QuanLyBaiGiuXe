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

namespace QuanLyBaiGiuXe
{
    public partial class XuLyMatThe: Form
    {
        Manager manager = new Manager();
        public XuLyMatThe()
        {
            InitializeComponent();
        }

        private void tbTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChoRaKoTinhPhi_Click(object sender, EventArgs e)
        {

        }

        private void btnChoRaTinhPhi_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XuLyMatThe_Load(object sender, EventArgs e)
        {
            LoadData();
            this.BeginInvoke(new Action(() => tbMaThe.Focus()));
            tbMaThe.Clear();
            tbMaThe.Focus();
        }
        private void LoadData()
        {
            List<LoaiXeItem> listXe = manager.GetDanhSachXe();
            listXe.Insert(0, new LoaiXeItem { MaLoaiXe = 0, TenLoaiXe = "Tất cả xe" });
            cbLoaiXe.DataSource = listXe;
        }

        private void tbMaThe_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
