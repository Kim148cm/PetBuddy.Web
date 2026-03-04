using PetBuddy.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;


namespace PetBuddy.Web.Controllers
{

    //[AdminAuthorize]
    public class AdminController : Controller
    {
        string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";



        // ================= INDEX + SEARCH + PAGING =================
        public ActionResult Index(string searchString, int page = 1)
        {
            List<SanPham> list = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham";

                if (!string.IsNullOrEmpty(searchString))
                    query += " WHERE TenSanPham LIKE @search";

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(searchString))
                    cmd.Parameters.AddWithValue("@search", "%" + searchString + "%");

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

            int pageSize = 5;
            var paged = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)list.Count / pageSize);

            return View(paged);
        }

        // ================= CREATE =================
        public ActionResult Create()
        {
            ViewBag.DanhMuc = GetDanhMuc();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham sp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO SanPham
                            (MaDanhMuc, TenSanPham, Gia, SoLuong, MoTa)
                            VALUES
                            (@MaDanhMuc, @TenSanPham, @Gia, @SoLuong, @MoTa)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ================= EDIT =================
        public ActionResult Edit(int id)
        {
            SanPham sp = new SanPham();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE MaSanPham=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    sp.MaSanPham = (int)reader["MaSanPham"];
                    sp.MaDanhMuc = (int)reader["MaDanhMuc"];
                    sp.TenSanPham = reader["TenSanPham"].ToString();
                    sp.Gia = (decimal)reader["Gia"];
                    sp.SoLuong = (int)reader["SoLuong"];
                    sp.MoTa = reader["MoTa"].ToString();
                }
            }

            ViewBag.DanhMuc = GetDanhMuc();
            return View(sp);
        }

        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE SanPham SET
                            MaDanhMuc=@MaDanhMuc,
                            TenSanPham=@TenSanPham,
                            Gia=@Gia,
                            SoLuong=@SoLuong,
                            MoTa=@MoTa
                            WHERE MaSanPham=@MaSanPham";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSanPham", sp.MaSanPham);
                cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ================= DELETE =================
        public ActionResult Delete(int id)
        {
            SanPham sp = new SanPham();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SanPham WHERE MaSanPham=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    sp.MaSanPham = (int)reader["MaSanPham"];
                    sp.TenSanPham = reader["TenSanPham"].ToString();
                    sp.Gia = (decimal)reader["Gia"];
                }
            }

            return View(sp);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int MaSanPham)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SanPham WHERE MaSanPham=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", MaSanPham);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ================= LOAD DANH MUC =================
        private List<SelectListItem> GetDanhMuc()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMucSanPham";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = reader["MaDanhMuc"].ToString(),
                        Text = reader["TenDanhMuc"].ToString()
                    });
                }
            }

            return list;
        }
    }
}
