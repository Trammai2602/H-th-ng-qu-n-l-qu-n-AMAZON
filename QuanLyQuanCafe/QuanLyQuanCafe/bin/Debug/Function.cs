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
    class Function
    {
        //SqlConnection connection;
        //SqlCommand command;

        //SqlDataAdapter adapter = new SqlDataAdapter();
        //DataTable table = new DataTable();
        private static Function instance; // Ctrl + R + E

        public static Function Instance
        {
            get { if (instance == null) instance = new Function(); return Function.instance; }
            private set { Function.instance = value; }
        }

        private Function() 
        {
            SqlConnection connection = new SqlConnection(connectionSTR);
            Con = connection; // Assign the SqlConnection object to the public static Con
         }
        private string connectionSTR = "Data Source=CUONG-COMPUTER-;Initial Catalog=Quanlycaphe1;Integrated Security=True";

       
        
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;

        }
        public static SqlConnection Con;
        public static void Connect()
        {
            //đường dẫn tới csdl
            //Con.ConnectionString = Properties.Settings.Default.QuanlycapheConnectionString;
            Con.ConnectionString = "Data Source=CUONG-COMPUTER-;Initial Catalog=Quanlycaphe1;Integrated Security=True";

            //Kiểm tra trạng thái của kết nối
            if (Con.State != ConnectionState.Open)//nếu kết nối chưa mở thì thực hiện open
            {
                Con.Open();                  //Mở kết nối
                MessageBox.Show("Kết nối thành công");
            }
            else MessageBox.Show("Kết nối không thành công");

        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(sql, Con); // Thiết lập kết nối ở đây
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
        }
        public static void RunSQL(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(Con.ConnectionString))
            {
                connection.Open(); // Mở kết nối

                using (SqlCommand cmd = new SqlCommand())
                {
                            cmd.Connection = connection; // Gán kết nối

                            cmd.CommandText = sql; // Gán lệnh SQL

                            if (parameters != null)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }

                            try
                            {
                                cmd.ExecuteNonQuery(); // Thực hiện câu lệnh SQL
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        } // Giải phóng bộ nhớ cmd
                    } // Giải phóng bộ nhớ connection
                }

        public static bool CheckKey(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return false; // Nếu câu lệnh SQL rỗng, không thực hiện truy vấn
            }

            using (SqlConnection connection = new SqlConnection(Con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Kiểm tra và gán CommandText trước khi thực hiện ExecuteReader
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);

                        connection.Close();

                        return table.Rows.Count > 0;
                    }
                }
            }
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            using (SqlConnection connection = new SqlConnection(Con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ma = reader.GetValue(0).ToString();
                    }
                    reader.Close();
                }

                connection.Close();
            }
            return ma;
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table); //Đổ kết quả từ câu lệnh sql vào table
            return table;
        }
    }
}
