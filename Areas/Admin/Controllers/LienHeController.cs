using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Admin/LienHe
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.LIENHEs.ToList());
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.LIENHEs.SingleOrDefault(n => n.Id == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LIENHEs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var bl = db.LIENHEs.SingleOrDefault(n => n.Id == id);
            if (bl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bl);
        }
    }
}