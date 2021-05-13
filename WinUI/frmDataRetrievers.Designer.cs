
namespace Dashboard
{
    partial class frmDataRetrievers
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
            this.dgvDataRetrievers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataRetrievers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDataRetrievers
            // 
            this.dgvDataRetrievers.AllowUserToAddRows = false;
            this.dgvDataRetrievers.AllowUserToDeleteRows = false;
            this.dgvDataRetrievers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataRetrievers.Location = new System.Drawing.Point(30, 73);
            this.dgvDataRetrievers.Name = "dgvDataRetrievers";
            this.dgvDataRetrievers.ReadOnly = true;
            this.dgvDataRetrievers.RowTemplate.Height = 25;
            this.dgvDataRetrievers.Size = new System.Drawing.Size(601, 305);
            this.dgvDataRetrievers.TabIndex = 1;
            this.dgvDataRetrievers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataRetrievers_CellClick);
            this.dgvDataRetrievers.SelectionChanged += new System.EventHandler(this.dgvDataRetrievers_SelectionChanged);
            // 
            // frmDataRetrievers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDataRetrievers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDataRetrievers";
            this.Text = "frmDataRetrievers";
            this.Load += new System.EventHandler(this.frmDataRetrievers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataRetrievers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataRetrievers;
    }
}