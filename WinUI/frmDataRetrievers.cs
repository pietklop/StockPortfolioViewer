using System;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmDataRetrievers : Form
    {
        private readonly frmMain frmMain;
        private readonly DataRetrieverService dataRetrieverService;

        public frmDataRetrievers(frmMain frmMain, DataRetrieverService dataRetrieverService)
        {
            this.frmMain = frmMain;
            this.dataRetrieverService = dataRetrieverService;
            InitializeComponent();
        }

        private void frmDataRetrievers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            var retrievers = dataRetrieverService.GetRetrievers();
            dgvDataRetrievers.DataSource = retrievers;

            // column configuration
            dgvDataRetrievers.ApplyColumnDisplayFormatAttributes();
            dgvDataRetrievers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvDataRetrievers.GetColumn(nameof(RetrieverViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDataRetrievers.SetReadOnly();
            dgvDataRetrievers.SetVisualStyling();
        }

        private void dgvDataRetrievers_SelectionChanged(object sender, EventArgs e) => dgvDataRetrievers.ClearSelection();

        private void dgvDataRetrievers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var nameColumn = dgvDataRetrievers.GetColumn(nameof(RetrieverViewModel.Name));
            var name = dgvDataRetrievers[nameColumn.Index, e.RowIndex].Value.ToString();

            Close();

            frmMain.ShowDataRetriever(name);
        }
    }
}
