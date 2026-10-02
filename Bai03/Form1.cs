using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WINFORMSGUI
{
    public partial class Form1 : Form
    {
        BindingList<Product> products = new BindingList<Product>();
        BindingSource source = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            source.DataSource = products;
            dgvProducts.DataSource = source;

            cboCategory.Items.Add(new Category("Dien thoai", "DT"));
            cboCategory.Items.Add(new Category("Laptop", "LT"));
            cboCategory.Items.Add(new Category("Phu kien", "PK"));

            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "Code";
            cboCategory.SelectedIndex = 0;

            CapNhatStatus();
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                picAvatar.Image = new Bitmap(img);
                img.Dispose();
                picAvatar.Tag = open.FileName;
            }
        }

        private bool KiemTra()
        {
            errorProvider1.Clear();
            bool ok = true;
            decimal gia;
            int soLuong;

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider1.SetError(txtProductName, "Nhap ten san pham");
                ok = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out gia) || gia <= 0)
            {
                errorProvider1.SetError(txtUnitPrice, "Don gia phai lon hon 0");
                ok = false;
            }

            if (!int.TryParse(txtQuantity.Text, out soLuong) || soLuong < 0)
            {
                errorProvider1.SetError(txtQuantity, "So luong phai >= 0");
                ok = false;
            }

            return ok;
        }

        private Product LayDuLieu()
        {
            return new Product
            {
                ProductId = txtProductId.Text,
                ProductName = txtProductName.Text,
                Category = cboCategory.Text,
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Quantity = int.Parse(txtQuantity.Text),
                Avatar = picAvatar.Tag == null ? "" : picAvatar.Tag.ToString()
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
                return;

            products.Add(LayDuLieu());
            source.ResetBindings(false);
            CapNhatStatus();
            XoaTrang();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
                return;

            if (!KiemTra())
                return;

            Product p = dgvProducts.CurrentRow.DataBoundItem as Product;

            p.ProductId = txtProductId.Text;
            p.ProductName = txtProductName.Text;
            p.Category = cboCategory.Text;
            p.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            p.Quantity = int.Parse(txtQuantity.Text);
            p.Avatar = picAvatar.Tag == null ? "" : picAvatar.Tag.ToString();

            source.ResetCurrentItem();
            CapNhatStatus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
                return;

            DialogResult kq = MessageBox.Show(
                "Ban co chac muon xoa san pham nay?",
                "Xac nhan xoa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                Product p = dgvProducts.CurrentRow.DataBoundItem as Product;

                if (p != null)
                    products.Remove(p);

                source.ResetBindings(false);
                CapNhatStatus();
                XoaTrang();
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Product p = dgvProducts.Rows[e.RowIndex].DataBoundItem as Product;

            if (p == null)
                return;

            txtProductId.Text = p.ProductId;
            txtProductName.Text = p.ProductName;
            cboCategory.Text = p.Category;
            txtUnitPrice.Text = p.UnitPrice.ToString();
            txtQuantity.Text = p.Quantity.ToString();

            picAvatar.Image = null;
            picAvatar.Tag = null;

            if (!string.IsNullOrEmpty(p.Avatar) && File.Exists(p.Avatar))
            {
                Image img = Image.FromFile(p.Avatar);
                picAvatar.Image = new Bitmap(img);
                img.Dispose();
                picAvatar.Tag = p.Avatar;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.ToLower();

            BindingList<Product> ketQua = new BindingList<Product>();

            foreach (Product p in products)
            {
                if (p.ProductName.ToLower().Contains(tuKhoa))
                    ketQua.Add(p);
            }

            source.DataSource = ketQua;
            dgvProducts.DataSource = source;

            CapNhatStatus();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            XuatCsv();
        }

        private void menuExport_Click(object sender, EventArgs e)
        {
            XuatCsv();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void XuatCsv()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV Files|*.csv";
            save.FileName = "products.csv";

            if (save.ShowDialog() != DialogResult.OK)
                return;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Ma SP,Ten SP,Danh Muc,Don Gia,So Luong");

            foreach (Product p in products)
            {
                sb.AppendLine(
                    p.ProductId + "," +
                    p.ProductName + "," +
                    p.Category + "," +
                    p.UnitPrice + "," +
                    p.Quantity);
            }

            File.WriteAllText(save.FileName, sb.ToString(), Encoding.UTF8);

            MessageBox.Show("Xuat CSV thanh cong");
        }

        private void XoaTrang()
        {
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();

            picAvatar.Image = null;
            picAvatar.Tag = null;

            errorProvider1.Clear();
        }

        private void CapNhatStatus()
        {
            lblStatus.Text = "Tong so san pham: " + products.Count;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Category(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}