
namespace Dashboard.Input
{
    partial class frmStockSelection
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties3 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties4 = new PresentationControls.CheckBoxProperties();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbSector = new PresentationControls.CheckBoxComboBox();
            this.lblSectorsT = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblContinentsT = new System.Windows.Forms.Label();
            this.cmbContinents = new PresentationControls.CheckBoxComboBox();
            this.lblCountriesT = new System.Windows.Forms.Label();
            this.cmbCountries = new PresentationControls.CheckBoxComboBox();
            this.lblStocks = new System.Windows.Forms.Label();
            this.cmbStocks = new PresentationControls.CheckBoxComboBox();
            this.chkEtfOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(626, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 49);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbSector
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbSector.CheckBoxProperties = checkBoxProperties1;
            this.cmbSector.DisplayMemberSingleItem = "";
            this.cmbSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSector.FormattingEnabled = true;
            this.cmbSector.Location = new System.Drawing.Point(193, 87);
            this.cmbSector.Margin = new System.Windows.Forms.Padding(6);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(277, 40);
            this.cmbSector.TabIndex = 0;
            // 
            // lblSectorsT
            // 
            this.lblSectorsT.AutoSize = true;
            this.lblSectorsT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSectorsT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSectorsT.Location = new System.Drawing.Point(22, 83);
            this.lblSectorsT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSectorsT.Name = "lblSectorsT";
            this.lblSectorsT.Size = new System.Drawing.Size(124, 45);
            this.lblSectorsT.TabIndex = 7;
            this.lblSectorsT.Text = "Sectors";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(587, 341);
            this.btnOk.Margin = new System.Windows.Forms.Padding(6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 49);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblContinentsT
            // 
            this.lblContinentsT.AutoSize = true;
            this.lblContinentsT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblContinentsT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblContinentsT.Location = new System.Drawing.Point(22, 145);
            this.lblContinentsT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblContinentsT.Name = "lblContinentsT";
            this.lblContinentsT.Size = new System.Drawing.Size(174, 45);
            this.lblContinentsT.TabIndex = 10;
            this.lblContinentsT.Text = "Continents";
            // 
            // cmbContinents
            // 
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbContinents.CheckBoxProperties = checkBoxProperties2;
            this.cmbContinents.DisplayMemberSingleItem = "";
            this.cmbContinents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContinents.FormattingEnabled = true;
            this.cmbContinents.Location = new System.Drawing.Point(193, 149);
            this.cmbContinents.Margin = new System.Windows.Forms.Padding(6);
            this.cmbContinents.Name = "cmbContinents";
            this.cmbContinents.Size = new System.Drawing.Size(277, 40);
            this.cmbContinents.TabIndex = 9;
            // 
            // lblCountriesT
            // 
            this.lblCountriesT.AutoSize = true;
            this.lblCountriesT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCountriesT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblCountriesT.Location = new System.Drawing.Point(22, 207);
            this.lblCountriesT.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCountriesT.Name = "lblCountriesT";
            this.lblCountriesT.Size = new System.Drawing.Size(156, 45);
            this.lblCountriesT.TabIndex = 12;
            this.lblCountriesT.Text = "Countries";
            // 
            // cmbCountries
            // 
            checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbCountries.CheckBoxProperties = checkBoxProperties3;
            this.cmbCountries.DisplayMemberSingleItem = "";
            this.cmbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountries.FormattingEnabled = true;
            this.cmbCountries.Location = new System.Drawing.Point(193, 211);
            this.cmbCountries.Margin = new System.Windows.Forms.Padding(6);
            this.cmbCountries.Name = "cmbCountries";
            this.cmbCountries.Size = new System.Drawing.Size(277, 40);
            this.cmbCountries.TabIndex = 11;
            // 
            // lblStocks
            // 
            this.lblStocks.AutoSize = true;
            this.lblStocks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStocks.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblStocks.Location = new System.Drawing.Point(22, 269);
            this.lblStocks.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStocks.Name = "lblStocks";
            this.lblStocks.Size = new System.Drawing.Size(111, 45);
            this.lblStocks.TabIndex = 14;
            this.lblStocks.Text = "Stocks";
            // 
            // cmbStocks
            // 
            checkBoxProperties4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbStocks.CheckBoxProperties = checkBoxProperties4;
            this.cmbStocks.DisplayMemberSingleItem = "";
            this.cmbStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocks.FormattingEnabled = true;
            this.cmbStocks.Location = new System.Drawing.Point(193, 273);
            this.cmbStocks.Margin = new System.Windows.Forms.Padding(6);
            this.cmbStocks.Name = "cmbStocks";
            this.cmbStocks.Size = new System.Drawing.Size(277, 40);
            this.cmbStocks.TabIndex = 13;
            // 
            // chkEtfOnly
            // 
            this.chkEtfOnly.AutoSize = true;
            this.chkEtfOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkEtfOnly.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkEtfOnly.Location = new System.Drawing.Point(500, 269);
            this.chkEtfOnly.Name = "chkEtfOnly";
            this.chkEtfOnly.Size = new System.Drawing.Size(161, 45);
            this.chkEtfOnly.TabIndex = 15;
            this.chkEtfOnly.Text = "ETF only";
            this.chkEtfOnly.UseVisualStyleBackColor = true;
            this.chkEtfOnly.CheckedChanged += new System.EventHandler(this.chkEtfOnly_CheckedChanged);
            // 
            // frmStockSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(674, 416);
            this.Controls.Add(this.chkEtfOnly);
            this.Controls.Add(this.lblStocks);
            this.Controls.Add(this.cmbStocks);
            this.Controls.Add(this.lblCountriesT);
            this.Controls.Add(this.cmbCountries);
            this.Controls.Add(this.lblContinentsT);
            this.Controls.Add(this.cmbContinents);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblSectorsT);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmbSector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmStockSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStockSelection";
            this.Load += new System.EventHandler(this.frmStockSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private PresentationControls.CheckBoxComboBox cmbSector;
        private System.Windows.Forms.Label lblSectorsT;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblContinentsT;
        private PresentationControls.CheckBoxComboBox cmbContinents;
        private System.Windows.Forms.Label lblCountriesT;
        private PresentationControls.CheckBoxComboBox cmbCountries;
        private System.Windows.Forms.Label lblStocks;
        private PresentationControls.CheckBoxComboBox cmbStocks;
        private System.Windows.Forms.CheckBox chkEtfOnly;
    }
}