using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    // Interface để định nghĩa các operations cơ bản
    public interface INhanVien
    {
        string MaNV { get; }
        string HoTen { get; set; }
        int NamSinh { get; set; }
        string GioiTinh { get; set; }
        double HeSoLuong { get; set; }
        int NamVaoLam { get; set; }
        char XepLoai();
        double TinhLuong();
        double ThuNhap();
        void Xuat();
    }

    // Abstract class với Encapsulation tốt hơn
    public abstract class NhanVienABC : INhanVien
    {
        #region Private Fields
        private string _maNV;
        private string _hoTen;
        private int _namSinh;
        private double _heSoLuong;
        private int _namVaoLam;
        private string _gioiTinh;
        #endregion

        #region Public Properties với Validation
        public string MaNV
        {
            get { return _maNV; }
            private set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mã nhân viên không được để trống");
                _maNV = value.Trim().ToUpper(); 
            }
        }

        public string HoTen
        {
            get { return _hoTen; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được để trống");
                _hoTen = value.Trim(); 
            }
        }

        public int NamSinh
        {
            get { return _namSinh; }
            set 
            { 
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException($"Năm sinh phải từ 1900 đến {DateTime.Now.Year}");
                _namSinh = value; 
            }
        }

        public double HeSoLuong
        {
            get { return _heSoLuong; }
            set 
            { 
                if (value <= 0)
                    throw new ArgumentException("Hệ số lương phải lớn hơn 0");
                _heSoLuong = value; 
            }
        }

        public int NamVaoLam
        {
            get { return _namVaoLam; }
            set 
            { 
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException($"Năm vào làm phải từ 1900 đến {DateTime.Now.Year}");
                if (value < NamSinh + 16)
                    throw new ArgumentException("Năm vào làm phải sau năm sinh ít nhất 16 năm");
                _namVaoLam = value; 
            }
        }

        public string GioiTinh
        {
            get { return _gioiTinh; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Giới tính không được để trống");
                if (!value.Equals("Nam", StringComparison.OrdinalIgnoreCase) && 
                    !value.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Giới tính phải là 'Nam' hoặc 'Nữ'");
                _gioiTinh = value.Trim(); 
            }
        }

        public static double LuongCoBan { get; } = 2340000;
        #endregion

        #region Constructors
        public NhanVienABC(string maNhanVien, string hoTen, int namSinh, string gioiTinh, double heSL, int namVL)
        {
            this.MaNV = maNhanVien;
            this.HoTen = hoTen;
            this.NamSinh = namSinh;
            this.HeSoLuong = heSL;
            this.GioiTinh = gioiTinh;
            this.NamVaoLam = namVL;
        }

        public NhanVienABC()
        {
            MaNV = "NV001";
            HoTen = "Nguyễn Văn A";
            NamSinh = 1987;
            GioiTinh = "Nam";
            HeSoLuong = 5.55;
            NamVaoLam = 2005;
        }
        #endregion

        #region Methods
        public virtual double TinhPhuCapThamNien()
        {
            int soNamLamViec = DateTime.Now.Year - NamVaoLam;
            if (soNamLamViec >= 5)
            {
                return (double)(soNamLamViec / 100) * LuongCoBan;
            }
            return 0;
        }

        public virtual double ThuNhap()
        {
            char xepLoai = XepLoai();
            double heSoThuNhap;
            
            switch (xepLoai)
            {
                case 'A': heSoThuNhap = 1.0; break;
                case 'B': heSoThuNhap = 0.75; break;
                case 'C': heSoThuNhap = 0.5; break;
                default: heSoThuNhap = 0; break;
            }
            
            return heSoThuNhap * TinhLuong() + TinhPhuCapThamNien();
        }

        public abstract char XepLoai();
        public abstract double TinhLuong();

        public virtual void Xuat()
        {
            Console.WriteLine("=== THÔNG TIN NHÂN VIÊN ===");
            Console.WriteLine($"Mã NV: {MaNV}");
            Console.WriteLine($"Họ tên: {HoTen}");
            Console.WriteLine($"Năm sinh: {NamSinh}");
            Console.WriteLine($"Giới tính: {GioiTinh}");
            Console.WriteLine($"Hệ số lương: {HeSoLuong:F2}");
            Console.WriteLine($"Năm vào làm: {NamVaoLam}");
            Console.WriteLine($"Xếp loại: {XepLoai()}");
            Console.WriteLine($"Lương cơ bản: {TinhLuong():C0}");
            Console.WriteLine($"Phụ cấp thâm niên: {TinhPhuCapThamNien():C0}");
            Console.WriteLine($"Thu nhập: {ThuNhap():C0}");
        }

        // Override ToString để hỗ trợ Polymorphism
        public override string ToString()
        {
            return $"{MaNV} - {HoTen} - {XepLoai()} - {TinhLuong():C0}";
        }

        // Override Equals và GetHashCode để hỗ trợ so sánh
        public override bool Equals(object obj)
        {
            if (obj is NhanVienABC nv)
                return MaNV.Equals(nv.MaNV);
            return false;
        }

        public override int GetHashCode()
        {
            return MaNV.GetHashCode();
        }
        #endregion
    }
}
