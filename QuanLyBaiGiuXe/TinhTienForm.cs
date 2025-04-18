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
    public partial class TinhTienForm: Form
    {
        public TinhTienForm()
        {
            InitializeComponent();
        }

        private void trbTu_Scroll(object sender, EventArgs e)
        {
            tbTu.Text = trbTu.Value.ToString();
        }

        private void trbDen_Scroll(object sender, EventArgs e)
        {
            tbDen.Text = trbDen.Value.ToString();
        }

        private void trbKhoangGiao_Scroll(object sender, EventArgs e)
        {
            tbKhoangGiao.Text = trbKhoangGiao.Value.ToString();
        }

        private void trbPhuThuTu_Scroll(object sender, EventArgs e)
        {
            tbPhuThuTu.Text = trbPhuThuTu.Value.ToString();
        }

        private void trbPhuThuDen_Scroll(object sender, EventArgs e)
        {
            tbPhuThuDen.Text = trbPhuThuDen.Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void trbMoc1_Scroll(object sender, EventArgs e)
        {
            tbMoc1.Text = trbMoc1.Value.ToString();
        }

        private void trbMoc2_Scroll(object sender, EventArgs e)
        {
            tbMoc2.Text = trbMoc2.Value.ToString();
        }

        private void trbChuKy_Scroll(object sender, EventArgs e)
        {
            tbChuKy.Text = trbChuKy.Value.ToString();
        }
    }
}
