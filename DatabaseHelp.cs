using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    public static class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            return conn;
        }
    }
}
