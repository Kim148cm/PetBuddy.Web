using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetBuddy.Web.Models
{
    public class SanPham
    {
        public int MaSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
    }

}