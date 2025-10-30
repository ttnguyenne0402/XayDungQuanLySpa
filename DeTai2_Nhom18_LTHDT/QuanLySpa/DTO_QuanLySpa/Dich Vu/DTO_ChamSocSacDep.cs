using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace DTO_QuanLySpa
{
    public class DTO_ChamSocSacDep : DTO_DichVu
    {
        public override double TinhThanhTien()
        {
            return GiaTien * (1 + VAT);
        }
        public DTO_ChamSocSacDep()
        {

        }
        public DTO_ChamSocSacDep(string maDV, string tenDV, double giaTien)
            : base(maDV, tenDV, giaTien) { }
        public void NhapTT()
        {
            base.NhapTT();
        }
        public override void XuatTT(Table table)
        {
            table.AddRow(
                $"[bold blue]Chăm Sóc Sắc Đẹp[/]",
                $"[white]{MaDV}[/]",
                $"[white]{TenDV}[/]",
                $"[green]{GiaTien:N0}[/]",
                $"[yellow]0%[/]",
                $"[yellow]{VAT * 100:0.##}%[/]",
                $"[bold green]{TinhThanhTien():N0}[/]"
            );
        }
    }
}
