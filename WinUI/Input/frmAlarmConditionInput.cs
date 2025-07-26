using System;
using System.Globalization;
using System.Windows.Forms;
using Core;

namespace Dashboard.Input
{
    public partial class frmAlarmConditionInput : Form
    {
        private const float minPercent = 10;
        private const float plusPercent = 10;
        private readonly double currentNativeStockPrice;
        public double? LowerThreshold { get; private set; }
        public double? UpperThreshold { get; private set; }
        public string Remarks { get; private set; }

        public frmAlarmConditionInput(double? stockAlarmLowerThreshold, double? stockAlarmUpperThreshold, string stockRemarks, double currentNativeStockPrice)
        {
            this.currentNativeStockPrice = currentNativeStockPrice;
            InitializeComponent();
            txtLowerThreshold.Text = stockAlarmLowerThreshold.ToString();
            txtUpperThreshold.Text = stockAlarmUpperThreshold.ToString();
            txtRemarks.Text = stockRemarks;
            btnMinPercent.Text = $"-{minPercent:N0}%";
            btnPlusPercent.Text = $"+{minPercent:N0}%";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Remarks = txtRemarks.Text;

            try
            {
                LowerThreshold = txtLowerThreshold.Text.HasValue() ? double.Parse(txtLowerThreshold.Text.Replace(",", "."), CultureInfo.InvariantCulture): null;
                UpperThreshold = txtUpperThreshold.Text.HasValue() ? double.Parse(txtUpperThreshold.Text.Replace(",", "."), CultureInfo.InvariantCulture): null;
            }
            catch (Exception)
            { return; }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnMinPercent_Click(object sender, EventArgs e)
        {
            var digits = Math.Max(2 - (int)Math.Round(Math.Log10(currentNativeStockPrice)), 0);
            txtLowerThreshold.Text = $"{Math.Round(currentNativeStockPrice * (1 - minPercent / 100), digits)}";
        }

        private void btnPlusPercent_Click(object sender, EventArgs e)
        {
            var digits = Math.Max(2 - (int)Math.Round(Math.Log10(currentNativeStockPrice)), 0);
            txtUpperThreshold.Text = $"{Math.Round(currentNativeStockPrice * (1 + plusPercent / 100), digits)}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
