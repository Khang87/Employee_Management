using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    class Program
    {
        private static DanhSachNhanVien danhSachNhanVien = new DanhSachNhanVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Đọc dữ liệu từ file XML nếu có
            try
            {
                danhSachNhanVien.Doc_File_Xml(@"..\..\XMLFile1.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể đọc file XML: {ex.Message}");
            }

            HienThiMenuChinh();
        }

        static void HienThiMenuChinh()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    QUẢN LÝ NHÂN VIÊN                        ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Thêm nhân viên mới                                       ║");
                Console.WriteLine("║ 2. Xem danh sách nhân viên                                  ║");
                Console.WriteLine("║ 3. Tìm kiếm nhân viên                                       ║");
                Console.WriteLine("║ 4. Cập nhật thông tin nhân viên                             ║");
                Console.WriteLine("║ 5. Xóa nhân viên                                            ║");
                Console.WriteLine("║ 6. Sắp xếp danh sách                                        ║");
                Console.WriteLine("║ 7. Thống kê                                                 ║");
                Console.WriteLine("║ 8. Demo Polymorphism                                        ║");
                Console.WriteLine("║ 0. Thoát chương trình                                       ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.Write("Chọn chức năng (0-8): ");

                string luaChon = Console.ReadLine();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "1": ThemNhanVien(); break;
                    case "2": XemDanhSach(); break;
                    case "3": TimKiemNhanVien(); break;
                    case "4": CapNhatNhanVien(); break;
                    case "5": XoaNhanVien(); break;
                    case "6": SapXepDanhSach(); break;
                    case "7": ThongKe(); break;
                    case "8": DemoPolymorphism(); break;
                    case "0": ThoatChuongTrinh(); return;
                    default: 
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại.");
                        NhanPhimBatKy();
                        break;
                }
            }
        }

        static void ThemNhanVien()
        {
            Console.WriteLine("=== THÊM NHÂN VIÊN MỚI ===");
            Console.WriteLine("1. Nhân viên sản xuất");
            Console.WriteLine("2. Nhân viên kinh doanh");
            Console.WriteLine("3. Cán bộ quản lý");
            Console.Write("Chọn loại nhân viên (1-3): ");

            string loai = Console.ReadLine();
            try
            {
                NhanVienABC nhanVien = NhapThongTinNhanVien(loai);
                if (nhanVien != null)
                {
                    danhSachNhanVien.Them(nhanVien);
                    Console.WriteLine("✓ Thêm nhân viên thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Lỗi: {ex.Message}");
            }

            NhanPhimBatKy();
        }

        static NhanVienABC NhapThongTinNhanVien(string loai)
        {
            Console.Write("Mã nhân viên: ");
            string maNV = Console.ReadLine();
            Console.Write("Họ tên: ");
            string hoTen = Console.ReadLine();
            Console.Write("Năm sinh: ");
            int namSinh = int.Parse(Console.ReadLine());
            Console.Write("Giới tính (Nam/Nữ): ");
            string gioiTinh = Console.ReadLine();
            Console.Write("Hệ số lương: ");
            double heSoLuong = double.Parse(Console.ReadLine());
            Console.Write("Năm vào làm: ");
            int namVaoLam = int.Parse(Console.ReadLine());

            switch (loai)
            {
                case "1":
                    Console.Write("Số ngày nghỉ: ");
                    int soNgayNghi = int.Parse(Console.ReadLine());
                    return new NhanVienSX(maNV, hoTen, namSinh, heSoLuong, namVaoLam, gioiTinh, soNgayNghi);

                case "2":
                    Console.Write("Doanh số tối thiểu: ");
                    double doanhSoToiThieu = double.Parse(Console.ReadLine());
                    Console.Write("Doanh số thực tế: ");
                    double doanhSoThucTe = double.Parse(Console.ReadLine());
                    return new NhanVienKD(maNV, hoTen, namSinh, gioiTinh, heSoLuong, namVaoLam, doanhSoToiThieu, doanhSoThucTe);

                case "3":
                    Console.Write("Chức vụ: ");
                    string chucVu = Console.ReadLine();
                    Console.Write("Hệ số phụ cấp chức vụ: ");
                    double heSoPCCV = double.Parse(Console.ReadLine());
                    return new CanBoQuanLy(maNV, hoTen, namSinh, heSoLuong, namVaoLam, gioiTinh, heSoPCCV, chucVu);

                default:
                    Console.WriteLine("Loại nhân viên không hợp lệ!");
                    return null;
            }
        }

        static void XemDanhSach()
        {
            Console.WriteLine("=== DANH SÁCH NHÂN VIÊN ===");
            danhSachNhanVien.XuatDanhSach();
            NhanPhimBatKy();
        }

        static void TimKiemNhanVien()
        {
            Console.WriteLine("=== TÌM KIẾM NHÂN VIÊN ===");
            Console.WriteLine("1. Tìm theo mã nhân viên");
            Console.WriteLine("2. Tìm theo tên");
            Console.Write("Chọn cách tìm kiếm (1-2): ");

            string cach = Console.ReadLine();
            if (cach == "1")
            {
                Console.Write("Nhập mã nhân viên: ");
                string maNV = Console.ReadLine();
                var nhanVien = danhSachNhanVien.TimKiem(maNV);
                if (nhanVien != null)
                {
                    Console.WriteLine("\n=== KẾT QUẢ TÌM KIẾM ===");
                    nhanVien.Xuat();
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên với mã này!");
                }
            }
            else if (cach == "2")
            {
                Console.Write("Nhập tên cần tìm: ");
                string ten = Console.ReadLine();
                var ketQua = danhSachNhanVien.TimKiemTheoTen(ten);
                if (ketQua.Count > 0)
                {
                    Console.WriteLine($"\n=== KẾT QUẢ TÌM KIẾM ({ketQua.Count} kết quả) ===");
                    foreach (var nv in ketQua)
                    {
                        Console.WriteLine(nv.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên nào!");
                }
            }

            NhanPhimBatKy();
        }

        static void CapNhatNhanVien()
        {
            Console.WriteLine("=== CẬP NHẬT THÔNG TIN NHÂN VIÊN ===");
            Console.Write("Nhập mã nhân viên cần cập nhật: ");
            string maNV = Console.ReadLine();

            var nhanVienCu = danhSachNhanVien.TimKiem(maNV);
            if (nhanVienCu == null)
            {
                Console.WriteLine("Không tìm thấy nhân viên với mã này!");
                NhanPhimBatKy();
                return;
            }

            Console.WriteLine("Thông tin hiện tại:");
            nhanVienCu.Xuat();

            Console.WriteLine("\nNhập thông tin mới:");
            try
            {
                NhanVienABC nhanVienMoi = NhapThongTinNhanVien(GetLoaiNhanVien(nhanVienCu));
                if (nhanVienMoi != null)
                {
                    if (danhSachNhanVien.CapNhat(nhanVienMoi))
                    {
                        Console.WriteLine("✓ Cập nhật thành công!");
                    }
                    else
                    {
                        Console.WriteLine("✗ Cập nhật thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Lỗi: {ex.Message}");
            }

            NhanPhimBatKy();
        }

        static string GetLoaiNhanVien(NhanVienABC nhanVien)
        {
            if (nhanVien is NhanVienSX) return "1";
            if (nhanVien is NhanVienKD) return "2";
            if (nhanVien is CanBoQuanLy) return "3";
            return "1";
        }

        static void XoaNhanVien()
        {
            Console.WriteLine("=== XÓA NHÂN VIÊN ===");
            Console.Write("Nhập mã nhân viên cần xóa: ");
            string maNV = Console.ReadLine();

            var nhanVien = danhSachNhanVien.TimKiem(maNV);
            if (nhanVien == null)
            {
                Console.WriteLine("Không tìm thấy nhân viên với mã này!");
                NhanPhimBatKy();
                return;
            }

            Console.WriteLine("Thông tin nhân viên sẽ bị xóa:");
            nhanVien.Xuat();
            Console.Write("\nBạn có chắc chắn muốn xóa? (y/n): ");

            if (Console.ReadLine().ToLower() == "y")
            {
                if (danhSachNhanVien.Xoa(maNV))
                {
                    Console.WriteLine("✓ Xóa thành công!");
                }
                else
                {
                    Console.WriteLine("✗ Xóa thất bại!");
                }
            }
            else
            {
                Console.WriteLine("Đã hủy thao tác xóa.");
            }

            NhanPhimBatKy();
        }

        static void SapXepDanhSach()
        {
            Console.WriteLine("=== SẮP XẾP DANH SÁCH ===");
            Console.WriteLine("1. Sắp xếp theo tên");
            Console.WriteLine("2. Sắp xếp theo lương (cao đến thấp)");
            Console.Write("Chọn cách sắp xếp (1-2): ");

            string cach = Console.ReadLine();
            if (cach == "1")
            {
                danhSachNhanVien.SapXepTheoTen();
                Console.WriteLine("✓ Đã sắp xếp theo tên!");
            }
            else if (cach == "2")
            {
                danhSachNhanVien.SapXepTheoLuong();
                Console.WriteLine("✓ Đã sắp xếp theo lương!");
            }

            NhanPhimBatKy();
        }

        static void ThongKe()
        {
            Console.WriteLine("=== THỐNG KÊ ===");
            Console.WriteLine($"Tổng số nhân viên: {danhSachNhanVien.SoLuong}");
            danhSachNhanVien.ThongKeTheoLoai();
            danhSachNhanVien.ThongKeTheoXepLoai();
            NhanPhimBatKy();
        }

        static void DemoPolymorphism()
        {
            Console.WriteLine("=== DEMO POLYMORPHISM ===");
            Console.WriteLine("Tất cả nhân viên đều được xử lý thông qua interface INhanVien:");
            Console.WriteLine();

            var danhSach = danhSachNhanVien.LayTatCa();
            foreach (var nhanVien in danhSach)
            {
                // Polymorphism: Gọi cùng một method nhưng có hành vi khác nhau
                Console.WriteLine($"Loại: {nhanVien.GetType().Name}");
                Console.WriteLine($"Xếp loại: {nhanVien.XepLoai()}"); // Mỗi loại có cách xếp loại khác nhau
                Console.WriteLine($"Lương: {nhanVien.TinhLuong():C0}"); // Mỗi loại có cách tính lương khác nhau
                Console.WriteLine($"Thu nhập: {nhanVien.ThuNhap():C0}"); // Sử dụng virtual method
                Console.WriteLine("---");
            }

            NhanPhimBatKy();
        }

        static void ThoatChuongTrinh()
        {
            Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
        }

        static void NhanPhimBatKy()
        {
            Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}
