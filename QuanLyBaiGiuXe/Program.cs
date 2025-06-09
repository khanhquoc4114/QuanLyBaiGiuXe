using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //RestartAsAdmin();
            //Application.ApplicationExit += (s, e) => KillProcessListeningOnPort(8080);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DangNhap());
        }


        public static void RestartAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                var exeName = Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName)
                {
                    UseShellExecute = true,
                    Verb = "runas" // This triggers UAC
                };
                Process.Start(startInfo);
                Environment.Exit(0);
            }
        }

        //public static void KillProcessListeningOnPort(int port)
        //{
        //    try
        //    {
        //        var ipProps = IPGlobalProperties.GetIPGlobalProperties();
        //        var tcpConnections = ipProps.GetActiveTcpListeners();

        //        bool portInUse = tcpConnections.Any(ep => ep.Port == port);
        //        if (!portInUse)
        //        {
        //            Console.WriteLine($"Không có tiến trình nào đang listen trên cổng {port}.");
        //            return;
        //        }

        //        var processStartInfo = new ProcessStartInfo
        //        {
        //            FileName = "netstat",
        //            Arguments = "-ano",
        //            RedirectStandardOutput = true,
        //            UseShellExecute = false,
        //            CreateNoWindow = true
        //        };

        //        using (var process = Process.Start(processStartInfo))
        //        {
        //            string output = process.StandardOutput.ReadToEnd();
        //            process.WaitForExit();

        //            var lines = output.Split('\n');
        //            foreach (var line in lines)
        //            {
        //                if (line.Trim().StartsWith("TCP") && line.Contains($":{port}"))
        //                {
        //                    string[] tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (tokens.Length >= 5 && tokens[3].Equals("LISTENING", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        int pid = int.Parse(tokens[4]);
        //                        Console.WriteLine($"Đang kill PID: {pid} trên cổng {port}");

        //                        var proc = Process.GetProcessById(pid);
        //                        proc.Kill();
        //                        Console.WriteLine("Đã kill tiến trình.");
        //                        return;
        //                    }
        //                }
        //            }
        //        }

        //        Console.WriteLine("Không tìm thấy tiến trình phù hợp.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi: {ex.Message}");
        //    }
        //}
    }
}
