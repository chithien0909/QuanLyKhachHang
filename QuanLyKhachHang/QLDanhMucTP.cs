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
    public partial class QLDanhMucTP : Form
    {

        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daTP = null;
        DataTable dtTP = null;
        bool Them;
        public QLDanhMucTP()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                conn = new SqlConnection(strConnectionString);
                daTP = new SqlDataAdapter("SELECT * FROM THANHPHO", conn);
                dtTP = new DataTable();
                dtTP.Clear();
                daTP.Fill(dtTP);
                dgvTP.DataSource = dtTP;
                dgvTP.AutoResizeColumns();
                this.txtThanhPho.ResetText();
                this.txtTenTP.ResetText();

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
        private void QLDanhMucTP_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void QLDanhMucTP_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Giải phóng tài nguyên
            dtTP.Dispose();
            dtTP = null;
            conn = null;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Them = true;

            this.txtThanhPho.ResetText();
            this.txtTenTP.ResetText();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtThanhPho.Focus();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Them = false;
            this.panel1.Enabled = true;

            int r = dgvTP.CurrentCell.RowIndex;

            this.txtThanhPho.Text =
                dgvTP.Rows[r].Cells[0].Value.ToString();
            this.txtTenTP.Text =
                dgvTP.Rows[r].Cells[1].Value.ToString();

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel1.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnReturn.Enabled = false;

            this.txtThanhPho.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                int r = dgvTP.CurrentCell.RowIndex;
                string strThanhPho = dgvTP.Rows[r].Cells[0].Value.ToString();

                //Lệnh SQL 
                cmd.CommandText = System.String.Concat("DELETE FROM THANHPHO WHERE ThanhPho='" + strThanhPho + "'");
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                LoadData();

                MessageBox.Show("Đã xoá xong!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtTenTP.ResetText();
            this.txtThanhPho.ResetText();

            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnReturn.Enabled = true;

            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel1.Enabled = false;

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

                    cmd.CommandText = System.String.Concat("INSERT INTO THANHPHO VALUES(" + "N'" +
                    this.txtThanhPho.Text.ToString() + "', N'" +
                    this.txtTenTP.Text.ToString() + "')");
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm xong");

                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thêm được. Lỗi rồi!");
                }
            }
            if (!Them)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                int r = dgvTP.CurrentCell.RowIndex;

                string strThanhPho = dgvTP.Rows[r].Cells[0].Value.ToString();
                cmd.CommandText = System.String.Concat("UPDATE THANHPHO SET TenThanhPho=N'" + this.txtTenTP.Text.ToString() + "' Where ThanhPho='" + strThanhPho + "'");
                cmd.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Đã sửa xong");
            }
            conn.Close();
        }

    }
}
