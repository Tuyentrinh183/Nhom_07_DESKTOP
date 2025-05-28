using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Controllers
{
    public class ThongKe_BaoCaoController
    {
        private SqlConnection conn = new SqlConnection("Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True");

        public DataTable LayDanhSachDangMuon()
        {
            string query = @"SELECT MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach, 
                             MS.NgayMuon, MS.NgayTra, MS.GhiChu
                             FROM MuonTraSach MS
                             JOIN Sach S ON S.MaSach = MS.MaSach
                             JOIN SinhVien SV ON SV.MaSV = MS.MaSV
                             WHERE MS.NgayTra > CONVERT(date, GETDATE())";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int DemBang(string tenBang)
        {
            string query = $"SELECT COUNT(*) FROM {tenBang}";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count;
        }

        public static DataTable BaoCaoQuaHanTheoSinhVien()
        {
            string query = @"
        SELECT 
            SV.MaSV,
            SV.TenSV,
            SV.SoDienThoai,
            COUNT(MS.MaPhieuMuon) AS SoLuongQuaHan
        FROM MuonTraSach MS
        JOIN Sach S ON S.MaSach = MS.MaSach
        JOIN SinhVien SV ON SV.MaSV = MS.MaSV
        WHERE MS.NgayTra <= CONVERT(date, GETDATE())
        GROUP BY SV.MaSV, SV.TenSV, SV.SoDienThoai
        ORDER BY SoLuongQuaHan DESC
    ";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable BaoCaoSoLuongMuonSinhVienTheoThang()
        {
            string query = @"
        SELECT 
            SV.MaSV,
            SV.TenSV,
            FORMAT(MS.NgayMuon, 'yyyy-MM') AS Thang,
            COUNT(*) AS SoLuongMuon
        FROM MuonTraSach MS
        JOIN SinhVien SV ON MS.MaSV = SV.MaSV
        GROUP BY SV.MaSV, SV.TenSV, FORMAT(MS.NgayMuon, 'yyyy-MM')
        ORDER BY Thang, SoLuongMuon DESC ";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable BaoCaoHieuSuatSach()
        {
            string query = @"
        SELECT 
            S.MaSach,
            S.TenSach,
            COUNT(MS.MaSach) AS SoLuotMuon
        FROM 
            Sach S
        LEFT JOIN 
            MuonTraSach MS ON S.MaSach = MS.MaSach
        GROUP BY 
            S.MaSach, S.TenSach
        ORDER BY 
            SoLuotMuon DESC";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
    }
