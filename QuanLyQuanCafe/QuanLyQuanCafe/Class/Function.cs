using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    class Functions
    {
        //static:tĩnh(gọi trực tiếp, k cần khai báo)
        public static SqlConnection Con;  //Khai báo đối tượng kết nối tới csdl        

        public static void Connect()
        {
            Con = new SqlConnection();   //Khởi tạo đối tượng
            //đường dẫn tới csdl
            Con.ConnectionString = Properties.Settings.Default.QuanlycapheConnectionString;
            //Kiểm tra trạng thái của kết nối
            if (Con.State != ConnectionState.Open)//nếu kết nối chưa mở thì thực hiện open
            {
                Con.Open();                  //Mở kết nối
                MessageBox.Show("Kết nối thành công");
            }
            else MessageBox.Show("Kết nối không thành công");

        }
        //dừng kết nối tới database
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open) //nếu đang open thì đóng
            {
                Con.Close();   	//Đóng kết nối
                Con.Dispose(); 	//Giải phóng tài nguyên
                Con = null;
            }
        }
        //Phương thức thực thi lệnh select lấy dl
        public static DataTable GetDataTable(string sql)
        {
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Con);// Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            adapter.Fill(table);//Đổ kết quả từ câu lệnh sql vào table
            return table;
        }
        //Hàm thực hiện câu lệnh SQL
        public static void RunSQL(string sql, SqlParameter[] parameters = null)
        {
            SqlCommand cmd; //Đối tượng thuộc lớp SqlCommand
            cmd = new SqlCommand();
            cmd.Connection = Con; //Gán kết nối
            cmd.CommandText = sql; //Gán lệnh SQL
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
        }
        //Hàm kiểm tra khoá trùng
        /*       public static bool CheckKey(string sql)
               {
                   SqlDataAdapter adapter = new SqlDataAdapter(sql, Con);
                   DataTable table = new DataTable();
                   adapter.Fill(table);
                   if (table.Rows.Count > 0)
                       return true;
                   else return false;
               }
        */
        public static bool CheckKey(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(Con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        return table.Rows.Count > 0;
                    }
                }
            }
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
        //trong trường hợp xóa dữ liệu nếu dữ liệu đang được dùng bởi một đối tượng khác thì không được phép xóa.
        public static void RunSqlDel(string sql, SqlParameter[] parameters = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Functions.Con;
            cmd.CommandText = sql;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Dữ liệu đang được dùng, không thể xoá...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void FillCombo(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
        }
        //public class Function
        //{
        //        private string connectSTR = "Data Source=CUONG-COMPUTER-;Initial Catalog=Quanlycaphe;Integrated Security=True";
        //        private static Function instance; // Ctrl + R + E

        //        public static Function Instance
        //        {
        //            get { if (instance == null) instance = new Function(); return Function.instance; }
        //            private set { Function.instance = value; }
        //        }

        //        private Function() { }
        //        public DataTable ExecuteQuery(string query, object[] parameter = null)
        //        {
        //            DataTable data = new DataTable();

        //            using (SqlConnection connection = new SqlConnection(connectSTR))
        //            {
        //                connection.Open();

        //                SqlCommand command = new SqlCommand(query, connection);

        //                if (parameter != null)
        //                {
        //                    string[] listPara = query.Split(' ');
        //                    int i = 0;
        //                    foreach (string item in listPara)
        //                    {
        //                        if (item.Contains('@'))
        //                        {
        //                            command.Parameters.AddWithValue(item, parameter[i]);
        //                            i++;
        //                        }
        //                    }
        //                }

        //                SqlDataAdapter adapter = new SqlDataAdapter(command);

        //                adapter.Fill(data);

        //                connection.Close();
        //            }

        //            return data;
        //        }

        //        public int ExecuteNonQuery(string query, object[] parameter = null)
        //        {
        //            int data = 0;

        //            using (SqlConnection connection = new SqlConnection(connectSTR))
        //        //using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            SqlCommand command = new SqlCommand(query, connection);

        //            if (parameter != null)
        //            {
        //                string[] listPara = query.Split(' ');
        //                int i = 0;
        //                foreach (string item in listPara)
        //                {
        //                    if (item.Contains('@'))
        //                    {
        //                        //command.Parameters.AddWithValue(item, parameter[i]);
        //                        command.Parameters.Add(new SqlParameter(item, parameter[i]));
        //                        i++;
        //                    }
        //                }
        //            }

        //            data = command.ExecuteNonQuery();

        //            connection.Close();
        //        }

        //            return data;
        //        }


        //        public object ExecuteScalar(string query, object[] parameter = null)
        //        {
        //            object data = 0;

        //            using (SqlConnection connection = new SqlConnection(connectSTR))
        //            {
        //                connection.Open();

        //                SqlCommand command = new SqlCommand(query, connection);

        //                if (parameter != null)
        //                {
        //                    string[] listPara = query.Split(' ');
        //                    int i = 0;
        //                    foreach (string item in listPara)
        //                    {
        //                        if (item.Contains('@'))
        //                        {
        //                            command.Parameters.AddWithValue(item, parameter[i]);
        //                            i++;
        //                        }
        //                    }
        //                }

        //                data = command.ExecuteScalar();

        //                connection.Close();
        //            }

        //            return data;
        //        }
    }
}

