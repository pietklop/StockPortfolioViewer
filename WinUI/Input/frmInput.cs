using System;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public partial class frmInput : Form
    {
        private readonly InputType inputType;
        public string Input { get; private set; } = null;

        public frmInput(string caption, InputType inputType)
        {
            InitializeComponent();
            lblInputT.Text = caption;
            this.inputType = inputType;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (inputType)
            {
                case InputType.String:
                    if (string.IsNullOrEmpty(txtInput.Text))
                    {
                        Invalid();
                        return;
                    }
                    break;
                case InputType.PositiveDouble:
                    txtInput.Text = txtInput.Text.Replace(",", ".");
                    if (!double.TryParse(txtInput.Text, out double value) || value < 0)
                    {
                        Invalid();
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported inputType {inputType}");
            }
            Input = txtInput.Text;

            DialogResult = DialogResult.OK;
            Close();

            void Invalid()
            {
                txtInput.Text = "Invalid";
                txtInput.SelectionStart = 0;
                txtInput.SelectionLength = txtInput.TextLength;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnOk_Click(this, EventArgs.Empty);
        }
    }
}
