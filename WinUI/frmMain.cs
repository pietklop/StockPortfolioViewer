using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Dashboard.DI;
using log4net;

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
            HandleMenuButtonClick(btnMainOverview, CastleContainer.Resolve<frmOverview>());

            void SetWindowRoundCorners(int radius) => Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));
        }

        private void btnMainOverview_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, CastleContainer.Resolve<frmOverview>());
        private void btnTransactions_Click(object sender, EventArgs e) => HandleMenuButtonClick((Button)sender, null);

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
    }
}
