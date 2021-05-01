using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dashboard.Input
{
    public static class DistributionInputHelper
    {
        public static DistributionInputDto GetDistribution(Form ownerForm, string caption, List<string> members, List<AreaCountryInputDto> countries = null)
        {
            var dictPerc = new Dictionary<string, int>();
            var newCountries = new List<AreaCountryInputDto>();

            while (dictPerc.Sum(d => d.Value) < 100)
            {
                using var form = new frmDistributionInput(caption, members, dictPerc.Sum(d => d.Value), countries);
                if (form.ShowDialog(ownerForm) == DialogResult.OK)
                {
                    var member = form.MemberKey ?? form.Country.Country;
                    if (dictPerc.ContainsKey(member))
                    {
                        MessageBox.Show($"Duplicate entries are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    dictPerc[member] = form.MemberPercentage;

                    if (form.Country != null)
                    {
                        if (!countries.Select(c => c.Country).Contains(form.Country.Country))
                            newCountries.Add(form.Country);
                    }
                }
                else
                    return null;
            }

            return new DistributionInputDto(dictPerc.Keys.ToArray(), dictPerc.Values.Select(v => v / 100d).ToArray(), newCountries);
        }
    }
}