using QuanLyQuanCafe.DTO;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanCafe.DAO
{
    class NMHDAO
    {
        private static NMHDAO instance;
        private DataRow item;

        public static NMHDAO Instance
        {
            get { if (instance == null) instance = new NMHDAO(); return instance; }
            private set { instance = value; }
        }

        private NMHDAO() { }
        public NMHDAO(DataRow item)
        {
            this.item = item;
        }
        public List<NMH> GetListNMH()
        {
            List<NMH> list = new List<NMH>();

            string query = "select * from NHANVIEN";

            DataTable data = Function.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NMH nv = new NMH(item);
                list.Add(nv);
            }

            return list;
        }
        public bool InsertNMH(string manmh, string ten)
        {
            try
            {
                Console.WriteLine(manmh + ten );
                string query = string.Format("INSERT INTO NHANVIEN (MaNV, TenNV) VALUES ('{0}', N'{1}')", manmh, ten);
                int result = Function.Instance.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message + ex.ErrorCode);
                return false;
            }
        }
        public bool UpdateNMH(string manmh, string ten)
        {
            Console.WriteLine(manmh + ten);

            // Sử dụng dấu nháy đơn ('') cho giá trị kiểu chuỗi và không sử dụng nó cho giá trị kiểu decimal
            string query = string.Format("UPDATE NHANVIEN SET  TenNV = N'{0}' WHERE MaNV = '{1}'", ten, manmh);

            int result = Function.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteNMH(string manmh)
        {

            string query = "DELETE FROM NHANVIEN WHERE MaNV = @manmh";
            int result = Function.Instance.ExecuteNonQuery(query, new object[] { manmh });

            return result > 0;
        }
    }

}
