using DAL_QuanLySpa;
using DTO_QuanLySpa;
using DTO_QuanLySpa.Khach_Hang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Diagnostics.Eventing.Reader;
namespace BLL_QuanLySpa
{
    public class BLL_DanhSachKhachHang
    {
        private BLL_DanhSachDichVu dsDV_BLL = new BLL_DanhSachDichVu();
        private DAL_DanhSachKhachHang dsKH_DAL;
        private List<DTO_KhachHang> dsKH_DTO = new List<DTO_KhachHang>();
        public BLL_DanhSachDichVu DSDV_BLL
        {
            get { return dsDV_BLL; }
            set { dsDV_BLL = value; }
        }
        public DAL_DanhSachKhachHang DSKH_DAL
        {
            get { return dsKH_DAL; }
            set { dsKH_DAL = value; }
        }

        public List<DTO_KhachHang> DSKH_DTO
        {
            get { return dsKH_DTO; }
            set { dsKH_DTO = value; }
        }
        public BLL_DanhSachKhachHang(BLL_DanhSachDichVu dsDVChung)
        {
            if (dsDVChung.GetDichVu().Any())
            {
                dsDV_BLL = dsDVChung;
                dsKH_DAL = new DAL_DanhSachKhachHang(dsDV_BLL.GetDichVu());
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Không thể khởi tạo danh sách khách hàng vì dữ liệu dịch vụ rỗng hoặc lỗi![/]");
                dsKH_DAL = new DAL_DanhSachKhachHang(new List<DTO_DichVu>());
            }
        }
        public void DocFile()
        {
            dsKH_DTO = dsKH_DAL.DocFILE("../../../Data/DanhSachKhachHang.xml", dsKH_DAL.DSDV_DAL);
        }
        public void XuatTT()
        {
            var table = new Table();
            table.Title = new TableTitle("[yellow]DANH SÁCH KHÁCH HÀNG VÀ DỊCH VỤ SỬ DỤNG[/]");
            table.Border = TableBorder.Rounded;
            table.AddColumn("[cyan]Mã Khách Hàng[/]");
            table.AddColumn("[white]Tên Khách Hàng[/]");
            table.AddColumn("[blue]Số Điện Thoại[/]");
            table.AddColumn("[yellow]Mã Dịch Vụ[/]");
            table.AddColumn("[red]Tên Dịch Vụ[/]");
            table.AddColumn("[blue]Giá Tiền[/]");
            table.AddColumn("[yellow]Thành Tiền[/]");
            table.AddColumn("[white]Tổng Tiền[/]");
            foreach (var kh in dsKH_DTO)
                kh.XuatTT(table);
            AnsiConsole.Write(table);
        }
        public List<DTO_KhachHang> GetKhachHang()
        {
            return dsKH_DTO;
        }
        public void ThemKhachHangMoi()
        {
            Console.Write("Hãy nhập số lượng khách hàng: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                DTO_KhachHang kh = new DTO_KhachHang();
                kh.NhapTT(dsDV_BLL.GetDichVu());
                dsKH_DTO.Add(kh);
            }
            AnsiConsole.MarkupLine("[green]Đã thêm khách hàng mới thành công![/]");
        }
        public DTO_KhachHang TimTheoMa(string ma)
        {
            return dsKH_DTO.FirstOrDefault(kh => kh.MaKH.ToLower() == ma.ToLower());
        }

        public DTO_KhachHang TimTheoTen(string ten)
        {
            return dsKH_DTO.FirstOrDefault(kh => kh.TenKH.ToLower().Contains(ten.ToLower()));
        }
        public void ThemDichVuChoKhachHang(List<DTO_KhachHang> dsKH, List<DTO_DichVu> dsDV)
        {
            Console.Write("Hãy nhập mã kháchh hàng: ");
            string ma = Console.ReadLine();
            var kh = dsKH.FirstOrDefault(x => x.MaKH.ToLower() == ma.ToLower());
            if (kh != null)
            {
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
                foreach (var x in dsDV)
                {
                    x.XuatTT(table);
                }
                AnsiConsole.Write(table);
                Console.Write("Nhập mã dịch vụ muốn thêm: ");
                string maDV = Console.ReadLine();
                if (kh.DSDV_DTO.FirstOrDefault(x => x.MaDV.ToLower() == maDV.ToLower()) != null)
                {
                    AnsiConsole.MarkupLine("[red]Khách hàng này đã có dịch vụ đó rồi![/]");
                    return;
                }
                var dv = dsDV.FirstOrDefault(x => x.MaDV.ToLower() == maDV.ToLower());
                if (dv == null)
                {
                    AnsiConsole.MarkupLine("[red]Mã dịch vụ không tồn tại trong hệ thống![/]");
                    return;
                }
                kh.DSDV_DTO.Add(dv);
                Console.WriteLine($"Đã thêm dịch vụ {maDV} cho khách hàng {kh.TenKH} có mã là {kh.MaKH}!");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Mã khách hàng không tồn tại trong hệ thống![/]");
            }
        }
        public void XuatDichVuTheoTenKH(string tenKH)
        {
            var khachhang = dsKH_DTO.FirstOrDefault(x => x.TenKH.ToLower().Contains(tenKH.ToLower()));
            if (khachhang == null)
            {
                AnsiConsole.MarkupLine($"[yellow]Khong tim thay khach hang '{tenKH}'![/]");
                return;
            }
            else
            {
                if (khachhang.DSDV_DTO.Any())
                {
                    var table = new Table();
                    table.Title = new TableTitle($"[yellow]DANH SÁCH DỊCH VỤ MÀ KHÁCH HÀNG '{khachhang.TenKH}' SỬ DỤNG[/]");
                    table.Border = TableBorder.Rounded;
                    table.AddColumn("[cyan]Loại DV[/]");
                    table.AddColumn("[yellow]Mã DV[/]");
                    table.AddColumn("[magenta]Tên DV[/]");
                    table.AddColumn("[blue]Giá Gốc[/]");
                    table.AddColumn("[red]Giảm[/]");
                    table.AddColumn("[blue]VAT[/]");
                    table.AddColumn("[cyan]Thành Tiền[/]");
                    foreach (var dv in khachhang.DSDV_DTO)
                    {
                        dv.XuatTT(table);
                    }
                    AnsiConsole.Write(table);
                    double tongTien = khachhang.DSDV_DTO.Sum(x => x.TinhThanhTien());
                    AnsiConsole.MarkupLine($"[yellow]Tổng tiền khách hàng '{khachhang.TenKH}' phải trả là: {tongTien:N0} VNĐ[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[yellow]Khách hàng '{khachhang.TenKH}' hiện chưa sử dụng dịch vụ nào[/]");
                }
            }
        }
        public void XuatKhachHangNhieuHon3DichVu()
        {
            var ds3DV = dsKH_DTO.Where(x => x.DSDV_DTO.Count() > 3).ToList();
            if (ds3DV.Any())
            {
                var table = new Table();
                table.Title = new TableTitle("[yellow]DANH SÁCH KHÁCH HÀNG THỰC HIỆN NHIỀU HƠN 3 DỊCH VỤ TẠI SPA[/]");
                table.Border = TableBorder.Rounded;
                table.AddColumn("[cyan]Mã Khách Hàng[/]");
                table.AddColumn("[white]Tên Khách Hàng[/]");
                table.AddColumn("[blue]Số Điện Thoại[/]");
                table.AddColumn("[yellow]Mã Dịch Vụ[/]");
                table.AddColumn("[red]Tên Dịch Vụ[/]");
                table.AddColumn("[blue]Giá Tiền[/]");
                table.AddColumn("[yellow]Thành Tiền[/]");
                table.AddColumn("[white]Tổng Tiền[/]");
                foreach (var kh in ds3DV)
                    kh.XuatTT(table);
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Không có khách hàng nào sử dụng trên 3 dịch vụ cả![/]");
                return;
            }
        }
        public void XoaKhachHangTheoMa(string maKH)
        {
            var khCanXoa = dsKH_DTO.FirstOrDefault(x => x.MaKH.ToLower() == maKH.ToLower());
            if (khCanXoa == null)
            {
                AnsiConsole.MarkupLine($"[yellow]Không tìm thấy dịch vụ có mã dịch vụ là: {maKH.ToUpper()}[/]");
                return;
            }
            else
            {
                dsKH_DTO.Remove(khCanXoa);
                AnsiConsole.MarkupLine($"[yellow]Đã xóa thành công dịch vụ có mã: {maKH.ToUpper()}[/]");
            }
        }
        public void SapXepKhachHangTheoThanhTienGiamDan()
        {
            dsKH_DTO = dsKH_DTO.OrderByDescending(x => x.DSDV_DTO.Sum(dv => dv.TinhThanhTien())).ThenBy(x => x.MaKH).ToList();
            if (dsKH_DTO.Any())
            {
                AnsiConsole.MarkupLine("[yellow]Sắp xếp thành công[/]");
                XuatTT();
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Sắp xếp không thành công[/]");
                return;
            }
        }
        public double TongThanhTienSpa()
        {
            return dsKH_DTO.Sum(x => x.DSDV_DTO.Sum(dv => dv.TinhThanhTien()));
        }
    }
}
