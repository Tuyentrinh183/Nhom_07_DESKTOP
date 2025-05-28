using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class SinhVienController
    {
            private string ConnStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";

            public List<SinhVienModel> LayDanhSach()
            {
                var ds = new List<SinhVienModel>();
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM SinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ds.Add(new SinhVienModel
                        {
                            MaSV = reader["MaSV"].ToString(),
                            TenSV = reader["TenSV"].ToString(),
                            NganhHoc = reader["NganhHoc"].ToString(),
                            KhoaHoc = reader["KhoaHoc"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString()
                        });
                    }
                }
                return ds;
            }

            public bool Them(SinhVienModel sv)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "INSERT INTO SinhVien(MaSV, TenSV, NganhHoc, KhoaHoc, SoDienThoai) VALUES(@MaSV, @TenSV, @NganhHoc, @KhoaHoc, @SoDienThoai)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                    cmd.Parameters.AddWithValue("@TenSV", sv.TenSV);
                    cmd.Parameters.AddWithValue("@NganhHoc", sv.NganhHoc);
                    cmd.Parameters.AddWithValue("@KhoaHoc", sv.KhoaHoc);
                    cmd.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Sua(SinhVienModel sv)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "UPDATE SinhVien SET TenSV = @TenSV, NganhHoc = @NganhHoc, KhoaHoc = @KhoaHoc, SoDienThoai = @SoDienThoai WHERE MaSV = @MaSV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                    cmd.Parameters.AddWithValue("@TenSV", sv.TenSV);
                    cmd.Parameters.AddWithValue("@NganhHoc", sv.NganhHoc);
                    cmd.Parameters.AddWithValue("@KhoaHoc", sv.KhoaHoc);
                    cmd.Parameters.AddWithValue("@SoDienThoai", sv.SoDienThoai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Xoa(string maSV)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public List<SinhVienModel> TimKiem(string cot, string tukhoa)
            {
                var list = new List<SinhVienModel>();
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = $"SELECT * FROM SinhVien WHERE {cot} LIKE @key";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@key", "%" + tukhoa + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new SinhVienModel
                        {
                            MaSV = reader["MaSV"].ToString(),
                            TenSV = reader["TenSV"].ToString(),
                            NganhHoc = reader["NganhHoc"].ToString(),
                            KhoaHoc = reader["KhoaHoc"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString()
                        });
                    }
                }
                return list;
            }
            public SinhVienModel LayThongTinTheoMaSV(string maSV)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT MaSV, TenSV FROM SinhVien WHERE MaSV = @MaSV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new SinhVienModel
                    {
                        MaSV = reader["MaSV"].ToString(),
                        TenSV = reader["TenSV"].ToString()
                    };
                }

                return null;
            }
        }

    }
}
