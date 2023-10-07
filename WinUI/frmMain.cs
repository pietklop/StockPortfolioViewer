using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Core;
using DAL;
using Dashboard.Input;
using Imports.DeGiro;
using log4net;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DI;
using Services.Helpers;
using Services.Ui;
using Syroot.Windows.IO;

namespace Dashboard
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        private readonly ILog log;
        private readonly Settings settings;
        public int nTotalStocks;
        
        public string SelectedStockName;
        private string selectedStockIsin;
        public string SelectedStockIsin
        {
            get => selectedStockIsin;
            set
            {
                selectedStockIsin = value;
                btnSingleTransactions.Visible = !selectedStockIsin.IsNullOrEmpty();
                btnSingleDividends.Visible = !selectedStockIsin.IsNullOrEmpty();
                btnSinglePerformance.Visible = !selectedStockIsin.IsNullOrEmpty();
            }
        }

        public frmMain(ILog log, Settings settings)
        {
            this.log = log;
            this.settings = settings;
            InitializeComponent();
            HandleMenuButtonClick(btnMainOverview, CastleContainer.Instance.Resolve<frmOverview>(new Arguments { { nameof(frmMain), this } }));

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = $"v{version.Major}.{version.Minor}.{version.Build}";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Properties.UserSettings.Default.Maximized)
            {
                Location = Properties.UserSettings.Default.Location;
                WindowState = FormWindowState.Maximized;
                Size = Properties.UserSettings.Default.Size;
            }
            else if (Properties.UserSettings.Default.Minimized)
            {
                Location = Properties.UserSettings.Default.Location;
                WindowState = FormWindowState.Minimized;
                Size = Properties.UserSettings.Default.Size;
            }
            else if (Properties.UserSettings.Default.Location.X > 0 && Properties.UserSettings.Default.Size.Width > 100)
            {
                Location = Properties.UserSettings.Default.Location;
                Size = Properties.UserSettings.Default.Size;
            }

            ShowValuesFromDatabase();
        }

        private void ShowValuesFromDatabase()
        {
            using var db = new StockDbContext();

            ShowDollarEuroRate();
            ShowLastDivOrTransactionImportToolTip();

            void ShowDollarEuroRate()
            {
                var usd = db.Currencies.Single(c => c.Key == "USD");
                lblEuroInDollars.Text = $"EUR {usd.Ratio.FormatCurrency("$", false)} ({usd.LastUpdate.ToShortDateString()})";
            }

            void ShowLastDivOrTransactionImportToolTip()
            {
                var lastTransaction = db.Transactions.Include(t => t.StockValue)
                    .OrderByDescending(t => t.StockValue.TimeStamp).FirstOrDefault()?.StockValue.TimeStamp;
                var lastDividend = db.Dividends.OrderByDescending(d => d.TimeStamp).FirstOrDefault()?.TimeStamp;
                
                var lastImport = lastTransaction.HasValue && lastDividend.HasValue
                    ? lastTransaction.Value.GetLatest(lastDividend.Value)
                    : lastTransaction ?? lastDividend;
                if (lastImport == null) return;
                
                toolTip.SetToolTip(btnImport, $"Last transaction or dividend import: {lastImport.Value.ToShortDateString()}");
            }
        }

        private void EnableStockDetails(string stockName, string isin)
        {
            SelectedStockIsin = isin;
            SelectedStockName = stockName;
            btnStockDetails.Text = $"{stockName.MaxLength(10)} details";
            btnStockDetails.Enabled = true;
        }

        public void ShowStockDetails(string stockName, string isin)
        {
            LoadForm(stockName, CastleContainer.Instance.Resolve<frmStockDetail>(new Arguments { { "stockIsin", isin } }));
            EnableStockDetails(stockName, isin);
        }

        public void ShowStockPerformance(string stockName, string isin)
        {
            LoadForm(stockName, CastleContainer.Instance.Resolve<frmStockPerformance>(new Arguments { { "stockIsins", new List<string> { isin } } }));
            EnableStockDetails(stockName, isin);
        }

        public void ShowDividends(string isin) =>
            LoadForm("Dividends", CastleContainer.Instance.Resolve<frmDividends>(new Arguments{{"stockIsin", isin}}));

        public void ShowStockTransactions(string stockName, string isin)
        {
            LoadForm("Transactions", CastleContainer.Instance.Resolve<frmTransactions>(new Arguments { { "stockIsin", isin } }));
            EnableStockDetails(stockName, isin);
        }

        public void ShowDataRetriever(string name) =>
            LoadForm(name, CastleContainer.Instance.Resolve<frmDataRetriever>(new Arguments{{"dataRetrieverName", name}}));

        private void btnMainOverview_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmOverview>());
        private void btnTransactions_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmTransactions>());
        private void btnDividends_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmDividends>());
        private void btnPerformance_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmStockPerformance>());
        private void btnStockDetails_Click(object sender, EventArgs e) => ShowStockDetails(SelectedStockName, selectedStockIsin);
        private void btnHistory_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmStockHistoryOverview>());

        private void btnSingleTransactions_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Instance.Resolve<frmTransactions>(new Arguments {{ "stockIsin", SelectedStockIsin }}));
        private void btnSingleDividends_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Instance.Resolve<frmDividends>(new Arguments {{ "stockIsin", SelectedStockIsin }}));
        private void btnSinglePerformance_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Instance.Resolve<frmStockPerformance>(new Arguments {{ "stockIsins", new List<string>{ SelectedStockIsin }}}));

        private void btnDataRetrieval_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmDataRetrievers>());
        private void btnImport_Click(object sender, EventArgs e) => HandleImport();

        private void HandleMenuButtonClick(Button button, Form formToShow) => LoadForm(button.Text, formToShow);

        private void LoadForm(string viewName, Form form)
        {
            lblViewName.Text = viewName == "1" ? SelectedStockName : viewName;
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.TopMost = true;
            foreach (Control control in pnlFormLoader.Controls)
            {
                var frm = control as Form;
                frm?.Close();
                frm?.Dispose();
            }
            pnlFormLoader.Controls.Clear();
            pnlFormLoader.Controls.Add(form);
            form.Show();
        }

        private void HandleImport()
        {
            if (TryImportNewPortfolio()) return;
            ImportUsingFileDialog();
        }

        private bool TryImportNewPortfolio()
        {
            try
            {
                var lastFileDt = Properties.UserSettings.Default.LastPortfolioImportFileDate;

                string pattern = "*Portfolio*.csv";
                var downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
                var dirInfo = new DirectoryInfo(downloadsPath);
                var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).FirstOrDefault();
                if (file == null) return false;

                if (file.LastWriteTime.Date <= lastFileDt.Date) return false;

                Properties.UserSettings.Default.LastPortfolioImportFileDate = file.LastWriteTime;
                Properties.UserSettings.Default.Save();

                bool doImport = InputHelper.GetConfirmation(this, $"Import '{file.Name}'?");

                if (!doImport) return false;

                var importer = CastleContainer.Resolve<Importer>();
                var importProcessor = CastleContainer.Resolve<ImportProcessor>();

                int nAddedDividends = 0;
                int nStockValueUpdates = 0;
                int nAddedTransactions = ImportFile(file.FullName, importer, importProcessor, 0, ref nAddedDividends, ref nStockValueUpdates);

                DoPostImportActions(nAddedTransactions, nAddedDividends, nStockValueUpdates);

            }
            catch (Exception ex)
            {
                ShowImportException(ex);
            }

            return true;
        }

        private void ImportUsingFileDialog()
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = settings.DebugMode ? settings.DebugPath : new KnownFolder(KnownFolderType.Downloads).Path;
                    openFileDialog.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "Select files to import";
                    var clipBoardFilePath = TryGetCsvFilePathFromClipBoard();
                    if (clipBoardFilePath != null) openFileDialog.FileName = clipBoardFilePath;
                    
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    var importer = CastleContainer.Resolve<Importer>();
                    var importProcessor = CastleContainer.Resolve<ImportProcessor>();

                    int nAddedTransactions = 0;
                    int nAddedDividends = 0;
                    int nStockValueUpdates = 0;
                    foreach (var filePath in openFileDialog.FileNames)
                        nAddedTransactions = ImportFile(filePath, importer, importProcessor, nAddedTransactions, ref nAddedDividends, ref nStockValueUpdates);

                    DoPostImportActions(nAddedTransactions, nAddedDividends, nStockValueUpdates);
                }
            }
            catch (Exception ex)
            {
                ShowImportException(ex);
            }

            string TryGetCsvFilePathFromClipBoard()
            {
                var clipText = Clipboard.GetText(TextDataFormat.Text).Trim();
                if (string.IsNullOrEmpty(clipText)) return null;

                return clipText.EndsWith(".csv") && File.Exists(clipText) ? clipText : null;
            }
        }

        private void ShowImportException(Exception ex)
        {
            log.Error($"Error during import", ex);
            MessageBox.Show($"Error occured during file import: '{ex.Message}'. See log for more details", "Import results", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DoPostImportActions(int nAddedTransactions, int nAddedDividends, int nStockValueUpdates)
        {
            if (nAddedTransactions == 0 && nAddedDividends == 0)
            {
                if (nStockValueUpdates > 0)
                    MessageBox.Show($"Successfully updated {nStockValueUpdates} stocks", "Import result", MessageBoxButtons.OK);
                else
                    MessageBox.Show($"No transactions or dividends are added. Possibly these were already imported earlier", "Import result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show($"Successfully added {nAddedTransactions} transactions and {nAddedDividends} dividends", "Import result", MessageBoxButtons.OK);

            var formOverview = pnlFormLoader.Controls[0] as frmOverview;
            formOverview?.Reload();
        }

        private int ImportFile(string filePath, Importer importer, ImportProcessor importProcessor, int nAddedTransactions, ref int nAddedDividends, ref int nStockValueUpdates)
        {
            log.Debug($"Try import: '{filePath}'");
            var lines = File.ReadAllLines(filePath);

            switch (importer.DetermineImportType(lines))
            {
                case ImportType.Transaction:
                    (int nT, int nDiv) = importProcessor.Process(importer.TransactionImport(lines, settings.DebugMode));
                    nAddedTransactions += nT;
                    nAddedDividends += nDiv;
                    break;
                case ImportType.StockValue:
                    nStockValueUpdates = importProcessor.Process(importer.StockValueImport(lines));
                    break;
                default:
                    MessageBox.Show($"Unrecognized file content", "Import result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return nAddedTransactions;
        }

        private void pnlInfo_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            Size = new Size(1200, 1000);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.UserSettings.Default.Location = RestoreBounds.Location;
                Properties.UserSettings.Default.Size = RestoreBounds.Size;
                Properties.UserSettings.Default.Maximized = true;
                Properties.UserSettings.Default.Minimized = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.UserSettings.Default.Location = Location;
                Properties.UserSettings.Default.Size = Size;
                Properties.UserSettings.Default.Maximized = false;
                Properties.UserSettings.Default.Minimized = false;
            }
            else
            {
                Properties.UserSettings.Default.Location = RestoreBounds.Location;
                Properties.UserSettings.Default.Size = RestoreBounds.Size;
                Properties.UserSettings.Default.Maximized = false;
                Properties.UserSettings.Default.Minimized = true;
            }
            Properties.UserSettings.Default.Save();
        }
    }
}
