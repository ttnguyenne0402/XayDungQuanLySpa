using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_QuanLySpa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            GUI_QuanLyKhachHang_DichVu ql = new GUI_QuanLyKhachHang_DichVu();
            ql.Menu();
        }
    }
}
