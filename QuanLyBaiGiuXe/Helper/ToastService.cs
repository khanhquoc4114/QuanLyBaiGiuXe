using System.Windows.Forms;

namespace QuanLyBaiGiuXe.Helper
{
    public static class ToastService
    {
        public static void Show(string message, Form parent, int duration = 1000)
        {
            using (var toast = new ToastForm(message, parent, duration))
            {
                toast.ShowDialog();
            }
        }
    }
}
