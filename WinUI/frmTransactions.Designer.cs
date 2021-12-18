
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
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.lblCurrentPriceT = new System.Windows.Forms.Label();
            this.lblCurrentPrice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Location = new System.Drawing.Point(193, 111);
            this.dgvTransactions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersWidth = 82;
            this.dgvTransactions.RowTemplate.Height = 25;
            this.dgvTransactions.Size = new System.Drawing.Size(1376, 1246);
            this.dgvTransactions.TabIndex = 1;
            this.dgvTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTransactions_CellFormatting);
            this.dgvTransactions.SelectionChanged += new System.EventHandler(this.dgvTransactions_SelectionChanged);
            // 
            // lblCurrentPriceT
            // 
            this.lblCurrentPriceT.AutoSize = true;
            this.lblCurrentPriceT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentPriceT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblCurrentPriceT.Location = new System.Drawing.Point(193, 42);
            this.lblCurrentPriceT.Name = "lblCurrentPriceT";
            this.lblCurrentPriceT.Size = new System.Drawing.Size(205, 45);
            this.lblCurrentPriceT.TabIndex = 2;
            this.lblCurrentPriceT.Text = "Current price";
            this.lblCurrentPriceT.Visible = false;
            // 
            // lblCurrentPrice
            // 
            this.lblCurrentPrice.AutoSize = true;
            this.lblCurrentPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblCurrentPrice.Location = new System.Drawing.Point(416, 42);
            this.lblCurrentPrice.Name = "lblCurrentPrice";
            this.lblCurrentPrice.Size = new System.Drawing.Size(97, 45);
            this.lblCurrentPrice.TabIndex = 3;
            this.lblCurrentPrice.Text = "$ 999";
            this.lblCurrentPrice.Visible = false;
            // 
            // frmTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1827, 1444);
            this.Controls.Add(this.lblCurrentPrice);
            this.Controls.Add(this.lblCurrentPriceT);
            this.Controls.Add(this.dgvTransactions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmTransactions";
            this.Text = "frmTransactions";
            this.Load += new System.EventHandler(this.frmTransactions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Label lblCurrentPriceT;
        private System.Windows.Forms.Label lblCurrentPrice;
    }
}