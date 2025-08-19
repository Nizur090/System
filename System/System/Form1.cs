using InventoryApp.Data;
using InventoryApp.Forms;
using InventoryApp.Models;
using InventoryApp.Repositories;

namespace InventoryApp
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private readonly ProductRepository _productRepository = new ProductRepository();
        private IList<Product> _currentProducts = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            Database.Initialize();
            ReloadProducts();
        }

        private void ReloadProducts()
        {
            _currentProducts = _productRepository.GetAll();
            gridProducts.DataSource = _currentProducts
                .Select(p => new { p.Id, p.Name, p.Sku, Price = p.Price.ToString("C2"), p.Quantity })
                .ToList();
        }

        private void OnSearchChanged()
        {
            var q = txtSearch.Text?.Trim() ?? string.Empty;
            _currentProducts = string.IsNullOrWhiteSpace(q) ? _productRepository.GetAll() : _productRepository.Search(q);
            gridProducts.DataSource = _currentProducts
                .Select(p => new { p.Id, p.Name, p.Sku, Price = p.Price.ToString("C2"), p.Quantity })
                .ToList();
        }

        private Product? GetSelected()
        {
            if (gridProducts.CurrentRow == null) return null;
            var idObj = gridProducts.CurrentRow.Cells["Id"].Value;
            if (idObj == null) return null;
            int id = Convert.ToInt32(idObj);
            return _currentProducts.FirstOrDefault(p => p.Id == id);
        }

        private void OnAdd()
        {
            using var dlg = new ProductForm();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK && dlg.Product != null)
            {
                var id = _productRepository.Add(dlg.Product);
                dlg.Product.Id = id;
                ReloadProducts();
            }
        }

        private void OnEdit()
        {
            var selected = GetSelected();
            if (selected == null) return;
            using var dlg = new ProductForm(new Product
            {
                Id = selected.Id,
                Name = selected.Name,
                Sku = selected.Sku,
                Price = selected.Price,
                Quantity = selected.Quantity,
                CreatedAt = selected.CreatedAt
            });
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK && dlg.Product != null)
            {
                _productRepository.Update(dlg.Product);
                ReloadProducts();
            }
        }

        private void OnDelete()
        {
            var selected = GetSelected();
            if (selected == null) return;
            var c = System.Windows.Forms.MessageBox.Show($"Excluir '{selected.Name}'?", "Confirmar", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (c == System.Windows.Forms.DialogResult.Yes)
            {
                _productRepository.Delete(selected.Id);
                ReloadProducts();
            }
        }

        private void OnAdjustStock(bool isInbound)
        {
            var selected = GetSelected();
            if (selected == null) return;
            var prompt = isInbound ? "+ Quantidade de Entrada:" : "- Quantidade de Saída:";
            var input = Microsoft.VisualBasic.Interaction.InputBox(prompt, "Movimentação de Estoque", "1");
            if (int.TryParse(input, out var qty) && qty > 0)
            {
                if (!isInbound) qty = -qty;
                _productRepository.AdjustStock(selected.Id, qty, isInbound ? "Entrada manual" : "Saída manual");
                ReloadProducts();
            }
        }
    }
}
