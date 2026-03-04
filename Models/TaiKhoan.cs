using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetBuddy.Web.Models
{
    public class TaiKhoan
    {
        public int MaTaiKhoan { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
