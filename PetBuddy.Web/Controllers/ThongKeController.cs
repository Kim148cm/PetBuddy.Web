using PetBuddy.Web;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;




public class ThongKeController : Controller
{
    string connectionString = @"Server=ThienKim;Database=PetBuddy;Trusted_Connection=True;";

    public ActionResult Index()
    {
        decimal doanhThuTuan = 0;
        decimal doanhThuThang = 0;
        int spTuan = 0;
        int spThang = 0;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // 🔹 Doanh thu 7 ngày
            string q1 = @"
            SELECT ISNULL(SUM(TongTien),0)
            FROM DonHang
            WHERE NgayDat >= DATEADD(DAY,-7,GETDATE())";

            SqlCommand cmd1 = new SqlCommand(q1, conn);
            doanhThuTuan = Convert.ToDecimal(cmd1.ExecuteScalar());

            // 🔹 Doanh thu 30 ngày
            string q2 = @"
            SELECT ISNULL(SUM(TongTien),0)
            FROM DonHang
            WHERE NgayDat >= DATEADD(DAY,-30,GETDATE())";

            SqlCommand cmd2 = new SqlCommand(q2, conn);
            doanhThuThang = Convert.ToDecimal(cmd2.ExecuteScalar());

            // 🔹 Sản phẩm bán 7 ngày
            string q3 = @"
            SELECT ISNULL(SUM(ct.SoLuong),0)
            FROM ChiTietDonHang ct
            JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
            WHERE dh.NgayDat >= DATEADD(DAY,-7,GETDATE())";

            SqlCommand cmd3 = new SqlCommand(q3, conn);
            spTuan = Convert.ToInt32(cmd3.ExecuteScalar());

            // 🔹 Sản phẩm bán 30 ngày
            string q4 = @"
            SELECT ISNULL(SUM(ct.SoLuong),0)
            FROM ChiTietDonHang ct
            JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
            WHERE dh.NgayDat >= DATEADD(DAY,-30,GETDATE())";

            SqlCommand cmd4 = new SqlCommand(q4, conn);
            spThang = Convert.ToInt32(cmd4.ExecuteScalar());
        }

        ViewBag.DoanhThuTuan = doanhThuTuan;
        ViewBag.DoanhThuThang = doanhThuThang;
        ViewBag.SPTuan = spTuan;
        ViewBag.SPThang = spThang;

        return View();
    }

}
