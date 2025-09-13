using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    public class CanBoQuanLy : NhanVienABC
    {
        #region Private Fields
        private string _chucVu;
        private double _heSoPhuCapChucVu;
        #endregion

        #region Public Properties
        public string ChucVu
        {
            get { return _chucVu; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Chức vụ không được để trống");
                _chucVu = value.Trim(); 
            }
        }

        public double HeSoPhuCapChucVu
        {
            get { return _heSoPhuCapChucVu; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentException("Hệ số phụ cấp chức vụ không được âm");
                _heSoPhuCapChucVu = value; 
            }
        }

        public static double DonGiaPhuCapChucVu { get; } = 1350;
        #endregion

        #region Constructors
        public CanBoQuanLy(string maNV, string hoTen, int namSinh, double heSoLuong, int namVaoLam, string gioiTinh, 
            double heSoPhuCapChucVu, string chucVu) 
            : base(maNV, hoTen, namSinh, gioiTinh, heSoLuong, namVaoLam)
        {
            this.HeSoPhuCapChucVu = heSoPhuCapChucVu;
            this.ChucVu = chucVu;
        }

        public CanBoQuanLy() : base()
        {
            ChucVu = "Trưởng phòng";
            HeSoPhuCapChucVu = 2.0;
        }
        #endregion

        #region Override Methods
        public override double TinhLuong()
        {
            return HeSoLuong * LuongCoBan + (HeSoPhuCapChucVu * DonGiaPhuCapChucVu);
        }

        public override char XepLoai()
        {
            return 'A'; // Cán bộ quản lý luôn xếp loại A
        }

        public override void Xuat()
        {
            base.Xuat();
            Console.WriteLine($"Chức vụ: {ChucVu}");
            Console.WriteLine($"Hệ số phụ cấp chức vụ: {HeSoPhuCapChucVu:F2}");
            Console.WriteLine($"Đơn giá phụ cấp chức vụ: {DonGiaPhuCapChucVu:C0}");
            Console.WriteLine($"Phụ cấp chức vụ: {HeSoPhuCapChucVu * DonGiaPhuCapChucVu:C0}");
            Console.WriteLine("=== KẾT THÚC THÔNG TIN CÁN BỘ QUẢN LÝ ===");
        }

        public override string ToString()
        {
            return $"{base.ToString()} - QL - {ChucVu}";
        }
        #endregion
    }
}
