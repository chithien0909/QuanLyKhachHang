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
    public partial class homePage : Form
    {

        Form loginForm = new LoginForm();
        public homePage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginForm.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Chắc không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void XemDanhMuc(int intDanhMuc)
        {
            Form DanhMuc = new DanhMuc();
            DanhMuc.Text = intDanhMuc.ToString();
            DanhMuc.ShowDialog();
        }

        private void danhMụcThànhPhốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(1);
        }

        private void danhMụcKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(2);
        }

        private void danhMụcNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(3);
        }

        private void danhMucSanPham_Click(object sender, EventArgs e)
        {
            XemDanhMuc(4);
        }

        private void danhMụcHóaĐƠnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(5);
        }

        private void danhMụcChiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemDanhMuc(6);
        }

        private void danhMụcThànhPhốToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form QLDanhMucTP = new QLDanhMucTP();
            QLDanhMucTP.Text = "Quản lý Danh mục Thành Phố";
            QLDanhMucTP.ShowDialog();
        }

        private void danhMụcKháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form QLKhachHang = new QuanLyDanhMucKH();
            QLKhachHang.Text = "Quản lý Danh mục Khách Hàng";
            QLKhachHang.ShowDialog();
        }

        private void danhMụcNhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form Form = new DanhMucNhanVien();
            Form.Text = "Quản lý Danh mục Nhân Viên";
            Form.ShowDialog();
        }

        private void danhMụcSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new DanhMucSanPham();
            Form.Text = "Quản lý Danh mục Sản Phẩm";
            Form.ShowDialog();
        }

        private void danhMụcHóaĐƠnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form Form = new DanhMucHoaDon();
            Form.Text = "Quản lý Danh mục Hoá Đơn";
            Form.ShowDialog();
        }

        private void danhMụcChiTiếtHóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form Form = new QuanLyDanhMucChiTietHD();
            Form.Text = "Quản lý Danh mục Chi Tiết Hoá Đơn";
            Form.ShowDialog();
        }

        private void kháchHàngTheoThànhPhốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new KHTheoTP();
            Form.Text = "Quản lý khách theo Thành Phố";
            Form.ShowDialog();
        }

        private void hóaĐơnTheoKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new QLHDTheoKH();
            Form.Text = "Quản lý Hoá Đơn theo Khách Hàng";
            Form.ShowDialog();
        }

        private void hóaĐơnTheoSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new HDTheoSP();
            Form.Text = "Quản lý Hoá Đơn theo Sản Phẩm";
            Form.ShowDialog();
        }

        private void hóaĐơnTheoNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new QLHDTheoNV();
            Form.Text = "Quản lý Hoá Đơn theo Nhân Viên";
            Form.ShowDialog();
        }

        private void chiTiếtHóaĐơnTheoHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form = new QLHDTheoHD();
            Form.Text = "Quản lý Hoá Đơn theo Hoá Đơn";
            Form.ShowDialog();
        }






    }
}
