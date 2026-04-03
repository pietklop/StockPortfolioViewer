using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Dashboard.Infrastructure;

public partial class frmException : Form
{
    private readonly Exception exception;

    public frmException(Exception exception, string message = null)
    {
        this.exception = exception;
        InitializeComponent();

        message = message == null ? "" : $"{message}\r\n";
        txtMessage.Text = $"{message}{exception.Message}";
    }

    private void frmException_Load(object sender, EventArgs e)
    {
        if (Owner != null && StartPosition == FormStartPosition.CenterParent)
            Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                Owner.Location.Y + Owner.Height / 2 - Height / 2);
    }

    private void btnCopyDetails_Click(object sender, EventArgs e)
    {
        string content = Content(exception);
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        var versionString = $"v{version.Major}.{version.Minor}.{version.Build}";
        string info = $"{versionString}\r\n{content}\r\n{exception.StackTrace}";

        Clipboard.SetText(info);
    }

    private string Content(Exception ex)
    {
        if (ex.InnerException != null)
            return $"{Content(ex.InnerException)}\r\n{ex.Message}";
        return ex.Message;
    }

    private void btnClose_Click(object sender, EventArgs e) => Close();

    private void frmException_FormClosed(object sender, FormClosedEventArgs e) => Owner?.Focus();
    private void frmException_FormClosing(object sender, FormClosingEventArgs e) => Owner?.Focus();
}