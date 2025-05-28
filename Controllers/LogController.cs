using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien.Controllers
{
    public class LogController
    {
        private SqlConnection conn = new SqlConnection("Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True");

        public void GhiLog(string tenNguoiDung, string hanhDong)
        {
            try
            {
                string query = "INSERT INTO LogGiaoDich (TenNguoiDung, ThoiGian, HanhDong) VALUES (@Ten, GETDATE(), @HanhDong)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", tenNguoiDung);
                cmd.Parameters.AddWithValue("@HanhDong", hanhDong);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi log: " + ex.Message);
                conn.Close();
            }
        }
    }
}
