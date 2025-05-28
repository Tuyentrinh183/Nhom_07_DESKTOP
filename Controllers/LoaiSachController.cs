using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class LoaiSachController
    {
        private string ConnStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";

        public List<LoaiSachModel> LayDanhSach()
        {
            var list = new List<LoaiSachModel>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT * FROM LoaiSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LoaiSachModel
                    {
                        MaLoai = reader["MaLoai"].ToString(),
                        TenLoaiSach = reader["TenLoaiSach"].ToString(),
                        GhiChu = reader["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }

        public bool ThemLoai(LoaiSachModel loai)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "INSERT INTO LoaiSach (MaLoai, TenLoaiSach, GhiChu) VALUES (@Ma, @Ten, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", loai.MaLoai);
                cmd.Parameters.AddWithValue("@Ten", loai.TenLoaiSach);
                cmd.Parameters.AddWithValue("@GhiChu", loai.GhiChu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaLoai(LoaiSachModel loai)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "UPDATE LoaiSach SET TenLoaiSach = @Ten, GhiChu = @GhiChu WHERE MaLoai = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", loai.MaLoai);
                cmd.Parameters.AddWithValue("@Ten", loai.TenLoaiSach);
                cmd.Parameters.AddWithValue("@GhiChu", loai.GhiChu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaLoai(string maLoai)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "DELETE FROM LoaiSach WHERE MaLoai = @Ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ma", maLoai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                string query = "SELECT * FROM LoaiSach WHERE TenLoaiSach LIKE @TuKhoa";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
