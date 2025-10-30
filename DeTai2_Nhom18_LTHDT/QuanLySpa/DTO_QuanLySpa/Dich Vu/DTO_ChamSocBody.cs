using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace DTO_QuanLySpa
{
    public class DTO_ChamSocBody : DTO_DichVu, DTO_IGiamGiaDichVu
    {
        public double TinhGiamGia()
        {
            return 0.07;
        }

        public override double TinhThanhTien()
        {
            return GiaTien * (1 - TinhGiamGia()) * (1 + VAT);
        }

        public DTO_ChamSocBody() : base()
        {

        }
        public DTO_ChamSocBody(string maDV, string tenDV, double giaTien)
            : base(maDV, tenDV, giaTien) { }
        public void NhapTT()
        {
            base.NhapTT();
        }
        public override void XuatTT(Table table)
        {
            table.AddRow(
                $"[bold blue]Chăm Sóc Body[/]",
                $"[white]{MaDV}[/]",
                $"[white]{TenDV}[/]",
                $"[green]{GiaTien:N0}[/]",
                $"[yellow]{TinhGiamGia() * 100:0.##}%[/]",
                $"[yellow]{VAT * 100:0.##}%[/]",
                $"[bold green]{TinhThanhTien():N0}[/]"
            );
        }
    }
}
