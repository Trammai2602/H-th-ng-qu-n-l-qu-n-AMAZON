using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    class NCC
    {
        public NCC(string mancc, string tenncc, string dc, string sdt)
        {
            this.MaNCC = mancc;
            this.TenNCC = tenncc;
            this.DiaChi = dc;
            this.SDT = sdt;
        }

        public NCC(DataRow row)
        {
            this.MaNCC = (string)row["MaNCC"];
            this.TenNCC = (string)row["TenNCC"];
            this.DiaChi = (string)row["DiaChi"];
            this.SDT = (string)row["SDT"];
        }

        public string MaNCC
        {
            get { return mancc; }
            set { mancc = value; }
        }
        public string TenNCC
        {
            get { return tenncc; }
            set { tenncc = value; }
        }
        public string DiaChi
        {
            get { return dc; }
            set { dc = value; }
        }
        public string SDT
        {
            get { return sdt; }
            set { sdt = value; }
        }

        private string dc;



        private string tenncc;



        private string mancc;
        private string sdt;


    }
}
