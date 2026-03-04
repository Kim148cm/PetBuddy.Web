using PetBuddy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace PetBuddy.Web.Controllers
{
    public class SanPhamController : Controller
    {
        string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";

        // ================= DANH SÁCH TẤT CẢ =================
        public ActionResult Index()
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSanPham = (int)reader["MaSanPham"],
                        MaDanhMuc = (int)reader["MaDanhMuc"],
                        TenSanPham = reader["TenSanPham"].ToString(),
                        Gia = (decimal)reader["Gia"],
                        SoLuong = (int)reader["SoLuong"],
                        MoTa = reader["MoTa"].ToString()
                    });
                }
            }

            return View(list);
        }

        // ================= THEO DANH MỤC =================
        public ActionResult DanhMuc(int id)
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE MaDanhMuc=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSanPham = (int)reader["MaSanPham"],
                        MaDanhMuc = (int)reader["MaDanhMuc"],
                        TenSanPham = reader["TenSanPham"].ToString(),
                        Gia = (decimal)reader["Gia"],
                        SoLuong = (int)reader["SoLuong"],
                        MoTa = reader["MoTa"].ToString()
                    });
                }
            }

            return View("Index", list);
        }

        // ================= CHI TIẾT =================
        public ActionResult ChiTiet(int id)
        {
            SanPham sp = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE MaSanPham=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    sp = new SanPham
                    {
                        MaSanPham = (int)reader["MaSanPham"],
                        MaDanhMuc = (int)reader["MaDanhMuc"],
                        TenSanPham = reader["TenSanPham"].ToString(),
                        Gia = (decimal)reader["Gia"],
                        SoLuong = (int)reader["SoLuong"],
                        MoTa = reader["MoTa"].ToString()
                    };
                }
            }

            if (sp == null)
                return HttpNotFound();

            return View(sp);
        }

        // ================= TÌM KIẾM =================
        public ActionResult TimKiem(string keyword)
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE TenSanPham LIKE @search";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", "%" + keyword + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSanPham = (int)reader["MaSanPham"],
                        MaDanhMuc = (int)reader["MaDanhMuc"],
                        TenSanPham = reader["TenSanPham"].ToString(),
                        Gia = (decimal)reader["Gia"],
                        SoLuong = (int)reader["SoLuong"],
                        MoTa = reader["MoTa"].ToString()
                    });
                }
            }

            return View("Index", list);
        }
        public ActionResult ThucAnMeo()
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE MaDanhMuc = 1"; // 1 = thức ăn mèo
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSanPham = (int)reader["MaSanPham"],
                        MaDanhMuc = (int)reader["MaDanhMuc"],
                        TenSanPham = reader["TenSanPham"].ToString(),
                        Gia = (decimal)reader["Gia"],
                        SoLuong = (int)reader["SoLuong"],
                        MoTa = reader["MoTa"].ToString()
                    });
                }
            }

            return View("DanhMuc", list);
        }
    }
}