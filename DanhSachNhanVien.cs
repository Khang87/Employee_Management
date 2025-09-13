using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NhanVien
{
    // Interface cho CRUD operations
    public interface IQuanLyNhanVien<T> where T : INhanVien
    {
        void Them(T nhanVien);
        bool Xoa(string maNV);
        bool CapNhat(T nhanVien);
        T TimKiem(string maNV);
        List<T> TimKiemTheoTen(string ten);
        List<T> LayTatCa();
        int DemSoLuong();
        void XuatDanhSach();
        void SapXepTheoTen();
        void SapXepTheoLuong();
    }

    public class DanhSachNhanVien : IQuanLyNhanVien<NhanVienABC>
    {
        #region Private Fields
        private List<NhanVienABC> _danhSach;
        #endregion

        #region Properties
        public int SoLuong => _danhSach.Count;
        public bool IsEmpty => _danhSach.Count == 0;
        #endregion

        #region Constructors
        public DanhSachNhanVien()
        {
            _danhSach = new List<NhanVienABC>();
        }
        #endregion

        #region CRUD Operations
        // CREATE - Thêm nhân viên mới
        public void Them(NhanVienABC nhanVien)
        {
            if (nhanVien == null)
                throw new ArgumentNullException(nameof(nhanVien), "Nhân viên không được null");

            if (TimKiem(nhanVien.MaNV) != null)
                throw new InvalidOperationException($"Nhân viên với mã {nhanVien.MaNV} đã tồn tại");

            _danhSach.Add(nhanVien);
        }

        // READ - Tìm kiếm nhân viên theo mã
        public NhanVienABC TimKiem(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return null;

            return _danhSach.FirstOrDefault(nv => nv.MaNV.Equals(maNV.Trim().ToUpper()));
        }

        // READ - Tìm kiếm nhân viên theo tên
        public List<NhanVienABC> TimKiemTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return new List<NhanVienABC>();

            return _danhSach.Where(nv => nv.HoTen.IndexOf(ten.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // READ - Lấy tất cả nhân viên
        public List<NhanVienABC> LayTatCa()
        {
            return new List<NhanVienABC>(_danhSach);
        }

        // UPDATE - Cập nhật thông tin nhân viên
        public bool CapNhat(NhanVienABC nhanVien)
        {
            if (nhanVien == null)
                return false;

            var index = _danhSach.FindIndex(nv => nv.MaNV.Equals(nhanVien.MaNV));
            if (index >= 0)
            {
                _danhSach[index] = nhanVien;
                return true;
            }
            return false;
        }

        // DELETE - Xóa nhân viên theo mã
        public bool Xoa(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                return false;

            var nhanVien = TimKiem(maNV);
            if (nhanVien != null)
            {
                _danhSach.Remove(nhanVien);
                return true;
            }
            return false;
        }
        #endregion

        #region Utility Methods
        public int DemSoLuong()
        {
            return _danhSach.Count;
        }

        public void XuatDanhSach()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Danh sách nhân viên trống!");
                return;
            }

            Console.WriteLine($"\n=== DANH SÁCH NHÂN VIÊN ({SoLuong} người) ===");
            for (int i = 0; i < _danhSach.Count; i++)
            {
                Console.WriteLine($"\n--- Nhân viên {i + 1} ---");
                _danhSach[i].Xuat();
            }
        }

        public void SapXepTheoTen()
        {
            _danhSach.Sort((nv1, nv2) => nv1.HoTen.CompareTo(nv2.HoTen));
        }

        public void SapXepTheoLuong()
        {
            _danhSach.Sort((nv1, nv2) => nv2.TinhLuong().CompareTo(nv1.TinhLuong()));
        }

        // Thống kê theo loại nhân viên
        public void ThongKeTheoLoai()
        {
            var thongKe = _danhSach.GroupBy(nv => nv.GetType().Name)
                                  .Select(g => new { Loai = g.Key, SoLuong = g.Count() })
                                  .OrderBy(x => x.Loai);

            Console.WriteLine("\n=== THỐNG KÊ THEO LOẠI NHÂN VIÊN ===");
            foreach (var item in thongKe)
            {
                Console.WriteLine($"{item.Loai}: {item.SoLuong} người");
            }
        }

        // Thống kê theo xếp loại
        public void ThongKeTheoXepLoai()
        {
            var thongKe = _danhSach.GroupBy(nv => nv.XepLoai())
                                  .Select(g => new { XepLoai = g.Key, SoLuong = g.Count() })
                                  .OrderBy(x => x.XepLoai);

            Console.WriteLine("\n=== THỐNG KÊ THEO XẾP LOẠI ===");
            foreach (var item in thongKe)
            {
                Console.WriteLine($"Xếp loại {item.XepLoai}: {item.SoLuong} người");
            }
        }
        #endregion

        #region File Operations
        public void Doc_File_Xml(string file)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNodeList nodeList = doc.SelectNodes("CongTy/NhanVien");
                
                if (nodeList == null || nodeList.Count == 0)
                {
                    Console.WriteLine("Không tìm thấy dữ liệu nhân viên trong file XML");
                    return;
                }

                foreach (XmlNode node in nodeList)
                {
                    try
                    {
                        string MaNV = node["MaNV"].InnerText;
                        string HoTen = node["HoTen"].InnerText;
                        int NamSinh = int.Parse(node["NamSinh"].InnerText);
                        string Gt = node["GioiTinh"].InnerText;
                        double HSL = double.Parse(node["HeSoLuong"].InnerText);
                        int NVL = int.Parse(node["NamVaoLam"].InnerText);

                        NhanVienABC nhanVien = null;

                        if (node.Attributes["loai"].Value == "1")
                        {
                            int SoNgayNghi = int.Parse(node["SoNgayNghi"].InnerText);
                            nhanVien = new NhanVienSX(MaNV, HoTen, NamSinh, HSL, NVL, Gt, SoNgayNghi);
                        }
                        else if (node.Attributes["loai"].Value == "2")
                        {
                            double dsToiThieu = double.Parse(node["DoanhSoToiThieu"].InnerText);
                            double dsThucTe = double.Parse(node["DoanhSoThucTe"].InnerText);
                            nhanVien = new NhanVienKD(MaNV, HoTen, NamSinh, Gt, HSL, NVL, dsToiThieu, dsThucTe);
                        }
                        else
                        {
                            double heSoPCCV = double.Parse(node["HeSoChucVu"].InnerText);
                            string ChucVu = node["ChucVu"].InnerText;
                            nhanVien = new CanBoQuanLy(MaNV, HoTen, NamSinh, HSL, NVL, Gt, heSoPCCV, ChucVu);
                        }

                        if (nhanVien != null)
                        {
                            Them(nhanVien);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi đọc nhân viên: {ex.Message}");
                    }
                }

                Console.WriteLine($"Đã đọc thành công {SoLuong} nhân viên từ file XML");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file XML: {ex.Message}");
            }
        }

        // Giữ lại method cũ để tương thích
        public void XuatDs()
        {
            XuatDanhSach();
        }
        #endregion
    }
}
