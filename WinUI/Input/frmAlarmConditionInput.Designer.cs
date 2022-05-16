
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
            this.lblAlarmT = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbConditionAction = new System.Windows.Forms.ComboBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.lblThresholdT = new System.Windows.Forms.Label();
            this.lblRemarksT = new System.Windows.Forms.Label();
            this.lblNativeCurrT = new System.Windows.Forms.Label();
            this.btnMin10Procent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAlarmT
            // 
            this.lblAlarmT.AutoSize = true;
            this.lblAlarmT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAlarmT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAlarmT.Location = new System.Drawing.Point(8, 4);
            this.lblAlarmT.Name = "lblAlarmT";
            this.lblAlarmT.Size = new System.Drawing.Size(101, 21);
            this.lblAlarmT.TabIndex = 7;
            this.lblAlarmT.Text = "Trigger when";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(335, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(324, 126);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(35, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbConditionAction
            // 
            this.cmbConditionAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionAction.FormattingEnabled = true;
            this.cmbConditionAction.Location = new System.Drawing.Point(123, 9);
            this.cmbConditionAction.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.cmbConditionAction.Name = "cmbConditionAction";
            this.cmbConditionAction.Size = new System.Drawing.Size(107, 23);
            this.cmbConditionAction.TabIndex = 10;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemarks.Location = new System.Drawing.Point(123, 74);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(191, 76);
            this.txtRemarks.TabIndex = 11;
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(123, 40);
            this.txtThreshold.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(55, 23);
            this.txtThreshold.TabIndex = 12;
            // 
            // lblThresholdT
            // 
            this.lblThresholdT.AutoSize = true;
            this.lblThresholdT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThresholdT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblThresholdT.Location = new System.Drawing.Point(8, 37);
            this.lblThresholdT.Name = "lblThresholdT";
            this.lblThresholdT.Size = new System.Drawing.Size(79, 21);
            this.lblThresholdT.TabIndex = 13;
            this.lblThresholdT.Text = "Threshold";
            // 
            // lblRemarksT
            // 
            this.lblRemarksT.AutoSize = true;
            this.lblRemarksT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRemarksT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblRemarksT.Location = new System.Drawing.Point(8, 70);
            this.lblRemarksT.Name = "lblRemarksT";
            this.lblRemarksT.Size = new System.Drawing.Size(71, 21);
            this.lblRemarksT.TabIndex = 14;
            this.lblRemarksT.Text = "Remarks";
            // 
            // lblNativeCurrT
            // 
            this.lblNativeCurrT.AutoSize = true;
            this.lblNativeCurrT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNativeCurrT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNativeCurrT.Location = new System.Drawing.Point(191, 40);
            this.lblNativeCurrT.Name = "lblNativeCurrT";
            this.lblNativeCurrT.Size = new System.Drawing.Size(129, 21);
            this.lblNativeCurrT.TabIndex = 15;
            this.lblNativeCurrT.Text = "(Native currency)";
            // 
            // btnMin10Procent
            // 
            this.btnMin10Procent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin10Procent.BackColor = System.Drawing.Color.Gainsboro;
            this.btnMin10Procent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin10Procent.Location = new System.Drawing.Point(244, 8);
            this.btnMin10Procent.Name = "btnMin10Procent";
            this.btnMin10Procent.Size = new System.Drawing.Size(46, 23);
            this.btnMin10Procent.TabIndex = 16;
            this.btnMin10Procent.Text = "-10%";
            this.btnMin10Procent.UseVisualStyleBackColor = false;
            this.btnMin10Procent.Click += new System.EventHandler(this.btnMin10Procent_Click);
            // 
            // frmAlarmConditionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(367, 156);
            this.Controls.Add(this.btnMin10Procent);
            this.Controls.Add(this.lblNativeCurrT);
            this.Controls.Add(this.lblRemarksT);
            this.Controls.Add(this.lblThresholdT);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.cmbConditionAction);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAlarmT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "frmAlarmConditionInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAlarmConditionInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlarmT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbConditionAction;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Label lblThresholdT;
        private System.Windows.Forms.Label lblRemarksT;
        private System.Windows.Forms.Label lblNativeCurrT;
        private System.Windows.Forms.Button btnMin10Procent;
    }
}