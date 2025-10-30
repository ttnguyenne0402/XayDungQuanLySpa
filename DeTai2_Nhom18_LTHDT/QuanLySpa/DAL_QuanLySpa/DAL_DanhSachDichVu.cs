using DTO_QuanLySpa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL_QuanLySpa
{
    public class DAL_DanhSachDichVu
    {
        private List<DTO_DichVu> dsDV = new List<DTO_DichVu>();

        public List<DTO_DichVu> DSDV
        {
            get { return dsDV; }
            set { dsDV = value; }
        }

        public List<DTO_DichVu> DocFILE(string filename)
        {
            dsDV.Clear();
            try
            {
                XmlDocument read = new XmlDocument();
                read.Load(filename);
                XmlNodeList list = read.SelectNodes("/DanhSachDichVu/DichVu");
                foreach (XmlNode x in list)
                {
                    string maDV = x["MaDV"].InnerText;
                    string tenDV = x["TenDV"].InnerText;
                    double giaTien = double.Parse(x["GiaTien"].InnerText);
                    DTO_DichVu dv;
                    if (x.Attributes["loai"].Value.ToLower() == "cham soc sac dep")
                        dv = new DTO_ChamSocSacDep();
                    else if (x.Attributes["loai"].Value.ToLower() == "cham soc body")
                        dv = new DTO_ChamSocBody();
                    else dv = new DTO_DuongSinhTriLieu();
                    if (dv != null)
                    {
                        dv.MaDV = maDV;
                        dv.TenDV = tenDV;
                        dv.GiaTien = giaTien;
                        dsDV.Add(dv);
                    }
                }
                return dsDV;
            }
            catch
            {
                return new List<DTO_DichVu>();
            }
        }
    }
}
