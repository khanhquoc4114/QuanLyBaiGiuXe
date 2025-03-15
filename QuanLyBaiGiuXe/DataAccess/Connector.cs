using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyBaiGiuXe.DataAccess
{
    public class Connector : IDisposable
    {
        private readonly string connectionString = "Data Source= localhost;Initial Catalog=testDoXe;Integrated Security=True";
        private SqlConnection connection;

        public Connector()
        {
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            connection.Dispose();
        }
    }
}
