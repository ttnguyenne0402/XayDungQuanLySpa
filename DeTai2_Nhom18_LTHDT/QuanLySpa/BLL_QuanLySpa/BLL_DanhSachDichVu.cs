using DAL_QuanLySpa;
using DTO_QuanLySpa;
using DTO_QuanLySpa.Khach_Hang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace BLL_QuanLySpa
{
    public class BLL_DanhSachDichVu
    {
        private DAL_DanhSachDichVu dsDV_DAL = new DAL_DanhSachDichVu();
        private List<DTO_DichVu> dsDV_DTO = new List<DTO_DichVu>();

        public DAL_DanhSachDichVu DSDV_DAL
        {
            get { return dsDV_DAL; }
            set { dsDV_DAL = value; }
        }
        public List<DTO_DichVu> DSDV_DTO
        {
            get { return dsDV_DTO; }
            set { dsDV_DTO = value; }
        }

        public void DocFile()
        {
            DSDV_DTO = DSDV_DAL.DocFILE("../../../Data/DanhSachDichVu.xml");

        }
        public void XuatTT()
        {
            if (DSDV_DTO == null || DSDV_DTO.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Danh sach rong![/]");
                return;
            }
            var table = new Table();
            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ[/]");
            table.Border = TableBorder.Rounded;
            table.AddColumn("[cyan]Loại DV[/]");
            table.AddColumn("[yellow]Mã DV[/]");
            table.AddColumn("[magenta]Tên DV[/]");
            table.AddColumn("[blue]Giá Gốc[/]");
            table.AddColumn("[red]Giảm[/]");
            table.AddColumn("[blue]VAT[/]");
            table.AddColumn("[cyan]Thành Tiền[/]");
            foreach (var dv in DSDV_DTO)
            {
                dv.XuatTT(table);
            }
            AnsiConsole.Write(table);
        }
        public void ThemDichVuMoi()
        {
            Console.Write("Hãy nhập số lượng dịch vụ bạn muốn thêm: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Lỗi, số lượng không hợp lệ");
                return;
            }
            for (int i = 0; i < n; i++)
            {
                DTO_DichVu dv = null;
                var chon = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[bold aqua]Chọn dịch vụ bạn muốn thực hiện:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "1️. Chăm Sóc Sắc Đẹp",
                        "2️. Chăm Sóc Body",
                        "3️. Dưỡng Sinh Trị Liệu",
                    }));
                switch (chon)
                {
                    case "1️. Chăm Sóc Sắc Đẹp":
                        dv = new DTO_ChamSocSacDep();
                        break;
                    case "2️. Chăm Sóc Body":
                        dv = new DTO_ChamSocBody();
                        break;
                    case "3️. Dưỡng Sinh Trị Liệu":
                        dv = new DTO_DuongSinhTriLieu();
                        break;
                }
                dv.NhapTT();
                if (DSDV_DTO.FirstOrDefault(x => x.MaDV.ToLower() == dv.MaDV.ToLower()) != null)
                {
                    AnsiConsole.MarkupLine("[red]Lỗi, mã dịch vụ đã tồn tại![/]");
                    return;
                }
                DSDV_DTO.Add(dv);
                AnsiConsole.MarkupLine("[blue]Đã thêm dịch vụ thành công[/]");
            }
        }
        public List<DTO_DichVu> GetDichVu()
        {
            return DSDV_DTO;
        }
        public DTO_DichVu TimDichVuTheoMa(string ma)
        {
            return DSDV_DTO.FirstOrDefault(x => x.MaDV.ToLower() == ma.ToLower());
        }
        public List<DTO_DichVu> TimKiemTheoTen(string tenDV)
        {
            return DSDV_DTO.Where(dv => dv.TenDV.ToLower().Contains(tenDV.ToLower())).ToList();
        }
        public List<DTO_DichVu> CapNhatGiaChamSocSacDep(List<DTO_KhachHang> dsKH)
        {
            DSDV_DTO.OfType<DTO_ChamSocSacDep>().ToList().ForEach(dv => dv.GiaTien *= 1.03);
            dsKH.ForEach(x => x.DSDV_DTO.OfType<DTO_ChamSocSacDep>().ToList().ForEach(dv => dv.GiaTien *= 1.03));
            return DSDV_DTO;
        }
        public List<DTO_DichVu> CacDVTren500k()
        {
            return DSDV_DTO.Where(x => x.GiaTien > 500000).ToList();
        }
        public List<DTO_ChamSocSacDep> CacDVChamSocSacDep()
        {
            return DSDV_DTO.OfType<DTO_ChamSocSacDep>().ToList();
        }
        public List<DTO_DuongSinhTriLieu> CacDVDuongSinh()
        {
            return DSDV_DTO.OfType<DTO_DuongSinhTriLieu>().ToList();
        }
        public List<DTO_ChamSocBody> CacDVChamSocBody()
        {
            return DSDV_DTO.OfType<DTO_ChamSocBody>().ToList();
        }
        public void XoaDichVuTheoMa(string maDV, List<DTO_KhachHang> dsKH)
        {
            var dvCanXoa = dsDV_DTO.FirstOrDefault(x => x.MaDV.ToLower() == maDV.ToLower());
            if (dvCanXoa == null)
            {
                AnsiConsole.MarkupLine($"[yellow]Không tìm thấy dịch vụ có mã dịch vụ là: {maDV.ToUpper()}[/]");
                return;
            }
            else dsDV_DTO.Remove(dvCanXoa);
            foreach (var kh in dsKH)
            {
                if (kh.DSDV_DTO != null)
                {
                    kh.DSDV_DTO.RemoveAll(x => x.MaDV.ToLower() == maDV.ToLower());
                }
            }
            AnsiConsole.MarkupLine($"[yellow]Đã xóa thành công dịch vụ có mã: {maDV.ToUpper()}[/]");
        }
        public void SapXepDichVuTheoThanhTienTangDan()
        {
            DSDV_DTO = DSDV_DTO.OrderBy(x => x.TinhThanhTien()).ThenByDescending(x => x.MaDV).ToList();
            if (DSDV_DTO.Any())
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
        public List<DTO_DichVu> DanhSachNhungDichVuDuocGiamGia()
        {
            return DSDV_DTO.Where(x => x is DTO_IGiamGiaDichVu).ToList();
        }
    }
}
