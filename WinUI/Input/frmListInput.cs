using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public partial class frmListInput : Form
    {
        public int MemberIndex { get; private set; } = -1;
        
        public frmListInput(string caption, List<string> listMembers)
        {
            InitializeComponent();
            lblInputT.Text = caption;
            cmbMember.DataSource = listMembers;
            cmbMember.SelectedIndex = -1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbMember.SelectedIndex < 0) return;

            MemberIndex = cmbMember.SelectedIndex;

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
