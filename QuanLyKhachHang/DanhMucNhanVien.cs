using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyKhachHang
{
    public partial class DanhMucNhanVien : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daNV = null;
        DataTable dtNV = null;

        SqlDataAdapter daThanhPho = null;
        DataTable dtThanhPho = null;
        bool Them = false;
        public DanhMucNhanVien()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {

                conn = new SqlConnection(strConnectionString);

                daNV = new SqlDataAdapter("SELECT * FROM NHANVIEN", conn);
                dtNV = new DataTable();
                dtNV.Clear();
                daNV.Fill(dtNV);
                dgvNV.DataSource = dtNV;

                this.txtMaNV.ResetText();
                this.txtHo.ResetText();
                this.txtTen.ResetText();
                this.txtNgayNV.ResetText();
                this.txtDienThoai.ResetText();
                this.txtDiaChi.ResetText();



                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel1.Enabled = false;

                this.btnAdd.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnReturn.Enabled = true;


            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!");
            }
        }

        private void DanhMucNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                // Thực hiện lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                // Lấy thứ tự record hiện hành
                int r = dgvNV.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strMANV =
                dgvNV.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From NHANVIEN Where MaKH='" + strMANV + "'");
                // Thực hiện câu lệnh SQL
                cmd.ExecuteNonQuery();
                // Cập nhật lại DataGridView
                LoadData();
                // Thông báo
                MessageBox.Show("Đã xóa xong!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!!!");
            }
            // Đóng kết nối
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Them = true;

            this.txtMaNV.ResetText();
            this.txtHo.ResetText();
            this.txtTen.ResetText();
            this.txtNgayNV.ResetText();
            this.txtDienThoai.ResetText();
            this.txtDiaChi.ResetText();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaNV.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;

            int r = dgvNV.CurrentCell.RowIndex;
            this.txtMaNV.Text =
                dgvNV.Rows[r].Cells[0].Value.ToString();
            this.txtHo.Text =
                dgvNV.Rows[r].Cells[1].Value.ToString();
            this.txtTen.Text =
                dgvNV.Rows[r].Cells[2].Value.ToString();
            this.cbNu.Checked =
                dgvNV.Rows[r].Cells[3].Value.ToString() == "1" ? true : false;
            this.txtNgayNV.Text = dgvNV.Rows[r].Cells[4].Value.ToString();

            this.txtDiaChi.Text =
                dgvNV.Rows[r].Cells[5].Value.ToString();
            this.txtDienThoai.Text =
                dgvNV.Rows[r].Cells[6].Value.ToString();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaNV.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();

            if (Them)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    DateTime dateNV = Convert.ToDateTime(this.txtNgayNV.Text);
                    int isNu = this.cbNu.Checked ? 1 : 0;
                    cmd.CommandText = System.String.Concat("Insert Into NHANVIEN Values(" + "'" +
                        this.txtMaNV.Text.ToString() + "', N'" +
                        this.txtHo.Text.ToString() + "', N'" +
                        this.txtTen.Text.ToString() + "'," +
                        isNu + ",@DateNV, N'" +
                        this.txtDiaChi.Text.ToString() + "','" +
                        this.txtDienThoai.Text.ToString() +
                        "', NULL)");
                    cmd.Parameters.AddWithValue("@DateNV", dateNV);
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm xong!");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            if (!Them)
            {
                try
                {
                    // Thực hiện lệnh
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    // Thứ tự dòng hiện hành
                    int r = dgvNV.CurrentCell.RowIndex;
                    // MaKH hiện hành
                    string strMaNV =
                        dgvNV.Rows[r].Cells[0].Value.ToString();
                    // Câu lệnh SQL
                    int isNu = this.cbNu.Checked ? 1 : 0;
                    cmd.CommandText = System.String.Concat("Update NHANVIEN Set Ho=N'" +
                    this.txtHo.Text.ToString() + "', Ten=N'" +
                    this.txtTen.Text.ToString() + "',Nu=" +
                    isNu + ", DiaChi=N'" +
                    this.txtDiaChi.Text.ToString() + "', DienThoai='" +
                    this.txtDienThoai.Text.ToString() + "', NgayNV=@DateNV Where MaNV='" + strMaNV + "'");
                    DateTime dateNV = Convert.ToDateTime(this.txtNgayNV.Text);
                    cmd.Parameters.AddWithValue("@DateNV", dateNV);
                    // Cập nhật
                    cmd.ExecuteNonQuery();
                    // Load lại dữ liệu trên DataGridView
                    LoadData();
                    // Thông báo
                    MessageBox.Show("Đã sửa xong!");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không sửa được. Lỗi rồi!");
                } 
            }
            conn.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}
