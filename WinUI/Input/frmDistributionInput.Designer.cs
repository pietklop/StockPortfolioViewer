
namespace Dashboard.Input
{
    partial class frmDistributionInput
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
            btnClose = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            lblInputT = new System.Windows.Forms.Label();
            numPercentage = new System.Windows.Forms.NumericUpDown();
            cmbMember = new System.Windows.Forms.ComboBox();
            cmbCountry = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)numPercentage).BeginInit();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(220, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(24, 23);
            btnClose.TabIndex = 5;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnOk.BackColor = System.Drawing.Color.Gainsboro;
            btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOk.Location = new System.Drawing.Point(199, 33);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(35, 23);
            btnOk.TabIndex = 2;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // lblInputT
            // 
            lblInputT.AutoSize = true;
            lblInputT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblInputT.ForeColor = System.Drawing.Color.Gainsboro;
            lblInputT.Location = new System.Drawing.Point(12, 9);
            lblInputT.Name = "lblInputT";
            lblInputT.Size = new System.Drawing.Size(59, 21);
            lblInputT.TabIndex = 6;
            lblInputT.Text = "Enter ...";
            // 
            // numPercentage
            // 
            numPercentage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numPercentage.Location = new System.Drawing.Point(144, 33);
            numPercentage.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPercentage.Name = "numPercentage";
            numPercentage.Size = new System.Drawing.Size(40, 23);
            numPercentage.TabIndex = 1;
            numPercentage.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numPercentage.Enter += numPercentage_Enter;
            // 
            // cmbMember
            // 
            cmbMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMember.FormattingEnabled = true;
            cmbMember.Location = new System.Drawing.Point(12, 33);
            cmbMember.Name = "cmbMember";
            cmbMember.Size = new System.Drawing.Size(121, 23);
            cmbMember.TabIndex = 0;
            cmbMember.SelectedIndexChanged += cmbMember_SelectedIndexChanged;
            // 
            // cmbCountry
            // 
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Location = new System.Drawing.Point(12, 34);
            cmbCountry.Name = "cmbCountry";
            cmbCountry.Size = new System.Drawing.Size(121, 23);
            cmbCountry.TabIndex = 7;
            cmbCountry.Visible = false;
            // 
            // frmDistributionInput
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            ClientSize = new System.Drawing.Size(246, 68);
            Controls.Add(cmbCountry);
            Controls.Add(cmbMember);
            Controls.Add(numPercentage);
            Controls.Add(lblInputT);
            Controls.Add(btnClose);
            Controls.Add(btnOk);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmDistributionInput";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmDistributionInput";
            ((System.ComponentModel.ISupportInitialize)numPercentage).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblInputT;
        private System.Windows.Forms.NumericUpDown numPercentage;
        private System.Windows.Forms.ComboBox cmbMember;
        private System.Windows.Forms.ComboBox cmbCountry;
    }
}