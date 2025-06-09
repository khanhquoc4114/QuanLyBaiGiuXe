using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBaiGiuXe.Models
{
    public class LoaiXeItem
    {
        public int MaLoaiXe { get; set; }
        public string TenLoaiXe { get; set; }

        public override string ToString() => TenLoaiXe;
    }
}
