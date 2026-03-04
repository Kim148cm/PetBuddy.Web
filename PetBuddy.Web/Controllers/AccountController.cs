using PetBuddy.Web.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PetBuddy.Web.Controllers
{
    public class AccountController : Controller
    {
        string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";

        // ================= LOGIN =================
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.MatKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View(model);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
        SELECT nd.MaNguoiDung, vt.TenVaiTro
        FROM NguoiDung nd
        INNER JOIN VaiTro vt ON nd.MaVaiTro = vt.MaVaiTro
        WHERE nd.Email = @Email 
        AND nd.MatKhau = @MatKhau
        AND nd.TrangThai = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", model.Email.Trim());
                cmd.Parameters.AddWithValue("@MatKhau", model.MatKhau.Trim());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Session["UserId"] = reader["MaNguoiDung"];
                    Session["Role"] = reader["TenVaiTro"].ToString();

                    if (Session["Role"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        return RedirectToAction("Index", "Admin");
                    else
                        return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Sai email hoặc mật khẩu";
            return View(model);
        }

        // ================= REGISTER =================
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra email tồn tại
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", model.Email);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                    return View(model);
                }

                // Lấy MaVaiTro của KhachHang
                string roleQuery = "SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro='KhachHang'";
                SqlCommand roleCmd = new SqlCommand(roleQuery, conn);
                object result = roleCmd.ExecuteScalar();

                if (result == null)
                {
                    ModelState.AddModelError("", "Chưa có vai trò KhachHang trong DB!");
                    return View(model);
                }

                int roleId = Convert.ToInt32(result);

                string insertQuery = @"INSERT INTO NguoiDung
                    (HoTen, Email, MatKhau, MaVaiTro, TrangThai)
                    VALUES
                    (@HoTen, @Email, @MatKhau, @MaVaiTro, 1)";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@HoTen", model.HoTen);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@MatKhau", model.MatKhau);
                cmd.Parameters.AddWithValue("@MaVaiTro", roleId);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}
