using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    public class NhanVienSX : NhanVienABC
    {
        #region Private Fields
        private int _soNgayNghi;
        #endregion

        #region Public Properties
        public static double HeSoPhuCapNgayNghi { get; } = 0.1;

        public int SoNgayNghi
        {
            get { return _soNgayNghi; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Số ngày nghỉ không được âm");
                _soNgayNghi = value; 
            }
        }
        #endregion

        #region Constructors
        public NhanVienSX(string maNV, string hoTen, int namSinh, double heSoLuong, int namVaoLam, string gioiTinh, int soNgayNghi) 
            : base(maNV, hoTen, namSinh, gioiTinh, heSoLuong, namVaoLam)
        {
            this.SoNgayNghi = soNgayNghi;
        }

        public NhanVienSX() : base()
        {
            SoNgayNghi = 0;
        }
        #endregion

        #region Override Methods
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
            return HeSoLuong * LuongCoBan * (1 + HeSoPhuCapNgayNghi);
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($"Số ngày nghỉ: {SoNgayNghi}");
            Console.WriteLine($"Hệ số phụ cấp ngày nghỉ: {HeSoPhuCapNgayNghi:P0}");
            Console.WriteLine("=== KẾT THÚC THÔNG TIN NHÂN VIÊN SẢN XUẤT ===");
        }

        public override string ToString()
        {
            return $"{base.ToString()} - SX - Nghỉ: {SoNgayNghi} ngày";
        }
        #endregion
    }
}
