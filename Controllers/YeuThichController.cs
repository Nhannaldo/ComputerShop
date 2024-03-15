using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Controllers
{
    public class YeuThichController : Controller
    {
        // GET: YeuThich
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        // GET: LoveProduct
        public List<CHITIETYEUTHICH> ListLove()
        {
            List<CHITIETYEUTHICH> lst = Session["item"] as List<CHITIETYEUTHICH>;
            if (lst == null)
            {
                lst = new List<CHITIETYEUTHICH>();
                Session["item"] = lst;
            }
            return lst;
        }
        public ActionResult AddYeuThich(int id, string url)
        {
            YEUTHICH l = (YEUTHICH)Session["LoveID"];
            List<CHITIETYEUTHICH> lst = ListLove();
            CHITIETYEUTHICH yt = lst.Find(s => s.MaSP == id);
            if (yt == null)
            {
                yt = new CHITIETYEUTHICH(id,l.Id_Like);
                lst.Add(yt);
            }
            else
            {
                return Redirect(url);
            }
            CHITIETYEUTHICH lv = new CHITIETYEUTHICH();
            foreach (var item in lst)
            {
                lv.Id_Like = l.Id_Like;
                lv.MaSP = item.MaSP;
                lv.Hinh = item.Hinh;
                lv.TenSP = item.TenSP;
                lv.DonGia = item.DonGia;
            }
            db.CHITIETYEUTHICHes.InsertOnSubmit(lv);
            db.SubmitChanges();
            return Redirect(url);
        }
        public ActionResult YeuThich()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            YEUTHICH lst = (YEUTHICH)Session["LoveID"];
            return View(db.CHITIETYEUTHICHes.Where(n => n.Id_Like == lst.Id_Like).GroupBy(m => m.MaSP).Select(a => a.First()).ToList());
        }
        public ActionResult DeleteItem(int id)
        {
            List<CHITIETYEUTHICH> lst = ListLove();
            CHITIETYEUTHICH sp = lst.SingleOrDefault(n => n.MaSP == id);
            var kq = from s in db.CHITIETYEUTHICHes where s.MaSP == id select s;
            foreach (var item in kq)
            {
                db.CHITIETYEUTHICHes.DeleteOnSubmit(item);
            }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (sp == null)
            {
                lst.RemoveAll(n => n.MaSP == id);
                if (lst.Count == 0)
                {
                    return RedirectToAction("Index", "Website");
                }
            }
            return RedirectToAction("YeuThich");
        }
        public ActionResult YeuThichPartial()
        {
            return PartialView();
        }

    }
}

