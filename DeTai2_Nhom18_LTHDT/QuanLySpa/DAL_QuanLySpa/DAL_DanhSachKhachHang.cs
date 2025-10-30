using DTO_QuanLySpa;
using DTO_QuanLySpa.Khach_Hang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL_QuanLySpa
{
    public class DAL_DanhSachKhachHang
    {
        private List<DTO_KhachHang> dsKH_DTO = new List<DTO_KhachHang>();
        private DAL_DanhSachDichVu dsDV_DAL = new DAL_DanhSachDichVu();
        public List<DTO_KhachHang> DSKH_DTO
        {
            get { return dsKH_DTO; }
            set { dsKH_DTO = value; }
        }
        public DAL_DanhSachDichVu DSDV_DAL
        {
            get { return dsDV_DAL; }
            set { dsDV_DAL = value; }
        }
        public DAL_DanhSachKhachHang(List<DTO_DichVu> dsDichVu)
        {
            dsDV_DAL.DSDV = dsDichVu;
        }
        public List<DTO_KhachHang> DocFILE(string fileName, DAL_DanhSachDichVu dsDV_DALChung)
        {
            dsKH_DTO.Clear();
            try
            {
                XmlDocument read = new XmlDocument();
                read.Load(fileName);
                XmlNodeList listKH = read.SelectNodes("/DanhSachKhachHang/KhachHang");
                foreach (XmlNode x in listKH)
                {
                    DTO_KhachHang kh = new DTO_KhachHang();
                    kh.MaKH = x["MaKH"].InnerText;
                    kh.TenKH = x["TenKH"].InnerText;
                    kh.SoDienThoai = x["SoDienThoai"].InnerText;
                    List<DTO_DichVu> dsDV_DAL = new List<DTO_DichVu>();
                    XmlNodeList listDV = x.SelectNodes("DanhSachDichVu/MaDV");
                    foreach (XmlNode dv in listDV)
                    {
                        string MaDV = dv.InnerText;
                        DTO_DichVu DichVu = dsDV_DALChung.DSDV.FirstOrDefault(d => d.MaDV == MaDV);
                        if (DichVu != null)
                        {
                            dsDV_DAL.Add(DichVu);
                        }

                    }
                    kh.DSDV_DTO = dsDV_DAL;
                    dsKH_DTO.Add(kh);
                }

                return dsKH_DTO;
            }
            catch
            {
                return new List<DTO_KhachHang>();
            }

        }
    }
}
