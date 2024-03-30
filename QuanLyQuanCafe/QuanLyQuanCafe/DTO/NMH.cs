using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCafe.DTO
{
    class NMH
    {
        public NMH(string manmh, string tennmh)
        {
            this.MaNMH = manmh;
            this.TenNMH = tennmh;
        }

        public NMH(DataRow row)
        {
            this.MaNMH = (string)row["MaNV"];
            this.TenNMH = (string)row["TenNV"];
        }

        public string MaNMH
        {
            get { return manmh; }
            set { manmh = value; }
        }
        public string TenNMH
        {
            get { return tennmh; }
            set { tennmh = value; }
        }

        private string tennmh;
        private string manmh;
    }
}
