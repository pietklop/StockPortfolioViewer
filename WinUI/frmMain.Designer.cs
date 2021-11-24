
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
            this.btnImport = new System.Windows.Forms.Button();
            this.btnMainOverview = new System.Windows.Forms.Button();
            this.btnDataRetrieval = new System.Windows.Forms.Button();
            this.btnPerformance = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnDividends = new System.Windows.Forms.Button();
            this.btnSingleTransactions = new System.Windows.Forms.Button();
            this.btnSingleDividends = new System.Windows.Forms.Button();
            this.btnSinglePerformance = new System.Windows.Forms.Button();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblSpv = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlFormLoader = new System.Windows.Forms.Panel();
            this.metroStyleFormManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblViewName = new System.Windows.Forms.Label();
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
            this.pnlMenu.Controls.Add(this.pnlNav);
            this.pnlMenu.Controls.Add(this.pnlInfo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 64);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(6);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(345, 1594);
            this.pnlMenu.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnImport, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnMainOverview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDataRetrieval, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnPerformance, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnTransactions, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDividends, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSingleTransactions, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSingleDividends, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSinglePerformance, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 307);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 1287);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnImport.Location = new System.Drawing.Point(6, 1208);
            this.btnImport.Margin = new System.Windows.Forms.Padding(6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(283, 73);
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
            this.btnMainOverview.Location = new System.Drawing.Point(6, 6);
            this.btnMainOverview.Margin = new System.Windows.Forms.Padding(6);
            this.btnMainOverview.Name = "btnMainOverview";
            this.btnMainOverview.Size = new System.Drawing.Size(283, 73);
            this.btnMainOverview.TabIndex = 1;
            this.btnMainOverview.Text = "Main overview";
            this.btnMainOverview.UseVisualStyleBackColor = true;
            this.btnMainOverview.Click += new System.EventHandler(this.btnMainOverview_Click);
            this.btnMainOverview.Leave += new System.EventHandler(this.btnMainOverview_Leave);
            // 
            // btnDataRetrieval
            // 
            this.btnDataRetrieval.FlatAppearance.BorderSize = 0;
            this.btnDataRetrieval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataRetrieval.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDataRetrieval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDataRetrieval.Location = new System.Drawing.Point(6, 1123);
            this.btnDataRetrieval.Margin = new System.Windows.Forms.Padding(6);
            this.btnDataRetrieval.Name = "btnDataRetrieval";
            this.btnDataRetrieval.Size = new System.Drawing.Size(283, 73);
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
            this.btnPerformance.Location = new System.Drawing.Point(6, 261);
            this.btnPerformance.Margin = new System.Windows.Forms.Padding(6);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(283, 73);
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
            this.btnTransactions.Location = new System.Drawing.Point(6, 91);
            this.btnTransactions.Margin = new System.Windows.Forms.Padding(6);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(280, 73);
            this.btnTransactions.TabIndex = 1;
            this.btnTransactions.Text = "Transactions";
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            this.btnTransactions.Leave += new System.EventHandler(this.btnTransactions_Leave);
            // 
            // btnDividends
            // 
            this.btnDividends.FlatAppearance.BorderSize = 0;
            this.btnDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDividends.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDividends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDividends.Location = new System.Drawing.Point(6, 176);
            this.btnDividends.Margin = new System.Windows.Forms.Padding(6);
            this.btnDividends.Name = "btnDividends";
            this.btnDividends.Size = new System.Drawing.Size(283, 73);
            this.btnDividends.TabIndex = 4;
            this.btnDividends.Text = "Dividends";
            this.btnDividends.UseVisualStyleBackColor = true;
            this.btnDividends.Click += new System.EventHandler(this.btnDividends_Click);
            this.btnDividends.Leave += new System.EventHandler(this.btnDividends_Leave);
            // 
            // btnSingleTransactions
            // 
            this.btnSingleTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSingleTransactions.FlatAppearance.BorderSize = 0;
            this.btnSingleTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSingleTransactions.Font = new System.Drawing.Font("Nirmala UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSingleTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSingleTransactions.Location = new System.Drawing.Point(301, 91);
            this.btnSingleTransactions.Margin = new System.Windows.Forms.Padding(6);
            this.btnSingleTransactions.Name = "btnSingleTransactions";
            this.btnSingleTransactions.Size = new System.Drawing.Size(38, 73);
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
            this.btnSingleDividends.Location = new System.Drawing.Point(301, 176);
            this.btnSingleDividends.Margin = new System.Windows.Forms.Padding(6);
            this.btnSingleDividends.Name = "btnSingleDividends";
            this.btnSingleDividends.Size = new System.Drawing.Size(38, 73);
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
            this.btnSinglePerformance.Location = new System.Drawing.Point(301, 261);
            this.btnSinglePerformance.Margin = new System.Windows.Forms.Padding(6);
            this.btnSinglePerformance.Name = "btnSinglePerformance";
            this.btnSinglePerformance.Size = new System.Drawing.Size(38, 73);
            this.btnSinglePerformance.TabIndex = 10;
            this.btnSinglePerformance.Text = "1";
            this.btnSinglePerformance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSinglePerformance.UseVisualStyleBackColor = true;
            this.btnSinglePerformance.Visible = false;
            this.btnSinglePerformance.Click += new System.EventHandler(this.btnSinglePerformance_Click);
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.pnlNav.Location = new System.Drawing.Point(0, 427);
            this.pnlNav.Margin = new System.Windows.Forms.Padding(6);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(9, 213);
            this.pnlNav.TabIndex = 2;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblSpv);
            this.pnlInfo.Controls.Add(this.lblVersion);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(6);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(345, 307);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblSpv
            // 
            this.lblSpv.AutoSize = true;
            this.lblSpv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSpv.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSpv.Location = new System.Drawing.Point(6, 0);
            this.lblSpv.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSpv.Name = "lblSpv";
            this.lblSpv.Size = new System.Drawing.Size(84, 37);
            this.lblSpv.TabIndex = 6;
            this.lblSpv.Text = "SPV";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(6, 43);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(106, 26);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "version ...";
            // 
            // pnlFormLoader
            // 
            this.pnlFormLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFormLoader.Location = new System.Drawing.Point(345, 213);
            this.pnlFormLoader.Margin = new System.Windows.Forms.Padding(6);
            this.pnlFormLoader.Name = "pnlFormLoader";
            this.pnlFormLoader.Size = new System.Drawing.Size(1792, 1444);
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
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.pnlTitle.Controls.Add(this.lblViewName);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(345, 64);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(6);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1793, 149);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblViewName
            // 
            this.lblViewName.AutoSize = true;
            this.lblViewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblViewName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblViewName.Location = new System.Drawing.Point(529, 34);
            this.lblViewName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(381, 79);
            this.lblViewName.TabIndex = 4;
            this.lblViewName.Text = "View name";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(2138, 1658);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlFormLoader);
            this.Controls.Add(this.pnlMenu);
            this.DisplayHeader = false;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(0, 64, 0, 0);
            this.Resizable = false;
            this.Text = "SPV";
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
        private System.Windows.Forms.Panel pnlNav;
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
    }
}

