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
    public partial class TacGia : Form
    {
        TacGiaController controller = new TacGiaController();
        int bien = 1;
        public TacGia()
        {
            InitializeComponent();
        }

        private void TacGia_Load(object sender, EventArgs e)
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
            dgvTacGia.DataSource = controller.LayDanhSach();
        }
        private void dgvTacGia_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaTacGia.Text = dgvTacGia.Rows[r].Cells[0].Value.ToString();
            txtTenTacGia.Text = dgvTacGia.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvTacGia.Rows[r].Cells[2].Value.ToString();
        }
        private void SetControls(bool edit)
        {
            txtMaTacGia.Enabled = edit;
            txtTenTacGia.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaTacGia.Clear();
            txtTenTacGia.Clear();
            txtGhiChu.Clear();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenTacGia.Focus();
            bien = 2;
            SetControls(true);
            txtMaTacGia.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Thông báo", MessageBoxButtons.YesNo);
            if (dlr == DialogResult.No) return;

            string maTG = txtMaTacGia.Text;
            if (controller.Xoa(maTG))
            {
                MessageBox.Show("Xoá thành công");
                Display();
            }
            else
            {
                MessageBox.Show("Xoá thất bại");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (txtMaTacGia.Text == "" || txtTenTacGia.Text == "" || txtGhiChu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            TacGiaModel tg = new TacGiaModel
            {
                MaTG = txtMaTacGia.Text,
                TenTG = txtTenTacGia.Text,
                GhiChu = txtGhiChu.Text
            };

            bool result = false;
            if (bien == 1)
            {
                result = controller.Them(tg);
                if (result) MessageBox.Show("Thêm thành công");
            }
            else
            {
                result = controller.Sua(tg);
                if (result) MessageBox.Show("Cập nhật thành công");
            }

            if (result)
            {
                Display();
                SetControls(false);
            }
            else
            {
                MessageBox.Show("Thao tác thất bại");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower();
            var list = controller.LayDanhSach();
            dgvTacGia.DataSource = list.Where(t => t.TenTG.ToLower().Contains(keyword)).ToList();
        }
    }
}
