using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    class NhanVienKD : NhanVienABC
    {
        double doanhSoBangHangToiThieu;

        public double DoanhSoBangHangToiThieu
        {
            get { return doanhSoBangHangToiThieu; }
            set { doanhSoBangHangToiThieu = value; }
        }
        double doanhSoBangHangThucTe;

        public double DoanhSoBangHangThucTe
        {
            get { return doanhSoBangHangThucTe; }
            set { doanhSoBangHangThucTe = value; }
        }

        public NhanVienKD(string MaNV, string HoTen, int NamSinh, string Gt, double heSL, int NVL, double DoanhSoBanHangToiThieu, double DoanhSoBanHangThucTe) : base(MaNV, HoTen, NamSinh, Gt, heSL, NVL)
        {
            this.DoanhSoBangHangToiThieu = DoanhSoBanHangToiThieu;
            this.DoanhSoBangHangThucTe = DoanhSoBanHangThucTe;

        }

        public override char XepLoai()
        {
            if (DoanhSoBangHangThucTe >= DoanhSoBangHangToiThieu * 2)
                return 'A';
            else if (DoanhSoBangHangThucTe == DoanhSoBangHangToiThieu)
                return 'B';
            else if (DoanhSoBangHangThucTe < DoanhSoBangHangToiThieu)
                return 'C';
            else
                return 'D';
        }
        public override double TinhLuong()
        {
            return HSL * NhanVienABC.lcb + (0.15 * (DoanhSoBangHangThucTe - DoanhSoBangHangToiThieu));
        }
        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine("Doanh so bang hang thuc te : {0} \n Doanh so bang hanh toi thieu : {1}", DoanhSoBangHangThucTe, DoanhSoBangHangToiThieu);
        }
    }
}
