using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    class CanBoQuanLy : NhanVienABC
    {
        string chucVu;

        public string ChucVu
        {
            get { return chucVu; }
            set { chucVu = value; }
        }
        double heSoPCCV;

        public double HeSoPCCV
        {
            get { return heSoPCCV; }
            set { heSoPCCV = value; }
        }
        public CanBoQuanLy(string maNV, string HoTen, int NamSinh, double HSL, int NVL, string Gt, double heSoPCCV, string ChucVu) : base(maNV, HoTen, NamSinh, Gt, HSL, NVL)
        {

            this.HeSoPCCV = heSoPCCV;
            this.ChucVu = ChucVu;

        }
        public override double TinhLuong()
        {
            return HSL * NhanVienABC.lcb + (HeSoPCCV * 1350);
        }
        public override char XepLoai()
        {
            return 'A';
        }
        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("Chuc vu : {0}\nHe so PCCV: {1}", ChucVu, HeSoPCCV);
        }
    }
}
