using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    class HANGHOA
    {
        public HANGHOA(string mahh, string tenhh, string dvt, decimal price)
        {
            this.MaHH = mahh;
            this.TenHH = tenhh;
            this.DonViTinh = dvt;
            this.DonGia = price;
        }

        public HANGHOA(DataRow row)
        {
            this.MaHH = (string)row["MaHH"];
            this.TenHH = row["TenHH"].ToString();
            this.DonViTinh = (string)row["DonViTinhHH"];
            this.DonGia = (decimal)Convert.ToDouble(row["DonGiaHH"].ToString());
        }

        private decimal price;
        public string MaHH
        {
            get { return mahh; }
            set { mahh = value; }
        }
        public string TenHH
        {
            get { return tenhh; }
            set { tenhh = value; }
        }
        public string DonViTinh
        {
            get { return dvt; }
            set { dvt = value; }
        }
        public decimal DonGia
        {
            get { return price; }
            set { price = value; }
        }

        private string dvt;

        

        private string tenhh;

        

        private string mahh;

        
    }
}
