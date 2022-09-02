using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDoDienMay_Nhom11.Models
{
    public class CartItem
    {
        public string iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public string itags { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        DataClasses1DataContext dt = new DataClasses1DataContext();

        public CartItem(string MaSach)
        {
            SanPham sanpham = dt.SanPhams.Single(n => n.MaSP == MaSach);
            if (sanpham != null)
            {
                iMaSP = MaSach;
                sTenSP = sanpham.TenSP;
                sAnh = sanpham.ImageSP;
                dDonGia = double.Parse(sanpham.GiaTien.ToString());
                itags = sanpham.Tags;
                iSoLuong = 1;
            }
        }
    }

    public class GioHang
    {
        public List<CartItem> ds;
        public GioHang()
        {
            ds = new List<CartItem>();
        }
        public GioHang(List<CartItem> dsGH)
        {
            if (dsGH == null)
                ds = new List<CartItem>();
            else
                ds = dsGH;
        }
        public int SoMatHang()
        {
            if (ds == null)
                return 0;
            return ds.Count;
        }
        public int TongSLHang()
        {
            int tong = 0;
            if (ds != null)
            {
                tong = ds.Sum(n => n.iSoLuong);
                return tong;
            }
            return 0;
        }
        public double TongThanhTien()
        {
            double tong = 0;
            if (ds != null)
            {
                tong = ds.Sum(n => n.ThanhTien);
                return tong;
            }
            return 0;
        }
        public int Them(string iMa)
        {
            CartItem sp = ds.Find(n => n.iMaSP == iMa);
            if (sp == null)
            {
                CartItem sanpham = new CartItem(iMa);
                if (sanpham == null)
                {
                    return -1;
                }
                ds.Add(sanpham);
            }
            else
            {
                sp.iSoLuong++;
            }
            return 1;
        }

        public int Xoa(string iMa)
        {
            CartItem sp = ds.Find(n => n.iMaSP == iMa);
            if (sp != null)
            {
                CartItem sanpham = new CartItem(iMa);
                ds.Remove(sanpham);
                sp.iSoLuong--;
            }
            return 1;  
        }

        public int XoaGioHang()
        {
            if (ds == null)
                return 1;
            else
            {
                ds.Clear();
                return 1;
            }
        }
    }
}
