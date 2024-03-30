using QuanLyQuanCafe.DTO;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyQuanCafe.DAO;

namespace QuanLyQuanCafe
{
    class HHDAO
    {
        private static HHDAO instance; // Ctrl + R + E
        private DataRow item;
        

        public static HHDAO Instance
        {
            get { if (instance == null) instance = new HHDAO(); return HHDAO.instance; }
            private set { HHDAO.instance = value; }
        }

        private HHDAO() { }

        public HHDAO(DataRow item)
        {
            this.item = item;
        }

        public List<HANGHOA> GetListHH()
        {
            List<HANGHOA> list = new List<HANGHOA>();
            string query = "select * from HANGHOA";
            DataTable data = Function.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                HANGHOA hh = new HANGHOA(item);
                list.Add(hh);

            }
            return list;
        }
        public bool InsertHH(string mahh, string ten, string dvt, decimal price)
        {
            try
            {
                Console.WriteLine(mahh + ten + dvt + price.ToString());
                string query = string.Format("INSERT INTO HANGHOA (MaHH, TenHH, DonViTinhHH, DonGiaHH) VALUES ('{0}', N'{1}', N'{2}', {3})", mahh, ten, dvt, price);
                int result = Function.Instance.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message + ex.ErrorCode);
                return false;
            }
        }


        public bool UpdateHH(string mahh, string ten, string dvt, decimal price)
        {
            Console.WriteLine(mahh + ten + dvt + price.ToString());

            // Sử dụng dấu nháy đơn ('') cho giá trị kiểu chuỗi và không sử dụng nó cho giá trị kiểu decimal
            string query = string.Format("UPDATE HANGHOA SET  TenHH = N'{0}', DonViTinhHH = N'{1}', DonGiaHH = {2} WHERE MaHH = '{3}'", ten, dvt, price, mahh);

            int result = Function.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteFood(string mahh)
        {
            HDCTDAO.Instance.DeleteHHbyMAhh(mahh);

            string query = "DELETE FROM HANGHOA WHERE MaHH = @mahh";
            int result = Function.Instance.ExecuteNonQuery(query, new object[] { mahh });

            return result > 0;
        }

    }
}
