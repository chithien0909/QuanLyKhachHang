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
    public partial class QuanLyDanhMucKH : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daKH = null;
        DataTable dtKH = null;

        SqlDataAdapter daThanhPho = null;
        DataTable dtThanhPho = null;
        bool Them = false;
        public QuanLyDanhMucKH()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {

                conn = new SqlConnection(strConnectionString);
                daThanhPho = new SqlDataAdapter("SELECT * FROM THANHPHO", conn);
                dtThanhPho = new DataTable();
                dtThanhPho.Clear();
                daThanhPho.Fill(dtThanhPho);
                (dgvKH.Columns["ThanhPho"] as
                    DataGridViewComboBoxColumn).DataSource = dtThanhPho;
                (dgvKH.Columns["ThanhPho"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "TenThanhPho";
                (dgvKH.Columns["ThanhPho"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "ThanhPho";

                daKH = new SqlDataAdapter("SELECT * FROM KHACHHANG", conn);
                dtKH = new DataTable();
                dtKH.Clear();
                daKH.Fill(dtKH);
                dgvKH.DataSource = dtKH;

                this.txtMaKH.ResetText();
                this.txtTenCty.ResetText();
                this.txtDiaChi.ResetText();
                this.txtDienThoai.ResetText();


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
                MessageBox.Show("Không lấy được nội dung trong table THANHPHO. Lỗi rồi!!");
            }
        }

        private void Quanlydanhmucdon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Them = true;

            this.txtMaKH.ResetText();
            this.txtTenCty.ResetText();
            this.txtDiaChi.ResetText();
            this.txtDienThoai.ResetText();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.cbThanhPho.DataSource = dtThanhPho;
            this.cbThanhPho.DisplayMember = "TenThanhPho";
            this.cbThanhPho.ValueMember = "ThanhPho";

            this.txtMaKH.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;

            this.cbThanhPho.DataSource = dtThanhPho;
            this.cbThanhPho.DisplayMember = "TenThanhPho";
            this.cbThanhPho.ValueMember = "ThanhPho";
            int r = dgvKH.CurrentCell.RowIndex;

            this.txtMaKH.Text =
                dgvKH.Rows[r].Cells[0].Value.ToString();
            this.txtTenCty.Text =
                dgvKH.Rows[r].Cells[1].Value.ToString();
            this.txtDiaChi.Text =
                dgvKH.Rows[r].Cells[2].Value.ToString();
            this.cbThanhPho.SelectedValue =
                dgvKH.Rows[r].Cells[3].Value.ToString();
            this.txtDienThoai.Text =
                dgvKH.Rows[r].Cells[3].Value.ToString();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaKH.Focus();

            
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
                    cmd.CommandText = System.String.Concat("Insert Into KhachHang Values(" + "'" +
                        this.txtMaKH.Text.ToString() + "', N'" +
                        this.txtTenCty.Text.ToString() + "', N'" +
                        this.txtDiaChi.Text.ToString() + "','" +
                        this.cbThanhPho.SelectedValue.ToString() +
                        "','" + this.txtDienThoai.Text.ToString() +
                        "')");
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
                    int r = dgvKH.CurrentCell.RowIndex;
                    // MaKH hiện hành
                    string strMAKH =
                        dgvKH.Rows[r].Cells[0].Value.ToString();
                    // Câu lệnh SQL
                    cmd.CommandText = System.String.Concat("Update KhachHang Set TenCty='" +
                    this.txtTenCty.Text.ToString() + "', DiaChi='"
                    + this.txtDiaChi.Text.ToString() + "',ThanhPho='" +
                    this.cbThanhPho.SelectedValue.ToString() + "', DienThoai='" +
                    this.txtDienThoai.Text.ToString() + "' Where MaKH='" + strMAKH + "'");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
            this.txtMaKH.ResetText();
            this.dgvKH.Focus();
            this.txtTenCty.ResetText();
            this.txtDiaChi.ResetText();
            this.txtDienThoai.ResetText();
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
                int r = dgvKH.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strMAKH =
                dgvKH.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From KHACHHANG Where MaKH='" + strMAKH + "'");
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

    }
}
