using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class TacGiaController
    {
        private string ConnStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";

        public List<TacGiaModel> LayDanhSach()
        {
            var list = new List<TacGiaModel>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT * FROM TacGia";
                SqlCommand cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new TacGiaModel
                    {
                        MaTG = reader["MaTacGia"].ToString(),   
                        TenTG = reader["TacGia"].ToString(),    
                        GhiChu = reader["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }

        public bool Them(TacGiaModel tg)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "INSERT INTO TacGia (MaTG, TenTG, GhiChu) VALUES (@MaTG, @TenTG, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTG", tg.MaTG);
                cmd.Parameters.AddWithValue("@TenTG", tg.TenTG);
                cmd.Parameters.AddWithValue("@GhiChu", tg.GhiChu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(TacGiaModel tg)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "UPDATE TacGia SET TenTG = @TenTG, GhiChu = @GhiChu WHERE MaTG = @MaTG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTG", tg.MaTG);
                cmd.Parameters.AddWithValue("@TenTG", tg.TenTG);
                cmd.Parameters.AddWithValue("@GhiChu", tg.GhiChu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(string maTG)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "DELETE FROM TacGia WHERE MaTG = @MaTG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTG", maTG);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
