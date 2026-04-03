
namespace Dashboard.StockDetails
{
    partial class frmDistributionEdit
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
            components = new System.ComponentModel.Container();
            dgvLines = new System.Windows.Forms.DataGridView();
            btnAdd = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            btnSave = new System.Windows.Forms.Button();
            lblTotal = new System.Windows.Forms.Label();
            btnReset = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            SuspendLayout();
            // 
            // dgvLines
            // 
            dgvLines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLines.Location = new System.Drawing.Point(12, 29);
            dgvLines.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            dgvLines.Name = "dgvLines";
            dgvLines.RowHeadersWidth = 82;
            dgvLines.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            dgvLines.Size = new System.Drawing.Size(304, 411);
            dgvLines.TabIndex = 6;
            dgvLines.CellClick += dgvLines_CellClick;
            dgvLines.SelectionChanged += dgvLines_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnAdd.ForeColor = System.Drawing.Color.Gainsboro;
            btnAdd.Location = new System.Drawing.Point(205, 453);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(46, 26);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add";
            toolTip.SetToolTip(btnAdd, "Add a registration");
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 20000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnSave.ForeColor = System.Drawing.Color.Gainsboro;
            btnSave.Location = new System.Drawing.Point(270, 453);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(46, 26);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            toolTip.SetToolTip(btnSave, "Add a registration");
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblTotal.ForeColor = System.Drawing.Color.Gainsboro;
            lblTotal.Location = new System.Drawing.Point(12, 455);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(89, 21);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "Total: 999%";
            toolTip.SetToolTip(lblTotal, "Nee, dit is niet je verjaardag");
            // 
            // btnReset
            // 
            btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnReset.ForeColor = System.Drawing.Color.Gainsboro;
            btnReset.Location = new System.Drawing.Point(129, 453);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(58, 26);
            btnReset.TabIndex = 11;
            btnReset.Text = "Reset";
            toolTip.SetToolTip(btnReset, "Add a registration");
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(306, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(24, 23);
            btnClose.TabIndex = 8;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmDistributionEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(328, 491);
            Controls.Add(btnReset);
            Controls.Add(lblTotal);
            Controls.Add(btnSave);
            Controls.Add(btnClose);
            Controls.Add(btnAdd);
            Controls.Add(dgvLines);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmDistributionEdit";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmDay";
            Load += frmDistributionEdit_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLines;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnReset;
    }
}