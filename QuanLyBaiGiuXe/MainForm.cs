using Newtonsoft.Json.Linq;
using QuanLyBaiGiuXe.Models;
using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using QuanLyBaiGiuXe.Helper;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing.Imaging;
using System.Collections.Generic;
using QuanLyBaiGiuXe.Properties;
using System.Threading.Tasks;
using System.Threading;

namespace QuanLyBaiGiuXe
{
    public partial class MainForm : Form
    {
        Manager manager = new Manager();
        VeManager veManager = new VeManager();
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        string MaSoXe = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }
        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            LoadData();
            LoadCamera();
            this.BeginInvoke(new Action(() => txtMaThe.Focus()));
            txtMaThe.Clear();
            txtMaThe.Focus();
            ctsPingModel = new CancellationTokenSource();
            await PingModelUntilReadyAsync(ctsPingModel.Token);
        }
        #region Nhận diện
        //private void SendImageAndReceiveResult(string filePath)
        //{
        //    try
        //    {
        //        byte[] imageData = File.ReadAllBytes(filePath);
        //        string serverIP = "127.0.0.1";

        //        using (TcpClient client = new TcpClient(serverIP, 54321))
        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            client.ReceiveTimeout = 10000;
        //            client.SendTimeout = 10000;
        //            byte[] fileSizeBytes = BitConverter.GetBytes(imageData.Length);
        //            if (BitConverter.IsLittleEndian)
        //                Array.Reverse(fileSizeBytes);

        //            stream.Write(fileSizeBytes, 0, 4);
        //            stream.Write(imageData, 0, imageData.Length);

        //            byte[] sizeBuffer = new byte[4];
        //            stream.Read(sizeBuffer, 0, 4);
        //            if (BitConverter.IsLittleEndian)
        //                Array.Reverse(sizeBuffer);
        //            int responseSize = BitConverter.ToInt32(sizeBuffer, 0);

        //            byte[] responseData = new byte[responseSize];
        //            int totalRead = 0;
        //            while (totalRead < responseSize)
        //            {
        //                int bytesRead = stream.Read(responseData, totalRead, responseSize - totalRead);
        //                if (bytesRead == 0) break;
        //                totalRead += bytesRead;
        //            }

        //            string jsonResult = Encoding.UTF8.GetString(responseData);
        //            Console.WriteLine("Response from server: " + jsonResult);
        //            var json = JObject.Parse(jsonResult);
        //            string plateText = json["plate_text"]?.ToString() ?? "unknown";
        //            tbBienSoVao.Text = plateText;
        //            MaSoXe = plateText;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi nhận kết quả từ model: " + ex.Message);
        //    }
        //}

        private void SendImageAndReceiveResult(string filePath)
        {
            try
            {
                byte[] imageData = File.ReadAllBytes(filePath);
                string serverIP = "127.0.0.1";

                using (TcpClient client = new TcpClient(serverIP, 54321))
                using (NetworkStream stream = client.GetStream())
                {
                    client.ReceiveTimeout = 10000;
                    client.SendTimeout = 10000;

                    // Prefix: 4 bytes "IMG "
                    byte[] prefix = Encoding.ASCII.GetBytes("IMG ");
                    stream.Write(prefix, 0, 4);

                    // Gửi kích thước ảnh (big-endian)
                    byte[] fileSizeBytes = BitConverter.GetBytes(imageData.Length);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(fileSizeBytes);
                    stream.Write(fileSizeBytes, 0, 4);

                    // Gửi nội dung ảnh
                    stream.Write(imageData, 0, imageData.Length);

                    // Nhận kích thước kết quả trả về
                    byte[] sizeBuffer = new byte[4];
                    stream.Read(sizeBuffer, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(sizeBuffer);
                    int responseSize = BitConverter.ToInt32(sizeBuffer, 0);

                    // Nhận dữ liệu JSON
                    byte[] responseData = new byte[responseSize];
                    int totalRead = 0;
                    while (totalRead < responseSize)
                    {
                        int bytesRead = stream.Read(responseData, totalRead, responseSize - totalRead);
                        if (bytesRead == 0) break;
                        totalRead += bytesRead;
                    }

                    string jsonResult = Encoding.UTF8.GetString(responseData);
                    if (!string.IsNullOrWhiteSpace(jsonResult) && jsonResult.TrimStart().StartsWith("{"))
                    {
                        var json = JObject.Parse(jsonResult);
                        string plateText = json["plate_text"]?.ToString() ?? "unknown";
                        tbBienSoVao.Text = plateText;
                        MaSoXe = plateText;
                    }
                    else
                    {
                        MessageBox.Show("Phản hồi không phải JSON: " + jsonResult);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi/nhận từ server: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pb1.Image != null)
            {
                string tempPath = Path.Combine(Application.StartupPath, "images/captured_image.jpg");
                string imageDir = Path.Combine(Application.StartupPath, "images");
                if (!Directory.Exists(imageDir))
                {
                    Directory.CreateDirectory(imageDir);
                }
                try
                {
                    if (File.Exists(tempPath))
                    {
                        File.Delete(tempPath);
                    }

                    using (Bitmap img = new Bitmap(pb1.Image))
                    {
                        img.Save(tempPath, ImageFormat.Jpeg);
                    }
                    SendImageAndReceiveResult(tempPath);
                }
                finally
                {
                    if (File.Exists(tempPath)) File.Delete(tempPath);
                }
            }
            else
            {
                MessageBox.Show("Chưa có hình ảnh trong PictureBox!");
            }
        }
        private void LoadCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Không tìm thấy camera!");
                return;
            }
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                if (pb1.InvokeRequired)
                {
                    pb1.BeginInvoke(new Action(() =>
                    {
                        pb1.Image?.Dispose();
                        pb1.Image = bitmap;
                        pb1.SizeMode = PictureBoxSizeMode.Zoom;
                    }));
                }
                else
                {
                    pb1.Image?.Dispose();
                    pb1.Image = bitmap;
                    pb1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch { }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
                videoSource = null;
            }

            pb1.Image?.Dispose();
            pb1.Image = null;
            ctsPingModel?.Cancel();
        }
        #endregion

        #region Vé Lượt
        private void LoadData()
        {
            var danhSachXe = manager.GetDanhSachXe();
            if (danhSachXe != null && danhSachXe.Count > 0)
            {
                LoadComboBox(cbLoaiXe, danhSachXe, includeTatCa: false);
            }
            else
            {
                cbLoaiXe.DataSource = null;
                cbLoaiXe.Text = "-- Không có dữ liệu --";
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

        private void ThucHienRaVao()
        {
            string mathe = txtMaThe.Text.Trim();
            if (string.IsNullOrEmpty(mathe))
            {
                new ToastForm("Vui lòng nhập mã thẻ!", this).Show();
                return;
            }

            string bienso = tbBienSoVao.Text.Trim();
            DateTime tgHienTai = DateTime.Now;
            string maloaixe = cbLoaiXe.SelectedValue.ToString();

            bool ktra = veManager.KiemTraTrongBai(mathe, bienso);
            if (ktra)
            {
                // XE RA
                if (!XuLyPathAnh("ra", out string pathRa))
                {
                    new ToastForm("Không thể lưu ảnh xe ra!", this).Show();
                    return;
                }
                var ve = veManager.CapNhatVeLuot(mathe, bienso, tgHienTai.AddHours(8), pathRa);
                if (ve != null)
                {
                    new ToastForm($"Xe ra thành công!\nTiền: {ve.TongTien:N0}đ", this).Show();
                    tbBienSoVao.Text = ve.BienSo;
                    tbGioRa.Text = ve.ThoiGianRa.ToString("HH:mm:ss");
                    tbGioVao.Text = ve.ThoiGianVao.ToString("HH:mm:ss");
                    tbTongTien.Text = ve.TongTien.ToString("N0") + "đ";
                    tbNgayRa.Text = ve.ThoiGianRa.ToString("dd/MM/yyyy");
                    tbNgayVao.Text = ve.ThoiGianVao.ToString("dd/MM/yyyy");
                    tbBienSoRa.Text = MaSoXe;
                    tbLoaiVe.Text = (ve.LaVeThang) ? "Vé tháng" : "Vé lượt";
                    LoadImageToPictureBox(pbVao, ve.AnhVaoPath);
                    LoadImageToPictureBox(pbRa, ve.AnhRaPath);
                }
                else
                {
                    new ToastForm("Xe ra thất bại!", this).Show();
                }
            }
            else
            {
                // XE VÀO
                if (!XuLyPathAnh("vao", out string pathVao))
                {
                    new ToastForm("Không thể lưu ảnh xe vào!", this).Show();
                    return;
                }
                bool result = veManager.ThemVeLuot(mathe, bienso, tgHienTai, maloaixe,pathVao);
                if (result)
                {
                    new ToastForm("Xe vào thành công!", this).Show();
                    tbBienSoVao.Text = bienso;
                    tbGioVao.Text = tgHienTai.ToString("HH:mm:ss");
                    tbNgayVao.Text = tgHienTai.ToString("dd/MM/yyyy");
                    tbGioRa.Text = "null";
                    tbNgayRa.Text = "null";
                    tbTongTien.Text = "0đ";
                }
                else
                {
                    new ToastForm("Xe vào thất bại!", this).Show();
                }
            }
        }
        private bool XuLyPathAnh(string prefix, out string imagePath)
        {
            imagePath = string.Empty;

            try
            {
                string path = Settings.Default.ImagePath;
                string imageDir = Path.Combine($@"{path}\XeImages", DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(imageDir))
                    Directory.CreateDirectory(imageDir);

                string fileName = prefix + "_" + Guid.NewGuid().ToString("N").Substring(0, 9) + ".jpg";
                imagePath = Path.Combine(imageDir, fileName);

                using (Bitmap img = new Bitmap(pb1.Image))
                {
                    img.Save(imagePath, ImageFormat.Jpeg);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu ảnh: " + ex.Message);
                return false;
            }
        }
        #endregion

        private void txtMaThe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnNhanDien.PerformClick();
                ThucHienRaVao();
                txtMaThe.Clear();
            }
        }

        public static void SaveImageToDateFolder(byte[] imageBytes, string baseFolder, string fileName)
        {
            string todayFolder = DateTime.Now.ToString("dd_MM_yyyy");

            string fullPath = Path.Combine(baseFolder, todayFolder);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string imagePath = Path.Combine(fullPath, fileName);

            File.WriteAllBytes(imagePath, imageBytes);

            Console.WriteLine("Đã lưu ảnh vào: " + imagePath);
        }

        private void btnMatThe_Click(object sender, EventArgs e)
        {

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


        private CancellationTokenSource ctsPingModel = new CancellationTokenSource();
        public async Task PingModelUntilReadyAsync(CancellationToken cancellationToken)
        {
            lbStatus.Text = "🚀 Đang khởi tạo mô hình...";
            lbStatus.ForeColor = Color.Orange;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync("127.0.0.1", 54321);
                        using (NetworkStream stream = client.GetStream())
                        {
                            client.ReceiveTimeout = 5000;
                            client.SendTimeout = 5000;

                            byte[] prefix = Encoding.ASCII.GetBytes("CMD ");
                            await stream.WriteAsync(prefix, 0, prefix.Length);

                            byte[] commandBytes = Encoding.UTF8.GetBytes("status\n");
                            await stream.WriteAsync(commandBytes, 0, commandBytes.Length);

                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string response = await reader.ReadLineAsync();
                                if (response != null && response.Contains("model loaded"))
                                {
                                    lbStatus.Invoke((MethodInvoker)(() =>
                                    {
                                        lbStatus.Text = "✅ Mô hình nhận diện đã sẵn sàng!";
                                        lbStatus.ForeColor = Color.Green;
                                        btnNhanDien.Enabled = true;
                                    }));
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                await Task.Delay(2000, cancellationToken);
            }
        }
    }
}