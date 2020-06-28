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
    public partial class DanhMucHoaDon : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daKH = null;
        DataTable dtKH = null;

        SqlDataAdapter daNV = null;
        DataTable dtNV = null;

        SqlDataAdapter daHD = null;
        DataTable dtHD = null;
        bool Them = false;
        public DanhMucHoaDon()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {

                conn = new SqlConnection(strConnectionString);

                daKH = new SqlDataAdapter("SELECT * FROM KHACHHANG", conn);
                dtKH = new DataTable();
                dtKH.Clear();
                daKH.Fill(dtKH);
                (dgvHoaDon.Columns["TenCty"] as
                    DataGridViewComboBoxColumn).DataSource = dtKH;
                (dgvHoaDon.Columns["TenCty"] as
                    DataGridViewComboBoxColumn).DisplayMember = 
                    "TenCty";
                (dgvHoaDon.Columns["TenCty"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaKH";



                daNV = new SqlDataAdapter("SELECT * FROM NHANVIEN", conn);
                dtNV = new DataTable();
                dtNV.Clear();
                daNV.Fill(dtNV);
                dtNV.Columns.Add("FullName");
                foreach (DataRow item in dtNV.Rows)
                {
                    item["FullName"] = item["Ho"] + " " + item["Ten"];
                }
                (dgvHoaDon.Columns["MaNV"] as
                    DataGridViewComboBoxColumn).DataSource = dtNV;
                (dgvHoaDon.Columns["MaNV"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "FullName";
                (dgvHoaDon.Columns["MaNV"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaNV";
                

                daHD = new SqlDataAdapter("SELECT * FROM HOADON", conn);
                dtHD = new DataTable();
                dtHD.Clear();
                daHD.Fill(dtHD);
                dgvHoaDon.DataSource = dtHD;

                this.txtMaHD.ResetText();
                this.txtNgayLap.ResetText();
                this.txtNgayNhan.ResetText();
                this.cbMaKH.ResetText();
                this.cbMaNV.ResetText();

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
        private void DanhMucHoaDon_Load(object sender, EventArgs e)
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
                int r = dgvHoaDon.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strMaHD =
                dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From HoaDon Where MaHD='" + strMaHD + "'");
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

            this.txtMaHD.ResetText();
            this.cbMaKH.ResetText();
            this.cbMaNV.ResetText();
            this.txtNgayLap.ResetText();
            this.txtNgayNhan.ResetText();

            cbMaKH.DataSource = dtKH;
            cbMaKH.DisplayMember = "TenCty";
            cbMaKH.ValueMember = "MaKH";

            cbMaNV.DataSource = dtNV;
            cbMaNV.DisplayMember = "FullName";
            cbMaNV.ValueMember = "MaNV";


            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaHD.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;

            cbMaKH.DataSource = dtKH;
            cbMaKH.DisplayMember = "TenCty";
            cbMaKH.ValueMember = "MaKH";

            cbMaNV.DataSource = dtNV;
            cbMaNV.DisplayMember = "FullName";
            cbMaNV.ValueMember = "MaNV";

            int r = dgvHoaDon.CurrentCell.RowIndex;
            this.txtMaHD.Text =
                dgvHoaDon.Rows[r].Cells[0].Value.ToString();
            this.cbMaKH.Text = 
                dgvHoaDon.Rows[r].Cells[1].FormattedValue.ToString();

            this.cbMaKH.SelectedValue =
                dgvHoaDon.Rows[r].Cells[1].Value.ToString();

            this.cbMaNV.Text =
                dgvHoaDon.Rows[r].Cells[2].FormattedValue.ToString();
            this.cbMaNV.SelectedValue =
                dgvHoaDon.Rows[r].Cells[2].Value.ToString();

            this.txtNgayLap.Text =
                dgvHoaDon.Rows[r].Cells[3].Value.ToString();
            this.txtNgayNhan.Text =
                dgvHoaDon.Rows[r].Cells[4].Value.ToString();


            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaHD.Focus();
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
                    DateTime ngayLap = Convert.ToDateTime(this.txtNgayLap.Text.ToString());
                    DateTime ngayNhan = Convert.ToDateTime(this.txtNgayNhan.Text.ToString());
                    cmd.CommandText = System.String.Concat("Insert Into HOADON Values(@MaHD, @MaKH, @MaNV,@NgayLap, @NgayNhan)");

                    cmd.Parameters.AddWithValue("@MaHD", this.txtMaHD.Text.ToString());
                    cmd.Parameters.AddWithValue("@MaKH", this.cbMaKH.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MaNV", this.cbMaNV.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ngayLap", ngayLap);
                    cmd.Parameters.AddWithValue("@ngayNhan", ngayNhan);
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
                    int r = dgvHoaDon.CurrentCell.RowIndex;
                    // MaKH hiện hành
                    string strMaHD =
                        dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                    // Câu lệnh SQL
                    cmd.CommandText = System.String.Concat("Update HOADON Set MaKH=@MaKH, MaNV=@MaNV, NgayLapHD=@NgayLap, NgayNhanHang=@NgayNhan Where MaHD='" + strMaHD + "'");
                    DateTime ngayLap = Convert.ToDateTime(this.txtNgayLap.Text.ToString());
                    DateTime ngayNhan = Convert.ToDateTime(this.txtNgayNhan.Text.ToString());


                    cmd.Parameters.AddWithValue("@MaKH", this.cbMaKH.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MaNV", this.cbMaNV.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap);
                    cmd.Parameters.AddWithValue("@NgayNhan", ngayNhan);
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
