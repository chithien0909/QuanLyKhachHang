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
    public partial class HDTheoSP : Form
    {

        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daSP = null;
        DataTable dtSP = null;

        SqlDataAdapter daHD = null;
        DataTable dtHD = null;
        public HDTheoSP()
        {
            InitializeComponent();
        }
        void LoadData(string MaSP)
        {
            try
            {

                conn = new SqlConnection(strConnectionString);


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




                daHD = new SqlDataAdapter("SELECT * FROM ChiTietHoaDon Where MaSP = '" + MaSP + "'", conn);
                dtHD = new DataTable();
                dtHD.Clear();
                daHD.Fill(dtHD);
                dgvHD.DataSource = dtHD;

                cbSP.DataSource = dtSP;
                cbSP.DisplayMember = "TenSP";
                cbSP.ValueMember = "MaSP";
                cbSP.SelectedValue = MaSP;


                txtSoLuong.Text = dtHD.Rows.Count.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!");
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HDTheoSP_Load(object sender, EventArgs e)
        {
            LoadData("0001");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string MaSP = this.cbSP.SelectedValue.ToString();
            LoadData(MaSP);
        }

    }
}
