using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace DTO_QuanLySpa
{
    public abstract class DTO_DichVu
    {
        protected string maDV;
        protected string tenDV;
        protected double giaTien;
        public static double VAT = 0.10;
        public string MaDV
        {
            get { return maDV; }
            set
            {
                if (value.Length == 5 && value.StartsWith("DV") && value.Substring(2).All(char.IsDigit))
                    maDV = value;
                else throw new Exception("Loi, ma dich vu khong hop le");
            }
        }
        public string TenDV
        {
            get { return tenDV; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new Exception("Loi, ten dich vu khong hop le");
                tenDV = value;
            }
        }
        public double GiaTien
        {
            get { return giaTien; }
            set
            {
                if (value > 0) giaTien = value;
                else throw new Exception("Loi, gia tien khong hop le");
            }
        }
        public DTO_DichVu()
        {
        }
        public DTO_DichVu(string maDV, string tenDV, double giaTien)
        {
            MaDV = maDV;
            TenDV = tenDV;
            GiaTien = giaTien;
        }
        public abstract double TinhThanhTien();
        public void NhapTT()
        {
            Console.Write("Mã Dịch Vụ: ");
            MaDV = Console.ReadLine().ToUpper();
            Console.Write("Tên Dịch Vụ: ");
            TenDV = Console.ReadLine();
            Console.Write("Giá Tiền: ");
            GiaTien = double.Parse(Console.ReadLine());
        }
        public abstract void XuatTT(Table table);
    }
}
