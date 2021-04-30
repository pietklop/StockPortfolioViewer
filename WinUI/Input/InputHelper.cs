using System.Globalization;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public static class InputHelper
    {
        public static bool GetConfirmation(Form ownerForm, string caption)
        {
            using (frmInput form = new frmInput(caption, InputType.Confirmation))
                return form.ShowDialog(ownerForm) == DialogResult.OK;
        }

        public static string GetString(Form ownerForm, string caption, string defaultValue = null)
        {
            string input = null;
            using (frmInput form = new frmInput(caption, InputType.String, defaultValue))
            {
                if (form.ShowDialog(ownerForm) == DialogResult.OK)
                    input = form.Input;
            }

            return input;
        }

        public static double? GetPositiveValue(Form ownerForm, string caption)
        {
            double input;
            using (frmInput form = new frmInput(caption, InputType.PositiveDouble))
            {
                if (form.ShowDialog(ownerForm) != DialogResult.OK)
                    return null;
                input = double.Parse(form.Input, CultureInfo.InvariantCulture);
            }

            return input;
        }
    }
}