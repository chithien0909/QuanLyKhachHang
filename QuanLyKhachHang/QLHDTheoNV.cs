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
    public partial class QLHDTheoNV : Form
    {
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daKH = null;
        DataTable dtKH = null;

        SqlDataAdapter daNV = null;
        DataTable dtNV = null;

        SqlDataAdapter daHD = null;
        DataTable dtHD = null;
        public QLHDTheoNV()
        {
            InitializeComponent();
        }
        void LoadData(string MaNhanVien)
        {
            try
            {

                conn = new SqlConnection(strConnectionString);


                daKH = new SqlDataAdapter("SELECT * FROM KhachHang", conn);
                dtKH = new DataTable();
                dtKH.Clear();
                daKH.Fill(dtKH);
                (dgvHD.Columns["MaKH"] as
                    DataGridViewComboBoxColumn).DataSource = dtKH;
                (dgvHD.Columns["MaKH"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "TenCty";
                (dgvHD.Columns["MaKH"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaKH";

                daNV = new SqlDataAdapter("SELECT * FROM NhanVien", conn);
                dtNV = new DataTable();
                dtNV.Clear();
                daNV.Fill(dtNV);
                dtNV.Columns.Add("FullName");
                foreach (DataRow item in dtNV.Rows)
                {
                    item["FullName"] = item["Ho"] + " " + item["Ten"];
                }

                (dgvHD.Columns["MaNV"] as
                    DataGridViewComboBoxColumn).DataSource = dtNV;
                (dgvHD.Columns["MaNV"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "FullName";
                (dgvHD.Columns["MaNV"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "MaNV";




                daHD = new SqlDataAdapter("SELECT * FROM HoaDon Where MaNV = '" + MaNhanVien + "'", conn);
                dtHD = new DataTable();
                dtHD.Clear();
                daHD.Fill(dtHD);
                dgvHD.DataSource = dtHD;

                cbNV.DataSource = dtNV;
                cbNV.DisplayMember = "FullName";
                cbNV.ValueMember = "MaNV";
                cbNV.SelectedValue = MaNhanVien;


                txtSoLuong.Text = dtHD.Rows.Count.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table HoaDon. Lỗi rồi!!");
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QLHDTheoNV_Load(object sender, EventArgs e)
        {

            LoadData("0001");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string MaNhanVien = this.cbNV.SelectedValue.ToString();
            LoadData(MaNhanVien);
        }
    }
}
