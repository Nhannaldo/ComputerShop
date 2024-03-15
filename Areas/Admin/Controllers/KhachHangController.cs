using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.KHACHHANGs.ToList());
        }
        public ActionResult Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var kq = db.KHACHHANGs.Where(s => s.TenKH.Contains(name)||s.DienThoai.Contains(name));
                ViewBag.Search = name;
                return View(kq.ToList());
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KHACHHANGs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var bl = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (bl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bl);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nsp = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nsp = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["MaKH"]));
            if (ModelState.IsValid)
            {
                
                nsp.TenKH = f["TenKH"];
                nsp.DiaChi = f["DiaChi"];
                nsp.DienThoai = f["DienThoai"];
                nsp.NgaySinh = Convert.ToDateTime(f["NgaySinh"]);
                nsp.Email = f["Email"];
                nsp.TaiKhoan = f["TaiKhoan"];
                nsp.MatKhau = f["MatKhau"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nsp);
        }
    }
}