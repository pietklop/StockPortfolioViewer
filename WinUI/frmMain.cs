﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Castle.MicroKernel;
using Core;
using Imports.DeGiro;
using log4net;
using Services;
using Services.DI;
using Syroot.Windows.IO;

namespace Dashboard
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        private readonly ILog log;
        private readonly Settings settings;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public frmMain(ILog log, Settings settings)
        {
            this.log = log;
            this.settings = settings;
            InitializeComponent();
            SetWindowRoundCorners(25);
            HandleMenuButtonClick(btnMainOverview, CastleContainer.Instance.Resolve<frmOverview>(new Arguments { { nameof(frmMain), this } }));

            void SetWindowRoundCorners(int radius) => Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = $"v{version.Major}.{version.Minor}.{version.Build}";
        }

        public void ShowStockDetails(string stockName, string isin) =>
            LoadForm(stockName, CastleContainer.Instance.Resolve<frmStockDetail>(new Arguments{{"stockIsin", isin}}));

        public void ShowStockPerformance(string stockName, string isin) =>
            LoadForm(stockName, CastleContainer.Instance.Resolve<frmStockPerformance>(new Arguments{{"stockIsins", new List<string>{isin}}}));

        public void ShowDividends(string isin) =>
            LoadForm("Dividends", CastleContainer.Instance.Resolve<frmDividends>(new Arguments{{"stockIsin", isin}}));

        public void ShowStockTransactions(string isin) =>
            LoadForm("Transactions", CastleContainer.Instance.Resolve<frmTransactions>(new Arguments{{"stockIsin", isin}}));
        
        public void ShowDataRetriever(string name) =>
            LoadForm(name, CastleContainer.Instance.Resolve<frmDataRetriever>(new Arguments{{"dataRetrieverName", name}}));

        private void btnMainOverview_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmOverview>());
        private void btnTransactions_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmTransactions>());
        private void btnDividends_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmDividends>());
        private void btnPerformance_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmStockPerformance>());

        private void btnDataRetrieval_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmDataRetrievers>());
        private void btnImport_Click(object sender, EventArgs e) => ImportUsingFileDialog();

        private void btnMainOverview_Leave(object sender, EventArgs e) => SetDefaultButtonBackColor((Button)sender);
        private void btnTransactions_Leave(object sender, EventArgs e) => SetDefaultButtonBackColor((Button)sender);
        private void btnDividends_Leave(object sender, EventArgs e) => SetDefaultButtonBackColor((Button)sender);

        private void HandleMenuButtonClick(Button button, Form formToShow)
        {
            SetNavPanel(button);
            LoadForm(button.Text, formToShow);
        }

        private void LoadForm(string viewName, Form form)
        {
            lblViewName.Text = viewName;
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

        private void SetNavPanel(Button button)
        {
            pnlNav.Height = button.Height;
            pnlNav.Top = button.Top;
            pnlNav.Left = button.Left;
            button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void SetDefaultButtonBackColor(Button button) => button.BackColor = Color.FromArgb(24, 30, 54);

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    var importer = CastleContainer.Resolve<Importer>();
                    var importProcessor = CastleContainer.Resolve<ImportProcessor>();

                    int nAddedTransactions = 0;
                    int nAddedDividends = 0;
                    int nStockValueUpdates = 0;
                    foreach (var filePath in openFileDialog.FileNames)
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
                    }

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
            }
            catch (Exception ex)
            {
                log.Error($"Error during import", ex);
                MessageBox.Show($"Error occured during file import: '{ex.Message}'. See log for more details", "Import results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
