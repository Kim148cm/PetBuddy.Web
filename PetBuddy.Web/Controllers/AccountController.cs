using System.Web.Mvc;

namespace PetBuddy.Controllers
{
    public class AccountController : Controller
    {
        // ================= LOGIN =================
        public ActionResult Login()
        {
            // GHI CHÚ:
            // Trả về Views/Account/Login.cshtml
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // GHI CHÚ:
            // Demo: giả lập đăng nhập thành công
            if (email == "admin@gmail.com" && password == "123")
            {
                Session["MaTaiKhoan"] = 1;
                Session["HoTen"] = "Admin";

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai email hoặc mật khẩu";
            return View();
        }

        // ================= REGISTER =================
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string ho, string ten, string email, string password)
        {
            // GHI CHÚ:
            // Sau khi đăng ký → quay về Login
            return RedirectToAction("Login");
        }

        // ================= FORGOT PASSWORD =================
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            // GHI CHÚ:
            // Demo gửi mail
            ViewBag.Message = "Đã gửi email khôi phục mật khẩu!";
            return View();
        }

        // ================= LOGOUT =================
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
