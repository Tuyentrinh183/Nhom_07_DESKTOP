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
using static System.Net.Mime.MediaTypeNames;
using QuanLyThuVien.Controllers;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtTaiKhoan.Enabled = true;
            txtMatKhau.Enabled = true;
        }
 
        private void btnLamMoi_Click(object sender, EventArgs e)
        {

            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            this.txtTaiKhoan_Leave(sender, e);
            this.txtMatKhau_Leave(sender,e);       
            txtTaiKhoan.Enabled = true;
            txtMatKhau.Enabled = true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            AccountController newLogin = new AccountController();
            NhanVienModel account;
            bool dangNhap = newLogin.KiemTraDangNhap(
                txtTaiKhoan.Text, 
                txtMatKhau.Text,
                out account
                );

            if (!dangNhap)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công. Xin chào " + account.TenNhanVien);
                this.Hide();
                TrangChu home = new TrangChu(account.TenNhanVien);
                home.Show();
            }
            return;

        }

        private void txtTaiKhoan_Enter(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "Nhập Tên Tài Khoản")
            {
                txtTaiKhoan.Text = "";
                txtTaiKhoan.ForeColor = Color.Black;
            }
        }
        private void txtTaiKhoan_Leave(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "")
            {
                txtTaiKhoan.Text = "Nhập Tên Tài Khoản";
                txtTaiKhoan.ForeColor = Color.LightGray;
            }
        }
        private void txtMatKhau_Enter(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Nhập Mật Khẩu")
            {
                txtMatKhau.Text = "";
                txtMatKhau.ForeColor = Color.Black;
            }
        }
        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "")
            {
                txtMatKhau.Text = "Nhập Mật Khẩu";
                txtMatKhau.ForeColor = Color.LightGray;
            }
        }

        
    }
}
