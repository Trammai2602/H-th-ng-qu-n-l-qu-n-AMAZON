using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class HDCTDAO
    {
        private static HDCTDAO instance;
        private DataRow item;

        public static HDCTDAO Instance
        {
            get { if (instance == null) instance = new HDCTDAO(); return HDCTDAO.instance; }
            private set { HDCTDAO.instance = value; }
        }

        private HDCTDAO() { }
        public HDCTDAO(DataRow item)
        {
            this.item = item;
        }
        public bool DeleteHHbyMAhh(string mahh)
        {
            string query = "DELETE FROM MUA_CHI_TIET WHERE MaHH = @mahh";
            int result = Function.Instance.ExecuteNonQuery(query, new object[] { mahh });

            return result > 0;
        }
        //public List<MUA_CHI_TIETDAO> GetListHDCT()
        //{
        //    List<MUA_CHI_TIETDAO> listhdct = new List<MUA_CHI_TIETDAO>();

        //    DataTable data = Function.Instance.ExecuteQuery("SELECT a.MaDatHang, b.MaHH, a.SoLuongHH, a.ThanhTienHH FROM MUA_CHI_TIET AS a, HANGHOA AS b WHERE a.MaDatHang = N'" + textMDH.Text + "' AND a.MaHH=b.MaHH ");

        //    foreach (DataRow item in data.Rows)
        //    {
        //        MUA_CHI_TIETDAO info = new MUA_CHI_TIETDAO(item);
        //        listhdct.Add(info);
        //    }

        //    return listhdct;
        //}
    }
}
