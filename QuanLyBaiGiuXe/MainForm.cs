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
        private VideoCaptureDevice videoSource1;
        private VideoCaptureDevice videoSource2;
        string MaSoXe = string.Empty;
        private int selectedCameraIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        private void UpdateCameraLabelStyle()
        {
            // Camera 0
            lblCamera0.Font = selectedCameraIndex == 0
                ? new Font(lblCamera0.Font, FontStyle.Bold)
                : new Font(lblCamera0.Font, FontStyle.Regular);

            lblCamera0.ForeColor = selectedCameraIndex == 0 ? Color.DarkBlue : Color.Gray;
            lblCamera0.BackColor = selectedCameraIndex == 0 ? Color.LightCyan : SystemColors.Control;

            // Camera 1
            lblCamera1.Font = selectedCameraIndex == 1
                ? new Font(lblCamera1.Font, FontStyle.Bold)
                : new Font(lblCamera1.Font, FontStyle.Regular);

            lblCamera1.ForeColor = selectedCameraIndex == 1 ? Color.DarkBlue : Color.Gray;
            lblCamera1.BackColor = selectedCameraIndex == 1 ? Color.LightCyan : SystemColors.Control;
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedCameraIndex == 1 && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                selectedCameraIndex = 0;
            }
            else if (selectedCameraIndex == 0 && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                selectedCameraIndex = 1;
            }
            UpdateCameraLabelStyle();
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            LoadData();
            LoadCamera();
            this.BeginInvoke(new Action(() => txtMaThe.Focus()));
            txtMaThe.Clear();
            txtMaThe.Focus();
            UpdateCameraLabelStyle();
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
            Image selectedImage = selectedCameraIndex == 0 ? pb1.Image : pbCamera1.Image;
            if (selectedImage == null)
            {
                MessageBox.Show($"❌ Chưa có hình ảnh từ Camera {selectedCameraIndex}!");
                return;
            }

            string tempPath = Path.Combine(Application.StartupPath, $"camera{selectedCameraIndex}_image.jpg");

            try
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);

                using (Bitmap img = new Bitmap(selectedImage))
                {
                    img.Save(tempPath, ImageFormat.Jpeg);
                }

                SendImageAndReceiveResult(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý ảnh: " + ex.Message);
            }
            finally
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);
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

            // Camera 1 -> pb1
            videoSource1 = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource1.NewFrame += VideoSource1_NewFrame;
            videoSource1.Start();

            // Camera 2 -> pbCamera1 (nếu có)
            if (videoDevices.Count > 1)
            {
                videoSource2 = new VideoCaptureDevice(videoDevices[1].MonikerString);
                videoSource2.NewFrame += VideoSource2_NewFrame;
                videoSource2.Start();
            }
        }

        private void VideoSource1_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            if (pb1.InvokeRequired)
            {
                pb1.BeginInvoke(new Action(() =>
                {
                    pb1.Image?.Dispose();
                    pb1.Image = bitmap;
                }));
            }
            else
            {
                pb1.Image?.Dispose();
                pb1.Image = bitmap;
            }
        }

        private void VideoSource2_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            if (pbCamera1.InvokeRequired)
            {
                pbCamera1.BeginInvoke(new Action(() =>
                {
                    pbCamera1.Image?.Dispose();
                    pbCamera1.Image = bitmap;
                }));
            }
            else
            {
                pbCamera1.Image?.Dispose();
                pbCamera1.Image = bitmap;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource1 != null)
            {
                videoSource1.SignalToStop();
                videoSource1.WaitForStop();
                videoSource1.NewFrame -= VideoSource1_NewFrame;
                videoSource1 = null;
            }

            if (videoSource2 != null)
            {
                videoSource2.SignalToStop();
                videoSource2.WaitForStop();
                videoSource2.NewFrame -= VideoSource2_NewFrame;
                videoSource2 = null;
            }

            pb1.Image?.Dispose();
            pb1.Image = null;

            pbCamera1.Image?.Dispose();
            pbCamera1.Image = null;

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
            TextBox tbBienSoVao_Current = selectedCameraIndex == 0 ? tbBienSoVao : tbBienSoVao2;
            TextBox tbBienSoRa_Current = selectedCameraIndex == 0 ? tbBienSoRa : tbBienSoRa2;
            TextBox tbGioRa_Current = selectedCameraIndex == 0 ? tbGioRa : tbGioRa2;
            TextBox tbGioVao_Current = selectedCameraIndex == 0 ? tbGioVao : tbGioVao2;
            TextBox tbNgayRa_Current = selectedCameraIndex == 0 ? tbNgayRa : tbNgayRa2;
            TextBox tbNgayVao_Current = selectedCameraIndex == 0 ? tbNgayVao : tbNgayVao2;
            TextBox tbTongTien_Current = selectedCameraIndex == 0 ? tbTongTien : tbTongTien2;
            TextBox tbLoaiVe_Current = selectedCameraIndex == 0 ? tbLoaiVe : tbLoaiVe2;

            PictureBox pbVao_Current = selectedCameraIndex == 0 ? pbVao : pbVao1;
            PictureBox pbRa_Current = selectedCameraIndex == 0 ? pbRa : pbRa1;

            string mathe = txtMaThe.Text.Trim();
            if (string.IsNullOrEmpty(mathe))
            {
                new ToastForm("Vui lòng nhập mã thẻ!", this).Show();
                return;
            }

            string bienso = tbBienSoVao_Current.Text.Trim();
            DateTime tgHienTai = DateTime.Now;
            string maloaixe = cbLoaiXe.SelectedValue.ToString();

            bool ktra = veManager.KiemTraTrongBai(mathe, bienso);
            if (ktra)
            {
                // 🚗 XE RA
                if (!XuLyPathAnh("ra", out string pathRa))
                {
                    new ToastForm("Không thể lưu ảnh xe ra!", this).Show();
                    return;
                }

                var ve = veManager.CapNhatVeLuot(mathe, bienso, tgHienTai.AddHours(8), pathRa);
                if (ve != null)
                {
                    new ToastForm($"Xe ra thành công!\nTiền: {ve.TongTien:N0}đ", this).Show();

                    tbBienSoVao_Current.Text = ve.BienSo;
                    tbGioRa_Current.Text = ve.ThoiGianRa.ToString("HH:mm:ss");
                    tbGioVao_Current.Text = ve.ThoiGianVao.ToString("HH:mm:ss");
                    tbTongTien_Current.Text = ve.TongTien.ToString("N0") + "đ";
                    tbNgayRa_Current.Text = ve.ThoiGianRa.ToString("dd/MM/yyyy");
                    tbNgayVao_Current.Text = ve.ThoiGianVao.ToString("dd/MM/yyyy");
                    tbBienSoRa_Current.Text = MaSoXe;
                    tbLoaiVe_Current.Text = (ve.LaVeThang) ? "Vé tháng" : "Vé lượt";

                    LoadImageToPictureBox(pbVao_Current, ve.AnhVaoPath);
                    LoadImageToPictureBox(pbRa_Current, ve.AnhRaPath);
                }
                else
                {
                    new ToastForm("Xe ra thất bại!", this).Show();
                }
            }
            else
            {
                // 🚘 XE VÀO
                if (!XuLyPathAnh("vao", out string pathVao))
                {
                    new ToastForm("Không thể lưu ảnh xe vào!", this).Show();
                    return;
                }

                bool result = veManager.ThemVeLuot(mathe, bienso, tgHienTai, maloaixe, pathVao);
                if (result)
                {
                    new ToastForm("Xe vào thành công!", this).Show();

                    tbBienSoVao_Current.Text = bienso;
                    tbGioVao_Current.Text = tgHienTai.ToString("HH:mm:ss");
                    tbNgayVao_Current.Text = tgHienTai.ToString("dd/MM/yyyy");
                    tbGioRa_Current.Text = "null";
                    tbNgayRa_Current.Text = "null";
                    tbTongTien_Current.Text = "0đ";

                    LoadImageToPictureBox(pbVao_Current, pathVao);
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

                PictureBox selectedBox = selectedCameraIndex == 0 ? pb1 : pbCamera1;

                if (selectedBox.Image == null)
                {
                    MessageBox.Show("❌ Không có hình ảnh từ camera đang chọn!");
                    return false;
                }

                using (Bitmap img = new Bitmap(selectedBox.Image))
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
            var form = new TraCuuXeVaoRaForm();
            form.ShowDialog();
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
                try
                {
                    await Task.Delay(2000, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private void txtMaThe_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}