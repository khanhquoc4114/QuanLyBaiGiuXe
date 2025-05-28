using QuanLyBaiGiuXe.Models;
using System;
using System.Data;

namespace QuanLyBaiGiuXe.Helper
{
    class VeLuotManager
    {
        Manager manager = new Manager();

        private bool isMorning(DateTime dt, int demTu, int demDen)
        {
            int gio = dt.Hour;
            if (demTu < demDen)
                return gio < demTu || gio >= demDen;
            else
                return !(gio >= demTu || gio < demDen);
        }

        private double DemGioNgay(DateTime start, DateTime end, int demTu, int demDen)
        {
            double gioNgay = 0;
            for (DateTime t = start; t < end; t = t.AddMinutes(30))
            {
                if (isMorning(t, demTu, demDen))
                    gioNgay += 0.5;
            }
            return gioNgay;
        }

        public int TinhTienCongVan(DateTime gioVao, DateTime gioRa, string MaLoaiXe)
        {
            DataTable table = manager.GetTinhTienCongVanByID(MaLoaiXe);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                int demTu = row.Field<int>("DemTu");
                int demDen = row.Field<int>("DemDen");
                int gioGiaoNgayDem = row.Field<int>("GioGiaoNgayDem");
                int phuThuTu = row.Field<int>("PhuThuTu");
                int phuThuDen = row.Field<int>("PhuThuDen");
                int giaThuong = row.Field<int>("GiaThuong");
                int giaDem = row.Field<int>("GiaDem");
                int giaNgayDem = row.Field<int>("GiaNgayDem");
                int giaPhuThu = row.Field<int>("GiaPhuThu");

                TimeSpan thoiGianGui = gioRa - gioVao;
                int tongGio = (int)Math.Ceiling(thoiGianGui.TotalHours);

                bool vaoNgay = isMorning(gioVao, demTu, demDen);
                bool raNgay = isMorning(gioRa, demTu, demDen);

                // Kiểm tra có phụ thu
                bool coPhuThu = gioRa.Hour >= phuThuTu && gioRa.Hour < phuThuDen;

                int tien = 0;

                if (thoiGianGui.TotalHours > gioGiaoNgayDem || gioRa.Date > gioVao.Date)
                {
                    // Nếu gửi xe nhiều ngày hoặc vượt quá khoảng giao ngày đêm
                    int soNgay = (int)Math.Ceiling(thoiGianGui.TotalHours / 24.0);
                    tien = soNgay * giaNgayDem;
                }
                else
                {
                    // Trong khoảng thời gian ngày hoặc đêm
                    if (vaoNgay && raNgay)
                        tien = giaThuong;
                    else if (!vaoNgay && !raNgay)
                        tien = giaDem;
                    else
                    {
                        // Một đầu ngày, một đầu đêm, xét theo giờ gửi
                        if (thoiGianGui.TotalHours > gioGiaoNgayDem)
                            tien = giaNgayDem;
                        else
                        {
                            double gioNgay = DemGioNgay(gioVao, gioRa, demTu, demDen);
                            double gioDem = tongGio - gioNgay;
                            tien = gioNgay >= gioDem ? giaThuong : giaDem;
                        }
                    }
                }

                if (coPhuThu)
                    tien += giaPhuThu;

                return tien;
            }
            return 0;
        }

        public int TinhTienLuyTien(DateTime gioVao, DateTime gioRa, string MaLoaiXe)
        {
            DataTable table = manager.GetTinhTienLuyTienByID(MaLoaiXe);
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];  // Lấy dòng đầu tiên

                int moc1 = row.Field<int>("Moc1");
                int giaMoc1 = row.Field<int>("GiaMoc1");
                int moc2 = row.Field<int>("Moc2");
                int giaMoc2 = row.Field<int>("GiaMoc2");
                int giaVuotMoc = row.Field<int>("GiaVuotMoc");
                int chuKy = row.Field<int>("ChuKy");
                int congMoc = row.Field<int>("CongMoc");

                if (chuKy <= 0) chuKy = 1;

                int tongThoiGian = (int)Math.Ceiling((gioRa - gioVao).TotalMinutes / 60.0);

                if (tongThoiGian <= moc1)
                {
                    return giaMoc1;
                }
                else if (tongThoiGian <= moc2)
                {
                    return giaMoc1 + giaMoc2;
                }
                else
                {
                    int soChuKy = tongThoiGian / chuKy;
                    int tien = giaVuotMoc * soChuKy;

                    if (congMoc == 1)
                        tien += giaMoc1;
                    else if (congMoc == 2)
                        tien += giaMoc1 + giaMoc2;

                    return tien;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
