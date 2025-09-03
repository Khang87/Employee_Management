using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    abstract class NhanVienABC
    {
        private string maNV;

        protected string MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        private string hoTen;

        protected string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }
        private int namSinh;

        protected int NamSinh
        {
            get { return namSinh; }
            set { namSinh = value; }
        }
        protected double hSL;

        protected double HSL
        {
            get { return hSL; }
            set { hSL = value; }
        }
        protected int nVL;

        protected int NVL
        {
            get { return nVL; }
            set { nVL = value; }
        }
        protected string gt;

        protected string Gt
        {
            get { return gt; }
            set { gt = value; }
        }
        public static int lcb = 2340000;
        public NhanVienABC(string maNhanVien, string hoTen, int namSinh, string gioiTinh, double heSL, int namVL)
        {
            this.MaNV = maNhanVien;
            this.HoTen = hoTen;
            this.NamSinh = namSinh;
            this.HSL = heSL;
            this.Gt = gioiTinh;
            this.NVL = namVL;

        }
        public NhanVienABC()
        {
            MaNV = "555";
            HoTen = "Faiz";
            NamSinh = 1987;
            Gt = "Nam";
            HSL = 5.55;
            NVL = 2005;

        }
        public double TinhPCTN()
        {
            if (DateTime.Now.Year - NVL >= 5)
            {
                return (double)(DateTime.Now.Year - NVL / 100) * NhanVienABC.lcb;
            }
            return 0;

        }
        public double ThuNhap()
        {
            if (XepLoai() == 'A')
                return 1 * TinhLuong() + TinhPCTN();
            else if (XepLoai() == 'B')
                return 0.75 * TinhLuong() + TinhPCTN();
            else if (XepLoai() == 'C')
                return 0.5 * TinhLuong() + TinhPCTN();
            else
                return 0 * TinhLuong() + TinhPCTN();
        }
        public abstract char XepLoai();
        public abstract double TinhLuong();
        public virtual void Xuat()
        {
            Console.WriteLine("Ma NV:{0} \nHo ten:{1} \nNam sinh : {2} \nGioi tinh: {3} \nHe so luong: {4} \nNam vao lam: {5} ", MaNV, HoTen, NamSinh, Gt, HSL, NVL);
        }

    }
}
