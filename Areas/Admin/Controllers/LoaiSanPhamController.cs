using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.LOAISANPHAMs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaNhomSP = new SelectList(db.NHOMSANPHAMs.ToList().OrderBy(n => n.TenNhomSP), "MaNhomSP", "TenNhomSP");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LOAISANPHAM l,FormCollection f)
        {
            ViewBag.MaNhomSP = new SelectList(db.NHOMSANPHAMs.ToList().OrderBy(n => n.TenNhomSP), "MaNhomSP", "TenNhomSP");
            if (ModelState.IsValid)
            {
                l.TenLoaiSP = f["TenLoai"];
                l.MaNhomSP = int.Parse(f["MaNhomSP"]);
                db.LOAISANPHAMs.InsertOnSubmit(l);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var l = db.LOAISANPHAMs.SingleOrDefault(n => n.MaLoaiSP == id);
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNhomSP = new SelectList(db.NHOMSANPHAMs.ToList().OrderBy(n => n.TenNhomSP), "MaNhomSP", "TenNhomSP",l.MaNhomSP);
            return View(l);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var l = db.LOAISANPHAMs.SingleOrDefault(n => n.MaLoaiSP == int.Parse(f["MaLoai"]));
            ViewBag.MaNhomSP = new SelectList(db.NHOMSANPHAMs.ToList().OrderBy(n => n.TenNhomSP), "MaNhomSP", "TenNhomSP", l.MaNhomSP);
            if (ModelState.IsValid)
            {
                l.TenLoaiSP = f["TenLoai"];
                l.MaNhomSP = int.Parse(f["MaNhomSP"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(l);
        }
        public ActionResult Delete(int id)
        {
            var l = db.LOAISANPHAMs.SingleOrDefault(n => n.MaLoaiSP == id);
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LOAISANPHAMs.DeleteOnSubmit(l);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}