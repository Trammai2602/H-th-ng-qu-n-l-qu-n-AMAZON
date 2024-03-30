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
    class NCCDAO
    {
        private static NCCDAO instance;
        private DataRow item;

        public static NCCDAO Instance
        {
            get { if (instance == null) instance = new NCCDAO(); return instance; }
            private set { instance = value; }
        }

        private NCCDAO() { }
        public NCCDAO(DataRow item)
        {
            this.item = item;
        }
        public List<NCC> GetListNCC()
        {
            List<NCC> list = new List<NCC>();

            string query = "select * from NHACUNGCAP";

            DataTable data = Function.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NCC ncc = new NCC(item);
                list.Add(ncc);
            }

            return list;
        }
        public bool InsertNCC(string mancc, string ten,string dc,string sdt)
        {
            try
            {
                Console.WriteLine(mancc + ten+dc+sdt);
                string query = string.Format("INSERT INTO NHACUNGCAP (MaNCC, TenNCC,DiaChi,SDT) VALUES ('{0}', N'{1}',N'{2}','{3}')", mancc, ten,dc,sdt);
                int result = Function.Instance.ExecuteNonQuery(query);

                return result > 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message + ex.ErrorCode);
                return false;
            }
        }
        public bool UpdateNCC(string mancc, string ten, string dc, string sdt)
        {
            Console.WriteLine(mancc + ten + dc + sdt);

            // Sử dụng dấu nháy đơn ('') cho giá trị kiểu chuỗi và không sử dụng nó cho giá trị kiểu decimal
            string query = string.Format("UPDATE NHANVIEN SET  TenNCC = N'{0}',DiaChi=N'{1}', SDT='{2}' WHERE MaNCC = '{3}'", ten,dc,sdt, mancc);

            int result = Function.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteNCC(string mancc)
        {

            string query = "DELETE FROM NHACUNGCAP WHERE MaNCC = @mancc";
            int result = Function.Instance.ExecuteNonQuery(query, new object[] { mancc });

            return result > 0;
        }
    }
}
