using QuanLyQuanCafe.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class MainHH : Form
    {
        //SqlConnection connection;
        //SqlCommand command;
        //string str = @"Data Source=CUONG-COMPUTER-;Initial Catalog=Quanlycaphe;Integrated Security=True";
        //SqlDataAdapter adapter = new SqlDataAdapter();
        //DataTable table = new DataTable();
        public MainHH()
        {
            InitializeComponent();
            loadlistHH();
            loadlistNMH();
            loadlistNCC();
            //loadlistMua();
        }
        void loadlistHH()
        {
            dgvHH.DataSource = HHDAO.Instance.GetListHH();
        }
        void loadlistNMH()
        {
            dgvNMH.DataSource = NMHDAO.Instance.GetListNMH();
        }
        void loadlistNCC()
        {
            dgvNCC.DataSource = NCCDAO.Instance.GetListNCC();
        }
        //void loadlistMua()
        //{
        //    dgvHD.DataSource = MUADAO.Instance.GetListMua();
        //}
        //void loadlistMuaCT(string madh)
        //{
        //    dgvHDMCT.DataSource = MUA_CHI_TIETDAO.Instance.GetListHDCT(madh);
        //}
        //HÀNG HÓA
        private void buttonXem_Click(object sender, EventArgs e)
        {
            loadlistHH();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các controls trên form
            string mahh = txtMaHang.Text;
            string ten = txtTenHang.Text;
            string dvt = txtDonViTinh.Text;
            decimal price = (decimal)numGia.Value;

            try
            {
                // Gọi phương thức InsertHH để thêm dữ liệu
                if (HHDAO.Instance.InsertHH(mahh, ten, dvt, price))
                {
                    MessageBox.Show("Thêm hàng thành công");
                    loadlistHH(); // Load lại danh sách hàng hóa sau khi thêm
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm hàng");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
        }

        private void dgvHH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvHH.CurrentRow.Index;
            txtMaHang.Text = dgvHH.Rows[i].Cells[0].Value.ToString();
            txtTenHang.Text = dgvHH.Rows[i].Cells[1].Value.ToString();
            txtDonViTinh.Text = dgvHH.Rows[i].Cells[2].Value.ToString();
            numGia.Value = Decimal.TryParse(dgvHH.Rows[i].Cells[3].Value.ToString(), out decimal gia) ? gia : 0;
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            string mahh = txtMaHang.Text;
            string ten = txtTenHang.Text;
            string dvt = txtDonViTinh.Text;
            decimal price = (decimal)numGia.Value;

            if (HHDAO.Instance.UpdateHH(mahh, ten, dvt, price))
            {
                MessageBox.Show("Sửa hàng thành công");
                loadlistHH();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa hàng");
            }

        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            string mahh = txtMaHang.Text;

            if (HHDAO.Instance.DeleteFood(mahh))
            {
                MessageBox.Show("Xóa hàng thành công");
                loadlistHH();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa hàng");
            }
        }
        //NHÂN VIÊN
        private void buttonThemNV_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các controls trên form
            string manmh = textMANV.Text;
            string ten = txtTenNV.Text;

            try
            {
                if (NMHDAO.Instance.InsertNMH(manmh, ten))
                {
                    MessageBox.Show("Thêm nhân viên thành công");
                    loadlistNMH(); // Load lại danh sách hàng hóa sau khi thêm
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm nhân viên");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
        }

        private void dgvNMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvNMH.CurrentRow.Index;
            textMANV.Text = dgvNMH.Rows[i].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNMH.Rows[i].Cells[1].Value.ToString();
        }

        private void buttonSuaNV_Click(object sender, EventArgs e)
        {
            string mahh = textMANV.Text;
            string ten = txtTenNV.Text;

            if (NMHDAO.Instance.UpdateNMH(mahh, ten))
            {
                MessageBox.Show("Sửa thông tin thành công");
                loadlistNMH();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thông tin");
            }
        }

        private void buttonXoaNV_Click(object sender, EventArgs e)
        {
            string manmh = textMANV.Text;

            if (NMHDAO.Instance.DeleteNMH(manmh))
            {
                MessageBox.Show("Xóa nhân viên thành công");
                loadlistNMH();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa nhân viên");
            }
        }

        private void buttonXemNV_Click(object sender, EventArgs e)
        {
            loadlistNMH();
        }
        //NHÀ CUNG CẤP
        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvNCC.CurrentRow.Index;
            textMaNCC.Text = dgvNCC.Rows[i].Cells[0].Value.ToString();
            textTenNCC.Text = dgvNCC.Rows[i].Cells[1].Value.ToString();
            textDiaChiNCC.Text = dgvNCC.Rows[i].Cells[2].Value.ToString();
            textSdtNCC.Text = dgvNCC.Rows[i].Cells[3].Value.ToString();
        }

        private void buttonThemNCC_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các controls trên form
            string mancc = textMaNCC.Text;
            string ten = textTenNCC.Text;
            string dc = textDiaChiNCC.Text;
            string sdt = textSdtNCC.Text;
            try
            {
                if (NCCDAO.Instance.InsertNCC(mancc, ten, dc, sdt))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                    loadlistNCC(); // Load lại danh sách hàng hóa sau khi thêm
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm nhà cung cấp");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message);
            }
        }

        private void buttonSuaNCC_Click(object sender, EventArgs e)
        {
            string mancc = textMaNCC.Text;
            string ten = textTenNCC.Text;
            string dc = textDiaChiNCC.Text;
            string sdt = textSdtNCC.Text;

            if (NCCDAO.Instance.UpdateNCC(mancc, ten, dc, sdt))
            {
                MessageBox.Show("Sửa thông tin thành công");
                loadlistNCC();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thông tin");
            }
        }

        private void buttonXoaNCC_Click(object sender, EventArgs e)
        {
            string mancc = textMaNCC.Text;

            if (NCCDAO.Instance.DeleteNCC(mancc))
            {
                MessageBox.Show("Xóa thông tin thành công");
                loadlistNCC();

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thông tin");
            }
        }

        private void buttonXemNCC_Click(object sender, EventArgs e)
        {
            loadlistNCC();
        }
        DataTable tblhdct;
        //public static SqlConnection Con;
        private void LoadDataGridView()
        {
          
            string query = $"SELECT a.MaDatHang, b.MaHH, a.SoLuongHH, a.ThanhTienHH FROM MUA_CHI_TIET AS a, HANGHOA AS b WHERE a.MaDatHang = N'{textMDH.Text}' AND a.MaHH=b.MaHH";

            string query1 = string.Format("SELECT * from MUA");
            tblhdct = Function.GetDataToTable(query);
            tblhdct = Function.GetDataToTable(query1);
            SqlDataAdapter adapter1 = new SqlDataAdapter(query1, Function.Con);
            DataTable table1 = new DataTable();
            adapter1.Fill(table1);
            SqlDataAdapter adapter = new SqlDataAdapter(query, Function.Con);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvHD.DataSource = table;
            dgvHD.AllowUserToAddRows = false;
            dgvHD.EditMode = DataGridViewEditMode.EditProgrammatically;



            dgvHDMCT.DataSource = table1;
            dgvHDMCT.AllowUserToAddRows = false;
            dgvHDMCT.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            if (comboTimMDH.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboTimMDH.Focus();
                return;
            }
            textMDH.Text = comboTimMDH.Text;
            LoadDataGridView();
            buttonXoaCT.Enabled = true;
            buttonThemCT.Enabled = true;
            comboTimMDH.SelectedIndex = -1;
        }

        private void comboTimMDH_DropDown(object sender, EventArgs e)
        {
            Function.FillCombo("SELECT MaDatHang FROM MUA", comboTimMDH, "MaDatHang", "MaDatHang");
            comboTimMDH.SelectedIndex = -1;
        }

        private void tabHH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabHH.SelectedTab == tabHDMCT)
            {
                textDonGia.ReadOnly = true;
                textthanhtien.ReadOnly = true;
                texttongtien.ReadOnly = true;

                texttongtien.Text = "0";
                Function.FillCombo("SELECT MaNCC, TenNCC FROM NHACUNGCAP", comboNCC, "MaNCC", "MaNCC");
                comboNCC.SelectedIndex = -1;
                Function.FillCombo("SELECT MaNV, TenNV FROM NHANVIEN", comboNV, "MaNV", "TenNV");
                comboNV.SelectedIndex = -1;
                Function.FillCombo("SELECT MaHH, TenHH FROM HANGHOA", comboHH, "MaHH", "MaHH");
                comboHH.SelectedIndex = -1;
                if (Function.Con != null && Function.Con.State == ConnectionState.Open)
                {
                    buttonXoaCT.Enabled = true;
                }
                //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
                if (textMDH.Text != "")
                {
                    buttonXoaCT.Enabled = true;
                }
            }
        }

        

        private void comboHH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (comboHH.Text == "")
            {
                textDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra

            str = "SELECT DonGiaHH FROM HANGHOA WHERE MaHH =N'" + comboHH.SelectedValue + "'";
            textDonGia.Text = Function.GetFieldValues(str);
        }

        private void textSLCT_TextChanged_1(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg;
            if (textSLCT.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(textSLCT.Text);

            if (textDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(textDonGia.Text);
            if (double.TryParse(textSLCT.Text, out sl) && double.TryParse(textDonGia.Text, out dg))
            {
                // Giá trị chuyển đổi thành công, bạn có thể thực hiện phép nhân
                tt = sl * dg;
                textthanhtien.Text = tt.ToString();
            }
            else
            {
                // Hiển thị thông báo lỗi hoặc xử lý nếu giá trị không hợp lệ
                //MessageBox.Show("Giá trị không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            double tongtien = 0;

            foreach (var textBox in textthanhtien.Text)
            {
                double ttien;
                if (double.TryParse(textthanhtien.Text, out ttien))
                {
                    tongtien += ttien;
                }
            }

            texttongtien.Text = tongtien.ToString();
        }
        private void CapNhatTongTienDonHang(string maDatHang)
        {
            try
            {
                // Câu truy vấn SQL để lấy tổng tiền từ bảng MUA_CHI_TIET
                string queryMuaChiTiet = $"SELECT ISNULL(SUM(ThanhTienHH), 0) FROM MUA_CHI_TIET WHERE MaDatHang = N'{maDatHang}' GROUP BY MaDatHang";

                // Câu truy vấn SQL để lấy tổng tiền từ bảng MUA
                string queryMua = $"SELECT ISNULL(TongTienHH, 0) FROM MUA WHERE MaDatHang = N'{maDatHang}' ";

                // Thực hiện truy vấn và chuyển đổi kết quả về kiểu double
                double tongTienMuaChiTiet = Convert.ToDouble(Function.GetFieldValues(queryMuaChiTiet));
                double tongTienMua = Convert.ToDouble(Function.GetFieldValues(queryMua));

                // Cập nhật tổng tiền mới trong bảng MUA
                double TongmoiMua = tongTienMua + tongTienMuaChiTiet;

                // Chuỗi truy vấn UPDATE
                string updateQueryMua = $"UPDATE MUA SET TongTienHH = @TongTienHH WHERE MaDatHang = @MaDatHang";

                // Thực thi truy vấn UPDATE
                using (SqlCommand cmd = new SqlCommand(updateQueryMua, Function.Con))
                {
                    // Thêm các tham số cho truy vấn UPDATE
                    cmd.Parameters.AddWithValue("@TongTienHH", TongmoiMua);
                    cmd.Parameters.AddWithValue("@MaDatHang", maDatHang);

                    // Thực thi truy vấn UPDATE
                    cmd.ExecuteNonQuery();
                }

                // Hiển thị giá trị tổng tiền trên TextBox 
                texttongtien.Text = TongmoiMua.ToString();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonThemCT_Click(object sender, EventArgs e)
        {
            textMDH.Enabled = true; // Cho phép nhập mới
            textMDH.Focus();
            LoadDataGridView();

            string sql;
            sql = "SELECT MaDatHang FROM MUA WHERE MaDatHang=N'" + textMDH.Text + "'";
            if (!Function.CheckKey(sql))
            {
                // ... (Phần code thêm thông tin mới vào bảng MUA)

                sql = "INSERT INTO MUA(MaDatHang, MaNCC, MaNV, NgayMuaHang, TongTienHH) VALUES (N'" + textMDH.Text.Trim() + "', N'" + comboNCC.SelectedValue + "', N'" + comboNV.SelectedValue + "', '" + dateNgaymua.Value + "', N'" + texttongtien.Text + "')";
                Function.RunSQL(sql);
                LoadDataGridView();
            }

            // Kiểm tra và thêm thông tin các mặt hàng
            if (comboHH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboHH.Focus();
                return;
            }
            if ((textSLCT.Text.Trim().Length == 0) || (textSLCT.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textSLCT.Text = "";
                textSLCT.Focus();
                return;
            }

            sql = "SELECT MaHH FROM MUA_CHI_TIET WHERE MaHH=N'" + comboHH.SelectedValue + "' AND MaDatHang = N'" + textMDH.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                // Nếu mã hàng hóa đã tồn tại trong đơn hàng, cập nhật số lượng và thành tiền

                // Lấy giá trị số lượng và thành tiền hiện tại từ bảng
                string currentQuantitySQL = "SELECT SoLuongHH, ThanhTienHH FROM MUA_CHI_TIET WHERE MaHH=N'" + comboHH.SelectedValue + "' AND MaDatHang = N'" + textMDH.Text.Trim() + "'";
                DataTable currentValues = Function.GetDataToTable(currentQuantitySQL);

                double currentQuantity = Convert.ToDouble(currentValues.Rows[0]["SoLuongHH"]);
                double currentTotal = Convert.ToDouble(currentValues.Rows[0]["ThanhTienHH"]);

                // Tính giá trị mới của số lượng và thành tiền
                double newQuantity = currentQuantity + Convert.ToDouble(textSLCT.Text);
                double newTotal = currentTotal + Convert.ToDouble(textthanhtien.Text);

                string sqlUpdate = "UPDATE MUA_CHI_TIET SET SoLuongHH = " + newQuantity + ", ThanhTienHH = " + newTotal +
                                   " WHERE MaDatHang = N'" + textMDH.Text.Trim() + "' AND MaHH = N'" + comboHH.SelectedValue + "'";
                Function.RunSQL(sqlUpdate);
            }
            else
            {
                // Nếu chưa tồn tại, thực hiện thêm mới vào bảng
                string sqlInsert = "INSERT INTO MUA_CHI_TIET(MaDatHang, MaHH, SoLuongHH, ThanhTienHH) VALUES (N'" + textMDH.Text.Trim() + "', N'" + comboHH.SelectedValue + "', " + textSLCT.Text + ", " + textthanhtien.Text + ")";
                Function.RunSQL(sqlInsert);
            }

            // Code để kiểm tra và lấy giá trị tổng tiền mới từ bảng MUA_CHI_TIET
            double tong = Convert.ToDouble(Function.GetFieldValues("SELECT SUM(ThanhTienHH) FROM MUA_CHI_TIET WHERE MaDatHang = '" + textMDH.Text.Trim() + "'"));

            // Cập nhật giá trị tổng tiền mới vào bảng MUA
            string updateTotalSQL = "UPDATE MUA SET TongTienHH = " + tong + " WHERE MaDatHang = '" + textMDH.Text.Trim() + "'";
            texttongtien.Text = tong.ToString();
            Function.RunSQL(updateTotalSQL);
            LoadDataGridView();
        }


    } 
}


