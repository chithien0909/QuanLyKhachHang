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
    public partial class DanhMucSanPham : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daSP = null;
        DataTable dtSP = null;

        bool Them = false;
        public DanhMucSanPham()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {

                conn = new SqlConnection(strConnectionString);

                daSP = new SqlDataAdapter("SELECT * FROM SANPHAM", conn);
                dtSP= new DataTable();
                dtSP.Clear();
                daSP.Fill(dtSP);
                dgvDMSP.DataSource = dtSP;

                this.txtMaSP.ResetText();
                this.txtTenSP.ResetText();
                this.txtDVT.ResetText();
                this.txtDG.ResetText();



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
                MessageBox.Show("Không lấy được nội dung trong table SanPham. Lỗi rồi!!");
            }
        }

        private void DanhMucSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
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
                int r = dgvDMSP.CurrentCell.RowIndex;
                // Lấy MaKH của record hiện hành
                string strMaSP =
                dgvDMSP.Rows[r].Cells[0].Value.ToString();
                // Viết câu lệnh SQL
                cmd.CommandText = System.String.Concat("Delete From SANPHAM Where MaSP='" + strMaSP + "'");
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

            this.txtMaSP.ResetText();
            this.txtTenSP.ResetText();
            this.txtDVT.ResetText();
            this.txtDG.ResetText();


            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaSP.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;

            int r = dgvDMSP.CurrentCell.RowIndex;
            this.txtMaSP.Text =
                dgvDMSP.Rows[r].Cells[0].Value.ToString();
            this.txtTenSP.Text =
                dgvDMSP.Rows[r].Cells[1].Value.ToString();
            this.txtDVT.Text =
                dgvDMSP.Rows[r].Cells[2].Value.ToString();
            this.txtDG.Text =
                dgvDMSP.Rows[r].Cells[3].Value.ToString();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtMaSP.Focus();
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
                    cmd.CommandText = System.String.Concat("Insert Into SanPham Values(" + "'" +
                        this.txtMaSP.Text.ToString() + "', N'" +
                        this.txtTenSP.Text.ToString() + "', N'" +
                        this.txtDVT.Text.ToString() + "'," + 
                        Double.Parse(this.txtDG.Text) + ", NULL)");

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
                    int r = dgvDMSP.CurrentCell.RowIndex;
                    // MaKH hiện hành
                    string strMaSP =
                        dgvDMSP.Rows[r].Cells[0].Value.ToString();
                    // Câu lệnh SQL
                    cmd.CommandText = System.String.Concat("Update SANPHAM SET TenSP=N'" +
                    this.txtTenSP.Text.ToString() + "', DonViTinh=N'" +
                    this.txtDVT.Text.ToString() + "', DonGia=" +
                    Double.Parse(this.txtDG.Text.ToString()) + " Where MaSP='" + strMaSP + "'");
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
