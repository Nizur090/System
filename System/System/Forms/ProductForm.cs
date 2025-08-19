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
    }
}

