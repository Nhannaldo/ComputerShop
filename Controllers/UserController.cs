using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            //int state = int.Parse(Request.QueryString["id"]);
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                YEUTHICH l = db.YEUTHICHes.SingleOrDefault(n => n.MaKH == kh.MaKH);
                GIOHANG g = db.GIOHANGs.SingleOrDefault(n => n.MaKH == kh.MaKH);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Bạn đã đăng nhập thành công!";
                    Session["TaiKhoan"] = kh;
                    Session["LoveID"] = l;
                    Session["Cart"] = g;
                    /*
                    if (collection["remember"].Contains("true"))
                    {
                        Response.Cookies["TenDN"].Value = sTenDN;
                        Response.Cookies["MatKhau"].Value = sMatKhau;
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                    }*/
                    return RedirectToAction("Index", "Website");
                    /*
                    if (state == 1)
                    {
                        return RedirectToAction("Index", "SachOnline");
                    }
                    else
                    {
                        return RedirectToAction("DatHang", "GioHang");
                    }
                    */
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTaiKhoan = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var sNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã sử dụng";
            }
            else if (ModelState.IsValid)
            {
                kh.TenKH = sHoTen;
                kh.TaiKhoan = sTaiKhoan;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(sNgaySinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                YEUTHICH l = new YEUTHICH();
                l.MaKH = kh.MaKH;
                db.YEUTHICHes.InsertOnSubmit(l);
                db.SubmitChanges();
                GIOHANG g = new GIOHANG();
                g.MaKH = kh.MaKH;
                db.GIOHANGs.InsertOnSubmit(g);
                db.SubmitChanges();
                return RedirectToAction("DangNhap","User");
            }
            return this.DangKy();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Website");
        }
        [HttpGet]
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuenMatKhau(KHACHHANG kh,FormCollection f)
        {
            var sEmail = f["Email"];

            return View();
        }
    }
}