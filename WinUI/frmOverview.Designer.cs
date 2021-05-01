
namespace Dashboard
{
    partial class frmOverview
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            this.btnDistributionCurrency = new System.Windows.Forms.Button();
            this.btnDistributionArea = new System.Windows.Forms.Button();
            this.btnDistributionSector = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.BackImageTransparentColor = System.Drawing.Color.Red;
            this.chart.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(700, 280);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart";
            // 
            // dgvStockList
            // 
            this.dgvStockList.AllowUserToAddRows = false;
            this.dgvStockList.AllowUserToDeleteRows = false;
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Location = new System.Drawing.Point(86, 296);
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.ReadOnly = true;
            this.dgvStockList.RowTemplate.Height = 25;
            this.dgvStockList.Size = new System.Drawing.Size(741, 305);
            this.dgvStockList.TabIndex = 0;
            this.dgvStockList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellClick);
            this.dgvStockList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStockList_CellFormatting);
            this.dgvStockList.SelectionChanged += new System.EventHandler(this.dgvStockList_SelectionChanged);
            // 
            // btnDistributionCurrency
            // 
            this.btnDistributionCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnDistributionCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDistributionCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDistributionCurrency.Location = new System.Drawing.Point(731, 34);
            this.btnDistributionCurrency.Name = "btnDistributionCurrency";
            this.btnDistributionCurrency.Size = new System.Drawing.Size(96, 28);
            this.btnDistributionCurrency.TabIndex = 1;
            this.btnDistributionCurrency.Text = "Currency";
            this.btnDistributionCurrency.UseVisualStyleBackColor = false;
            this.btnDistributionCurrency.Click += new System.EventHandler(this.btnDistributionCurrency_Click);
            // 
            // btnDistributionArea
            // 
            this.btnDistributionArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnDistributionArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDistributionArea.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDistributionArea.Location = new System.Drawing.Point(731, 0);
            this.btnDistributionArea.Name = "btnDistributionArea";
            this.btnDistributionArea.Size = new System.Drawing.Size(96, 28);
            this.btnDistributionArea.TabIndex = 2;
            this.btnDistributionArea.Text = "Area";
            this.btnDistributionArea.UseVisualStyleBackColor = false;
            this.btnDistributionArea.Click += new System.EventHandler(this.btnDistributionArea_Click);
            // 
            // btnDistributionSector
            // 
            this.btnDistributionSector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnDistributionSector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDistributionSector.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDistributionSector.Location = new System.Drawing.Point(731, 68);
            this.btnDistributionSector.Name = "btnDistributionSector";
            this.btnDistributionSector.Size = new System.Drawing.Size(96, 28);
            this.btnDistributionSector.TabIndex = 3;
            this.btnDistributionSector.Text = "Sector";
            this.btnDistributionSector.UseVisualStyleBackColor = false;
            this.btnDistributionSector.Click += new System.EventHandler(this.btnDistributionSector_Click);
            // 
            // frmOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(933, 677);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.btnDistributionSector);
            this.Controls.Add(this.btnDistributionArea);
            this.Controls.Add(this.btnDistributionCurrency);
            this.Controls.Add(this.dgvStockList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOverview";
            this.Text = "frmOverview";
            this.Load += new System.EventHandler(this.frmOverview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.Button btnDistributionCurrency;
        private System.Windows.Forms.Button btnDistributionArea;
        private System.Windows.Forms.Button btnDistributionSector;
    }
}