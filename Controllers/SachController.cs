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
    public class SachController
    {
        private string ConnStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";

        public List<SachModel> LayDanhSach()
        {
            var ds = new List<SachModel>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT * FROM Sach";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new SachModel
                    {
                        MaSach = reader["MaSach"].ToString(),
                        TenSach = reader["TenSach"].ToString(),
                        MaTacGia = reader["MaTacGia"].ToString(),
                        MaXB = reader["MaXB"].ToString(),
                        MaLoai = reader["MaLoai"].ToString(),
                        SoTrang = Convert.ToInt32(reader["SoTrang"]),
                        GiaBan = Convert.ToDouble(reader["GiaBan"]),
                        SoLuong = Convert.ToInt32(reader["SoLuong"])
                    });
                }
            }
            return ds;
        }

        public bool ThemSach(SachModel sach)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"INSERT INTO Sach(MaSach, TenSach, MaTacGia, MaXB, MaLoai, SoTrang, GiaBan, SoLuong)
                                 VALUES(@MaSach, @TenSach, @MaTacGia, @MaXB, @MaLoai, @SoTrang, @GiaBan, @SoLuong)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", sach.MaSach);
                cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                cmd.Parameters.AddWithValue("@MaTacGia", sach.MaTacGia);
                cmd.Parameters.AddWithValue("@MaXB", sach.MaXB);
                cmd.Parameters.AddWithValue("@MaLoai", sach.MaLoai);
                cmd.Parameters.AddWithValue("@SoTrang", sach.SoTrang);
                cmd.Parameters.AddWithValue("@GiaBan", sach.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuong", sach.SoLuong);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaSach(SachModel sach)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"UPDATE Sach SET TenSach = @TenSach, MaTacGia = @MaTacGia, MaXB = @MaXB, 
                                 MaLoai = @MaLoai, SoTrang = @SoTrang, GiaBan = @GiaBan, SoLuong = @SoLuong
                                 WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", sach.MaSach);
                cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                cmd.Parameters.AddWithValue("@MaTacGia", sach.MaTacGia);
                cmd.Parameters.AddWithValue("@MaXB", sach.MaXB);
                cmd.Parameters.AddWithValue("@MaLoai", sach.MaLoai);
                cmd.Parameters.AddWithValue("@SoTrang", sach.SoTrang);
                cmd.Parameters.AddWithValue("@GiaBan", sach.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuong", sach.SoLuong);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaSach(string maSach)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "DELETE FROM Sach WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable tracuusach(int check_type, string check_value)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia";

                if (check_type == 1)
                {
                    query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia  where S.TenSach like N'%" + check_value + "%' order by Soluong";
                }
                else if (check_type == 2)
                {
                    query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong  from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where LS.TenLoaiSach like N'%" + check_value + "%'";
                }
                else if (check_type == 3)
                {
                    query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where TG.TacGia like N'%" + check_value + "%'";
                }
                else if (check_type == 4)
                {
                    query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where XB.NhaXuatBan like N'%" + check_value + "%'";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }

        public DataTable LayDanhSachMuonTra()
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"SELECT MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach, 
                                 MS.NgayMuon, MS.NgayTra, MS.GhiChu
                                 FROM MuonTraSach MS
                                 JOIN Sach S ON S.MaSach = MS.MaSach
                                 JOIN SinhVien SV ON SV.MaSV = MS.MaSV";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayDanhSachMaSV()
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaSV FROM SinhVien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayDanhSachTenSach()
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT TenSach FROM Sach", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayThongTinSachTheoTen(string tenSach)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"SELECT s.MaSach, s.TenSach, tg.TacGia, s.SoLuong 
                                 FROM Sach s JOIN TacGia tg ON s.MaTacGia = tg.MaTacGia 
                                 WHERE s.TenSach LIKE @TenSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenSach", "%" + tenSach + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable TimKiemMuonTra(string loai, string tuKhoa)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string column = loai == "MaSV" ? "SV.MaSV" : "S.MaSach";
                string query = $@"SELECT MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach, MS.NgayMuon, MS.NgayTra, MS.GhiChu 
                                 FROM MuonTraSach MS
                                 JOIN Sach S ON S.MaSach = MS.MaSach
                                 JOIN SinhVien SV ON SV.MaSV = MS.MaSV
                                 WHERE {column} LIKE @TuKhoa";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemPhieuMuon(MuonTraModel m)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = @"INSERT INTO MuonTraSach(MaSV, MaSach, NgayMuon, NgayTra, GhiChu)
                                 VALUES(@MaSV, @MaSach, @NgayMuon, @NgayTra, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", m.MaSV);
                cmd.Parameters.AddWithValue("@MaSach", m.MaSach);
                cmd.Parameters.AddWithValue("@NgayMuon", m.NgayMuon);
                cmd.Parameters.AddWithValue("@NgayTra", m.NgayTra);
                cmd.Parameters.AddWithValue("@GhiChu", m.GhiChu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool GiaHanPhieu(string maPhieu, DateTime ngayTra)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "UPDATE MuonTraSach SET NgayTra = @NgayTra WHERE MaPhieuMuon = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayTra", ngayTra);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool TraSach(string maPhieu)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "DELETE FROM MuonTraSach WHERE MaPhieuMuon = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CapNhatSoLuong(string maSach, int delta)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "UPDATE Sach SET SoLuong = SoLuong + @Delta WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Delta", delta);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int SoLuongConLai(string maSach)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT SoLuong FROM Sach WHERE MaSach = @MaSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int SoSachDangMuon(string maSV)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM MuonTraSach WHERE MaSV = @MaSV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
