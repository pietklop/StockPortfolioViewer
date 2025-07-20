
namespace Dashboard
{
    partial class frmTransactions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvTransactions = new System.Windows.Forms.DataGridView();
            lblCurrentPriceT = new System.Windows.Forms.Label();
            lblCurrentPrice = new System.Windows.Forms.Label();
            lblAvgBuy = new System.Windows.Forms.Label();
            lblAvgBuyT = new System.Windows.Forms.Label();
            lblTotalValue = new System.Windows.Forms.Label();
            lblTotalValueT = new System.Windows.Forms.Label();
            cmbViewMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).BeginInit();
            SuspendLayout();
            // 
            // dgvTransactions
            // 
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.AllowUserToDeleteRows = false;
            dgvTransactions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransactions.Location = new System.Drawing.Point(104, 52);
            dgvTransactions.Name = "dgvTransactions";
            dgvTransactions.ReadOnly = true;
            dgvTransactions.RowHeadersWidth = 82;
            dgvTransactions.Size = new System.Drawing.Size(741, 584);
            dgvTransactions.TabIndex = 1;
            dgvTransactions.CellFormatting += dgvTransactions_CellFormatting;
            dgvTransactions.SelectionChanged += dgvTransactions_SelectionChanged;
            // 
            // lblCurrentPriceT
            // 
            lblCurrentPriceT.AutoSize = true;
            lblCurrentPriceT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblCurrentPriceT.ForeColor = System.Drawing.Color.Gainsboro;
            lblCurrentPriceT.Location = new System.Drawing.Point(104, 20);
            lblCurrentPriceT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblCurrentPriceT.Name = "lblCurrentPriceT";
            lblCurrentPriceT.Size = new System.Drawing.Size(101, 21);
            lblCurrentPriceT.TabIndex = 2;
            lblCurrentPriceT.Text = "Current price";
            lblCurrentPriceT.Visible = false;
            // 
            // lblCurrentPrice
            // 
            lblCurrentPrice.AutoSize = true;
            lblCurrentPrice.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblCurrentPrice.ForeColor = System.Drawing.Color.Gainsboro;
            lblCurrentPrice.Location = new System.Drawing.Point(211, 20);
            lblCurrentPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblCurrentPrice.Name = "lblCurrentPrice";
            lblCurrentPrice.Size = new System.Drawing.Size(50, 21);
            lblCurrentPrice.TabIndex = 3;
            lblCurrentPrice.Text = "$ 999";
            lblCurrentPrice.Visible = false;
            // 
            // lblAvgBuy
            // 
            lblAvgBuy.AutoSize = true;
            lblAvgBuy.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblAvgBuy.ForeColor = System.Drawing.Color.Gainsboro;
            lblAvgBuy.Location = new System.Drawing.Point(389, 20);
            lblAvgBuy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblAvgBuy.Name = "lblAvgBuy";
            lblAvgBuy.Size = new System.Drawing.Size(50, 21);
            lblAvgBuy.TabIndex = 5;
            lblAvgBuy.Text = "$ 999";
            lblAvgBuy.Visible = false;
            // 
            // lblAvgBuyT
            // 
            lblAvgBuyT.AutoSize = true;
            lblAvgBuyT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblAvgBuyT.ForeColor = System.Drawing.Color.Gainsboro;
            lblAvgBuyT.Location = new System.Drawing.Point(316, 20);
            lblAvgBuyT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblAvgBuyT.Name = "lblAvgBuyT";
            lblAvgBuyT.Size = new System.Drawing.Size(67, 21);
            lblAvgBuyT.TabIndex = 4;
            lblAvgBuyT.Text = "Avg buy";
            lblAvgBuyT.Visible = false;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblTotalValue.ForeColor = System.Drawing.Color.Gainsboro;
            lblTotalValue.Location = new System.Drawing.Point(537, 20);
            lblTotalValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new System.Drawing.Size(50, 21);
            lblTotalValue.TabIndex = 7;
            lblTotalValue.Text = "$ 999";
            lblTotalValue.Visible = false;
            // 
            // lblTotalValueT
            // 
            lblTotalValueT.AutoSize = true;
            lblTotalValueT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblTotalValueT.ForeColor = System.Drawing.Color.Gainsboro;
            lblTotalValueT.Location = new System.Drawing.Point(491, 20);
            lblTotalValueT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblTotalValueT.Name = "lblTotalValueT";
            lblTotalValueT.Size = new System.Drawing.Size(42, 21);
            lblTotalValueT.TabIndex = 6;
            lblTotalValueT.Text = "Total";
            lblTotalValueT.Visible = false;
            // 
            // cmbViewMode
            // 
            cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbViewMode.FormattingEnabled = true;
            cmbViewMode.Location = new System.Drawing.Point(641, 22);
            cmbViewMode.Name = "cmbViewMode";
            cmbViewMode.Size = new System.Drawing.Size(123, 23);
            cmbViewMode.TabIndex = 8;
            cmbViewMode.SelectionChangeCommitted += cmbViewMode_SelectionChangeCommitted;
            // 
            // frmTransactions
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
            ClientSize = new System.Drawing.Size(984, 677);
            Controls.Add(cmbViewMode);
            Controls.Add(lblTotalValue);
            Controls.Add(lblTotalValueT);
            Controls.Add(lblAvgBuy);
            Controls.Add(lblAvgBuyT);
            Controls.Add(lblCurrentPrice);
            Controls.Add(lblCurrentPriceT);
            Controls.Add(dgvTransactions);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmTransactions";
            Text = "frmTransactions";
            Load += frmTransactions_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTransactions).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Label lblCurrentPriceT;
        private System.Windows.Forms.Label lblCurrentPrice;
        private System.Windows.Forms.Label lblAvgBuy;
        private System.Windows.Forms.Label lblAvgBuyT;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotalValueT;
        private System.Windows.Forms.ComboBox cmbViewMode;
    }
}