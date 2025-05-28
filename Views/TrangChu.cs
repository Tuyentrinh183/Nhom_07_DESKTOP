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
using System.IO;
using QuanLyThuVien.Controllers;
using QuanLyThuVien.Models;


namespace QuanLyThuVien.Views
{
    public partial class TrangChu : Form
    {
        private string _message;
        public TrangChu(string Message)
        {
            InitializeComponent();
            _message = Message;
            txtXinChao.Text = _message;
            return;
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {
            dgvSinhVien.DataSource = new SinhVienController().LayDanhSach();
            dgvMuonSach.DataSource = new SachController().LayDanhSachMuonTra();
            dgvTimSach.DataSource = new SachController().tracuusach(0,"");
            dgvQuanLy.DataSource = AccountController.LayDanhSachNhanVien();
            
            cbMaSV.DataSource = new SachController().LayDanhSachMaSV();
            cbMaSV.DisplayMember = "MaSV";
            cbMaSV.ValueMember = "MaSV";

            cbTenSach.DataSource = new SachController().LayDanhSachTenSach();
            cbTenSach.DisplayMember = "TenSach";
            cbTenSach.ValueMember = "TenSach";
          
            LoadThongKe();
        }


        private void btnSachh_Click(object sender, EventArgs e)
        {
            Sach S = new Sach();
            S.Show();
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            LoaiSach LS = new LoaiSach();
            LS.Show();
        }

        private void btnTG_Click(object sender, EventArgs e)
        {
            TacGia TG = new TacGia();
            TG.Show();
        }

        private void btnXBB_Click(object sender, EventArgs e)
        {
            NhaXuatBann XB = new NhaXuatBann();
            XB.Show();
        }

        private void txtTimKiemm_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnTenSach.Checked)
            {
                dgvTimSach.DataSource = new SachController().tracuusach(1, txtTimKiemm.Text);
            }
            if (btnLoaiSach.Checked)
            {
                dgvTimSach.DataSource = new SachController().tracuusach(2, txtTimKiemm.Text);
            }
            if (btnTacGia.Checked)
            {
                dgvTimSach.DataSource = new SachController().tracuusach(3, txtTimKiemm.Text);
            }
            if (btnNXB.Checked)
            {
                dgvTimSach.DataSource = new SachController().tracuusach(4, txtTimKiemm.Text);
            }
        }

        int bien = 1;
        private void btnThemSV_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtNganhHoc.Clear();
            txtSDT.Clear();
            txtMaSinhVien.Focus();
            bien = 1;

            SetControls(true);
        }

        private void SetControls(bool edit)
        {
            txtMaSinhVien.Enabled = edit;
            txtHoTen.Enabled = edit;
            txtKhoa.Enabled = edit;
            txtNganhHoc.Enabled = edit;
            txtSDT.Enabled = edit;
            btnThemSV.Enabled = !edit;
            btnSuaSV.Enabled = !edit;
            btnXoaSV.Enabled = !edit;
            btnGhiSV.Enabled = edit;
            btnHuySV.Enabled = edit;
            //.Enabled = edit;

            cbTenSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayTra.Enabled = edit;
            cbMaSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Enabled = false;
            bien = 2;

            SetControls(true);
            txtMaSinhVien.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;

            string ma = txtMaSinhVien.Text;
            bool result = new SinhVienController().Xoa(ma);
            MessageBox.Show(result ? "Xoá thành công" : "Không thể xoá. Sinh viên đang mượn sách");
            if (result) dgvSinhVien.DataSource = new SinhVienController().LayDanhSach();
        }

        private void btnGhiSV_Click(object sender, EventArgs e)
        {
            if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập lại !!!");
                return;
            }

            SinhVienModel sv = new SinhVienModel
            {
                MaSV = txtMaSinhVien.Text,
                TenSV = txtHoTen.Text,
                NganhHoc = txtNganhHoc.Text,
                KhoaHoc = txtKhoa.Text,
                SoDienThoai = txtSDT.Text
            };

            bool result = bien == 1 ? new SinhVienController().Them(sv) : new SinhVienController().Sua(sv);
            MessageBox.Show(result ? "Thành công" : "Thất bại");

            if (result)
                dgvSinhVien.DataSource = new SinhVienController().LayDanhSach();
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void dgvSinhVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSinhVien.Text = dgvSinhVien.Rows[r].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[r].Cells[1].Value.ToString();
            txtNganhHoc.Text = dgvSinhVien.Rows[r].Cells[2].Value.ToString();
            txtKhoa.Text = dgvSinhVien.Rows[r].Cells[3].Value.ToString();
            txtSDT.Text = dgvSinhVien.Rows[r].Cells[4].Value.ToString();
        }

        private void txtTimKiemSV_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnMSV.Checked)
            {
                dgvSinhVien.DataSource = new SinhVienController().TimKiem("MaSV", txtTimKiemSV.Text.Trim());
            }
            else if (btnTSV.Checked)
            {
                dgvSinhVien.DataSource = new SinhVienController().TimKiem("TenSV", txtTimKiemSV.Text.Trim());
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() == "" || txtTenNV.Text.Trim() == "" || txtSoDienThoai.Text.Trim() == "" || cbGioiTinh.Text.Trim() == "" || txtDiaChi.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Thông tin nhập không được để trống", "Thông báo");
                return;
            }

            if (txtSoDienThoai.Text.Length != 10 || !txtSoDienThoai.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải là 10 chữ số", "Thông báo");
                return;
            }

            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải ít nhất 6 ký tự", "Thông báo");
                return;
            }

            // Tạo đối tượng model
            NhanVienModel nv = new NhanVienModel
            {
                MaNhanVien = txtMaNV.Text.Trim(),
                TenNhanVien = txtTenNV.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                GioiTinh = cbGioiTinh.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim()
            };

            AccountController controller = new AccountController();
            bool result = controller.DangKyTaiKhoan(nv);
            MessageBox.Show(result ? "Đăng ký tài khoản thành công" : "Đăng ký thất bại", "Thông báo");
        }
        

        private void btnDoiPass_Click(object sender, EventArgs e)
        {
            DoiMatKhau DoiPass = new DoiMatKhau();
            DoiPass.Show();
        }
        private void LoadDanhSachMuonTra()
        {
            dgvMuonSach.DataSource = new SachController().LayDanhSachMuonTra();
            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
            ttSoLuong.Enabled = false;
            ttTenTG.Enabled = false;
        }

        private void LoadComboBoxMuonTra()
        {
            cbMaSV.DataSource = new SachController().LayDanhSachMaSV();
            cbMaSV.DisplayMember = "MaSV";
            cbMaSV.ValueMember = "MaSV";

            cbTenSach.DataSource = new SachController().LayDanhSachTenSach();
            cbTenSach.DisplayMember = "TenSach";
            cbTenSach.ValueMember = "TenSach";
        }
       
        
        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new SachController().LayThongTinSachTheoTen(cbTenSach.Text);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ttMaSach.Text = dr["MaSach"].ToString();
                ttTenSach.Text = dr["TenSach"].ToString();
                ttTenTG.Text = dr["TacGia"].ToString();
                ttSoLuong.Text = dr["SoLuong"].ToString();
            }
        }

        private void txtTimKiemSachMuon_KeyUp(object sender, KeyEventArgs e)
        {
            if (raMaSV.Checked)
            {
                dgvMuonSach.DataSource = new SachController().TimKiemMuonTra("MaSV", txtTimKiemSachMuon.Text);
            }
            else if (raMaSach.Checked)
            {
                dgvMuonSach.DataSource = new SachController().TimKiemMuonTra("MaSach", txtTimKiemSachMuon.Text);
            }
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            bien = 5;
            cbTenSach.Focus();
            SetControls(true);
            cbNgayMuon.Visible = false;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            bien = 6;
            SetControls(true);
            txtMaPhieuMUon.Enabled = false;
            cbTenSach.Enabled = false;
            cbMaSV.Enabled = false;
            txtGhiChu.Enabled = false;
            cbNgayMuon.Enabled = false;
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn trả sách?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            int row = dgvMuonSach.CurrentRow.Index;
            string maPhieu = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
            string maSach = dgvMuonSach.Rows[row].Cells[3].Value.ToString();
            string tenSach = dgvMuonSach.Rows[row].Cells[4].Value.ToString();
            string maSV = dgvMuonSach.Rows[row].Cells[1].Value.ToString();

            if (new SachController().TraSach(maPhieu))
            {
                new SachController().CapNhatSoLuong(maSach, +1);
                MessageBox.Show("Trả sách thành công");
                new LogController().GhiLog(_message, $"Trả sách: {tenSach} - SV: {maSV}");
                LoadDanhSachMuonTra();
            }
            else
            {
                MessageBox.Show("Trả sách thất bại");
            }


        }
        private void btnGhii_Click(object sender, EventArgs e)
        {
            int soNgay = (cbNgayTra.Value - DateTime.Now).Days;
            if (soNgay <= 0)
            {
                MessageBox.Show("Thời gian trả không hợp lệ");
                return;
            }

            if (bien == 5)
            {
                string maSV = cbMaSV.Text;
                string maSach = ttMaSach.Text;
                string tenSach = ttTenSach.Text;
                string ghiChu = txtGhiChu.Text;

                SachController sach = new SachController();

                int soLuongCon = sach.SoLuongConLai(maSach);
                if (soLuongCon <= 0)
                {
                    MessageBox.Show("Không còn sách để mượn");
                    return;
                }

                int daMuon = sach.SoSachDangMuon(maSV);
                if (daMuon >= 3)
                {
                    MessageBox.Show("Sinh viên đã mượn đủ 3 sách");
                    return;
                }

                MuonTraModel m = new MuonTraModel
                {
                    MaSV = maSV,
                    MaSach = maSach,
                    NgayMuon = DateTime.Now,
                    NgayTra = cbNgayTra.Value,
                    GhiChu = ghiChu
                };

                if (sach.ThemPhieuMuon(m))
                {
                    sach.CapNhatSoLuong(maSach, -1);
                    MessageBox.Show("Mượn sách thành công");
                    new LogController().GhiLog(_message, $"Mượn sách: {tenSach} - SV: {maSV}");
                    LoadDanhSachMuonTra();
                    SetControls(false);
                }
            }
            else if (bien == 6)
            {
                int row = dgvMuonSach.CurrentRow.Index;
                string maPhieu = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
                string tenSach = dgvMuonSach.Rows[row].Cells[4].Value.ToString();
                string maSV = dgvMuonSach.Rows[row].Cells[1].Value.ToString();

                if (new SachController().GiaHanPhieu(maPhieu, cbNgayTra.Value))
                {
        
                    MessageBox.Show("Gia hạn thành công");
                    new LogController().GhiLog(_message, $"Gia hạn sách: {tenSach} - SV: {maSV}");
                    LoadDanhSachMuonTra();
                    SetControls(false);
                }
            }
        }

        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
        
        private void dgvMuonSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaPhieuMUon.Text = dgvMuonSach.Rows[r].Cells[0].Value.ToString();
            cbMaSV.Text = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            cbTenSach.Text = dgvMuonSach.Rows[r].Cells[4].Value.ToString();
            cbNgayMuon.Text = dgvMuonSach.Rows[r].Cells[5].Value.ToString();
            cbNgayTra.Text = dgvMuonSach.Rows[r].Cells[6].Value.ToString();
            txtGhiChu.Text = dgvMuonSach.Rows[r].Cells[7].Value.ToString();
        }
        public void LoadThongKe()
        {
            var thongke = new ThongKe_BaoCaoController();

            dgvDSMuon.DataSource = thongke.LayDanhSachDangMuon();
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();

            TkAdmin.Text = thongke.DemBang("NhanVien").ToString();
            TkSinhVien.Text = thongke.DemBang("SinhVien").ToString();
            TkSach.Text = thongke.DemBang("Sach").ToString();
            TkMuonSach.Text = thongke.DemBang("MuonTraSach").ToString();
            TKLoaiSach.Text = thongke.DemBang("LoaiSach").ToString();
            TKTacGia.Text = thongke.DemBang("TacGia").ToString();
            TKNhaXB.Text = thongke.DemBang("NhaXuatBan").ToString();
        }
        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            dgvDSMuon.DataSource = ThongKe_BaoCaoController.BaoCaoQuaHanTheoSinhVien();
            lbdangmuon.Visible = false;
            lbquahan.Visible = true;
            lbTong.Text = "Tổng SV quá hạn: " + dgvDSMuon.RowCount;
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            dgvDSMuon.DataSource = ThongKe_BaoCaoController.BaoCaoSoLuongMuonSinhVienTheoThang();
            lbdangmuon.Visible = false;
            lbquahan.Visible = false;
            lbTong.Text = "Số dòng thống kê: " + dgvDSMuon.RowCount;
        }

        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sinhVien = new SinhVienController().LayThongTinTheoMaSV(cbMaSV.Text);
            if (sinhVien != null)
            {
                ttMaSV.Text = sinhVien.MaSV;
                ttTenSV.Text = sinhVien.TenSV;
            }
        }

        private void dgvDSMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;

            new AccountController().DangXuat(_message); 

            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (dgvQuanLy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo");
                return;
            }

            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;

            string maNV = dgvQuanLy.CurrentRow.Cells["MaNhanVien"].Value.ToString();

            bool ketQua = AccountController.XoaNhanVien(maNV);
            if (ketQua)
            {
                MessageBox.Show("Xóa thành công.", "Thông báo");
                dgvQuanLy.DataSource = AccountController.LayDanhSachNhanVien(); 
            }
            else
            {
                MessageBox.Show("Không thể xóa nhân viên.", "Lỗi");
            }
        }

        
        private void btnHieuSuatSach_Click(object sender, EventArgs e)
        {
            dgvDSMuon.AutoGenerateColumns = true;
            dgvDSMuon.DataSource = ThongKe_BaoCaoController.BaoCaoHieuSuatSach();
            lbTong.Text = "Tổng: " + dgvDSMuon.RowCount + " sách";

            lbHieuSuatSach.Visible = true;
            lbquahan.Visible = false;
            lbdangmuon.Visible = false;
        }

        private void ExportDataGridViewToCSV(DataGridView dgv, string tenFileMacDinh)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file (*.csv)|*.csv";
            sfd.FileName = tenFileMacDinh + ".csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    // Header
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        sb.Append(dgv.Columns[i].HeaderText);
                        if (i < dgv.Columns.Count - 1) sb.Append(",");
                    }
                    sb.AppendLine();

                    // Rows
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                sb.Append(row.Cells[i].Value?.ToString());
                                if (i < dgv.Columns.Count - 1) sb.Append(",");
                            }
                            sb.AppendLine();
                        }
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuất báo cáo thành công!", "Thông báo");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            string tenFile = "BaoCao";

            if (lbquahan.Visible)
                tenFile = "BaoCao_DanhSachQuaHan";
            else if (lbdangmuon.Visible)
                tenFile = "BaoCao_DangMuon";
            else if (lbHieuSuatSach.Visible)
                tenFile = "BaoCao_HieuSuatSach";
            else
            {
                MessageBox.Show("Không xác định được loại báo cáo để xuất!", "Thông báo");
                return;
            }
            ExportDataGridViewToCSV(dgvDSMuon, tenFile);
            new LogController().GhiLog(_message, "Xuất báo cáo: " + tenFile);

        }
        
    }
}
