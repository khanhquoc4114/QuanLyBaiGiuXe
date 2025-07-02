using QuanLyBaiGiuXe.DataAccess;
using QuanLyBaiGiuXe.Helper;
using QuanLyBaiGiuXe.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace QuanLyBaiGiuXe
{
    internal static class Program
    {
        static int port = 54321;
        static LoginManager loginManager = new LoginManager();
        static Process modelProcess = null;

        [STAThread]
        static void Main()
        {
            KillAllProcessesListeningOnPort(port);
            LoadModel();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SqlConnection conn = new SqlConnection(Session.connectionString))
            {
                conn.Open();
                AppConfig.Load(conn);
            }


            CultureInfo ci = new CultureInfo("vi-VN");
            ci.DateTimeFormat.ShortTimePattern = "HH:mm";
            ci.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            if (Properties.Settings.Default.RememberMe == true)
            {
                string user = Properties.Settings.Default.SavedUsername;
                string pass = Properties.Settings.Default.SavedPassword;
                pass = HashHelper.ComputeSHA256Hash(pass);

                string error = null;
                string maNV = loginManager.GetMaNhanVien(user, pass, out error);
                string name = loginManager.GetTen(user, pass);
                //MessageBox.Show("Nhớ: " + Properties.Settings.Default.RememberMe.ToString() + "\n Mã nhân viên: " + maNV +  error);
                if (!string.IsNullOrEmpty(maNV))
                {
                    Session.MaNhanVien = maNV;
                    Application.Run(new MenuForm()); // mặc định phải là MenuForm()
                    return;
                }
            }
            Application.Run(new DangNhap());
        }

        public static void KillAllProcessesListeningOnPort(int port)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "netstat",
                    Arguments = "-ano",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    var lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    var pids = new HashSet<int>();

                    foreach (var line in lines)
                    {
                        if (line.Contains($":{port}") && line.Contains("LISTENING"))
                        {
                            var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length >= 5 && int.TryParse(tokens[4], out int pid))
                                pids.Add(pid);
                        }
                    }

                    foreach (int pid in pids)
                    {
                        try
                        {
                            var proc = Process.GetProcessById(pid);
                            proc.Kill();
                            proc.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Khong the kill tien trinh {pid}: {ex.Message}");
                        }
                    }

                    if (pids.Count == 0)
                        Console.WriteLine($"Khong co tien trinh dung cong {port}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi kill tien trinh TCP tren port: " + ex.Message);
            }
        }

        private static void LoadModel()
        {
            modelProcess = new Process();
            modelProcess.StartInfo.FileName = "python";
            modelProcess.StartInfo.Arguments = "-u main.py";
            modelProcess.StartInfo.WorkingDirectory = Path.Combine(Application.StartupPath, "python-server");
            modelProcess.StartInfo.UseShellExecute = false;
            modelProcess.StartInfo.CreateNoWindow = true;
            modelProcess.StartInfo.RedirectStandardOutput = true;
            modelProcess.StartInfo.RedirectStandardError = true;

            modelProcess.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[Python STDOUT] " + e.Data);
            };
            modelProcess.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[Python STDERR] " + e.Data);
            };

            modelProcess.Start();
            modelProcess.BeginOutputReadLine();
            modelProcess.BeginErrorReadLine();
        }

        public static void KillModelProcess()
        {
            if (modelProcess != null)
            {
                try
                {
                    if (!modelProcess.HasExited)
                    {
                        modelProcess.Kill();
                        modelProcess.WaitForExit();
                    }
                }
                catch { }
                finally
                {
                    modelProcess.Dispose();
                    modelProcess = null;
                }
            }
        }
    }
}

