
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
            this.SuspendLayout();
            // 
            // lblAlarmT
            // 
            this.lblAlarmT.AutoSize = true;
            this.lblAlarmT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAlarmT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAlarmT.Location = new System.Drawing.Point(15, 9);
            this.lblAlarmT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAlarmT.Name = "lblAlarmT";
            this.lblAlarmT.Size = new System.Drawing.Size(204, 45);
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
            this.btnClose.Location = new System.Drawing.Point(622, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 49);
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
            this.btnOk.Location = new System.Drawing.Point(602, 269);
            this.btnOk.Margin = new System.Windows.Forms.Padding(6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 49);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbConditionAction
            // 
            this.cmbConditionAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionAction.FormattingEnabled = true;
            this.cmbConditionAction.Location = new System.Drawing.Point(228, 20);
            this.cmbConditionAction.Name = "cmbConditionAction";
            this.cmbConditionAction.Size = new System.Drawing.Size(196, 40);
            this.cmbConditionAction.TabIndex = 10;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemarks.Location = new System.Drawing.Point(228, 157);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(352, 157);
            this.txtRemarks.TabIndex = 11;
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(228, 85);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(98, 39);
            this.txtThreshold.TabIndex = 12;
            // 
            // lblThresholdT
            // 
            this.lblThresholdT.AutoSize = true;
            this.lblThresholdT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThresholdT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblThresholdT.Location = new System.Drawing.Point(15, 78);
            this.lblThresholdT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblThresholdT.Name = "lblThresholdT";
            this.lblThresholdT.Size = new System.Drawing.Size(161, 45);
            this.lblThresholdT.TabIndex = 13;
            this.lblThresholdT.Text = "Threshold";
            // 
            // lblRemarksT
            // 
            this.lblRemarksT.AutoSize = true;
            this.lblRemarksT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRemarksT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblRemarksT.Location = new System.Drawing.Point(15, 150);
            this.lblRemarksT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRemarksT.Name = "lblRemarksT";
            this.lblRemarksT.Size = new System.Drawing.Size(140, 45);
            this.lblRemarksT.TabIndex = 14;
            this.lblRemarksT.Text = "Remarks";
            // 
            // lblNativeCurrT
            // 
            this.lblNativeCurrT.AutoSize = true;
            this.lblNativeCurrT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNativeCurrT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNativeCurrT.Location = new System.Drawing.Point(354, 85);
            this.lblNativeCurrT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNativeCurrT.Name = "lblNativeCurrT";
            this.lblNativeCurrT.Size = new System.Drawing.Size(260, 45);
            this.lblNativeCurrT.TabIndex = 15;
            this.lblNativeCurrT.Text = "(Native currency)";
            // 
            // frmAlarmConditionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(682, 333);
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
    }
}