
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
            this.btnPerformance = new System.Windows.Forms.Button();
            this.btnDividends = new System.Windows.Forms.Button();
            this.btnDataRetrieval = new System.Windows.Forms.Button();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnMainOverview = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlFormLoader = new System.Windows.Forms.Panel();
            this.metroStyleFormManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblViewName = new System.Windows.Forms.Label();
            this.lblSpv = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleFormManager)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.pnlMenu.Controls.Add(this.btnPerformance);
            this.pnlMenu.Controls.Add(this.btnDividends);
            this.pnlMenu.Controls.Add(this.btnDataRetrieval);
            this.pnlMenu.Controls.Add(this.pnlNav);
            this.pnlMenu.Controls.Add(this.btnImport);
            this.pnlMenu.Controls.Add(this.btnTransactions);
            this.pnlMenu.Controls.Add(this.btnMainOverview);
            this.pnlMenu.Controls.Add(this.pnlInfo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 30);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(186, 747);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnPerformance
            // 
            this.btnPerformance.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPerformance.FlatAppearance.BorderSize = 0;
            this.btnPerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerformance.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPerformance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnPerformance.Location = new System.Drawing.Point(0, 264);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(186, 40);
            this.btnPerformance.TabIndex = 5;
            this.btnPerformance.Text = "Performance";
            this.btnPerformance.UseVisualStyleBackColor = true;
            this.btnPerformance.Click += new System.EventHandler(this.btnPerformance_Click);
            // 
            // btnDividends
            // 
            this.btnDividends.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDividends.FlatAppearance.BorderSize = 0;
            this.btnDividends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDividends.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDividends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDividends.Location = new System.Drawing.Point(0, 224);
            this.btnDividends.Name = "btnDividends";
            this.btnDividends.Size = new System.Drawing.Size(186, 40);
            this.btnDividends.TabIndex = 4;
            this.btnDividends.Text = "Dividends";
            this.btnDividends.UseVisualStyleBackColor = true;
            this.btnDividends.Click += new System.EventHandler(this.btnDividends_Click);
            this.btnDividends.Leave += new System.EventHandler(this.btnDividends_Leave);
            // 
            // btnDataRetrieval
            // 
            this.btnDataRetrieval.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDataRetrieval.FlatAppearance.BorderSize = 0;
            this.btnDataRetrieval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataRetrieval.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDataRetrieval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnDataRetrieval.Location = new System.Drawing.Point(0, 667);
            this.btnDataRetrieval.Name = "btnDataRetrieval";
            this.btnDataRetrieval.Size = new System.Drawing.Size(186, 40);
            this.btnDataRetrieval.TabIndex = 3;
            this.btnDataRetrieval.Text = "Data retrieval";
            this.btnDataRetrieval.UseVisualStyleBackColor = true;
            this.btnDataRetrieval.Click += new System.EventHandler(this.btnDataRetrieval_Click);
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.pnlNav.Location = new System.Drawing.Point(0, 200);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(5, 100);
            this.pnlNav.TabIndex = 2;
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnImport.Location = new System.Drawing.Point(0, 707);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(186, 40);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnTransactions.Location = new System.Drawing.Point(0, 184);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(186, 40);
            this.btnTransactions.TabIndex = 1;
            this.btnTransactions.Text = "Transactions";
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            this.btnTransactions.Leave += new System.EventHandler(this.btnTransactions_Leave);
            // 
            // btnMainOverview
            // 
            this.btnMainOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMainOverview.FlatAppearance.BorderSize = 0;
            this.btnMainOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainOverview.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMainOverview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnMainOverview.Location = new System.Drawing.Point(0, 144);
            this.btnMainOverview.Name = "btnMainOverview";
            this.btnMainOverview.Size = new System.Drawing.Size(186, 40);
            this.btnMainOverview.TabIndex = 1;
            this.btnMainOverview.Text = "Main overview";
            this.btnMainOverview.UseVisualStyleBackColor = true;
            this.btnMainOverview.Click += new System.EventHandler(this.btnMainOverview_Click);
            this.btnMainOverview.Leave += new System.EventHandler(this.btnMainOverview_Leave);
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
            this.pnlFormLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFormLoader.Location = new System.Drawing.Point(186, 100);
            this.pnlFormLoader.Name = "pnlFormLoader";
            this.pnlFormLoader.Size = new System.Drawing.Size(965, 677);
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
            this.pnlTitle.Location = new System.Drawing.Point(186, 30);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(965, 70);
            this.pnlTitle.TabIndex = 1;
            // 
            // lblViewName
            // 
            this.lblViewName.AutoSize = true;
            this.lblViewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblViewName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblViewName.Location = new System.Drawing.Point(285, 16);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(188, 39);
            this.lblViewName.TabIndex = 4;
            this.lblViewName.Text = "View name";
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
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Resizable = false;
            this.Text = "SPV";
            this.pnlMenu.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDataRetrieval;
        private System.Windows.Forms.Button btnDividends;
        private System.Windows.Forms.Button btnPerformance;
        private MetroFramework.Components.MetroStyleManager metroStyleFormManager;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblViewName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSpv;
    }
}

