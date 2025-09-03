using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanVien
{
    class Program
    {
        static void Main(string[] args)
        {
            DanhSachNhanVien ds = new DanhSachNhanVien();
            ds.Doc_File_Xml(@"..\..\XMLFile1.xml");
            ds.XuatDs();
            Console.ReadLine();
        }
    }
}
