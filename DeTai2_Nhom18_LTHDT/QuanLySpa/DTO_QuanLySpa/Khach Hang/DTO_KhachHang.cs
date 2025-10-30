using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace DTO_QuanLySpa.Khach_Hang
{
    public class DTO_KhachHang
    {
        private string maKH;
        private string tenKH;
        private string soDienThoai;
        private List<DTO_DichVu> dsDV_DTO = new List<DTO_DichVu>();

        public string MaKH
        {
            get { return maKH; }
            set
            {
                if (value.Length == 5 && value.StartsWith("KH") && value.Substring(2).All(char.IsDigit))
                    maKH = value;
                else
                    maKH = "KH000";
            }
        }
        public string TenKH
        {
            get { return tenKH; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Tên khách hàng không hợp lệ!");
                tenKH = value.Trim();
            }
        }

        public string SoDienThoai
        {
            get { return soDienThoai; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Số điện thoại không hợp lệ!");
                soDienThoai = value.Trim();
            }
        }

        public List<DTO_DichVu> DSDV_DTO
        {
            get { return dsDV_DTO; }
            set { dsDV_DTO = value; }
        }
        public void NhapTT(List<DTO_DichVu> danhSachChung)
        {
            Console.Write("Mã Khách Hàng: ");
            MaKH = Console.ReadLine();
            Console.Write("Tên Khách Hàng: ");
            TenKH = Console.ReadLine();
            Console.Write("Số Điện Thoại: ");
            SoDienThoai = Console.ReadLine();
            if (danhSachChung.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Danh sach rong![/]");
                return;
            }
            Console.Write("\nNhập số lượng dịch vụ mà khách hàng sử dụng: ");
            int soLuong;
            if (!int.TryParse(Console.ReadLine(), out soLuong) || soLuong <= 0)
            {
                Console.WriteLine("Số lượng không hợp lệ!");
                return;
            }
            var table = new Table();
            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ CÓ SẴN[/]");
            table.Border = TableBorder.Rounded;
            table.AddColumn("[cyan]Loại DV[/]");
            table.AddColumn("[yellow]Mã DV[/]");
            table.AddColumn("[magenta]Tên DV[/]");
            table.AddColumn("[blue]Giá Gốc[/]");
            table.AddColumn("[red]Giảm[/]");
            table.AddColumn("[blue]VAT[/]");
            table.AddColumn("[cyan]Thành Tiền[/]");
            foreach (var dv in danhSachChung)
            {
                dv.XuatTT(table);
            }
            AnsiConsole.Write(table);
            for (int i = 0; i < soLuong; i++)
            {
                Console.Write($"Nhập mã dịch vụ thứ {i + 1}: ");
                string ma = Console.ReadLine();
                var dv = danhSachChung.FirstOrDefault(x => x.MaDV.ToLower() == ma.ToLower());
                if (dv != null)
                {
                    dsDV_DTO.Add(dv);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy mã dịch vụ này!");
                    i--;
                }
            }

        }
        public void XuatTT(Table table)
        {
            if (dsDV_DTO.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng");
                return;
            }
            double tongTienKH = dsDV_DTO.Sum(dv => dv.TinhThanhTien());
            bool laDongDauTien = true;
            foreach (var dv in dsDV_DTO)
            {
                double thanhTien = dv.TinhThanhTien();

                if (laDongDauTien)
                {
                    table.AddRow(
                        $"[white]{MaKH}[/]",
                        $"[yellow]{TenKH}[/]",
                        $"[cyan]{SoDienThoai}[/]",
                        $"[white]{dv.MaDV}[/]",
                        $"[blue]{dv.TenDV}[/]",
                        $"[white]{dv.GiaTien:N0}[/]",
                        $"[yellow]{thanhTien:N0}[/]",
                        $"[green]{tongTienKH:N0}[/]"
                    );
                    laDongDauTien = false;
                }
                else
                {
                    table.AddRow(
                        "", "", "",
                        $"[white]{dv.MaDV}[/]",
                        $"[blue]{dv.TenDV}[/]",
                        $"[white]{dv.GiaTien:N0}[/]",
                        $"[yellow]{thanhTien:N0}[/]",
                        ""
                    );
                }
            }
        }
    }
}
