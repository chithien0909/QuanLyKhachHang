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
    public partial class QuanLyDanhMucChiTietHD : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;

        SqlDataAdapter daSP = null;
        DataTable dtSP = null;

        SqlDataAdapter daCTHD = null;
        DataTable dtCTHD = null;

        SqlDataAdapter daHD = null;
        DataTable dtHD = null;
        bool Them = false;


        public QuanLyDanhMucChiTietHD()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {

                conn = new SqlConnection(strConnectionString);


                daHD = new SqlDataAdapter("SELECT * FROM HoaDon", conn);
                dtHD = new DataTable();
                dtHD.Clear();
                daHD.Fill(dtHD);
                (dgvHD.Columns["MaHD"] as
                    DataGridViewComboBoxColumn).DataSource = dtHD;
                (dgvHD.Columns["MaHD"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "MaHD";
                (dgvHD.Columns["MaHD"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaHD";

                daSP = new SqlDataAdapter("SELECT * FROM SanPham", conn);
                dtSP = new DataTable();
                dtSP.Clear();
                daSP.Fill(dtSP);

                (dgvHD.Columns["MaSP"] as
                    DataGridViewComboBoxColumn).DataSource = dtSP;
                (dgvHD.Columns["MaSP"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "TenSP";
                (dgvHD.Columns["MaSP"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaSP";




                daCTHD = new SqlDataAdapter("SELECT * FROM ChiTietHoaDon", conn);
                dtCTHD = new DataTable();
                dtCTHD.Clear();
                daCTHD.Fill(dtCTHD);
                dgvHD.DataSource = dtCTHD;

                this.cbMaHD.ResetText();
                this.cbMaSP.ResetText();
                this.txtSoLuong.ResetText();
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
        private void QuanLyDanhMucChiTietHD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
                int r = dgvHD.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strMaHD =
                dgvHD.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From ChiTietHoaDon Where MaHD='" + strMaHD + "'");
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

            this.cbMaHD.ResetText();
            this.cbMaSP.ResetText();
            this.txtSoLuong.ResetText();

            cbMaHD.DataSource = dtHD;
            cbMaHD.DisplayMember = "MaHD";
            cbMaHD.ValueMember = "MaHD";

            cbMaSP.DataSource = dtSP;
            cbMaSP.DisplayMember = "TenSP";
            cbMaSP.ValueMember = "MaSP";


            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.cbMaHD.Focus();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;


            cbMaHD.DataSource = dtHD;
            cbMaHD.DisplayMember = "MaHD";
            cbMaHD.ValueMember = "MaHD";

            cbMaSP.DataSource = dtSP;
            cbMaSP.DisplayMember = "TenSP";
            cbMaSP.ValueMember = "MaSP";

            int r = dgvHD.CurrentCell.RowIndex;

            this.cbMaHD.Text =
                dgvHD.Rows[r].Cells[0].FormattedValue.ToString();
            this.cbMaHD.SelectedValue =
                dgvHD.Rows[r].Cells[0].Value.ToString();

            this.cbMaSP.Text =
                dgvHD.Rows[r].Cells[1].FormattedValue.ToString();
            this.cbMaSP.SelectedValue =
                dgvHD.Rows[r].Cells[1].Value.ToString();
            
            this.txtSoLuong.Text =
                dgvHD.Rows[r].Cells[2].Value.ToString();


            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.cbMaHD.Focus();
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
                    cmd.CommandText = System.String.Concat("Insert Into ChiTietHoaDon Values(@MaHD, @MaSP, @SoLuong)");

                    cmd.Parameters.AddWithValue("@SoLuong", this.txtSoLuong.Text.ToString());
                    cmd.Parameters.AddWithValue("@MaHD", this.cbMaHD.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MaSP", this.cbMaSP.SelectedValue.ToString());

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
                    int r = dgvHD.CurrentCell.RowIndex;
                    // MaKH hiện hành
                    string strMaHD =
                        dgvHD.Rows[r].Cells[0].Value.ToString();
                    // Câu lệnh SQL
                    cmd.CommandText = System.String.Concat("Update ChiTietHoaDon Set MaSP=@MaSP, SoLuong=@SoLuong Where MaHD='" + strMaHD + "'");

                    cmd.Parameters.AddWithValue("@MaSP", this.cbMaSP.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SoLuong", this.txtSoLuong.Text.ToString());
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

    }
}
