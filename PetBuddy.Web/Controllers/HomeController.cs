using PetBuddy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetBuddy.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string keyword)
        {
            List<SanPham> list = new List<SanPham>();

            if (string.IsNullOrEmpty(keyword))
                return View(list);

            string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT * FROM SanPham
                         WHERE TenSanPham LIKE @keyword
                         OR MoTa LIKE @keyword";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SanPham sp = new SanPham();
                    sp.MaSanPham = (int)reader["MaSanPham"];
                    sp.MaDanhMuc = (int)reader["MaDanhMuc"];
                    sp.TenSanPham = reader["TenSanPham"].ToString();
                    sp.Gia = (decimal)reader["Gia"];
                    sp.SoLuong = (int)reader["SoLuong"];
                    sp.MoTa = reader["MoTa"].ToString();

                    list.Add(sp);
                }
            }

            ViewBag.Keyword = keyword;
            return View(list);
        }



    }
}