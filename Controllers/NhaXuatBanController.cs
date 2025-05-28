using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
        public class NhaXuatBanController
        {
            private string ConnStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";

            public List<NhaXuatBanModel> LayDanhSach()
            {
                var ds = new List<NhaXuatBanModel>();
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM NhaXuatBan";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ds.Add(new NhaXuatBanModel
                        {
                            MaXB = reader["MaXB"].ToString(),
                            NhaXuatBan = reader["NhaXuatBan"].ToString(),
                            GhiChu = reader["GhiChu"].ToString()
                        });
                    }
                }
                return ds;
            }

            public bool Them(NhaXuatBanModel nxb)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "INSERT INTO NhaXuatBan (MaXB, NhaXuatBan, GhiChu) VALUES (@MaXB, @Ten, @GhiChu)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaXB", nxb.MaXB);
                    cmd.Parameters.AddWithValue("@Ten", nxb.NhaXuatBan);
                    cmd.Parameters.AddWithValue("@GhiChu", nxb.GhiChu);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Sua(NhaXuatBanModel nxb)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "UPDATE NhaXuatBan SET NhaXuatBan = @Ten, GhiChu = @GhiChu WHERE MaXB = @MaXB";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaXB", nxb.MaXB);
                    cmd.Parameters.AddWithValue("@Ten", nxb.NhaXuatBan);
                    cmd.Parameters.AddWithValue("@GhiChu", nxb.GhiChu);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

            public bool Xoa(string maXB)
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    string query = "DELETE FROM NhaXuatBan WHERE MaXB = @MaXB";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaXB", maXB);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
    }
}
