using System;
using System.Globalization;
using System.Windows.Forms;
using DAL.Entities;

namespace Dashboard.Input
{
    public partial class frmAlarmConditionInput : Form
    {
        public AlarmCondition AlarmCondition { get; private set; }
        public double Threshold { get; private set; }
        public string Remarks { get; private set; }

        public frmAlarmConditionInput(AlarmCondition alarmCondition, double stockAlarmThreshold, string stockRemarks)
        {
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
