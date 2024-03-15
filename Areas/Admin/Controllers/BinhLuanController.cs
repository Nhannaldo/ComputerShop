using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: Admin/BinhLuan
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.BINHLUANs.ToList());
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.BINHLUANs.SingleOrDefault(n => n.MaBL == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.BINHLUANs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var bl = db.BINHLUANs.SingleOrDefault(n => n.MaBL == id);
            if (bl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bl);
        }
    }
}