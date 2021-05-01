using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public partial class frmDistributionInput : Form
    {
        private readonly List<AreaCountryInputDto> countries;
        public string MemberKey { get; private set; } = null;
        public int MemberPercentage { get; private set; }
        public AreaCountryInputDto Country { get; private set; }
        private bool countryInput = false;

        public frmDistributionInput(string caption, List<string> listMembers, int assignedPercentage, List<AreaCountryInputDto> countries)
        {
            this.countries = countries;
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

            if (countryInput)
            {
                if (cmbCountry.Text == "") return;
                Country = new AreaCountryInputDto(cmbMember.Text, cmbCountry.Text);
            }
            else
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

        private void cmbMember_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || countries == null || cmbMember.SelectedIndex < 0) return;
            cmbMember.Visible = false;
            cmbCountry.DataSource = countries.Where(c => c.Continent == cmbMember.Text).OrderBy(c => c.Country).Select(c => c.Country).ToList();
            cmbCountry.SelectedIndex = -1;
            cmbCountry.Visible = true;
            countryInput = true;
        }
    }
}
