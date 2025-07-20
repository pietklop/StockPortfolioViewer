
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
            dgvDividends = new System.Windows.Forms.DataGridView();
            cmbViewMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvDividends).BeginInit();
            SuspendLayout();
            // 
            // dgvDividends
            // 
            dgvDividends.AllowUserToAddRows = false;
            dgvDividends.AllowUserToDeleteRows = false;
            dgvDividends.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvDividends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDividends.Location = new System.Drawing.Point(104, 52);
            dgvDividends.Name = "dgvDividends";
            dgvDividends.ReadOnly = true;
            dgvDividends.Size = new System.Drawing.Size(741, 584);
            dgvDividends.TabIndex = 2;
            dgvDividends.CellFormatting += dgvDividends_CellFormatting;
            dgvDividends.SelectionChanged += dgvDividends_SelectionChanged;
            // 
            // cmbViewMode
            // 
            cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbViewMode.FormattingEnabled = true;
            cmbViewMode.Location = new System.Drawing.Point(104, 23);
            cmbViewMode.Name = "cmbViewMode";
            cmbViewMode.Size = new System.Drawing.Size(131, 23);
            cmbViewMode.TabIndex = 9;
            cmbViewMode.SelectionChangeCommitted += cmbViewMode_SelectionChangeCommitted;
            // 
            // frmDividends
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
            ClientSize = new System.Drawing.Size(932, 671);
            Controls.Add(cmbViewMode);
            Controls.Add(dgvDividends);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmDividends";
            Text = "frmDividends";
            Load += frmDividends_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDividends).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDividends;
        private System.Windows.Forms.ComboBox cmbViewMode;
    }
}