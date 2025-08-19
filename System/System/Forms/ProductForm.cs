using InventoryApp.Models;

namespace InventoryApp.Forms
{
    public partial class ProductForm : System.Windows.Forms.Form
    {
        public Product? Product { get; private set; }

        public ProductForm(Product? product = null)
        {
            InitializeComponent();
            if (product != null)
            {
                txtName.Text = product.Name;
                txtSku.Text = product.Sku;
                numPrice.Value = product.Price;
                numQty.Value = product.Quantity;
                Product = product;
            }
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSku.Text))
            {
                System.Windows.Forms.MessageBox.Show("Preencha Nome e SKU.");
                return;
            }
            var p = this.Product ?? new InventoryApp.Models.Product();
            p.Name = txtName.Text.Trim();
            p.Sku = txtSku.Text.Trim();
            p.Price = numPrice.Value;
            p.Quantity = (int)numQty.Value;
            p.CreatedAt = p.CreatedAt == default ? DateTime.UtcNow : p.CreatedAt;
            this.Product = p;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

