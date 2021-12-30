
namespace Dashboard
{
    partial class frmStockDetail
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
            this.dgvStockDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStockDetails
            // 
            this.dgvStockDetails.AllowUserToAddRows = false;
            this.dgvStockDetails.AllowUserToDeleteRows = false;
            this.dgvStockDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStockDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockDetails.Location = new System.Drawing.Point(86, 63);
            this.dgvStockDetails.Name = "dgvStockDetails";
            this.dgvStockDetails.ReadOnly = true;
            this.dgvStockDetails.RowTemplate.Height = 25;
            this.dgvStockDetails.Size = new System.Drawing.Size(350, 585);
            this.dgvStockDetails.TabIndex = 0;
            this.dgvStockDetails.TabStop = false;
            this.dgvStockDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockDetails_CellClick);
            // 
            // frmStockDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(933, 677);
            this.Controls.Add(this.dgvStockDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStockDetail";
            this.Text = "frmOverview";
            this.Load += new System.EventHandler(this.frmStockDetail_Load);
            this.Shown += new System.EventHandler(this.frmStockDetail_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStockDetails;
    }
}