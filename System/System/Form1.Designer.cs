namespace InventoryApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView gridProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnOut;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridProducts = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            this.SuspendLayout();

            this.txtSearch.PlaceholderText = "Buscar por nome ou SKU...";
            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Width = 400;
            this.txtSearch.TextChanged += (s, e) => this.OnSearchChanged();

            this.gridProducts.Location = new System.Drawing.Point(20, 60);
            this.gridProducts.Size = new System.Drawing.Size(960, 500);
            this.gridProducts.ReadOnly = true;
            this.gridProducts.AllowUserToAddRows = false;
            this.gridProducts.AllowUserToDeleteRows = false;
            this.gridProducts.AllowUserToOrderColumns = true;
            this.gridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProducts.MultiSelect = false;
            this.gridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.btnAdd.Text = "Novo";
            this.btnAdd.Location = new System.Drawing.Point(20, 580);
            this.btnAdd.Click += (s, e) => this.OnAdd();

            this.btnEdit.Text = "Editar";
            this.btnEdit.Location = new System.Drawing.Point(100, 580);
            this.btnEdit.Click += (s, e) => this.OnEdit();

            this.btnDelete.Text = "Excluir";
            this.btnDelete.Location = new System.Drawing.Point(180, 580);
            this.btnDelete.Click += (s, e) => this.OnDelete();

            this.btnIn.Text = "+ Entrada";
            this.btnIn.Location = new System.Drawing.Point(820, 580);
            this.btnIn.Click += (s, e) => this.OnAdjustStock(true);

            this.btnOut.Text = "- Saída";
            this.btnOut.Location = new System.Drawing.Point(900, 580);
            this.btnOut.Click += (s, e) => this.OnAdjustStock(false);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.gridProducts);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnOut);
            this.Text = "Gestão de Estoque";
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
