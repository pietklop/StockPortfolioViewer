
namespace Dashboard
{
    partial class frmStockRetriever
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
            this.lblStockName = new System.Windows.Forms.Label();
            this.dgvRetrieverList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStockName
            // 
            this.lblStockName.AutoSize = true;
            this.lblStockName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStockName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblStockName.Location = new System.Drawing.Point(59, 31);
            this.lblStockName.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblStockName.Name = "lblStockName";
            this.lblStockName.Size = new System.Drawing.Size(157, 31);
            this.lblStockName.TabIndex = 4;
            this.lblStockName.Text = "Stock name";
            this.lblStockName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgvRetrieverList
            // 
            this.dgvRetrieverList.AllowUserToAddRows = false;
            this.dgvRetrieverList.AllowUserToDeleteRows = false;
            this.dgvRetrieverList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetrieverList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvRetrieverList.Location = new System.Drawing.Point(0, 81);
            this.dgvRetrieverList.Name = "dgvRetrieverList";
            this.dgvRetrieverList.ReadOnly = true;
            this.dgvRetrieverList.RowTemplate.Height = 25;
            this.dgvRetrieverList.Size = new System.Drawing.Size(459, 125);
            this.dgvRetrieverList.TabIndex = 5;
            this.dgvRetrieverList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRetrieverList_CellClick);
            this.dgvRetrieverList.SelectionChanged += new System.EventHandler(this.dgvRetrieverList_SelectionChanged);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(435, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmStockRetriever
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(459, 206);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvRetrieverList);
            this.Controls.Add(this.lblStockName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStockRetriever";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStockRetriever";
            this.Load += new System.EventHandler(this.frmStockRetriever_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStockName;
        private System.Windows.Forms.DataGridView dgvRetrieverList;
        private System.Windows.Forms.Button btnClose;
    }
}