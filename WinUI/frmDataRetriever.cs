using System;
using System.Linq;
using System.Windows.Forms;
using DAL;
using Dashboard.Helpers;
using log4net;
using Messages.UI;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmDataRetriever : Form
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly string dataRetrieverName;
        private readonly DataRetrieverService dataRetrieverService;

        public frmDataRetriever(ILog log, StockDbContext db, string dataRetrieverName, DataRetrieverService dataRetrieverService)
        {
            this.log = log;
            this.db = db;
            this.dataRetrieverName = dataRetrieverName;
            this.dataRetrieverService = dataRetrieverService;
            InitializeComponent();
        }

        private void frmDataRetriever_Load(object sender, EventArgs e)
        {
            PopulateLimitationsGrid();
            PopulateStockGrid();
            PopulateDescription();
        }

        private void PopulateDescription() => txtDescription.Text = db.DataRetrievers.Single(d => d.Name == dataRetrieverName).Description;

        private void PopulateLimitationsGrid()
        {
            var stockList = dataRetrieverService.GetRetrieverLimitations(dataRetrieverName);
            dgvRetrieverLimitations.DataSource = stockList;

            // column configuration
            dgvRetrieverLimitations.ApplyColumnDisplayFormatAttributes();
            dgvRetrieverLimitations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var timespanColumn = dgvRetrieverLimitations.GetColumn(nameof(RetrieverLimitationViewModel.TimespanType));
            timespanColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvRetrieverLimitations.SetReadOnly();
            dgvRetrieverLimitations.SetVisualStyling();
        }

        private void PopulateStockGrid()
        {
            var stockList = dataRetrieverService.GetDetails(dataRetrieverName);
            dgvRetrieverDetails.DataSource = stockList;

            // column configuration
            dgvRetrieverDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvRetrieverDetails.GetColumn(nameof(PropertyViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var underlineColumn = dgvRetrieverDetails.GetColumn(nameof(PropertyViewModel.UnderlineRow));
            underlineColumn.Visible = false;

            dgvRetrieverDetails.SetReadOnly();
            dgvRetrieverDetails.SetVisualStyling();
        }

        private void dgvRetrieverLimitations_SelectionChanged(object sender, EventArgs e) => dgvRetrieverLimitations.ClearSelection();

        private void dgvRetrieverDetails_SelectionChanged(object sender, EventArgs e) => dgvRetrieverDetails.ClearSelection();
    }
}
