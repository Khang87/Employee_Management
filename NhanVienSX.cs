using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    class NhanVienSX : NhanVienABC
    {
        public static double HeSoPCNN = 0.1;
        int soNgayNghi;

        public int SoNgayNghi
        {
            get { return soNgayNghi; }
            set { soNgayNghi = value; }
        }
        public NhanVienSX(string maNV, string HoTen, int NamSinh, double HSL, int NVL, string Gt, int SoNgayNghi) : base(maNV, HoTen, NamSinh, Gt, HSL, NVL)
        {

            this.SoNgayNghi = SoNgayNghi;

        }
        public override char XepLoai()
        {
            if (SoNgayNghi <= 1)
                return 'A';
            else if (SoNgayNghi <= 3)
                return 'B';
            else if (SoNgayNghi <= 5)
                return 'C';
            else
                return 'D';
        }
        public override double TinhLuong()
        {
            return HSL * NhanVienABC.lcb * (1 + NhanVienSX.HeSoPCNN);
        }
        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("So ngay nghi :{0}\nLuong :{1}", SoNgayNghi, TinhLuong());
        }
    }
}
