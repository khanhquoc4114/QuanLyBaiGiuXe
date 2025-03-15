using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBaiGiuXe.DataAccess
{
    public class LoginManager
    {
        private readonly Connector db = new Connector();

        public LoginManager(){}

        public string UserCheck(string username, string password)
        {
            return ExecuteQuery("SELECT vaiTro FROM TaiKhoan WHERE username = @username AND password = @password", username, password);
        }

        public int? GetID(string username, string password)
        {
            string result = ExecuteQuery("SELECT ID FROM TaiKhoan WHERE username = @username AND password = @password", username, password);
            return result != null ? (int?)Convert.ToInt32(result) : null;
        }

        private string ExecuteQuery(string query, string username, string password)
        {
            using (SqlConnection connection = db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }
    }
}
