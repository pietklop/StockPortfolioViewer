
namespace Dashboard
{
    partial class frmDividends
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
            this.dgvDividends = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividends)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDividends
            // 
            this.dgvDividends.AllowUserToAddRows = false;
            this.dgvDividends.AllowUserToDeleteRows = false;
            this.dgvDividends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDividends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDividends.Location = new System.Drawing.Point(104, 52);
            this.dgvDividends.Name = "dgvDividends";
            this.dgvDividends.ReadOnly = true;
            this.dgvDividends.RowTemplate.Height = 25;
            this.dgvDividends.Size = new System.Drawing.Size(741, 584);
            this.dgvDividends.TabIndex = 2;
            this.dgvDividends.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDividends_CellFormatting);
            this.dgvDividends.SelectionChanged += new System.EventHandler(this.dgvDividends_SelectionChanged);
            // 
            // frmDividends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(932, 671);
            this.Controls.Add(this.dgvDividends);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDividends";
            this.Text = "frmDividends";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividends)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDividends;
    }
}