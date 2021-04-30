using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public static class DistributionInputHelper
    {
        public static DistributionInputDto GetDistribution(Form ownerForm, string caption, List<string> members)
        {
            var dictPerc = new Dictionary<string, int>();

            while (dictPerc.Sum(d => d.Value) < 100)
            {
                using var form = new frmDistributionInput(caption, members, dictPerc.Sum(d => d.Value));
                if (form.ShowDialog(ownerForm) == DialogResult.OK)
                {
                    if (dictPerc.ContainsKey(form.MemberKey))
                    {
                        MessageBox.Show($"Duplicate entries are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    dictPerc[form.MemberKey] = form.MemberPercentage;
                }
                else
                    return null;
            }

            return new DistributionInputDto(dictPerc.Keys.ToArray(), dictPerc.Values.Select(v => v / 100d).ToArray());
        }
    }
}