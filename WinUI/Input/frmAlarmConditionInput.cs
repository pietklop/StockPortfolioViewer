using System;
using System.Globalization;
using System.Windows.Forms;
using DAL.Entities;

namespace Dashboard.Input
{
    public partial class frmAlarmConditionInput : Form
    {
        private readonly double lastTransactionNativePrice;
        public AlarmCondition AlarmCondition { get; private set; }
        public double Threshold { get; private set; }
        public string Remarks { get; private set; }

        public frmAlarmConditionInput(AlarmCondition alarmCondition, double stockAlarmThreshold, string stockRemarks, double lastTransactionNativePrice)
        {
            this.lastTransactionNativePrice = lastTransactionNativePrice;
            InitializeComponent();
            cmbConditionAction.DataSource = Enum.GetValues(typeof(AlarmCondition));
            cmbConditionAction.SelectedIndex = (int)alarmCondition;
            txtThreshold.Text = stockAlarmThreshold.ToString();
            txtRemarks.Text = stockRemarks;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AlarmCondition = (AlarmCondition)cmbConditionAction.SelectedValue;
            Remarks = txtRemarks.Text;

            try
            {
                if (AlarmCondition != AlarmCondition.None)
                    Threshold = double.Parse(txtThreshold.Text.Replace(",", "."), CultureInfo.InvariantCulture);
            }
            catch (Exception)
            { return; }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnMin10Procent_Click(object sender, EventArgs e)
        {
            cmbConditionAction.SelectedIndex = (int)AlarmCondition.LowerThan;
            var digits = Math.Max(2 - (int)Math.Round(Math.Log10(lastTransactionNativePrice)), 0);
            txtThreshold.Text = $"{Math.Round(lastTransactionNativePrice * 0.9, digits)}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
