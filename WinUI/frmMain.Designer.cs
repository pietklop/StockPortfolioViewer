
namespace Dashboard
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlMenu = new System.Windows.Forms.Panel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            btnHistory = new System.Windows.Forms.Button();
            btnStockDetails = new System.Windows.Forms.Button();
            btnImport = new System.Windows.Forms.Button();
            btnMainOverview = new System.Windows.Forms.Button();
            btnBackup = new System.Windows.Forms.Button();
            btnPerformance = new System.Windows.Forms.Button();
            btnTransactions = new System.Windows.Forms.Button();
            btnDividends = new System.Windows.Forms.Button();
            btnSingleTransactions = new System.Windows.Forms.Button();
            btnSingleDividends = new System.Windows.Forms.Button();
            btnSinglePerformance = new System.Windows.Forms.Button();
            pnlInfo = new System.Windows.Forms.Panel();
            lblSpv = new System.Windows.Forms.Label();
            lblVersion = new System.Windows.Forms.Label();
            pnlFormLoader = new System.Windows.Forms.Panel();
            metroStyleFormManager = new MetroFramework.Components.MetroStyleManager(components);
            pnlTitle = new System.Windows.Forms.Panel();
            lblEuroInDollars = new System.Windows.Forms.Label();
            lblViewName = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            pnlMenu.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)metroStyleFormManager).BeginInit();
            pnlTitle.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = System.Drawing.Color.FromArgb(17, 17, 17);
            pnlMenu.Controls.Add(tableLayoutPanel1);
            pnlMenu.Controls.Add(pnlInfo);
            pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            pnlMenu.Location = new System.Drawing.Point(0, 30);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new System.Drawing.Size(186, 742);
            pnlMenu.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tableLayoutPanel1.Controls.Add(btnHistory, 0, 5);
            tableLayoutPanel1.Controls.Add(btnStockDetails, 0, 4);
            tableLayoutPanel1.Controls.Add(btnImport, 0, 8);
            tableLayoutPanel1.Controls.Add(btnMainOverview, 0, 0);
            tableLayoutPanel1.Controls.Add(btnBackup, 0, 7);
            tableLayoutPanel1.Controls.Add(btnPerformance, 0, 3);
            tableLayoutPanel1.Controls.Add(btnTransactions, 0, 1);
            tableLayoutPanel1.Controls.Add(btnDividends, 0, 2);
            tableLayoutPanel1.Controls.Add(btnSingleTransactions, 1, 1);
            tableLayoutPanel1.Controls.Add(btnSingleDividends, 1, 2);
            tableLayoutPanel1.Controls.Add(btnSinglePerformance, 1, 3);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 144);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new System.Drawing.Size(186, 598);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnHistory
            // 
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHistory.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnHistory.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnHistory.Location = new System.Drawing.Point(3, 203);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new System.Drawing.Size(152, 34);
            btnHistory.TabIndex = 12;
            btnHistory.Text = "Stock History";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // btnStockDetails
            // 
            btnStockDetails.Enabled = false;
            btnStockDetails.FlatAppearance.BorderSize = 0;
            btnStockDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStockDetails.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnStockDetails.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnStockDetails.Location = new System.Drawing.Point(3, 163);
            btnStockDetails.Name = "btnStockDetails";
            btnStockDetails.Size = new System.Drawing.Size(152, 34);
            btnStockDetails.TabIndex = 11;
            btnStockDetails.Text = "Stock details";
            btnStockDetails.UseVisualStyleBackColor = true;
            btnStockDetails.Click += btnStockDetails_Click;
            // 
            // btnImport
            // 
            btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnImport.FlatAppearance.BorderSize = 0;
            btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnImport.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnImport.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnImport.Location = new System.Drawing.Point(3, 561);
            btnImport.Name = "btnImport";
            btnImport.Size = new System.Drawing.Size(153, 34);
            btnImport.TabIndex = 8;
            btnImport.Text = "Import";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnMainOverview
            // 
            btnMainOverview.FlatAppearance.BorderSize = 0;
            btnMainOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMainOverview.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnMainOverview.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnMainOverview.Location = new System.Drawing.Point(3, 3);
            btnMainOverview.Name = "btnMainOverview";
            btnMainOverview.Size = new System.Drawing.Size(152, 34);
            btnMainOverview.TabIndex = 1;
            btnMainOverview.Text = "Main overview";
            btnMainOverview.UseVisualStyleBackColor = true;
            btnMainOverview.Click += btnMainOverview_Click;
            // 
            // btnBackup
            // 
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBackup.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnBackup.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnBackup.Location = new System.Drawing.Point(3, 521);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new System.Drawing.Size(152, 34);
            btnBackup.TabIndex = 3;
            btnBackup.Text = "Backup";
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.Click += btnBackup_Click;
            // 
            // btnPerformance
            // 
            btnPerformance.FlatAppearance.BorderSize = 0;
            btnPerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPerformance.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnPerformance.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnPerformance.Location = new System.Drawing.Point(3, 123);
            btnPerformance.Name = "btnPerformance";
            btnPerformance.Size = new System.Drawing.Size(152, 34);
            btnPerformance.TabIndex = 5;
            btnPerformance.Text = "Performance";
            btnPerformance.UseVisualStyleBackColor = true;
            btnPerformance.Click += btnPerformance_Click;
            // 
            // btnTransactions
            // 
            btnTransactions.FlatAppearance.BorderSize = 0;
            btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTransactions.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnTransactions.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnTransactions.Location = new System.Drawing.Point(3, 43);
            btnTransactions.Name = "btnTransactions";
            btnTransactions.Size = new System.Drawing.Size(151, 34);
            btnTransactions.TabIndex = 1;
            btnTransactions.Text = "Transactions";
            btnTransactions.UseVisualStyleBackColor = true;
            btnTransactions.Click += btnTransactions_Click;
            // 
            // btnDividends
            // 
            btnDividends.FlatAppearance.BorderSize = 0;
            btnDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDividends.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnDividends.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnDividends.Location = new System.Drawing.Point(3, 83);
            btnDividends.Name = "btnDividends";
            btnDividends.Size = new System.Drawing.Size(152, 34);
            btnDividends.TabIndex = 4;
            btnDividends.Text = "Dividends";
            btnDividends.UseVisualStyleBackColor = true;
            btnDividends.Click += btnDividends_Click;
            // 
            // btnSingleTransactions
            // 
            btnSingleTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            btnSingleTransactions.FlatAppearance.BorderSize = 0;
            btnSingleTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSingleTransactions.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSingleTransactions.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnSingleTransactions.Location = new System.Drawing.Point(162, 43);
            btnSingleTransactions.Name = "btnSingleTransactions";
            btnSingleTransactions.Size = new System.Drawing.Size(21, 34);
            btnSingleTransactions.TabIndex = 7;
            btnSingleTransactions.Text = "1";
            btnSingleTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnSingleTransactions.UseVisualStyleBackColor = true;
            btnSingleTransactions.Visible = false;
            btnSingleTransactions.Click += btnSingleTransactions_Click;
            // 
            // btnSingleDividends
            // 
            btnSingleDividends.Dock = System.Windows.Forms.DockStyle.Fill;
            btnSingleDividends.FlatAppearance.BorderSize = 0;
            btnSingleDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSingleDividends.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSingleDividends.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnSingleDividends.Location = new System.Drawing.Point(162, 83);
            btnSingleDividends.Name = "btnSingleDividends";
            btnSingleDividends.Size = new System.Drawing.Size(21, 34);
            btnSingleDividends.TabIndex = 9;
            btnSingleDividends.Text = "1";
            btnSingleDividends.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnSingleDividends.UseVisualStyleBackColor = true;
            btnSingleDividends.Visible = false;
            btnSingleDividends.Click += btnSingleDividends_Click;
            // 
            // btnSinglePerformance
            // 
            btnSinglePerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            btnSinglePerformance.FlatAppearance.BorderSize = 0;
            btnSinglePerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSinglePerformance.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSinglePerformance.ForeColor = System.Drawing.Color.FromArgb(0, 126, 249);
            btnSinglePerformance.Location = new System.Drawing.Point(162, 123);
            btnSinglePerformance.Name = "btnSinglePerformance";
            btnSinglePerformance.Size = new System.Drawing.Size(21, 34);
            btnSinglePerformance.TabIndex = 10;
            btnSinglePerformance.Text = "1";
            btnSinglePerformance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnSinglePerformance.UseVisualStyleBackColor = true;
            btnSinglePerformance.Visible = false;
            btnSinglePerformance.Click += btnSinglePerformance_Click;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(lblSpv);
            pnlInfo.Controls.Add(lblVersion);
            pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            pnlInfo.Location = new System.Drawing.Point(0, 0);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new System.Drawing.Size(186, 144);
            pnlInfo.TabIndex = 0;
            pnlInfo.DoubleClick += pnlInfo_DoubleClick;
            // 
            // lblSpv
            // 
            lblSpv.AutoSize = true;
            lblSpv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSpv.ForeColor = System.Drawing.Color.Gainsboro;
            lblSpv.Location = new System.Drawing.Point(3, 0);
            lblSpv.Name = "lblSpv";
            lblSpv.Size = new System.Drawing.Size(44, 20);
            lblSpv.TabIndex = 6;
            lblSpv.Text = "SPV";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            lblVersion.Location = new System.Drawing.Point(3, 20);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(53, 13);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "version ...";
            // 
            // pnlFormLoader
            // 
            pnlFormLoader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlFormLoader.Location = new System.Drawing.Point(186, 100);
            pnlFormLoader.Name = "pnlFormLoader";
            pnlFormLoader.Size = new System.Drawing.Size(960, 671);
            pnlFormLoader.TabIndex = 2;
            // 
            // metroStyleFormManager
            // 
            metroStyleFormManager.Owner = this;
            metroStyleFormManager.Style = MetroFramework.MetroColorStyle.Black;
            metroStyleFormManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pnlTitle
            // 
            pnlTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlTitle.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
            pnlTitle.Controls.Add(lblEuroInDollars);
            pnlTitle.Controls.Add(lblViewName);
            pnlTitle.Location = new System.Drawing.Point(186, 30);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new System.Drawing.Size(960, 70);
            pnlTitle.TabIndex = 1;
            // 
            // lblEuroInDollars
            // 
            lblEuroInDollars.AutoSize = true;
            lblEuroInDollars.ForeColor = System.Drawing.Color.Gainsboro;
            lblEuroInDollars.Location = new System.Drawing.Point(6, 52);
            lblEuroInDollars.Name = "lblEuroInDollars";
            lblEuroInDollars.Size = new System.Drawing.Size(40, 15);
            lblEuroInDollars.TabIndex = 5;
            lblEuroInDollars.Text = "Euro $";
            // 
            // lblViewName
            // 
            lblViewName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblViewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblViewName.ForeColor = System.Drawing.Color.Gainsboro;
            lblViewName.Location = new System.Drawing.Point(285, 16);
            lblViewName.Name = "lblViewName";
            lblViewName.Size = new System.Drawing.Size(188, 39);
            lblViewName.TabIndex = 4;
            lblViewName.Text = "View name";
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 20000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            ClientSize = new System.Drawing.Size(1151, 777);
            Controls.Add(pnlTitle);
            Controls.Add(pnlFormLoader);
            Controls.Add(pnlMenu);
            DisplayHeader = false;
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(1);
            Name = "frmMain";
            Padding = new System.Windows.Forms.Padding(0, 30, 5, 5);
            Text = "SPV";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            pnlMenu.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)metroStyleFormManager).EndInit();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnMainOverview;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Panel pnlFormLoader;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnDividends;
        private System.Windows.Forms.Button btnPerformance;
        private MetroFramework.Components.MetroStyleManager metroStyleFormManager;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblViewName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSpv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSingleTransactions;
        private System.Windows.Forms.Button btnSingleDividends;
        private System.Windows.Forms.Button btnSinglePerformance;
        private System.Windows.Forms.Button btnStockDetails;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Label lblEuroInDollars;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

