using System;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Models;
using System.Management;
using NetTopologySuite.Operation.Buffer;
using System.IO.Ports;
using QuanLyBaiGiuXe.Helper;


namespace QuanLyBaiGiuXe
{
    public partial class A_Test: Form
    {
        Manager manager = new Manager();
        VeManager veManager = new VeManager();
        TinhTienManager tinhTienManager = new TinhTienManager();
        SerialPort serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        public A_Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tinhtien = 0;
            DateTime tgvao = DateTime.Now.AddHours(-11);
            DateTime tgra = DateTime.Now;
            tinhtien = tinhTienManager.TinhTien("0", tgvao, tgra, 1);
            tbHidden.Text = tinhtien.ToString();
        }

        private void A_Test_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => tbHidden.Focus()));
            tbHidden.Visible = true;        // phải là true
            tbHidden.Enabled = true;        // phải là true
            tbHidden.TabStop = true;        // nên bật
            tbHidden.ReadOnly = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void A_Test_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void A_Test_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void tbHidden_TextChanged(object sender, EventArgs e)
        {
            if (tbHidden.Text.EndsWith("\r") || tbHidden.Text.EndsWith("\n"))
            {
                string uid = tbHidden.Text.Trim();
                richTextBox1.AppendText(uid);

                tbHidden.Focus();
            }
        }

        private void tbHidden_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.Text = richTextBox1.Text;
        }
    }
}
