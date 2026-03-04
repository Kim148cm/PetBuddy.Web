using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using PetBuddy.Web.Models;

public class CartController : Controller
{
    string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";

    // Hiển thị giỏ hàng
    public ActionResult Index()
    {
        if (Session["MaNguoiDung"] == null)
            return RedirectToAction("Login", "Account");

        // ✅ FIX: dùng Convert thay vì ép kiểu trực tiếp
        int maNguoiDung = Convert.ToInt32(Session["MaNguoiDung"]);

        List<GioHangItemViewModel> list = new List<GioHangItemViewModel>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string query = @"
            SELECT sp.MaSanPham, sp.TenSanPham, sp.Gia, ct.SoLuong
            FROM GioHang gh
            JOIN ChiTietGioHang ct ON gh.MaGioHang = ct.MaGioHang
            JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
            WHERE gh.MaNguoiDung = @MaNguoiDung";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new GioHangItemViewModel
                {
                    MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                    TenSanPham = reader["TenSanPham"].ToString(),
                    Gia = Convert.ToDecimal(reader["Gia"]),
                    SoLuong = Convert.ToInt32(reader["SoLuong"])
                });
            }
        }

        return View(list);
    }

    // Thêm vào giỏ hàng
    public ActionResult AddToCart(int id)
    {
        if (Session["MaNguoiDung"] == null)
            return RedirectToAction("Login", "Account");

        // ✅ FIX: dùng Convert
        int maNguoiDung = Convert.ToInt32(Session["MaNguoiDung"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            // 1️⃣ Kiểm tra đã có giỏ hàng chưa
            string getCartQuery = "SELECT MaGioHang FROM GioHang WHERE MaNguoiDung=@MaNguoiDung";
            SqlCommand getCartCmd = new SqlCommand(getCartQuery, con);
            getCartCmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

            object result = getCartCmd.ExecuteScalar();

            int maGioHang;

            if (result == null)
            {
                string insertCart = "INSERT INTO GioHang(MaNguoiDung) VALUES(@MaNguoiDung); SELECT SCOPE_IDENTITY();";
                SqlCommand insertCmd = new SqlCommand(insertCart, con);
                insertCmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                maGioHang = Convert.ToInt32(insertCmd.ExecuteScalar());
            }
            else
            {
                maGioHang = Convert.ToInt32(result);
            }

            // 2️⃣ Kiểm tra sản phẩm đã có chưa
            string checkQuery = "SELECT SoLuong FROM ChiTietGioHang WHERE MaGioHang=@MaGioHang AND MaSanPham=@MaSanPham";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@MaGioHang", maGioHang);
            checkCmd.Parameters.AddWithValue("@MaSanPham", id);

            SqlDataReader reader = checkCmd.ExecuteReader();

            if (reader.Read())
            {
                int soLuong = Convert.ToInt32(reader["SoLuong"]);
                reader.Close();

                string updateQuery = "UPDATE ChiTietGioHang SET SoLuong=@SoLuong WHERE MaGioHang=@MaGioHang AND MaSanPham=@MaSanPham";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@SoLuong", soLuong + 1);
                updateCmd.Parameters.AddWithValue("@MaGioHang", maGioHang);
                updateCmd.Parameters.AddWithValue("@MaSanPham", id);
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                reader.Close();

                string insertQuery = "INSERT INTO ChiTietGioHang(MaGioHang, MaSanPham, SoLuong) VALUES(@MaGioHang,@MaSanPham,1)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@MaGioHang", maGioHang);
                insertCmd.Parameters.AddWithValue("@MaSanPham", id);
                insertCmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }
}
