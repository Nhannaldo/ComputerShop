using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {   
                
                if (name == "LinhKien")
                {
                    var kqq = from lk in db.SANPHAMs where lk.MaLoaiSP > 9 && lk.MaLoaiSP < 15 select lk;
                    return View(kqq.ToList());
                }
                else if (name == "PhuKien")
                {
                    var kqq= from pk in db.SANPHAMs where pk.MaLoaiSP > 15 && pk.MaLoaiSP < 24 select pk;
                    return View(kqq.ToList());
                }
                else
                {
                var kq = db.SANPHAMs.Where(s => s.TenSP.Contains(name));
                //var kq = from s in db.SANPHAMs where s.TenSP == strSearch) select s;

                //var kq = db.SACHes.Where(s => s.MaCD == int.Parse(strSearch)).OrderByDescending(s => s.SoLuongBan);
                //var kq = from s in db.SACHes where s.MaCD == int.Parse(strSearch) orderby s.SoLuongBan descending select s ;
                ViewBag.Kq = kq.Count();
                ViewBag.Search = name;
                return View(kq.ToList());
                }
                
            }
            return View();
            
        }
    }
}