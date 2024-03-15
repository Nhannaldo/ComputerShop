using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class NhomSanPhamController : Controller
    {
        // GET: Admin/NhomSanPham
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.NHOMSANPHAMs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHOMSANPHAM n,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                n.TenNhomSP = f["TenNhom"];
                db.NHOMSANPHAMs.InsertOnSubmit(n);
                db.SubmitChanges();
                return RedirectToAction("Index", "NhomSanPham");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nsp = db.NHOMSANPHAMs.SingleOrDefault(n => n.MaNhomSP == id);
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
            var nsp = db.NHOMSANPHAMs.SingleOrDefault(n => n.MaNhomSP == int.Parse(f["MaNhom"]));
            if (ModelState.IsValid)
            {
                nsp.TenNhomSP = f["TenNhom"];
                db.SubmitChanges();
                return RedirectToAction("Index", "NhomSanPham");
            }
            return View(nsp);
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.NHOMSANPHAMs.SingleOrDefault(n => n.MaNhomSP == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NHOMSANPHAMs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }

}