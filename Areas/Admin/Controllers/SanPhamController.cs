using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using System.IO;
namespace Website.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult Index()
        {
            return View(db.SANPHAMs.ToList());
        }
        public ActionResult Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var kq = db.SANPHAMs.Where(s => s.TenSP.Contains(name));
                ViewBag.Search = name;
                return View(kq.ToList());
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaloaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp,FormCollection f, HttpPostedFileBase file)
        {
            ViewBag.MaloaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            if (file == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh !";
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
                    sp.TenSP = f["TenSP"];
                    sp.MoTa = f["MoTa"];
                    sp.Hinh = sFileName;
                    sp.NgayCapNhat = Convert.ToDateTime(f["NgayCapNhat"]);
                    sp.SoLuongBan = int.Parse(f["SoLuongBan"]);
                    sp.GiaBan = decimal.Parse(f["GiaBan"]);
                    sp.MaLoaiSP = int.Parse(f["MaLoaiSP"]);
                    sp.ManHinh = f["ManHinh"];
                    sp.XuatXu = f["XuatXu"];
                    sp.BaoHanh = f["BaoHanh"];
                    sp.KichThuoc = f["KichThuoc"];
                    sp.TrongLuong = f["TrongLuong"];
                    sp.ChatLieu = f["ChatLieu"];
                    sp.BoXuLy = f["BoXuLy"];
                    sp.RAM = f["Ram"];
                    sp.HeDieuHanh = f["HeDieuHanh"];
                    sp.DoHoa = f["DoHoa"];
                    sp.Pin = f["Pin"];
                    sp.NamRaMat = int.Parse(f["NamRaMat"]);
                    sp.HinhCon = sFileName;
                    sp.MauSac = f["MauSac"];
                    sp.KetNoi = f["KetNoi"];
                    sp.Socket = f["SocKet"];
                    sp.TocDo = f["TocDo"];
                    sp.Cache = f["Cache"];
                    sp.NhanCPU = f["NhanCPU"];
                    sp.LuongCPU = f["LuongCPU"];
                    sp.DayChuyen = f["DayChuyen"];
                    sp.DienAp = f["DienAp"];
                    sp.DungLuong = f["DungLuong"];
                    sp.LoaiO = f["LoaiO"];
                    sp.ApSuat = f["ApSuat"];
                    sp.DoOn = f["DoOn"];
                    sp.DauVao = f["DauVao"];
                    sp.DongHo = f["DongHo"];
                    sp.PhanGiai = f["PhanGiai"];
                    sp.Directx = f["Directx"];
                    sp.Opengl = f["Opengl"];
                    db.SANPHAMs.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var nsp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (nsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SANPHAMs.DeleteOnSubmit(nsp);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var s = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(s);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var l = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP", l.MaLoaiSP);
            return View(l);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f,HttpPostedFileBase file)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["MaSP"]));
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP", sp.MaLoaiSP);
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
                    sp.Hinh = sFileName;
                    sp.HinhCon = sFileName;
                }
                sp.TenSP = f["TenSP"];
                sp.MoTa = f["MoTa"];
                sp.NgayCapNhat = Convert.ToDateTime(f["NgayCapNhat"]);
                sp.SoLuongBan = int.Parse(f["SoLuongBan"]);
                sp.GiaBan = decimal.Parse(f["GiaBan"]);
                sp.MaLoaiSP = int.Parse(f["MaLoaiSP"]);
                sp.ManHinh = f["ManHinh"];
                sp.XuatXu = f["XuatXu"];
                sp.BaoHanh = f["BaoHanh"];
                sp.KichThuoc = f["KichThuoc"];
                sp.TrongLuong = f["TrongLuong"];
                sp.ChatLieu = f["ChatLieu"];
                sp.BoXuLy = f["BoXuLy"];
                sp.RAM = f["Ram"];
                sp.HeDieuHanh = f["HeDieuHanh"];
                sp.DoHoa = f["DoHoa"];
                sp.Pin = f["Pin"];
                sp.NamRaMat = int.Parse(f["NamRaMat"]);
                sp.MauSac = f["MauSac"];
                sp.KetNoi = f["KetNoi"];
                sp.Socket = f["SocKet"];
                sp.TocDo = f["TocDo"];
                sp.Cache = f["Cache"];
                sp.NhanCPU = f["NhanCPU"];
                sp.LuongCPU = f["LuongCPU"];
                sp.DayChuyen = f["DayChuyen"];
                sp.DienAp = f["DienAp"];
                sp.DungLuong = f["DungLuong"];
                sp.LoaiO = f["LoaiO"];
                sp.ApSuat = f["ApSuat"];
                sp.DoOn = f["DoOn"];
                sp.DauVao = f["DauVao"];
                sp.DongHo = f["DongHo"];
                sp.PhanGiai = f["PhanGiai"];
                sp.Directx = f["Directx"];
                sp.Opengl = f["Opengl"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
    }
}