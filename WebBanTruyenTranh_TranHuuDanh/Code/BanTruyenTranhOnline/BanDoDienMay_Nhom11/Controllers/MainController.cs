using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDoDienMay_Nhom11.Models;
using System.IO;
using System.Web.Routing;
namespace BanDoDienMay_Nhom11.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        DataClasses1DataContext data = new DataClasses1DataContext();
        [HttpGet]
        public ActionResult Home()
        {
            List<SanPham> dt = data.SanPhams.ToList();
            return View(dt);
        }
        public ActionResult HTSPML()
        {
            string tk = "Action";
            List<SanPham> dt = data.SanPhams.Where(t => t.Tags.Contains(tk) == true).ToList();
            return View(dt);
        }
        public ActionResult CTSP(string id)
        {
            ChiTietMatHang mh = data.ChiTietMatHangs.SingleOrDefault(t => t.MaSP == id);
            return View(mh);
        }
        public ActionResult HTSPNoi()
        {
            string tk = "Adventure";
            List<SanPham> dt = data.SanPhams.Where(t => t.Tags.Contains(tk) == true).ToList();
            return View(dt);
        }
        public ActionResult HTSPQDH()
        {
            string tk = "Mystery";
            List<SanPham> dt = data.SanPhams.Where(t => t.Tags.Contains(tk) == true).ToList();
            return View(dt);
        }
        public ActionResult HTSPSQA()
        {
            string tk = "Trinh thám";
            List<SanPham> dt = data.SanPhams.Where(t => t.Tags.Contains(tk) == true).ToList();
            return View(dt);
        }
        //Máy giặt
        public ActionResult HTSPMG()
        {
            string tk = "Xuyên không";
            List<SanPham> dt = data.SanPhams.Where(t => t.Tags.Contains(tk) == true).ToList();
            return View(dt);
        }
        [HttpPost]
        public ActionResult XuLyTimKiem(FormCollection c)
        {
            string tk = c["txtTuKhoa"].ToString();
            List<SanPham> dt = data.SanPhams.Where(t => t.TenSP.Contains(tk) == true).ToList();
            return View("Home",dt);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult addsp()
        {
            List<SanPham> dssp = data.SanPhams.ToList();
            ViewBag.dssp = dssp;
            return View();
        }
        public ActionResult DNTB()
        {
            return View();
        }
        public ActionResult DNTC()
        {

            return PartialView();
        }
        [HttpGet]
        public ActionResult ThongTinKhachHang(int id)
        {
            CTKhachHang kh = data.CTKhachHangs.SingleOrDefault(t => t.MaKH == id);
            List<HoaDon> dshd = data.HoaDons.Where(j => j.MaKH == id).ToList();
            ViewBag.dshd = dshd;
            return View(kh);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XLXoa(FormCollection c , SanPham sp)
        {
            string masp = c["txtmasp"].ToString();
            try
            {
                sp = data.SanPhams.SingleOrDefault(t => t.MaSP == masp);
                if (sp != null)
                {
                    sp.MaSP = masp;
                    data.SanPhams.DeleteOnSubmit(sp);
                    data.SubmitChanges();
                    return RedirectToAction("addsp", "Main");
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XLSua(FormCollection c, HttpPostedFileBase upload2)
        {
            
            try
            {
                string masp = null;
                masp = c["txtmasp"];
                string tensp = c["txtnamesp"];
                int giasp = 0;
                giasp = int.Parse(c["txtgiatien"].ToString());
                string theloai = null;
                theloai = c["txttags"];
                string mota = null;
                mota = c["MoTaupdate"];
                SanPham sp = data.SanPhams.SingleOrDefault(t => t.MaSP == masp);
                if (sp != null)
                {
                    var filename = Path.GetFileName(upload2.FileName);
                    sp.TenSP = tensp;
                    sp.GiaTien = giasp;
                    sp.Tags = theloai;
                    sp.MoTa = mota;
                    sp.ImageSP = filename;
                    UpdateModel(sp);
                    data.SubmitChanges();
                    return RedirectToAction("addsp", "Main");
                }
                else
                    return RedirectToAction("addsp", "Main");
            }
            catch
            {
                return RedirectToAction("addsp", "Main");
            }
        }
        public ActionResult logout()
        {
            Session["ten"] = null;
            Session["adm"] = null;
            return Redirect("/");          
        }
       
        [HttpPost]
        public ActionResult addsp(FormCollection c)
        {
            if (c["tenDN"].ToString() == "admin" && c["pass"].ToString() == "admin")
            {
                Session["adm"] = "admin";
                return RedirectToAction("addsp", "Main");          
            }
            else
            {
                string tenkh = c["tenDN"];
                string pass = c["pass"];
                KhachHang kh = data.KhachHangs.SingleOrDefault(t => t.TenKH == tenkh & t.Pass == pass);
                if (kh == null)
                {
                    return RedirectToAction("DNTB", "Main");
                }
                else
                {
                    Session["ten"] = kh;
                    return RedirectToAction("Home", "Main");
                }
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XuLyDangKy(FormCollection c, KhachHang kh,CTKhachHang ctkh)
        {
            string tenDN = c["txtdn"];
            string phone = c["txtPhone"];
            string pass = c["txtPass"];
            try
            {
                if (c["CB1"] == "1")
                {
                    kh.TenKH = tenDN;
                    kh.SDT = phone;
                    kh.Pass = pass;
                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    ctkh.MaKH = kh.MaKH;
                    ctkh.TenKH = tenDN;
                    ctkh.SDT = phone;
                    data.CTKhachHangs.InsertOnSubmit(ctkh);
                    data.SubmitChanges();
                    return RedirectToAction("Home", "Main");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addsp2(SanPham sp, HttpPostedFileBase upload, FormCollection c)
        {
            try
            {
                if (upload == null)
                {
                    ViewBag.ThongBao = " vui long chon anh";
                    return RedirectToAction("addsp", "Main");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var filename = Path.GetFileName(upload.FileName);
                        upload.SaveAs(Server.MapPath("/Content/HinhAnh_SP/HinhAnh_SP/" + upload.FileName));
                        sp.ImageSP = filename;
                        data.SanPhams.InsertOnSubmit(sp);
                        data.SubmitChanges();
                    }
                }

                return RedirectToAction("addsp", "Main");
            }
            catch
            {
                return RedirectToAction("addsp", "Main");
            }
        }
        public ActionResult ChonMua(string id)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
                gh = new GioHang();
            int kq = gh.Them(id);
            Session["gh"] = gh;
            return RedirectToAction("/");
        }
        public ActionResult XemGioHang()
        {

            GioHang gh = (GioHang)Session["gh"];

            return View(gh);
        }
        public ActionResult Xoa(string id)
        {

            GioHang gh = (GioHang)Session["gh"];
            int kq = gh.Xoa(id);
            
            Session["gh"] = gh;

            return RedirectToAction("XemGioHang", "Main");
        }
        public ActionResult AddSL(string id)
        {

            GioHang gh = (GioHang)Session["gh"];
            int kq = gh.Them(id);
            Session["gh"] = gh;

            return RedirectToAction("XemGioHang", "Main");
        }
        public ActionResult XoaGio()
        {
            GioHang gh = (GioHang)Session["gh"];
            gh.XoaGioHang();
            Session["gh"] = gh;
            return RedirectToAction("XemGioHang", "Main");
        }
        [HttpGet]
        public ActionResult MuaHang()
        {
            KhachHang khach = Session["ten"] as KhachHang;
            if (khach == null)
            {
                return RedirectToAction("Login", "Main");
            }
            GioHang gh = (GioHang)Session["gh"];

            ViewBag.kh =  khach;
            return View(gh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XLMuaHang(FormCollection c)
        {
            try
            {
                GioHang gh = (GioHang)Session["gh"];
                KhachHang kh = (KhachHang)Session["ten"];
                if (Session["ten"] == null)
                    return RedirectToAction("Login", "Main");
                if (Session["gh"] == null || gh.ds.Count == 0)
                    return RedirectToAction("XemGioHang", "Main");

                HoaDon hd = new HoaDon();
                hd.MaKH = kh.MaKH;
                hd.NgayDat = DateTime.Now;
                hd.SDT = kh.SDT;
                hd.NgayGiao = DateTime.Parse(c["ngaygiao"]);
                hd.DiaChi = c["diachigiao"];
                data.HoaDons.InsertOnSubmit(hd);
                data.SubmitChanges();
                foreach (CartItem sp in gh.ds)
                {
                    CTHD ct = new CTHD();
                    ct.MaHD = hd.MaHD;
                    ct.MaSP = sp.iMaSP;
                    ct.SoLuong = sp.iSoLuong;
                    data.CTHDs.InsertOnSubmit(ct);
                }
                data.SubmitChanges();
                gh.XoaGioHang();

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DuyetDonHang()
        {
            List<HoaDon> hd = data.HoaDons.ToList();
            return View(hd);
        }
        public ActionResult HienHoaDon(int mahd)
        {
            List<CTHD> cthd = data.CTHDs.Where(t => t.MaHD == mahd).ToList();
            int tong = (int)cthd.Sum(ct => ct.SoLuong * ct.SanPham.GiaTien);
            ViewBag.thanhtien = tong;
            return PartialView(cthd);
        }
        public ActionResult HienTenSP(int mahd)
        {
            List<CTHD> cthd = data.CTHDs.Where(t => t.MaHD == mahd).ToList();
            return PartialView(cthd);
        }
        //
        [HttpPost]
        public ActionResult ThongTinKhachHang(FormCollection c)
        {
            try
            {
                int id = int.Parse(c["makh"]);
                CTKhachHang kh = data.CTKhachHangs.SingleOrDefault(t => t.MaKH == id);
                string diachi = c["diachi"];
                string email = c["email"];
                string gt = c["gt"];
                int cm = int.Parse(c["cmnd"]);
                kh.Diachi = diachi;
                kh.Email = email;
                kh.GioiTinh = gt;
                kh.CMND = cm;
                kh.NgaySinh = DateTime.Parse(c["ngaysinh"]);
                UpdateModel(kh);
                data.SubmitChanges();
                return RedirectToAction("Home", "Main");
            }
            catch
            {
                return RedirectToAction("Home", "Main");
            }
        }
        //XLDuyet
        [HttpPost]
        public ActionResult XLDuyet(FormCollection c)
        {
            int t = int.Parse(c["tong"]);
            for (int i = 1; i <= t; i++)
            {
                string tenck = i.ToString();
                if (c[tenck] != null)
                {
                    int mhd = int.Parse(c[tenck]);
                    HoaDon hd = data.HoaDons.SingleOrDefault(h => h.MaHD == mhd);
                    hd.NgayThanhToan = DateTime.Now;
                    UpdateModel(hd);
                    data.SubmitChanges();
                }
            }
            return RedirectToAction("DuyetDonHang", "Main");
        }
        public ActionResult ThongKe()
        {
            List<HoaDon> hd = data.HoaDons.ToList();
            return View(hd);
        }
        public ActionResult ThanhTien(int mahd)
        {
            List<CTHD> cthd = data.CTHDs.Where(t => t.MaHD == mahd).ToList();
            int tong = (int)cthd.Sum(ct => ct.SoLuong * ct.SanPham.GiaTien);
            ViewBag.thanhtien = tong;
            return PartialView(cthd);
        }
        public ActionResult XuLyThongKe()
        {
            List<CTHD> cthd = data.CTHDs.ToList();
            int tong = (int)cthd.Sum(ct => ct.SoLuong * ct.SanPham.GiaTien);
            ViewBag.thanhtien = tong;
            return PartialView(cthd);
        }
        public ActionResult XuLyThongKeTT()
        {
            List<CTHD> cthd = data.CTHDs.Where(t => t.HoaDon.NgayThanhToan != null).ToList();
            int tong = (int)cthd.Sum(ct => ct.SoLuong * ct.SanPham.GiaTien);
            ViewBag.thanhtienTT = tong;
            return PartialView(cthd);
        }
        public ActionResult XuLyThongKeChuaTT()
        {
            List<CTHD> cthd = data.CTHDs.Where(t => t.HoaDon.NgayThanhToan == null).ToList();
            int tong = (int)cthd.Sum(ct => ct.SoLuong * ct.SanPham.GiaTien);
            ViewBag.thanhtienCTT = tong;
            return PartialView(cthd);
        }
    }
}
