using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL;
using Messages.Dtos;
using PresentationControls;

namespace Dashboard.Input
{
    public partial class frmStockSelection : Form
    {
        private readonly StockDbContext db;
        public List<StockDto> Stocks = new List<StockDto>();

        public frmStockSelection(StockDbContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmStockSelection_Load(object sender, System.EventArgs e)
        {
            PopulateComboBox(cmbSector, db.Sectors.Select(s => s.Name).ToList());
            PopulateComboBox(cmbContinents, db.Areas.Where(a => a.IsContinent).Select(s => s.Name).ToList());
            PopulateComboBox(cmbCountries, db.Areas.Where(a => !a.IsContinent).Select(s => s.Name).ToList());
            PopulateCbmStocks();
        }

        private void chkEtfOnly_CheckedChanged(object sender, System.EventArgs e) => PopulateCbmStocks();

        private void PopulateCbmStocks()
        {
            var etfOnly = chkEtfOnly.Checked;
            PopulateComboBox(cmbStocks, db.Stocks.Where(s => s.Transactions.Sum(t => t.Quantity) > 0 && (!etfOnly || s.ExpenseRatio > 0)).Select(s => s.Name).ToList());
        }

        private void PopulateComboBox(CheckBoxComboBox cmb, List<string> items)
        {
            cmb.Items.Clear();
            foreach (var item in items.OrderBy(i => i))
                cmb.Items.Add(item);
        }

        private List<string> GetCheckedFields(CheckBoxComboBox cmb) => cmb.CheckBoxItems.Where(c => c.Checked).Select(c => c.Text).ToList();

        private void IntersectSelection(List<StockDto> stockSelection)
        {
            if (!stockSelection.Any()) return;

            Stocks = Stocks.Any() ? Stocks.Intersect(stockSelection).ToList() : stockSelection;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            var sectorFields = GetCheckedFields(cmbSector);
            if (sectorFields.Any())
                IntersectSelection(db.Stocks.Where(s => s.SectorShares.All(ss => sectorFields.Contains(ss.Sector.Name))).Select(st => new StockDto{ Isin = st.Isin, Name = st.Name}).ToList());
            
            var continentFields = GetCheckedFields(cmbContinents);
            var areaStocks = new List<StockDto>();
            if (continentFields.Any())
                areaStocks = db.Stocks.Where(s => s.AreaShares.All(ss => continentFields.Contains(ss.Area.Name) || (!ss.Area.IsContinent && continentFields.Contains(ss.Area.Continent.Name)))).Select(st => new StockDto { Isin = st.Isin, Name = st.Name }).ToList();
            var countryFields = GetCheckedFields(cmbCountries);
            if (countryFields.Any())
                areaStocks.AddRange(db.Stocks.Where(s => s.AreaShares.All(ss => countryFields.Contains(ss.Area.Name))).Select(st => new StockDto { Isin = st.Isin, Name = st.Name }).ToList());
            IntersectSelection(areaStocks);

            var stockFields = GetCheckedFields(cmbStocks);
            if (stockFields.Any())
            {
                var stocks = db.Stocks.Where(s => stockFields.Contains(s.Name)).Select(st => new StockDto { Isin = st.Isin, Name = st.Name }).ToList();
                Stocks.AddRange(stocks);
            }

            if (Stocks.Count == 0 && chkEtfOnly.Checked)
                Stocks = db.Stocks.Where(s => s.ExpenseRatio > 0).Select(st => new StockDto { Isin = st.Isin, Name = st.Name }).ToList();

            Stocks = Stocks.Distinct().ToList();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
