using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    public class NhanVienKD : NhanVienABC
    {
        #region Private Fields
        private double _doanhSoToiThieu;
        private double _doanhSoThucTe;
        #endregion

        #region Public Properties
        public double DoanhSoToiThieu
        {
            get { return _doanhSoToiThieu; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Doanh số tối thiểu không được âm");
                _doanhSoToiThieu = value; 
            }
        }

        public double DoanhSoThucTe
        {
            get { return _doanhSoThucTe; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Doanh số thực tế không được âm");
                _doanhSoThucTe = value; 
            }
        }

        public static double HeSoHoaHong { get; } = 0.15;
        #endregion

        #region Constructors
        public NhanVienKD(string maNV, string hoTen, int namSinh, string gioiTinh, double heSoLuong, int namVaoLam, 
            double doanhSoToiThieu, double doanhSoThucTe) 
            : base(maNV, hoTen, namSinh, gioiTinh, heSoLuong, namVaoLam)
        {
            this.DoanhSoToiThieu = doanhSoToiThieu;
            this.DoanhSoThucTe = doanhSoThucTe;
        }

        public NhanVienKD() : base()
        {
            DoanhSoToiThieu = 1000000;
            DoanhSoThucTe = 1000000;
        }
        #endregion

        #region Override Methods
        public override char XepLoai()
        {
            if (DoanhSoThucTe >= DoanhSoToiThieu * 2)
                return 'A';
            else if (DoanhSoThucTe >= DoanhSoToiThieu)
                return 'B';
            else if (DoanhSoThucTe >= DoanhSoToiThieu * 0.5)
                return 'C';
            else
                return 'D';
        }

        public override double TinhLuong()
        {
            double luongCoBan = HeSoLuong * LuongCoBan;
            double hoaHong = Math.Max(0, HeSoHoaHong * (DoanhSoThucTe - DoanhSoToiThieu));
            return luongCoBan + hoaHong;
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($"Doanh số tối thiểu: {DoanhSoToiThieu:C0}");
            Console.WriteLine($"Doanh số thực tế: {DoanhSoThucTe:C0}");
            Console.WriteLine($"Hoa hồng: {Math.Max(0, HeSoHoaHong * (DoanhSoThucTe - DoanhSoToiThieu)):C0}");
            Console.WriteLine($"Hệ số hoa hồng: {HeSoHoaHong:P0}");
            Console.WriteLine("=== KẾT THÚC THÔNG TIN NHÂN VIÊN KINH DOANH ===");
        }

        public override string ToString()
        {
            return $"{base.ToString()} - KD - DS: {DoanhSoThucTe:C0}";
        }
        #endregion
    }
}
