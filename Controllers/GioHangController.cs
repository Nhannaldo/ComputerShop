using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public List<CHITIETGIOHANG> ListGioHang()
        {
            GIOHANG c = (GIOHANG)Session["Cart"];
            return db.CHITIETGIOHANGs.Where(n => n.Id_Cart == c.Id_Cart).GroupBy(m => m.MaSP).Select(a => a.First()).ToList();
        }
        public ActionResult AddGioHang(int id, string url)
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            else
            {

            GIOHANG l = (GIOHANG)Session["Cart"];
            List<CHITIETGIOHANG> lst = ListGioHang();
            CHITIETGIOHANG yt = lst.Find(s => s.MaSP == id);
            CHITIETGIOHANG lv = new CHITIETGIOHANG();
            if (yt == null)
            {
                yt = new CHITIETGIOHANG(id, l.Id_Cart);
                lst.Add(yt);
                foreach (var item in lst)
                {
                    lv.Id_Cart = l.Id_Cart;
                    lv.MaSP = item.MaSP;
                    lv.Hinh = item.Hinh;
                    lv.TenSP = item.TenSP;
                    lv.DonGia = item.DonGia;
                    lv.SoLuong = 1;
                    lv.TongTien = item.DonGia * lv.SoLuong;
                }
                db.CHITIETGIOHANGs.InsertOnSubmit(lv);
            }
            else
            {
                var sp = db.CHITIETGIOHANGs.SingleOrDefault(n => n.MaSP == id);
                sp.SoLuong = sp.SoLuong + 1;
                sp.TongTien = sp.SoLuong * sp.DonGia;
                db.SubmitChanges();
                return Redirect(url);
            }

            db.SubmitChanges();
            return Redirect(url);
        }
        }
       
        public ActionResult GioHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            else
            {
                GIOHANG g = (GIOHANG)Session["Cart"];
                List<CHITIETGIOHANG> lst = ListGioHang();
                if (lst.Count() == 0)
                {
                    return RedirectToAction("Index", "Website");
                }
                ViewBag.Tong = db.CHITIETGIOHANGs.Sum(n => n.TongTien);
                return View(lst);
            }
        }
        public ActionResult DeleteItem(int id)
        {
            List<CHITIETGIOHANG> lst = ListGioHang();
            CHITIETGIOHANG sp = lst.SingleOrDefault(n => n.MaSP == id);
            var kq = from s in db.CHITIETGIOHANGs where s.MaSP == id select s;
            foreach (var item in kq)
            {
                db.CHITIETGIOHANGs.DeleteOnSubmit(item);
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
            return RedirectToAction("GioHang");
        }
        public ActionResult Update(int id,FormCollection f)
        {
            var sp = db.CHITIETGIOHANGs.SingleOrDefault(n => n.MaSP == id);
            sp.SoLuong = int.Parse(f["soluong"]);
            sp.TongTien = sp.SoLuong * sp.DonGia;
            db.SubmitChanges();
            return RedirectToAction("GioHang");
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ThanhToan()
        {
            GIOHANG lst = (GIOHANG)Session["Cart"];
            ViewBag.Tong = db.CHITIETGIOHANGs.Sum(n => n.TongTien);
            return View(db.CHITIETGIOHANGs.Where(n => n.Id_Cart == lst.Id_Cart).GroupBy(m => m.MaSP).Select(a => a.First()).ToList());
        }
        [HttpPost]
        public ActionResult ThanhToan(FormCollection f)
        {
            HOADON hd = new HOADON();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<CHITIETGIOHANG> lst = ListGioHang();
            hd.MaKH = kh.MaKH;
            hd.TinhTrang = "Đã giao";
            hd.NgayDat = Convert.ToDateTime( f["NgayDat"]);
            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();
            foreach (var item in lst)
            {
                CHITIETHOADON cthd = new CHITIETHOADON();
                cthd.MaHD = hd.MaHD;
                cthd.MaSP = (int)item.MaSP;
                cthd.SoLuong = item.SoLuong;
                cthd.DonGia = item.DonGia;
                cthd.DiaChi = f["DiaChi"];
                cthd.ThanhToan = true;
                db.CHITIETHOADONs.InsertOnSubmit(cthd);
            }

            db.SubmitChanges();
            lst = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}