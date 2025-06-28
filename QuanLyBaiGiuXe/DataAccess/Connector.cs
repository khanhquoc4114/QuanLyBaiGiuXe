using System;
using System.Data.SqlClient;
using System.Data;
using QuanLyBaiGiuXe.Helper;


namespace QuanLyBaiGiuXe.DataAccess
{
    public class Connector : IDisposable
    {
        private readonly string connectionString = Session.connectionString;
        private SqlConnection connection;

        public Connector()
        {
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
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
