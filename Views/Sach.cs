using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Controllers;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Views
{
    public partial class Sach : Form
    {
        SachController sachController = new SachController();
        string Conn = "Data Source=DESKTOP-020SF26\\MEOMEO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public Sach()
        {
            InitializeComponent();
        }
        private void Sach_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            SetControls(false);
            loadComboBox();
            Display();
        }
        private void Display()
        {
            dgvSach.DataSource = sachController.LayDanhSach();
            //dgvSach.Columns[0].HeaderText = "Mã Sách";
            //dgvSach.Columns[0].Width = 55;
            //dgvSach.Columns[1].HeaderText = "Tên Sách";
            //dgvSach.Columns[1].Width = 90;
            //dgvSach.Columns[2].HeaderText = "Tác Giả";
            //dgvSach.Columns[2].Width = 80;
            //dgvSach.Columns[3].HeaderText = "Nhà Xuất Bản";
            //dgvSach.Columns[3].Width = 80;
            //dgvSach.Columns[4].HeaderText = "Giá Bán";
            //dgvSach.Columns[4].Width = 50;
            //dgvSach.Columns[5].HeaderText = "Số Lượng";
            //dgvSach.Columns[5].Width = 60;
        }

        private void dgvSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSach.Text = dgvSach.Rows[r].Cells[0].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[r].Cells[1].Value.ToString();
            cbTacGia.Text = dgvSach.Rows[r].Cells[2].Value.ToString();
            cbNhaXB.Text = dgvSach.Rows[r].Cells[3].Value.ToString();
            cbLoaiSach.Text = dgvSach.Rows[r].Cells[4].Value.ToString();
            txtSoTrang.Text = dgvSach.Rows[r].Cells[5].Value.ToString();
            txtGiaBan.Text = dgvSach.Rows[r].Cells[6].Value.ToString();
            txtSoLuong.Text = dgvSach.Rows[r].Cells[7].Value.ToString();
        }
        public void loadComboBox()
        {
            string sSql1 = "select MaTacGia from TacGia";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cbTacGia.Items.Add(dr[0].ToString());
            }

            string sSql2 = "select MaXB from NhaXuatBan";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                cbNhaXB.Items.Add(dr[0].ToString());
            }

            string sSql3 = "select MaLoai from LoaiSach";
            mySqlCommand = new SqlCommand(sSql3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(mySqlCommand);
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                cbLoaiSach.Items.Add(dr[0].ToString());
            }
        }
  

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtTimSach_KeyUp(object sender, KeyEventArgs e)
        {
            string query = "select * from Sach where TenSach like N'%" + txtTimSach.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSach.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtSoLuong.Clear();
            txtSoTrang.Clear();
            txtGiaBan.Clear();
            txtMaSach.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenSach.Focus();
            bien = 2;
            SetControls(true);
            txtMaSach.Enabled = false;
        }
        private void SetControls(bool edit)
        {
            txtMaSach.Enabled = edit;
            txtTenSach.Enabled = edit;
            txtSoLuong.Enabled = edit;
            txtSoTrang.Enabled = edit;
            txtGiaBan.Enabled = edit;
            cbLoaiSach.Enabled = edit;
            cbNhaXB.Enabled = edit;
            cbTacGia.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            int row = dgvSach.CurrentRow.Index;
            string maSach = dgvSach.Rows[row].Cells[0].Value.ToString();

            if (sachController.XoaSach(maSach))
            {
                MessageBox.Show("Xoá sách thành công", "Thông báo");
                Display();
            }
            else
            {
                MessageBox.Show("Xoá sách thất bại", "Lỗi");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            SachModel sach = new SachModel
            {
                MaSach = txtMaSach.Text,
                TenSach = txtTenSach.Text,
                MaTacGia = cbTacGia.Text,
                MaXB = cbNhaXB.Text,
                MaLoai = cbLoaiSach.Text,
                SoTrang = Convert.ToInt32(txtSoTrang.Text),
                GiaBan = Convert.ToDouble(txtGiaBan.Text),
                SoLuong = Convert.ToInt32(txtSoLuong.Text)
            };

            if (bien == 1) // Thêm
            {
                if (sachController.ThemSach(sach))
                {
                    MessageBox.Show("Thêm sách thành công", "Thông báo");
                    Display();
                }
                else
                {
                    MessageBox.Show("Thêm sách thất bại", "Lỗi");
                }
            }
            else // Sửa
            {
                if (sachController.SuaSach(sach))
                {
                    MessageBox.Show("Sửa sách thành công", "Thông báo");
                    Display();
                }
                else
                {
                    MessageBox.Show("Sửa sách thất bại", "Lỗi");
                }
            }
            SetControls(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void Homee_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
