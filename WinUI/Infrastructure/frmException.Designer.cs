
namespace Dashboard.Infrastructure
{
    partial class frmException
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
            btnCopyDetails = new System.Windows.Forms.Button();
            lblErrorT = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            txtMessage = new System.Windows.Forms.TextBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // btnCopyDetails
            // 
            btnCopyDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCopyDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCopyDetails.ForeColor = System.Drawing.Color.Gainsboro;
            btnCopyDetails.Location = new System.Drawing.Point(167, 175);
            btnCopyDetails.Name = "btnCopyDetails";
            btnCopyDetails.Size = new System.Drawing.Size(167, 29);
            btnCopyDetails.TabIndex = 3;
            btnCopyDetails.Text = "Copy details to clipboard";
            toolTip.SetToolTip(btnCopyDetails, "You can mail this info to Piet for investigation");
            btnCopyDetails.UseVisualStyleBackColor = true;
            btnCopyDetails.Click += btnCopyDetails_Click;
            // 
            // lblErrorT
            // 
            lblErrorT.AutoSize = true;
            lblErrorT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblErrorT.ForeColor = System.Drawing.Color.Red;
            lblErrorT.Location = new System.Drawing.Point(12, 12);
            lblErrorT.Name = "lblErrorT";
            lblErrorT.Size = new System.Drawing.Size(47, 21);
            lblErrorT.TabIndex = 4;
            lblErrorT.Text = "Error";
            toolTip.SetToolTip(lblErrorT, "Ja dat is niet best");
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(321, 1);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(24, 23);
            btnClose.TabIndex = 5;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtMessage
            // 
            txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtMessage.Location = new System.Drawing.Point(12, 48);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new System.Drawing.Size(322, 110);
            txtMessage.TabIndex = 6;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 20000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // frmException
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(346, 216);
            Controls.Add(txtMessage);
            Controls.Add(btnClose);
            Controls.Add(lblErrorT);
            Controls.Add(btnCopyDetails);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmException";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmException";
            FormClosing += frmException_FormClosing;
            FormClosed += frmException_FormClosed;
            Load += frmException_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCopyDetails;
        private System.Windows.Forms.Label lblErrorT;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ToolTip toolTip;
    }
}