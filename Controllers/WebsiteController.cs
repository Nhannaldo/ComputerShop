using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using PagedList.Mvc;
using PagedList;
namespace Website.Controllers
{
    public class WebsiteController : Controller
    {
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        // GET: Website
        private List<SANPHAM> LaySachNhieu(int count)
        {
            return db.SANPHAMs.OrderByDescending(s => s.SoLuongBan).Take(count).ToList();
        }
        private List<SANPHAM> LaySPMoi(int count)
        {
            return db.SANPHAMs.OrderByDescending(s => s.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var list = LaySPMoi(10);
            return View(list);
        }
        public ActionResult GioiThieu()
        {

            return View();
        }
        public ActionResult LapTop(int ?page,string sort)
        {
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var laptop = from lt in db.SANPHAMs where lt.MaLoaiSP>0 && lt.MaLoaiSP<9 select lt;
            switch (sort)
            {
                case "spnoibat":
                    {
                        ViewBag.NameSort = "spnoibat";
                        laptop = laptop.OrderByDescending(s => s.NgayCapNhat);
                        break;
                    }
                case "tang":
                    {
                        ViewBag.NameSort = "tang";
                        laptop = laptop.OrderBy(s => s.GiaBan);
                        break;
                    }
                case "giam":
                    {
                        ViewBag.NameSort = "giam";
                        laptop = laptop.OrderByDescending(s => s.GiaBan);
                        break;
                    }
                default:
                    sort = null;
                    break;
            }
            return View(laptop.ToPagedList(iPageNum,iSize));
        }
        public ActionResult PhuKien(int ?page,string sort)
        {
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var phukien = from pk in db.SANPHAMs where pk.MaLoaiSP > 15 && pk.MaLoaiSP < 24 select pk;
            switch (sort)
            {
                case "spnoibat":
                    {
                        ViewBag.NameSort = "spnoibat";
                        phukien = phukien.OrderByDescending(s => s.NgayCapNhat);
                        break;
                    }
                case "tang":
                    {
                        ViewBag.NameSort = "tang";
                        phukien = phukien.OrderBy(s => s.GiaBan);

                        break;
                    }
                case "giam":
                    {
                        ViewBag.NameSort = "giam";
                        phukien = phukien.OrderByDescending(s => s.GiaBan);
                        break;
                    }
                default:
                    sort = null;
                    break;
            }
            return View(phukien.ToPagedList(iPageNum,iSize));
        }
        public ActionResult LinhKien(int ?page,string sort)
        {
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var linhkien = from lk in db.SANPHAMs where lk.MaLoaiSP > 9 && lk.MaLoaiSP < 15 select lk;
            switch (sort)
            {
                case "spnoibat":
                    {
                        ViewBag.NameSort = "spnoibat";
                        linhkien = linhkien.OrderByDescending(s => s.NgayCapNhat);
                        break;
                    }
                case "tang":
                    {
                        ViewBag.NameSort = "tang";
                        linhkien = linhkien.OrderBy(s => s.GiaBan);

                        break;
                    }
                case "giam":
                    {
                        ViewBag.NameSort = "giam";
                        linhkien = linhkien.OrderByDescending(s => s.GiaBan);
                        break;
                    }
                default:
                    sort = null;
                    break;
            }
            return View(linhkien.ToPagedList(iPageNum, iSize));
        }
        public ActionResult HienThiLapTop(int id,int? page)
        {
            ViewBag.MaLT = id;
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var laptop = from pk in db.SANPHAMs where pk.MaLoaiSP == id select pk;
            return View(laptop.ToPagedList(iPageNum,iSize));
        }
        public ActionResult HienThiLapTopLoc(string loc, int? page)
        {

            int iSize = 9;
            int iPageNum = (page ?? 1);
            var laptop = from lt in db.SANPHAMs where lt.MaLoaiSP > 0 && lt.MaLoaiSP < 9 select lt;
            switch (loc)
            {
                case "duoi10":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.GiaBan < 10000000);
                    break;
                case "1015":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.GiaBan >= 10000000 && n.GiaBan<=15000000);
                    break;
                case "1520":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.GiaBan >= 15000000 && n.GiaBan <= 20000000);
                    break;
                case "2025":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.GiaBan >= 20000000 && n.GiaBan <= 25000000);
                    break;
                case "tren25":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.GiaBan > 25000000);
                    break;
                case "4gb":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n=>n.RAM=="4GB");
                    break;
                case "8gb":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.RAM == "8GB");
                    break;
                case "16gb":
                    ViewBag.Loc = loc;
                    laptop = laptop.Where(n => n.RAM == "16GB");
                    break;
               
                
            }
            return View(laptop.ToPagedList(iPageNum, iSize));
        }
        public ActionResult HienThiPhuKien(int id,int ?page,string sort)
        {
            ViewBag.MaPK = id;
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var phukien = from pk in db.SANPHAMs where pk.MaLoaiSP==id select pk;
            switch (sort)
            {
                case "spnoibat":
                    {
                        ViewBag.NameSort = "spnoibat";
                        phukien = phukien.OrderByDescending(s => s.NgayCapNhat);
                        break;
                    }
                case "tang":
                    {
                        ViewBag.NameSort = "tang";
                        phukien = phukien.OrderBy(s => s.GiaBan);

                        break;
                    }
                case "giam":
                    {
                        ViewBag.NameSort = "giam";
                        phukien = phukien.OrderByDescending(s => s.GiaBan);
                        break;
                    }
                default:
                    sort = null;
                    break;
            }
            return View(phukien.ToPagedList(iPageNum, iSize));
        }
        public ActionResult HienThiLinhKien(int id,int ?page,string sort)
        {
            ViewBag.MaLK = id;
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var linhkien = from lk in db.SANPHAMs where lk.MaLoaiSP == id select lk;
            switch (sort)
            {
                case "spnoibat":
                    {
                        ViewBag.NameSort = "spnoibat";
                        linhkien = linhkien.OrderByDescending(s => s.NgayCapNhat);
                        break;
                    }
                case "tang":
                    {
                        ViewBag.NameSort = "tang";
                        linhkien = linhkien.OrderBy(s => s.GiaBan);

                        break;
                    }
                case "giam":
                    {
                        ViewBag.NameSort = "giam";
                        linhkien = linhkien.OrderByDescending(s => s.GiaBan);
                        break;
                    }
                default:
                    sort = null;
                    break;
            }
            return View(linhkien.ToPagedList(iPageNum, iSize));
        }
        public ActionResult HeaderPartial()
        {
            return PartialView();
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SliderPartial()
        {
            return PartialView();
        }
        public ActionResult BanNhieuPartial()
        {
            var listSachNhieu = LaySachNhieu(10);
            return PartialView(listSachNhieu);
            
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(FormCollection collection,LIENHE lh)
        {
            var sHoTen = collection["HoTen"];
            var sEmail = collection["Email"];
            var sNoiDung = collection["NoiDung"];
            if (ModelState.IsValid)
            {
                
                lh.HoTen = sHoTen;
                lh.Email = sEmail;
                lh.NoiDung = sNoiDung;
                db.LIENHEs.InsertOnSubmit(lh);
                db.SubmitChanges();
                ViewBag.ThongBao = "Gửi thông tin liên hệ thành công!";
                //return RedirectToAction("LienHe", "Website");
            }
            return this.LienHe();
        }
        public ActionResult TinTuc()
        {

            return View(db.SUKIENs.ToList());
        }
        public ActionResult ChiTietSanPham(int id)
        {
            var sanpham = from sp in db.SANPHAMs where sp.MaSP == id  select sp;
            return View(sanpham.Single());
        }
        public ActionResult DanhMucPhuKienPartial()
        {
            var phukien = from pk in db.LOAISANPHAMs where pk.MaNhomSP==3 select pk;
            return View(phukien.ToList());
        }
        public ActionResult DanhMucLinhKienPartial()
        {
            var linhkien = from lk in db.LOAISANPHAMs where lk.MaNhomSP == 2 select lk;
            return View(linhkien.ToList());
        }
        [HttpGet]
        [ChildActionOnly]
        public ActionResult BinhLuan(int id,int ?page)
        {
            
            var binhluan = from bl in db.BINHLUANs where bl.MaSP == id select bl;
            return PartialView(binhluan.OrderByDescending(n=>n.NgayBinhLuan).Take(5).ToList());
        }
        [HttpPost]
        public ActionResult BinhLuan(int id,FormCollection f,BINHLUAN bl)
        {
            if (Session["TaiKhoan"] != null)
            {
                
                var sHoTen = f["HoTen"];
                var sEmail = f["Email"];
                var sNoiDung = f["NoiDung"];
                var iDanhGia = f["DanhGia"];
                if (ModelState.IsValid)
                {
                    bl.HoTen = sHoTen;
                    bl.Email = sEmail;
                    bl.NoiDung = sNoiDung;
                    bl.DanhGia = int.Parse(iDanhGia);
                    bl.NgayBinhLuan = DateTime.Now;
                    bl.MaSP = id;
                    db.BINHLUANs.InsertOnSubmit(bl);
                    db.SubmitChanges();
                    return Redirect("~/Website/ChiTietSanPham/" + id);
                }
            }
            else
            {
                ViewBag.ThongBao = "Bạn phải đăng nhập trước khi bình luận !";
                return RedirectToAction("DangNhap","User");
            }
            return PartialView();
        }
        public ActionResult Test()
        {
            var laptop = from lt in db.SANPHAMs where lt.MaLoaiSP > 0 && lt.MaLoaiSP < 9 select lt;
            return View(laptop);
        }
        public ActionResult DanhMucLapTopPartial()
        {
            var laptop= from lt in db.LOAISANPHAMs where lt.MaNhomSP == 1 select lt;
            return PartialView(laptop);
        }
        
        public ActionResult ChiTietTinTuc(int id)
        {
            var tintuc = from t in db.SUKIENs where t.Id_SuKien == id select t; 
            return View(tintuc.Single());
        }
        
    }
}