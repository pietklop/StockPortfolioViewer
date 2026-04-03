using Core;
using Dashboard.Helpers;
using Dashboard.Infrastructure;
using Dashboard.Input;
using Messages.UI.Overview;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dashboard.StockDetails;

public partial class frmDistributionEdit : Form
{
    private readonly List<string> sectorSelectionList = null;
    /// <summary>
    /// List is used for selection but also to report back new added countries
    /// </summary>
    public List<AreaCountryInputDto> Countries = null;
    public List<DistributionElementDto> Distribution { get; private set; }
    public bool SaveRequest { get; private set; }
    private const string ValueDecrementBtnCol = "-";
    private const string ValueIncrementBtnCol = "+";
    private bool AreaEdit() => Countries != null;

    public frmDistributionEdit(PortfolioDistributionDto currentDistributionDto, List<AreaCountryInputDto> countries)
    {
        Countries = countries;
        InitializeComponent();
        Distribution = new List<DistributionElementDto>();
        for (int i = 0; i < currentDistributionDto.Labels.Length; i++)
            Distribution.Add(new DistributionElementDto(currentDistributionDto.Labels[i], currentDistributionDto.Fractions[i]));
    }

    public frmDistributionEdit(PortfolioDistributionDto currentDistributionDto, List<string> sectorSelectionList)
    {
        this.sectorSelectionList = sectorSelectionList;
        InitializeComponent();
        Distribution = new List<DistributionElementDto>();
        for (int i = 0; i < currentDistributionDto.Labels.Length; i++)
            Distribution.Add(new DistributionElementDto(currentDistributionDto.Labels[i], currentDistributionDto.Fractions[i]));
    }

    private void frmDistributionEdit_Load(object sender, EventArgs e)
    {
        PopulateGrid(true);
        ValidateAndUpdateTotal();
    }

    private void PopulateGrid(bool updateOrdering)
    {
        if (updateOrdering) Distribution = Distribution.OrderBy(d => d.Key == Constants.Unknown).ThenByDescending(d => d.Fraction).ToList();
        dgvLines.DataSource = Distribution.Select(r => new DistributionViewModel(r)).ToList();
        dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        var keyCol = dgvLines.GetColumn(nameof(DistributionViewModel.Key));
        keyCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        if (!dgvLines.ColumnExists(nameof(ValueDecrementBtnCol)))
            dgvLines.AddButtonColumn(nameof(ValueDecrementBtnCol), ValueDecrementBtnCol, "");
        if (!dgvLines.ColumnExists(nameof(ValueIncrementBtnCol)))
            dgvLines.AddButtonColumn(nameof(ValueIncrementBtnCol), ValueIncrementBtnCol, "");
        dgvLines.ApplyColumnDisplayFormatAttributes();
        dgvLines.SetReadOnly();
        dgvLines.SetVisualStyling();
    }

    private void dgvLines_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

        var fractionCol = dgvLines.GetColumn(nameof(DistributionViewModel.Fraction));
        var incrementCol = dgvLines.GetColumn(nameof(ValueIncrementBtnCol));
        var decrementCol = dgvLines.GetColumn(nameof(ValueDecrementBtnCol));

        if (e.ColumnIndex == fractionCol.Index)
        {
            HandleDurationCorrectionInput(e.RowIndex);
        }
        else if (e.ColumnIndex == incrementCol.Index)
        {
            Distribution[e.RowIndex].UpdateValue(Distribution[e.RowIndex].Fraction + 0.01);
        }
        else if (e.ColumnIndex == decrementCol.Index)
        {
            var newFraction = Distribution[e.RowIndex].Fraction - 0.01;
            if (newFraction < 0.0001)
                Distribution.RemoveAt(e.RowIndex);
            else
                Distribution[e.RowIndex].UpdateValue(newFraction);
        }
        else { return; /* nothing */ }

        TryFixDistribution();
        PopulateGrid(false);
        ValidateAndUpdateTotal();
    }

    private void TryFixDistribution()
    {
        if (ValidDistribution()) return;
        var sum = DistributionSum();

        var corr = 1 - sum;

        var unk = Distribution.FirstOrDefault(d => d.Key == Constants.Unknown);
        if (unk == null)
        {
            if (corr > 0.0001)
                Distribution.Add(new DistributionElementDto(Constants.Unknown, corr));
        }
        else
        {
            if (unk.Fraction + corr < 0.0001)
                Distribution.Remove(unk);
            else unk.UpdateValue(unk.Fraction + corr);
        }
    }

    private void ValidateAndUpdateTotal()
    {
        if (ValidDistribution())
        {
            lblTotal.ForeColor = Color.Gainsboro;
            btnSave.Enabled = true;
        }
        else
        {
            lblTotal.ForeColor = Color.Red;
            btnSave.Enabled = false;
        }

        lblTotal.Text = $"Total: {DistributionSum():P0}";
    }

    private bool ValidDistribution()
    {
        var sum = DistributionSum();
        if (sum > 1.0001 || sum < 0.9999) return false;

        // should all have at least 1% to be valid
        return Distribution.Select(d => d.Fraction).All(f => f > 0.009999);
    }

    private void HandleDurationCorrectionInput(int elementIndex)
    {
        var element = Distribution[elementIndex];
        var orgValue = element.Fraction;
        try
        {
            var value = InputHelper.GetPositiveValue(this, "Enter value");
            if (value == null) return;

            if (value.Value <= 0)
            {
                Distribution.Remove(element);
                return;
            }

            element.UpdateValue(Math.Round(value.Value) / 100);
        }
        catch (Exception ex)
        {
            element.UpdateValue(orgValue); // undo change
            new frmException(ex).ShowDialog(this);
        }
    }

    private double DistributionSum() => Distribution.Sum(d => d.Fraction);

    private void dgvLines_SelectionChanged(object sender, EventArgs e) => dgvLines.ClearSelection();

    private void btnReset_Click(object sender, EventArgs e)
    {
        Distribution = new List<DistributionElementDto>();
        PopulateGrid(true);
        ValidateAndUpdateTotal();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var uniqueList = Countries == null
            ? sectorSelectionList.Except(Distribution.Select(d => d.Key)).ToList() // non selected sectors
            : Countries.Select(c => c.Continent).Distinct().ToList(); // continents
        var inputForm = new frmDistributionInput("Add distribution element", uniqueList, 0, Countries);
        if (inputForm.ShowDialog(this) == DialogResult.OK)
        {
            var elementName = AreaEdit() ? inputForm.Country.Country: inputForm.MemberKey;

            if (Distribution.Any(d => d.Key.ToLower() == elementName.ToLower()))
            {
                InputHelper.GetConfirmation(this, "Can not add a duplicate element");
                return;
            }
            if (AreaEdit())
            {
                if (Countries!.All(c => c.Country != inputForm.Country.Country))
                    Countries.Add(inputForm.Country); // new country added
            }

            var fraction = inputForm.MemberPercentage / 100.0;
            if (fraction < 0.0001)
                return;
            Distribution.Add(new DistributionElementDto(elementName, fraction));
            TryFixDistribution();
            PopulateGrid(true);
            ValidateAndUpdateTotal();
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidDistribution())
            InputHelper.GetConfirmation(this, "Total must be 100%");

        SaveRequest = true;
        Close();
    }

    private void btnClose_Click(object sender, EventArgs e) => Close();
}