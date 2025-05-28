using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class AccountController
    {
        private string connStr = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";   
        
        public bool KiemTraDangNhap(string username, string password, out NhanVienModel account)
        {
            account = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM NhanVien WHERE MaNhanVien = @user AND MatKhau = @pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = cmd.ExecuteReader();

                bool is_success = reader.Read() ? true : false;
                if (is_success)
                {
                    account = new NhanVienModel
                    {
                        MaNhanVien = reader["MaNhanVien"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        TenNhanVien = reader["TenNhanVien"].ToString()
                    };
                    this.GhiLog(account.TenNhanVien);
                }
                return is_success;
            }
        }
        public bool KiemTraMatKhauCu(string maNV, string matKhauCu)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNV AND MatKhau = @MatKhauCu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@MatKhauCu", matKhauCu);

                int count = (int)cmd.ExecuteScalar();
                return count == 1;
            }
        }

        public bool DoiMatKhau(string maNV, string matKhauMoi)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE NhanVien SET MatKhau = @MatKhauMoi WHERE MaNhanVien = @MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DangKyTaiKhoan(NhanVienModel nv)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"INSERT INTO NhanVien (MaNhanVien, TenNhanVien, SoDienThoai, GioiTinh, DiaChi, MatKhau)
                         VALUES (@MaNV, @TenNV, @SoDT, @GioiTinh, @DiaChi, @MatKhau)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNhanVien);
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("@SoDT", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("@MatKhau", nv.MatKhau);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public void DangXuat(string tenNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO LogGiaoDich (TenNguoiDung, ThoiGian, HanhDong) VALUES (@Ten, GETDATE(), @HanhDong)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", tenNguoiDung);
                cmd.Parameters.AddWithValue("@HanhDong", "Đăng xuất hệ thống");
                cmd.ExecuteNonQuery();
            }
        }
        public static bool XoaNhanVien(string maNV)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }
        private void GhiLog(string tenNV)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO LogGiaoDich (TenNguoiDung, ThoiGian, HanhDong) VALUES (@Ten, GETDATE(), @HanhDong)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", tenNV);
                cmd.Parameters.AddWithValue("@HanhDong", "Đăng nhập hệ thống");
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable LayDanhSachNhanVien()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM NhanVien";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
