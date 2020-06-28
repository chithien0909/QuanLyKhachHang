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
    public partial class QLHDTheoHD : Form
    {

        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daSP = null;
        DataTable dtSP = null;

        SqlDataAdapter daCTHD = null;
        DataTable dtCTHD = null;

        SqlDataAdapter daHD = null;
        DataTable dtHD = null;


        public QLHDTheoHD()
        {
            InitializeComponent();
        }
        void LoadData(string MaHD)
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




                daCTHD = new SqlDataAdapter("SELECT * FROM ChiTietHoaDon Where MaHD = '" + MaHD + "'", conn);
                dtCTHD = new DataTable();
                dtCTHD.Clear();
                daCTHD.Fill(dtCTHD);
                dgvHD.DataSource = dtCTHD;

                daHD = new SqlDataAdapter("SELECT * FROM HoaDon", conn);
                dtHD = new DataTable();
                dtHD.Clear();
                daHD.Fill(dtHD);

                cbHD.DataSource = dtHD;
                cbHD.DisplayMember = "MaHD";
                cbHD.ValueMember = "MaHD";
                cbHD.SelectedValue = MaHD;


                txtSoLuong.Text = dtCTHD.Rows.Count.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!");
            }
        }
        private void QLHDTheoHD_Load(object sender, EventArgs e)
        {

            LoadData("00001");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string MaHD = this.cbHD.SelectedValue.ToString();
            LoadData(MaHD);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
