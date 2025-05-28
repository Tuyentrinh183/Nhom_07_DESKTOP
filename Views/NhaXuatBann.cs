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
using QuanLyThuVien.Models;
using QuanLyThuVien.Controllers;

namespace QuanLyThuVien.Views 
{
    public partial class NhaXuatBann : Form
    {
        NhaXuatBanController controller = new NhaXuatBanController();
        int bien = 1;
        public NhaXuatBann()
        {
            InitializeComponent();
        }

        private void NhaXuatBann_Load(object sender, EventArgs e)
        {
            SetControls(false);
            Display();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
        private void Display()
        {
            dgvNXB.DataSource = controller.LayDanhSach();
        }
        private void SetControls(bool edit)
        {
            txtMaXB.Enabled = edit;
            txtTenXB.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void dgvNXB_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaXB.Text = dgvNXB.Rows[r].Cells[0].Value.ToString();
            txtTenXB.Text = dgvNXB.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvNXB.Rows[r].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaXB.Clear();
            txtTenXB.Clear();
            txtGhiChu.Clear();
            txtMaXB.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenXB.Focus();
            bien = 2;
            SetControls(true);
            txtMaXB.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;

            int row = dgvNXB.CurrentRow.Index;
            string ma = dgvNXB.Rows[row].Cells[0].Value.ToString();

            if (controller.Xoa(ma))
            {
                Display();
                MessageBox.Show("Xóa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Không thể xóa", "Lỗi");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtMaXB.Text == "" || txtTenXB.Text == "" || txtGhiChu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            var nxb = new NhaXuatBanModel
            {
                MaXB = txtMaXB.Text,
                NhaXuatBan = txtTenXB.Text,
                GhiChu = txtGhiChu.Text
            };

            if (bien == 1)
            {
                // kiểm tra trùng mã
                foreach (DataGridViewRow r in dgvNXB.Rows)
                {
                    if (r.Cells[0].Value?.ToString() == txtMaXB.Text)
                    {
                        MessageBox.Show("Trùng mã nhà xuất bản");
                        return;
                    }
                }

                if (controller.Them(nxb))
                {
                    Display();
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
            }
            else if (bien == 2)
            {
                if (controller.Sua(nxb))
                {
                    Display();
                    MessageBox.Show("Sửa thành công", "Thông báo");
                }
            }

            SetControls(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower();
            var all = controller.LayDanhSach();
            dgvNXB.DataSource = all.Where(x => x.NhaXuatBan.ToLower().Contains(keyword)).ToList();
        }
    }
}
