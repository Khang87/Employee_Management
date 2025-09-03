using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NhanVien
{
    class DanhSachNhanVien
    {
        List<NhanVienABC> ds = new List<NhanVienABC>();
        public void Doc_File_Xml(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNodeList nodeList = doc.SelectNodes("CongTy/NhanVien");
            foreach (XmlNode node in nodeList)
            {

                string MaNV = node["MaNV"].InnerText;
                string HoTen = node["HoTen"].InnerText;
                int NamSinh = int.Parse(node["NamSinh"].InnerText);
                string Gt = node["GioiTinh"].InnerText;
                double HSL = double.Parse(node["HeSoLuong"].InnerText);
                int NVL = int.Parse(node["NamVaoLam"].InnerText);
                if (node.Attributes["loai"].Value == "1")
                {
                    int SoNgayNghi = int.Parse(node["SoNgayNghi"].InnerText);
                    NhanVienSX nvsx = new NhanVienSX(MaNV, HoTen, NamSinh, HSL, NVL, Gt, SoNgayNghi);
                    ds.Add(nvsx);
                }
                else if (node.Attributes["loai"].Value == "2")
                {
                    double dsToiThieu = double.Parse(node["DoanhSoToiThieu"].InnerText);
                    double dsThucTe = double.Parse(node["DoanhSoThucTe"].InnerText);
                    NhanVienKD nvkd = new NhanVienKD(MaNV, HoTen, NamSinh, Gt, HSL, NVL, dsToiThieu, dsThucTe);
                    ds.Add(nvkd);
                }
                else
                {
                    double heSoPCCV = double.Parse(node["HeSoChucVu"].InnerText);
                    string ChucVu = node["ChucVu"].InnerText;
                    CanBoQuanLy cbql = new CanBoQuanLy(MaNV, HoTen, NamSinh, HSL, NVL, Gt, heSoPCCV, ChucVu);
                    ds.Add(cbql);
                }
            }

        }
        public void XuatDs()
        {
            Console.WriteLine("DANH SACH NHAN VIEN : ");
            foreach (NhanVienABC i in ds)
            {
                i.Xuat();
            }

        }
    }
}
