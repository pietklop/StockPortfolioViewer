using System;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;

namespace Dashboard
{
    public partial class frmDistribution : Form
    {
        private readonly PortfolioDistributionDto distribution;
        public bool ResetRequest { get; private set; }

        public frmDistribution(PortfolioDistributionDto distribution)
        {
            this.distribution = distribution;
            InitializeComponent();
        }

        private void frmDistribution_Load(object sender, EventArgs e)
        {
            ChartHelper.ConfigPieChart(chart);
            PopulatePieChart(distribution);
        }

        private void PopulatePieChart(PortfolioDistributionDto dto) =>
            ChartHelper.PopulateChart(chart, dto.Labels, dto.Fractions);

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetRequest = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
