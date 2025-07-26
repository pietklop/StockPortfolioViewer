
namespace Dashboard.Input
{
    partial class frmAlarmConditionInput
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
            lblAlarmT = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            txtRemarks = new System.Windows.Forms.TextBox();
            txtLowerThreshold = new System.Windows.Forms.TextBox();
            lblThresholdT = new System.Windows.Forms.Label();
            lblRemarksT = new System.Windows.Forms.Label();
            lblNativeCurrT = new System.Windows.Forms.Label();
            btnMinPercent = new System.Windows.Forms.Button();
            btnPlusPercent = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtUpperThreshold = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // lblAlarmT
            // 
            lblAlarmT.AutoSize = true;
            lblAlarmT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblAlarmT.ForeColor = System.Drawing.Color.Gainsboro;
            lblAlarmT.Location = new System.Drawing.Point(8, 4);
            lblAlarmT.Name = "lblAlarmT";
            lblAlarmT.Size = new System.Drawing.Size(59, 21);
            lblAlarmT.TabIndex = 7;
            lblAlarmT.Text = "Trigger";
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(299, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(24, 23);
            btnClose.TabIndex = 8;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOk.BackColor = System.Drawing.Color.Gainsboro;
            btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOk.Location = new System.Drawing.Point(288, 157);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(35, 23);
            btnOk.TabIndex = 9;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // txtRemarks
            // 
            txtRemarks.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            txtRemarks.Location = new System.Drawing.Point(87, 105);
            txtRemarks.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            txtRemarks.Multiline = true;
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new System.Drawing.Size(191, 76);
            txtRemarks.TabIndex = 11;
            // 
            // txtLowerThreshold
            // 
            txtLowerThreshold.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtLowerThreshold.Location = new System.Drawing.Point(87, 40);
            txtLowerThreshold.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            txtLowerThreshold.Name = "txtLowerThreshold";
            txtLowerThreshold.Size = new System.Drawing.Size(55, 23);
            txtLowerThreshold.TabIndex = 12;
            // 
            // lblThresholdT
            // 
            lblThresholdT.AutoSize = true;
            lblThresholdT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblThresholdT.ForeColor = System.Drawing.Color.Gainsboro;
            lblThresholdT.Location = new System.Drawing.Point(8, 37);
            lblThresholdT.Name = "lblThresholdT";
            lblThresholdT.Size = new System.Drawing.Size(52, 21);
            lblThresholdT.TabIndex = 13;
            lblThresholdT.Text = "Below";
            // 
            // lblRemarksT
            // 
            lblRemarksT.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblRemarksT.AutoSize = true;
            lblRemarksT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblRemarksT.ForeColor = System.Drawing.Color.Gainsboro;
            lblRemarksT.Location = new System.Drawing.Point(8, 101);
            lblRemarksT.Name = "lblRemarksT";
            lblRemarksT.Size = new System.Drawing.Size(71, 21);
            lblRemarksT.TabIndex = 14;
            lblRemarksT.Text = "Remarks";
            // 
            // lblNativeCurrT
            // 
            lblNativeCurrT.AutoSize = true;
            lblNativeCurrT.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblNativeCurrT.ForeColor = System.Drawing.Color.Gainsboro;
            lblNativeCurrT.Location = new System.Drawing.Point(86, 4);
            lblNativeCurrT.Name = "lblNativeCurrT";
            lblNativeCurrT.Size = new System.Drawing.Size(129, 21);
            lblNativeCurrT.TabIndex = 15;
            lblNativeCurrT.Text = "(Native currency)";
            // 
            // btnMinPercent
            // 
            btnMinPercent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnMinPercent.BackColor = System.Drawing.Color.Gainsboro;
            btnMinPercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMinPercent.Location = new System.Drawing.Point(165, 40);
            btnMinPercent.Name = "btnMinPercent";
            btnMinPercent.Size = new System.Drawing.Size(50, 23);
            btnMinPercent.TabIndex = 16;
            btnMinPercent.Text = "-x%";
            btnMinPercent.UseVisualStyleBackColor = false;
            btnMinPercent.Click += btnMinPercent_Click;
            // 
            // btnPlusPercent
            // 
            btnPlusPercent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnPlusPercent.BackColor = System.Drawing.Color.Gainsboro;
            btnPlusPercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPlusPercent.Location = new System.Drawing.Point(165, 72);
            btnPlusPercent.Name = "btnPlusPercent";
            btnPlusPercent.Size = new System.Drawing.Size(50, 23);
            btnPlusPercent.TabIndex = 19;
            btnPlusPercent.Text = "+x%";
            btnPlusPercent.UseVisualStyleBackColor = false;
            btnPlusPercent.Click += btnPlusPercent_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.ForeColor = System.Drawing.Color.Gainsboro;
            label1.Location = new System.Drawing.Point(8, 69);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(54, 21);
            label1.TabIndex = 18;
            label1.Text = "Above";
            // 
            // txtUpperThreshold
            // 
            txtUpperThreshold.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtUpperThreshold.Location = new System.Drawing.Point(87, 72);
            txtUpperThreshold.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            txtUpperThreshold.Name = "txtUpperThreshold";
            txtUpperThreshold.Size = new System.Drawing.Size(55, 23);
            txtUpperThreshold.TabIndex = 17;
            // 
            // frmAlarmConditionInput
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            ClientSize = new System.Drawing.Size(331, 187);
            Controls.Add(btnPlusPercent);
            Controls.Add(label1);
            Controls.Add(txtUpperThreshold);
            Controls.Add(btnMinPercent);
            Controls.Add(lblNativeCurrT);
            Controls.Add(lblRemarksT);
            Controls.Add(lblThresholdT);
            Controls.Add(txtLowerThreshold);
            Controls.Add(txtRemarks);
            Controls.Add(btnOk);
            Controls.Add(btnClose);
            Controls.Add(lblAlarmT);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            Name = "frmAlarmConditionInput";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmAlarmConditionInput";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlarmT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtLowerThreshold;
        private System.Windows.Forms.Label lblThresholdT;
        private System.Windows.Forms.Label lblRemarksT;
        private System.Windows.Forms.Label lblNativeCurrT;
        private System.Windows.Forms.Button btnMinPercent;
        private System.Windows.Forms.Button btnPlusPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUpperThreshold;
    }
}