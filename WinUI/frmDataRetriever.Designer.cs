
namespace Dashboard
{
    partial class frmDataRetriever
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
            this.dgvRetrieverLimitations = new System.Windows.Forms.DataGridView();
            this.dgvRetrieverDetails = new System.Windows.Forms.DataGridView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverLimitations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRetrieverLimitations
            // 
            this.dgvRetrieverLimitations.AllowUserToAddRows = false;
            this.dgvRetrieverLimitations.AllowUserToDeleteRows = false;
            this.dgvRetrieverLimitations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetrieverLimitations.Location = new System.Drawing.Point(91, 72);
            this.dgvRetrieverLimitations.Name = "dgvRetrieverLimitations";
            this.dgvRetrieverLimitations.ReadOnly = true;
            this.dgvRetrieverLimitations.RowTemplate.Height = 25;
            this.dgvRetrieverLimitations.Size = new System.Drawing.Size(327, 116);
            this.dgvRetrieverLimitations.TabIndex = 1;
            this.dgvRetrieverLimitations.TabStop = false;
            this.dgvRetrieverLimitations.SelectionChanged += new System.EventHandler(this.dgvRetrieverLimitations_SelectionChanged);
            // 
            // dgvRetrieverDetails
            // 
            this.dgvRetrieverDetails.AllowUserToAddRows = false;
            this.dgvRetrieverDetails.AllowUserToDeleteRows = false;
            this.dgvRetrieverDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetrieverDetails.Location = new System.Drawing.Point(91, 228);
            this.dgvRetrieverDetails.Name = "dgvRetrieverDetails";
            this.dgvRetrieverDetails.ReadOnly = true;
            this.dgvRetrieverDetails.RowTemplate.Height = 25;
            this.dgvRetrieverDetails.Size = new System.Drawing.Size(327, 209);
            this.dgvRetrieverDetails.TabIndex = 2;
            this.dgvRetrieverDetails.TabStop = false;
            this.dgvRetrieverDetails.SelectionChanged += new System.EventHandler(this.dgvRetrieverDetails_SelectionChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDescription.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtDescription.Location = new System.Drawing.Point(91, 490);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(534, 79);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "Description";
            // 
            // frmDataRetriever
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(800, 680);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.dgvRetrieverDetails);
            this.Controls.Add(this.dgvRetrieverLimitations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDataRetriever";
            this.Text = "frmDataRetriever";
            this.Load += new System.EventHandler(this.frmDataRetriever_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverLimitations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetrieverDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRetrieverLimitations;
        private System.Windows.Forms.DataGridView dgvRetrieverDetails;
        private System.Windows.Forms.TextBox txtDescription;
    }
}