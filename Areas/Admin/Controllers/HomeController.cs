using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
    }
}