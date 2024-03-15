using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using System.IO;
namespace Website.Areas.Admin.Controllers
{
    public class TinTucController : Controller
    {
        // GET: Admin/TinTuc
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.SUKIENs.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Create(SUKIEN sk, FormCollection f,HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa!";
                ViewBag.TieuDe = f["TieuDe"];
                ViewBag.ChiTiet = f["ChiTiet"];
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                    }
                    sk.TieuDe = f["TieuDe"];
                    sk.Anh =sFileName;
                    sk.ChiTiet = f["ChiTiet"];
                    sk.NgayDang = Convert.ToDateTime(f["NgayDang"]);
                    sk.HienThi = true;
                    db.SUKIENs.InsertOnSubmit(sk);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nsp = db.SUKIENs.SingleOrDefault(n => n.Id_SuKien == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsp);
        }
        public ActionResult Details(int id)
        {
            var s = db.SUKIENs.SingleOrDefault(n => n.Id_SuKien == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(s);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f,HttpPostedFileBase file)
        {
            var nsp = db.SUKIENs.SingleOrDefault(n => n.Id_SuKien == int.Parse(f["MaSuKien"]));
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var sFileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                    }
                    nsp.Anh = sFileName;
                }
                nsp.TieuDe = f["TieuDe"];
                nsp.ChiTiet = f["ChiTiet"];
                nsp.NgayDang = Convert.ToDateTime(f["NgayDang"]);
                nsp.HienThi = true;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nsp);
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.SUKIENs.SingleOrDefault(n => n.Id_SuKien == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SUKIENs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}