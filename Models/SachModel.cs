using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    public class SachModel
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string MaTacGia { get; set; }
        public string MaXB { get; set; }
        public string MaLoai { get; set; }
        public int SoTrang { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
    }
}
