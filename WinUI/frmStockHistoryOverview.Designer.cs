
namespace Dashboard
{
    partial class frmStockHistoryOverview
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
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStockList
            // 
            this.dgvStockList.AllowUserToAddRows = false;
            this.dgvStockList.AllowUserToDeleteRows = false;
            this.dgvStockList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Location = new System.Drawing.Point(83, 156);
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.ReadOnly = true;
            this.dgvStockList.RowTemplate.Height = 25;
            this.dgvStockList.Size = new System.Drawing.Size(558, 465);
            this.dgvStockList.TabIndex = 1;
            this.dgvStockList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellClick);
            this.dgvStockList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStockList_CellFormatting);
            this.dgvStockList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStockList_ColumnHeaderMouseClick);
            this.dgvStockList.SelectionChanged += new System.EventHandler(this.dgvStockList_SelectionChanged);
            // 
            // frmStockHistoryOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(906, 659);
            this.Controls.Add(this.dgvStockList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStockHistoryOverview";
            this.Text = "frmStockHistoryOverview";
            this.Load += new System.EventHandler(this.frmStockHistoryOverview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStockList;
    }
}