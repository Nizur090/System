namespace InventoryApp.Forms
{
    partial class ProductForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSku;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQty;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSku = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // labels
            // 
            this.lblName.Text = "Nome";
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblSku.Text = "SKU";
            this.lblSku.Location = new System.Drawing.Point(20, 70);
            this.lblPrice.Text = "Preço";
            this.lblPrice.Location = new System.Drawing.Point(20, 120);
            this.lblQty.Text = "Quantidade";
            this.lblQty.Location = new System.Drawing.Point(20, 170);
            // 
            // inputs
            // 
            this.txtName.Location = new System.Drawing.Point(120, 16);
            this.txtName.Size = new System.Drawing.Size(260, 27);
            this.txtSku.Location = new System.Drawing.Point(120, 66);
            this.txtSku.Size = new System.Drawing.Size(260, 27);
            this.numPrice.Location = new System.Drawing.Point(120, 116);
            this.numPrice.Size = new System.Drawing.Size(120, 27);
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Maximum = 1000000;
            this.numPrice.Minimum = 0;
            this.numQty.Location = new System.Drawing.Point(120, 166);
            this.numQty.Size = new System.Drawing.Size(120, 27);
            this.numQty.Maximum = 1000000;
            this.numQty.Minimum = 0;
            // 
            // buttons
            // 
            this.btnOk.Text = "Salvar";
            this.btnOk.Location = new System.Drawing.Point(120, 220);
            this.btnOk.Click += (s, e) =>
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
            };

            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Location = new System.Drawing.Point(220, 220);
            this.btnCancel.Click += (s, e) =>
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            };

            // 
            // ProductForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 280);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSku);
            this.Controls.Add(this.txtSku);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.numQty);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Text = "Produto";
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

