using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Controllers;

namespace QuanLyThuVien.Views
{
    public partial class DoiMatKhau : Form
    {
        string Conn = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            txtTKDoi.Focus();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string maNV = txtTKDoi.Text.Trim();
            string matKhauCu = txtMKCu.Text.Trim();
            string matKhauMoi = txtMKMoi.Text.Trim();

            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự", "Thông báo");
                return;
            }

            if (matKhauMoi.Any(c => !char.IsLetterOrDigit(c)))
            {
                MessageBox.Show("Mật khẩu mới có ký tự không hợp lệ. Vui lòng nhập lại", "Thông báo");
                return;
            }

            AccountController accountCtrl = new AccountController();

            if (!accountCtrl.KiemTraMatKhauCu(maNV, matKhauCu))
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu cũ không đúng", "Thông báo");
                return;
            }

            if (accountCtrl.DoiMatKhau(maNV, matKhauMoi))
            {
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                Clear(); // Gọi hàm xóa dữ liệu form nếu có
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại", "Thông báo");
            }
        }
        
        public void Clear()
        {
            txtTKDoi.Text = "";
            txtMKMoi.Text = "";
            txtMKCu.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
