using QuanLyBaiGiuXe.Models;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace QuanLyBaiGiuXe.Helper
{
    class TinhTienManager
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

        public int TinhTienCongVan(DateTime gioVao, DateTime gioRa, int MaLoaiXe)
        {
            try
            {
                DataTable table = manager.GetTinhTienCongVanByID(MaLoaiXe);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    bool thuTienTruoc = row.Field<bool>("ThuTienTruoc");
                    int demTu = row.Field<byte>("DemTu");
                    int demDen = row.Field<byte>("DemDen");
                    int gioGiaoNgayDem = row.Field<byte>("GioGiaoNgayDem");
                    int phuThuTu = row.Field<byte>("PhuThuTu");
                    int phuThuDen = row.Field<byte>("PhuThuDen");

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
                        int soNgay = (int)Math.Ceiling(thoiGianGui.TotalHours / 24.0);
                        tien = soNgay * giaNgayDem;
                    }
                    else
                    {
                        if (vaoNgay && raNgay)
                            tien = giaThuong;
                        else if (!vaoNgay && !raNgay)
                            tien = giaDem;
                        else
                        {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int TinhTienLuyTien(DateTime gioVao, DateTime gioRa, int MaLoaiXe)
        {
            try
            {
                DataTable table = manager.GetTinhTienLuyTienByID(MaLoaiXe);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    byte moc1 = row.Field<byte>("Moc1");
                    int giaMoc1 = row.Field<int>("GiaMoc1");
                    byte moc2 = row.Field<byte>("Moc2");
                    int giaMoc2 = row.Field<int>("GiaMoc2");
                    int giaVuotMoc = row.Field<int>("GiaVuotMoc");
                    byte chuKy = row.Field<byte>("ChuKy");
                    byte congMoc = row.Field<byte>("CongMoc");

                    if (chuKy <= 0) chuKy = 1;

                    int khoangthoigian = (int)Math.Ceiling((gioRa - gioVao).TotalMinutes / 60.0);
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
            } catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin tính tiền lũy tiến: " + ex.Message);
            }
        }

        public int TinhTien(string matinhtien, DateTime giovao, DateTime giora, int maloaixe)
        {
            int khoangthoigian = (int)Math.Ceiling((giora - giovao).TotalMinutes);
            int phutmienphi = manager.GetPhutMienPhiById(maloaixe);
            if (khoangthoigian <= phutmienphi)
            {
                return 0;
            }

            if (matinhtien == "0")
            {
                return TinhTienCongVan(giovao, giora, maloaixe);
            } else if (matinhtien == "1"){
                return TinhTienLuyTien(giovao, giora, maloaixe);
            }
            return 0;
        }
    }
}
