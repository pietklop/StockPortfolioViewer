using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Castle.MicroKernel;
using Dashboard.DI;
using Imports.DeGiro;
using log4net;
using Services;
using Services.DI;
using Syroot.Windows.IO;

namespace Dashboard
{
    public partial class frmMain : Form
    {
        private readonly ILog log;

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

        public frmMain(ILog log)
        {
            this.log = log;
            InitializeComponent();
            SetWindowRoundCorners(25);
            HandleMenuButtonClick(btnMainOverview, CastleContainer.Instance.Resolve<frmOverview>(new Arguments { { nameof(frmMain), this } }));

            void SetWindowRoundCorners(int radius) => Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));
        }

        public void ShowStockDetails(string stockName, string isin) =>
            LoadForm(stockName, CastleContainer.Instance.Resolve<frmStockDetail>(new Arguments{{"stockIsin", isin}}));

        private void btnMainOverview_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmOverview>());
        private void btnTransactions_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, null);
        private void btnImport_Click(object sender, EventArgs e) => ImportUsingFileDialog();

        private void btnMainOverview_Leave(object sender, EventArgs e) => SetDefaultButtonBackColor((Button)sender);
        private void btnTransactions_Leave(object sender, EventArgs e) => SetDefaultButtonBackColor((Button)sender);

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
                    openFileDialog.InitialDirectory = new KnownFolder(KnownFolderType.Downloads).Path;
                    openFileDialog.Filter = "txt files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "Select files to import";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    var importer = CastleContainer.Resolve<Importer>();
                    var importProcessor = CastleContainer.Resolve<ImportProcessor>();

                    int nAddedTransactions = 0;
                    int nAddedDividends = 0;
                    foreach (var filePath in openFileDialog.FileNames)
                    {
                        (int nT, int nDiv) = importProcessor.Process(importer.Import(filePath));
                        nAddedTransactions += nT;
                        nAddedDividends += nDiv;
                    }
                    if (nAddedTransactions == 0 && nAddedDividends == 0)
                        MessageBox.Show($"No transactions or dividends are added. Possibly these were already imported earlier", "Import result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else    
                        MessageBox.Show($"Successfully added {nAddedTransactions} transactions and {nAddedDividends} dividends", "Import result", MessageBoxButtons.OK);
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
