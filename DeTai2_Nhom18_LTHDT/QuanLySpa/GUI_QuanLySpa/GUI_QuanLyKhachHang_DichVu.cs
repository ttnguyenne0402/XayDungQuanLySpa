using BLL_QuanLySpa;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_QuanLySpa
{
    public class GUI_QuanLyKhachHang_DichVu
    {
        private BLL_DanhSachDichVu dsDV = new BLL_DanhSachDichVu();
        private BLL_DanhSachKhachHang dsKH;
        public BLL_DanhSachDichVu DSDV
        {
            get { return dsDV; }
            set { dsDV = value; }
        }
        public BLL_DanhSachKhachHang DSKH
        {
            get { return dsKH; }
            set { dsKH = value; }
        }
        public void Menu()
        {  
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("QUAN LY SPA").Color(Color.Yellow)); 
                var chon = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(30)
                        .AddChoices(new[]
                        {
                            "=============================| MENU |================================",
                            "=====================================================================",
                            "**  1. ĐỌC DANH SÁCH DỊCH VỤ TỪ FILE                               **",
                            "**  2. XUẤT DANH SÁCH DỊCH VỤ                                      **",
                            "**  3. ĐỌC DANH SÁCH KHÁCH HÀNG TỪ FILE                            **",
                            "**  4. XUẤT DANH SÁCH KHÁCH HÀNG                                   **",
                            "**  5. THÊM DỊCH VỤ TỪ BÀN PHÍM                                    **",
                            "**  6. THÊM KHÁCH HÀNG TỪ BÀN PHÍM                                 **",
                            "**  7. THÊM DỊCH VỤ MỚI CHO KHÁCH HÀNG                             **",
                            "**  8. TÌM DỊCH VỤ THEO MÃ DỊCH VỤ                                 **",
                            "**  9. TÌM KIẾM DỊCH VỤ THEO TÊN                                   **",
                            "** 10. TÌM KHÁCH HÀNG THEO MÃ KHÁCH HÀNG                           **",
                            "** 11. TÌM KHÁCH HÀNG THEO TÊN                                     **",
                            "** 12. XUẤT DANH SÁCH DỊCH VỤ KHI BIẾT TÊN KHÁCH HÀNG              **",
                            "** 13. CẬP NHẬT GIÁ DỊCH VỤ CHĂM SÓC SẮC ĐẸP (+3%)                 **",
                            "** 14. LIỆT KÊ CÁC DỊCH VỤ TRÊN 500K                               **",
                            "** 15. XUẤT DANH SÁCH CÁC DỊCH VỤ CHĂM SÓC SẮC ĐẸP                 **",
                            "** 16. XUẤT DANH SÁCH CÁC DỊCH VỤ CHĂM SÓC BODY                    **",
                            "** 17. XUẤT DANH SÁCH CÁC DỊCH VỤ DƯỠNG SINH TRỊ LIỆU              **",
                            "** 18. XUẤT DANH SÁCH KHÁCH HÀNG THỰC HIỆN TRÊN 3 DỊCH VỤ          **",
                            "** 19. XUẤT DANH SÁCH DỊCH VỤ ĐƯỢC GIẢM GIÁ                        **",
                            "** 20. XÓA DỊCH VỤ THEO MÃ DỊCH VỤ                                 **",
                            "** 21. XÓA KHÁCH HÀNG THEO MÃ KHÁCH HÀNG                           **",
                            "** 22. SẮP XẾP DANH SÁCH DỊCH VỤ THEO THÀNH TIỀN TĂNG DẦN          **",
                            "** 23. SẮP XẾP DANH SÁCH KHÁCH HÀNG THEO THÀNH TIỀN GIẢM DẦN       **",
                            "** 24. TỔNG THÀNH TIỀN SPA THU ĐƯỢC                                **",
                            "** THOÁT CHƯƠNG TRÌNH                                              **",
                        }));

                switch (chon)
                {
                    case "**  1. ĐỌC DANH SÁCH DỊCH VỤ TỪ FILE                               **":
                        dsDV.DocFile();
                        if (dsDV.GetDichVu().Any())
                            AnsiConsole.MarkupLine("[green]Đã đọc danh sách dịch vụ thành công![/]");
                        else
                            AnsiConsole.MarkupLine("[red]Đọc danh sách dịch vụ không thành công![/]");
                        break;

                    case "**  2. XUẤT DANH SÁCH DỊCH VỤ                                      **":
                        dsDV.XuatTT();
                        break;

                    case "**  3. ĐỌC DANH SÁCH KHÁCH HÀNG TỪ FILE                            **":
                        if (dsDV.GetDichVu().Any())
                        {
                            dsKH = new BLL_DanhSachKhachHang(dsDV);
                            dsKH.DocFile();
                            AnsiConsole.MarkupLine("[green]Đã đọc danh sách khách hàng thành công![/]");
                        }
                        else
                            AnsiConsole.MarkupLine("[red]Vui lòng đọc danh sách dịch vụ trước![/]");
                        break;

                    case "**  4. XUẤT DANH SÁCH KHÁCH HÀNG                                   **":
                        if (dsKH == null)
                            AnsiConsole.MarkupLine("[red]Bạn chưa đọc danh sách khách hàng từ file![/]");
                        else
                            dsKH.XuatTT();
                        break;

                    case "**  5. THÊM DỊCH VỤ TỪ BÀN PHÍM                                    **":
                        dsDV.ThemDichVuMoi();
                        break;

                    case "**  6. THÊM KHÁCH HÀNG TỪ BÀN PHÍM                                 **":
                        dsKH.ThemKhachHangMoi();
                        break;

                    case "**  7. THÊM DỊCH VỤ MỚI CHO KHÁCH HÀNG                             **":
                        dsKH.ThemDichVuChoKhachHang(dsKH.GetKhachHang(), dsDV.GetDichVu());
                        break;

                    case "**  8. TÌM DỊCH VỤ THEO MÃ DỊCH VỤ                                 **":
                        Console.Write("Nhập mã dịch vụ cần tìm: ");
                        string madv = Console.ReadLine();
                        var kq = dsDV.TimDichVuTheoMa(madv);
                        if (kq == null)
                            AnsiConsole.MarkupLine($"[red]Không tìm thấy dịch vụ nào có mã là: {madv.ToUpper()}[/]");
                        else
                        {
                            var table = new Table();
                            table.Title = new TableTitle($"[yellow]DỊCH VỤ CÓ MÃ DỊCH VỤ: {madv.ToUpper()}[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            kq.XuatTT(table);
                            AnsiConsole.Write(table);
                        }
                        break;

                    case "**  9. TÌM KIẾM DỊCH VỤ THEO TÊN                                   **":
                        Console.Write("Nhập tên dịch vụ cần tìm: ");
                        string tenDV = Console.ReadLine();
                        var kqten = dsDV.TimKiemTheoTen(tenDV);
                        if (!kqten.Any())
                            AnsiConsole.MarkupLine($"[red]Không tìm thấy dịch vụ nào có tên là: {tenDV}[/]");
                        else
                        {
                            var table = new Table();
                            table.Title = new TableTitle($"[yellow]DANH SÁCH DỊCH VỤ CÓ TÊN DỊCH VỤ: {tenDV}[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in kqten)
                                dv.XuatTT(table);
                            AnsiConsole.Write(table);
                        }
                        break;

                    case "** 10. TÌM KHÁCH HÀNG THEO MÃ KHÁCH HÀNG                           **":
                        Console.Write("Nhập mã khách hàng: ");
                        string makh = Console.ReadLine();
                        var kh = dsKH.TimTheoMa(makh);
                        if (kh == null)
                            AnsiConsole.MarkupLine($"[red]Không tìm thấy khách hàng nào có mã khách hàng là: {makh.ToUpper()}[/]");
                        else
                        {
                            var table = new Table();
                            table.Title = new TableTitle($"[yellow]THÔNG TIN KHÁCH HÀNG CÓ MÃ KHÁCH HÀNG: {makh.ToUpper()}[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Mã Khách Hàng[/]");
                            table.AddColumn("[white]Tên Khách Hàng[/]");
                            table.AddColumn("[blue]Số Điện Thoại[/]");
                            table.AddColumn("[yellow]Mã Dịch Vụ[/]");
                            table.AddColumn("[red]Tên Dịch Vụ[/]");
                            table.AddColumn("[blue]Giá Tiền[/]");
                            table.AddColumn("[yellow]Thành Tiền[/]");
                            table.AddColumn("[white]Tổng Tiền[/]");
                            kh.XuatTT(table);
                            AnsiConsole.Write(table);
                        }
                        break;

                    case "** 11. TÌM KHÁCH HÀNG THEO TÊN                                     **":
                        Console.Write("Nhập tên khách hàng: ");
                        string tenKH = Console.ReadLine();
                        var kqKH = dsKH.TimTheoTen(tenKH);
                        if (kqKH == null)
                            AnsiConsole.MarkupLine($"[red]Không tìm thấy khách hàng nào có tên khách hàng là: {tenKH}[/]");
                        else
                        {
                            var table = new Table();
                            table.Title = new TableTitle($"[yellow]THÔNG TIN KHÁCH HÀNG CÓ TÊN: {tenKH}[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Mã Khách Hàng[/]");
                            table.AddColumn("[white]Tên Khách Hàng[/]");
                            table.AddColumn("[blue]Số Điện Thoại[/]");
                            table.AddColumn("[yellow]Mã Dịch Vụ[/]");
                            table.AddColumn("[red]Tên Dịch Vụ[/]");
                            table.AddColumn("[blue]Giá Tiền[/]");
                            table.AddColumn("[yellow]Thành Tiền[/]");
                            table.AddColumn("[white]Tổng Tiền[/]");
                            kqKH.XuatTT(table);
                            AnsiConsole.Write(table);
                        }
                        break;
                    case "** 12. XUẤT DANH SÁCH DỊCH VỤ KHI BIẾT TÊN KHÁCH HÀNG              **":
                        Console.Write("Nhập tên khách hàng bạn cần: ");
                        string ten = Console.ReadLine();
                        dsKH.XuatDichVuTheoTenKH(ten);
                        break;
                    case "** 13. CẬP NHẬT GIÁ DỊCH VỤ CHĂM SÓC SẮC ĐẸP (+3%)                 **":
                        if (dsDV.CapNhatGiaChamSocSacDep(dsKH.GetKhachHang()).Any())
                            AnsiConsole.MarkupLine("[green]Đã cập nhật giá thành công![/]");
                        else AnsiConsole.MarkupLine("[green]Cập nhật giá không thành thành công![/]");
                        break;

                    case "** 14. LIỆT KÊ CÁC DỊCH VỤ TRÊN 500K                               **":
                        var dsTren500 = dsDV.CacDVTren500k();
                        if (!dsTren500.Any())
                            AnsiConsole.MarkupLine("[red]Không có dịch vụ nào trên 500k![/]");
                        else
                        {
                            var table = new Table();
                            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ CÓ GIÁ TRÊN 500.000 VNĐ[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in dsTren500)
                            {
                                dv.XuatTT(table);
                            }
                            AnsiConsole.Write(table);
                        }
                        break;

                    case "** 15. XUẤT DANH SÁCH CÁC DỊCH VỤ CHĂM SÓC SẮC ĐẸP                 **":
                        var dsDVSacDep = dsDV.CacDVChamSocSacDep();
                        if (dsDVSacDep.Any())
                        {
                            var table = new Table();
                            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ CHĂM SÓC SẮC ĐẸP[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in dsDVSacDep)
                            {
                                dv.XuatTT(table);
                            }
                            AnsiConsole.Write(table);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[yellow]Không có dịch vụ nào cả[/]");
                        }
                        break;

                    case "** 16. XUẤT DANH SÁCH CÁC DỊCH VỤ CHĂM SÓC BODY                    **":
                        var dsDVBody = dsDV.CacDVChamSocBody();
                        if (dsDVBody.Any())
                        {
                            var table = new Table();
                            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ CHĂM SÓC BODY[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in dsDVBody)
                            {
                                dv.XuatTT(table);
                            }
                            AnsiConsole.Write(table);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[yellow]Không có dịch vụ nào cả[/]");
                        }
                        break;

                    case "** 17. XUẤT DANH SÁCH CÁC DỊCH VỤ DƯỠNG SINH TRỊ LIỆU              **":
                        var dsDVDuongSinh = dsDV.CacDVDuongSinh();
                        if (dsDVDuongSinh.Any())
                        {
                            var table = new Table();
                            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ DƯỠNG SINH TRỊ LIỆU[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in dsDVDuongSinh)
                            {
                                dv.XuatTT(table);
                            }
                            AnsiConsole.Write(table);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[yellow]Không có dịch vụ nào cả[/]");
                        }
                        break;

                    case "** 18. XUẤT DANH SÁCH KHÁCH HÀNG THỰC HIỆN TRÊN 3 DỊCH VỤ          **":
                        DSKH.XuatKhachHangNhieuHon3DichVu();
                        break;
                    case "** 19. XUẤT DANH SÁCH DỊCH VỤ ĐƯỢC GIẢM GIÁ                        **":
                        var dsDVDuocGiamGia = DSDV.DanhSachNhungDichVuDuocGiamGia();
                        if (dsDVDuocGiamGia.Any())
                        {
                            var table = new Table();
                            table.Title = new TableTitle("[yellow]DANH SÁCH DỊCH VỤ ĐƯỢC GIẢM GIÁ[/]");
                            table.Border = TableBorder.Rounded;
                            table.AddColumn("[cyan]Loại DV[/]");
                            table.AddColumn("[yellow]Mã DV[/]");
                            table.AddColumn("[magenta]Tên DV[/]");
                            table.AddColumn("[blue]Giá Gốc[/]");
                            table.AddColumn("[red]Giảm[/]");
                            table.AddColumn("[blue]VAT[/]");
                            table.AddColumn("[cyan]Thành Tiền[/]");
                            foreach (var dv in dsDVDuocGiamGia)
                            {
                                dv.XuatTT(table);
                            }
                            AnsiConsole.Write(table);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Không có dịch vụ nào cả![/]");
                        }
                        break;
                    case "** 20. XÓA DỊCH VỤ THEO MÃ DỊCH VỤ                                 **":
                        Console.Write("Nhập mã dịch vụ cần xóa: ");
                        var madvcanxoa = Console.ReadLine();
                        dsDV.XoaDichVuTheoMa(madvcanxoa, dsKH.GetKhachHang());
                        break;
                    case "** 21. XÓA KHÁCH HÀNG THEO MÃ KHÁCH HÀNG                           **":
                        Console.Write("Nhập mã khách hàng cần xóa: ");
                        var makhcanxoa = Console.ReadLine();
                        DSKH.XoaKhachHangTheoMa(makhcanxoa);
                        break;
                    case "** 22. SẮP XẾP DANH SÁCH DỊCH VỤ THEO THÀNH TIỀN TĂNG DẦN          **":
                        DSDV.SapXepDichVuTheoThanhTienTangDan();
                        break;
                    case "** 23. SẮP XẾP DANH SÁCH KHÁCH HÀNG THEO THÀNH TIỀN GIẢM DẦN       **":
                        DSKH.SapXepKhachHangTheoThanhTienGiamDan();
                        break;
                    case "** 24. TỔNG THÀNH TIỀN SPA THU ĐƯỢC                                **":
                        double Tong = DSKH.TongThanhTienSpa();
                        AnsiConsole.MarkupLine($"[yellow]Tổng thành tiền Spa thu được là: {Tong:N0} VNĐ[/]");
                        break;
                    case "** THOÁT CHƯƠNG TRÌNH                                              **":
                        AnsiConsole.MarkupLine("[yellow]👋 Tạm biệt, cảm ơn bạn đã sử dụng chương trình![/]");
                        return;
                }
                AnsiConsole.MarkupLine("\n[grey]Nhấn phím bất kỳ để quay lại menu...[/]");
                Console.ReadKey();
            }
        }
    }
}
