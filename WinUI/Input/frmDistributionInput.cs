using System.Collections.Generic;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public partial class frmDistributionInput : Form
    {
        public string MemberKey { get; private set; } = null;
        public int MemberPercentage { get; private set; }

        public frmDistributionInput(string caption, List<string> listMembers, int assignedPercentage)
        {
            InitializeComponent();
            lblInputT.Text = caption;
            cmbMember.DataSource = listMembers;
            cmbMember.SelectedIndex = -1;
            numPercentage.Maximum = 100 - assignedPercentage;
            numPercentage.Value = numPercentage.Maximum;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (cmbMember.SelectedIndex < 0) return;

            MemberKey = cmbMember.Text;
            MemberPercentage = (int)numPercentage.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void numPercentage_Enter(object sender, System.EventArgs e)
        {
            numPercentage.Select();
            numPercentage.Select(0, numPercentage.Value.ToString().Length);
        }
    }
}
