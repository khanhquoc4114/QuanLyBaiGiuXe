using QuanLyBaiGiuXe.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBaiGiuXe.Models
{
    class Manager
    {
        Connector db = new Connector();
        public Manager() { }
        public DataTable GetAllVeThang()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from VeThang", db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
    }
}
