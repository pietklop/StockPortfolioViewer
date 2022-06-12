
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
            this.components = new System.ComponentModel.Container();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnStockDetails = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnMainOverview = new System.Windows.Forms.Button();
            this.btnDataRetrieval = new System.Windows.Forms.Button();
            this.btnPerformance = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnDividends = new System.Windows.Forms.Button();
            this.btnSingleTransactions = new System.Windows.Forms.Button();
            this.btnSingleDividends = new System.Windows.Forms.Button();
            this.btnSinglePerformance = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblSpv = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlFormLoader = new System.Windows.Forms.Panel();
            this.metroStyleFormManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblViewName = new System.Windows.Forms.Label();
            this.lblEuroInDollars = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleFormManager)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.pnlMenu.Controls.Add(this.tableLayoutPanel1);
            this.pnlMenu.Controls.Add(this.pnlInfo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 30);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(186, 742);
            this.pnlMenu.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Controls.Add(this.btnHistory, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnStockDetails, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnImport, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnMainOverview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDataRetrieval, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnPerformance, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnTransactions, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDividends, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSingleTransactions, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSingleDividends, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSinglePerformance, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 144);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(186, 598);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnHistory
            // 
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnHistory.Location = new System.Drawing.Point(3, 203);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(152, 34);
            this.btnHistory.TabIndex = 12;
            this.btnHistory.Text = "Stock History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnStockDetails
            // 
            this.btnStockDetails.Enabled = false;
            this.btnStockDetails.FlatAppearance.BorderSize = 0;
            this.btnStockDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockDetails.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStockDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnStockDetails.Location = new System.Drawing.Point(3, 163);
            this.btnStockDetails.Name = "btnStockDetails";
            this.btnStockDetails.Size = new System.Drawing.Size(152, 34);
            this.btnStockDetails.TabIndex = 11;
            this.btnStockDetails.Text = "Stock details";
            this.btnStockDetails.UseVisualStyleBackColor = true;
            this.btnStockDetails.Click += new System.EventHandler(this.btnStockDetails_Click);
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnImport.Location = new System.Drawing.Point(3, 561);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(153, 34);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnMainOverview
            // 
            this.btnMainOverview.FlatAppearance.BorderSize = 0;
            this.btnMainOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainOverview.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMainOverview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnMainOverview.Location = new System.Drawing.Point(3, 3);
            this.btnMainOverview.Name = "btnMainOverview";
            this.btnMainOverview.Size = new System.Drawing.Size(152, 34);
            this.btnMainOverview.TabIndex = 1;
            this.btnMainOverview.Text = "Main overview";
            this.btnMainOverview.UseVisualStyleBackColor = true;
            this.btnMainOverview.Click += new System.EventHandler(this.btnMainOverview_Click);
            // 
            // btnDataRetrieval
            // 
            this.btnDataRetrieval.FlatAppearance.BorderSize = 0;
            this.btnDataRetrieval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataRetrieval.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDataRetrieval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDataRetrieval.Location = new System.Drawing.Point(3, 521);
            this.btnDataRetrieval.Name = "btnDataRetrieval";
            this.btnDataRetrieval.Size = new System.Drawing.Size(152, 34);
            this.btnDataRetrieval.TabIndex = 3;
            this.btnDataRetrieval.Text = "Data retrieval";
            this.btnDataRetrieval.UseVisualStyleBackColor = true;
            this.btnDataRetrieval.Click += new System.EventHandler(this.btnDataRetrieval_Click);
            // 
            // btnPerformance
            // 
            this.btnPerformance.FlatAppearance.BorderSize = 0;
            this.btnPerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerformance.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPerformance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnPerformance.Location = new System.Drawing.Point(3, 123);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(152, 34);
            this.btnPerformance.TabIndex = 5;
            this.btnPerformance.Text = "Performance";
            this.btnPerformance.UseVisualStyleBackColor = true;
            this.btnPerformance.Click += new System.EventHandler(this.btnPerformance_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnTransactions.Location = new System.Drawing.Point(3, 43);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(151, 34);
            this.btnTransactions.TabIndex = 1;
            this.btnTransactions.Text = "Transactions";
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // btnDividends
            // 
            this.btnDividends.FlatAppearance.BorderSize = 0;
            this.btnDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDividends.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDividends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDividends.Location = new System.Drawing.Point(3, 83);
            this.btnDividends.Name = "btnDividends";
            this.btnDividends.Size = new System.Drawing.Size(152, 34);
            this.btnDividends.TabIndex = 4;
            this.btnDividends.Text = "Dividends";
            this.btnDividends.UseVisualStyleBackColor = true;
            this.btnDividends.Click += new System.EventHandler(this.btnDividends_Click);
            // 
            // btnSingleTransactions
            // 
            this.btnSingleTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSingleTransactions.FlatAppearance.BorderSize = 0;
            this.btnSingleTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSingleTransactions.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSingleTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSingleTransactions.Location = new System.Drawing.Point(162, 43);
            this.btnSingleTransactions.Name = "btnSingleTransactions";
            this.btnSingleTransactions.Size = new System.Drawing.Size(21, 34);
            this.btnSingleTransactions.TabIndex = 7;
            this.btnSingleTransactions.Text = "1";
            this.btnSingleTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSingleTransactions.UseVisualStyleBackColor = true;
            this.btnSingleTransactions.Visible = false;
            this.btnSingleTransactions.Click += new System.EventHandler(this.btnSingleTransactions_Click);
            // 
            // btnSingleDividends
            // 
            this.btnSingleDividends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSingleDividends.FlatAppearance.BorderSize = 0;
            this.btnSingleDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSingleDividends.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSingleDividends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSingleDividends.Location = new System.Drawing.Point(162, 83);
            this.btnSingleDividends.Name = "btnSingleDividends";
            this.btnSingleDividends.Size = new System.Drawing.Size(21, 34);
            this.btnSingleDividends.TabIndex = 9;
            this.btnSingleDividends.Text = "1";
            this.btnSingleDividends.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSingleDividends.UseVisualStyleBackColor = true;
            this.btnSingleDividends.Visible = false;
            this.btnSingleDividends.Click += new System.EventHandler(this.btnSingleDividends_Click);
            // 
            // btnSinglePerformance
            // 
            this.btnSinglePerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSinglePerformance.FlatAppearance.BorderSize = 0;
            this.btnSinglePerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinglePerformance.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSinglePerformance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSinglePerformance.Location = new System.Drawing.Point(162, 123);
            this.btnSinglePerformance.Name = "btnSinglePerformance";
            this.btnSinglePerformance.Size = new System.Drawing.Size(21, 34);
            this.btnSinglePerformance.TabIndex = 10;
            this.btnSinglePerformance.Text = "1";
            this.btnSinglePerformance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSinglePerformance.UseVisualStyleBackColor = true;
            this.btnSinglePerformance.Visible = false;
            this.btnSinglePerformance.Click += new System.EventHandler(this.btnSinglePerformance_Click);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblSpv);
            this.pnlInfo.Controls.Add(this.lblVersion);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(186, 144);
            this.pnlInfo.TabIndex = 0;
            this.pnlInfo.DoubleClick += new System.EventHandler(this.pnlInfo_DoubleClick);
            // 
            // lblSpv
            // 
            this.lblSpv.AutoSize = true;
            this.lblSpv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSpv.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSpv.Location = new System.Drawing.Point(3, 0);
            this.lblSpv.Name = "lblSpv";
            this.lblSpv.Size = new System.Drawing.Size(44, 20);
            this.lblSpv.TabIndex = 6;
            this.lblSpv.Text = "SPV";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(3, 20);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(53, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "version ...";
            // 
            // pnlFormLoader
            // 
            this.pnlFormLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFormLoader.Location = new System.Drawing.Point(186, 100);
            this.pnlFormLoader.Name = "pnlFormLoader";
            this.pnlFormLoader.Size = new System.Drawing.Size(960, 671);
            this.pnlFormLoader.TabIndex = 2;
            // 
            // metroStyleFormManager
            // 
            this.metroStyleFormManager.Owner = this;
            this.metroStyleFormManager.Style = MetroFramework.MetroColorStyle.Black;
            this.metroStyleFormManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.pnlTitle.Controls.Add(this.lblEuroInDollars);
            this.pnlTitle.Controls.Add(this.lblViewName);
            this.pnlTitle.Location = new System.Drawing.Point(186, 30);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(960, 70);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblViewName
            // 
            this.lblViewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblViewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblViewName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblViewName.Location = new System.Drawing.Point(285, 16);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(188, 39);
            this.lblViewName.TabIndex = 4;
            this.lblViewName.Text = "View name";
            // 
            // lblEuroInDollars
            // 
            this.lblEuroInDollars.AutoSize = true;
            this.lblEuroInDollars.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblEuroInDollars.Location = new System.Drawing.Point(6, 52);
            this.lblEuroInDollars.Name = "lblEuroInDollars";
            this.lblEuroInDollars.Size = new System.Drawing.Size(38, 15);
            this.lblEuroInDollars.TabIndex = 5;
            this.lblEuroInDollars.Text = "Euro $";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1151, 777);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlFormLoader);
            this.Controls.Add(this.pnlMenu);
            this.DisplayHeader = false;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 5, 5);
            this.Text = "SPV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleFormManager)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnMainOverview;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Panel pnlFormLoader;
        private System.Windows.Forms.Button btnDataRetrieval;
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
    }
}

