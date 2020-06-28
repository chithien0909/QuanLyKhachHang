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
    public partial class KHTheoTP : Form
    { 
        string strConnectionString = @"Data Source=DESKTOP-EKMHCM9\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daKH = null;
        DataTable dtKH = null;

        SqlDataAdapter daTP = null;
        DataTable dtTP = null;

        public KHTheoTP()
        {
            InitializeComponent();
        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadData(string ThanhPho)
        {
            try
            {

                conn = new SqlConnection(strConnectionString);


                daTP = new SqlDataAdapter("SELECT * FROM ThanhPho", conn);
                dtTP = new DataTable();
                dtTP.Clear();
                daTP.Fill(dtTP);
                (dgvKH.Columns["ThanhPho"] as
                    DataGridViewComboBoxColumn).DataSource = dtTP;
                (dgvKH.Columns["ThanhPho"] as
                    DataGridViewComboBoxColumn).DisplayMember =
                    "TenThanhPho";
                (dgvKH.Columns["ThanhPho"] as
                   DataGridViewComboBoxColumn).ValueMember =
                    "ThanhPho";

                daKH = new SqlDataAdapter("SELECT * FROM KHACHHANG where ThanhPho = '"+ThanhPho+"'", conn);
                dtKH = new DataTable();
                dtKH.Clear();
                daKH.Fill(dtKH);
                dgvKH.DataSource = dtKH;

                
                cbThanhPho.DataSource = dtTP;
                cbThanhPho.DisplayMember = "TenThanhPho";
                cbThanhPho.ValueMember = "ThanhPho";
                cbThanhPho.SelectedValue = ThanhPho;

                txtSoKH.Text = dtKH.Rows.Count.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table NhanVien. Lỗi rồi!!");
            }
        }
        private void KHTheoTP_Load(object sender, EventArgs e)
        {
            LoadData("01");
        }


        private void btnOk_Click(object sender, EventArgs e)
        {

            string ThanhPho = this.cbThanhPho.SelectedValue.ToString();
            LoadData(ThanhPho);
        }

        
    }
}
