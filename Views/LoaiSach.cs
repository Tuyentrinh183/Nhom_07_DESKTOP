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
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Views 
{
    public partial class LoaiSach : Form
    {
        LoaiSachController controller = new LoaiSachController();
        int bien = 1;
        public LoaiSach()
        {
            InitializeComponent();
        }

        private void LoaiSach_Load(object sender, EventArgs e)
        {
            SetControls(false);
            Display();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLoai.Clear();
            txtLoaiSach.Clear();
            txtGhiChu.Clear();
            txtMaLoai.Focus();
            bien = 1;
            SetControls(true);
        }

        private void dgvLoaiSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaLoai.Text = dgvLoaiSach.Rows[r].Cells[0].Value.ToString();
            txtLoaiSach.Text = dgvLoaiSach.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvLoaiSach.Rows[r].Cells[2].Value.ToString();
        }
        private void Display()
        {
            dgvLoaiSach.DataSource = controller.LayDanhSach();
        }
        private void SetControls(bool edit)
        {
            txtMaLoai.Enabled = edit;
            txtLoaiSach.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtLoaiSach.Focus();
            bien = 2;
            SetControls(true);
            txtMaLoai.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLoaiSach.CurrentRow == null) return;
                DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;

                string ma = dgvLoaiSach.CurrentRow.Cells[0].Value.ToString();
                if (controller.XoaLoai(ma))
                {
                    Display();
                    MessageBox.Show("Xoá thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Không thể xoá.", "Thông báo");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi xoá.", "Thông báo");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtMaLoai.Text == "" || txtLoaiSach.Text == "" || txtGhiChu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            LoaiSachModel loai = new LoaiSachModel
            {
                MaLoai = txtMaLoai.Text,
                TenLoaiSach = txtLoaiSach.Text,
                GhiChu = txtGhiChu.Text
            };

            bool ketQua;
            if (bien == 1)
            {
                foreach (DataGridViewRow row in dgvLoaiSach.Rows)
                {
                    if (txtMaLoai.Text == row.Cells[0].Value?.ToString())
                    {
                        MessageBox.Show("Trùng mã loại. Vui lòng nhập lại.", "Thông báo");
                        return;
                    }
                }
                ketQua = controller.ThemLoai(loai);
            }
            else
            {
                ketQua = controller.SuaLoai(loai);
            }

            if (ketQua)
            {
                Display();
                MessageBox.Show("Lưu thành công", "Thông báo");
                SetControls(false);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thông báo");
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            dgvLoaiSach.DataSource = controller.TimKiem(txtTimKiem.Text);
        }
    }
}
