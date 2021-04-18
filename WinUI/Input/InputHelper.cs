using System.Globalization;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public static class InputHelper
    {
        public static bool GetConfirmation(string caption)
        {
            using (frmInput form = new frmInput(caption, InputType.Confirmation))
                return form.ShowDialog() == DialogResult.OK;
        }

        public static string GetString(string caption)
        {
            string input = null;
            using (frmInput form = new frmInput(caption, InputType.String))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    input = form.Input;
            }

            return input;
        }

        public static double? GetPositiveValue(string caption)
        {
            double input;
            using (frmInput form = new frmInput(caption, InputType.PositiveDouble))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return null;
                input = double.Parse(form.Input, CultureInfo.InvariantCulture);
            }

            return input;
        }
    }
}