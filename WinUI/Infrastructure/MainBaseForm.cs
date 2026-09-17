using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Dashboard.Infrastructure
{
    /// <summary>
    /// Borderless dark window frame, replaces MetroFramework.Forms.MetroForm (2013, not maintained anymore and
    /// unable to deal with a dpi other than 96). Draws the same chrome: a black strip on top, a near black
    /// background and the three window buttons in the top right corner.
    /// The sizes below are in logical (96 dpi) pixels and are scaled to the monitor the window is on.
    /// </summary>
    public class MainBaseForm : Form
    {
        private static readonly Color BackgroundColor = Color.FromArgb(17, 17, 17);
        private static readonly Color TopStripColor = Color.Black;
        private static readonly Color ResizeGripColor = Color.FromArgb(109, 109, 109);

        private const int TopStripHeight = 5;
        private const int ButtonWidth = 25;
        private const int ButtonHeight = 20;
        private const int ButtonMargin = 5;
        /// <summary>
        /// Width of the grab area along the edges to resize the window
        /// </summary>
        private const int ResizeBorder = 5;

        private WindowButton minimizeButton;
        private WindowButton maximizeButton;
        private WindowButton closeButton;

        public MainBaseForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = BackgroundColor;
            AddWindowButtons();
        }

        /// <summary>
        /// Height of the area at the top the window can be dragged by, the sub-forms start below it
        /// </summary>
        protected int CaptionHeight => Padding.Top;

        #region Window buttons
        private void AddWindowButtons()
        {
            closeButton = AddWindowButton("r", (s, e) => Close());
            maximizeButton = AddWindowButton("1", (s, e) => ToggleMaximize());
            minimizeButton = AddWindowButton("0", (s, e) => WindowState = FormWindowState.Minimized);
            UpdateWindowButtonPositions();
        }

        private WindowButton AddWindowButton(string glyph, EventHandler onClick)
        {
            var button = new WindowButton { Text = glyph };
            button.Click += onClick;
            Controls.Add(button);
            button.BringToFront();
            return button;
        }

        private void ToggleMaximize() =>
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

        /// <summary>
        /// The buttons are placed by hand instead of by anchoring, so they stay in place after a dpi change too
        /// </summary>
        private void UpdateWindowButtonPositions()
        {
            if (closeButton == null) return;

            var width = LogicalToDeviceUnits(ButtonWidth);
            var height = LogicalToDeviceUnits(ButtonHeight);
            var margin = LogicalToDeviceUnits(ButtonMargin);

            closeButton.Bounds = new Rectangle(ClientSize.Width - margin - width, margin, width, height);
            maximizeButton.Bounds = new Rectangle(closeButton.Left - width, margin, width, height);
            minimizeButton.Bounds = new Rectangle(maximizeButton.Left - width, margin, width, height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindowButtonPositions();
            Invalidate();
        }

        /// <summary>
        /// Fires when the window is dragged to a monitor with another scaling
        /// </summary>
        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);
            UpdateWindowButtonPositions();
            Invalidate();
        }
        #endregion

        // No drop shadow on purpose: MetroForm put a barely visible one (max 13/255 over a thin ring) behind the
        // window, asking the desktop manager for the standard shadow instead gives a far heavier halo than that.

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackgroundColor);
            using (var brush = new SolidBrush(TopStripColor))
                e.Graphics.FillRectangle(brush, 0, 0, ClientSize.Width, LogicalToDeviceUnits(TopStripHeight));

            DrawResizeGrip(e.Graphics);
        }

        /// <summary>
        /// The dotted triangle in the bottom right corner, three rows of 1, 2 and 3 dots
        /// </summary>
        private void DrawResizeGrip(Graphics graphics)
        {
            const int rows = 3;
            var dot = LogicalToDeviceUnits(2);
            var step = LogicalToDeviceUnits(4);
            var margin = LogicalToDeviceUnits(6);

            var dots = new Rectangle[rows * (rows + 1) / 2];
            var index = 0;
            for (int row = 0; row < rows; row++)
                for (int column = 0; column <= row; column++)
                    dots[index++] = new Rectangle(
                        ClientSize.Width - margin - column * step,
                        ClientSize.Height - margin - (rows - 1 - row) * step,
                        dot, dot);

            using var brush = new SolidBrush(ResizeGripColor);
            graphics.FillRectangles(brush, dots);
        }

        #region Behaviour normally taken care of by the border of the window
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_GETMINMAXINFO = 0x0024;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;

        private const int HTCLIENT = 1, HTCAPTION = 2;
        private const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST && !DesignMode)
            {
                var hit = HitTest(PointToClient(new Point(m.LParam.ToInt32())));
                if (hit != HTCLIENT)
                {
                    m.Result = (IntPtr)hit;
                    return;
                }
            }
            if (m.Msg == WM_NCLBUTTONDBLCLK && !MaximizeBox) return;

            base.WndProc(ref m);

            // windows fills the struct first, correct it afterwards for the monitor this window is on
            if (m.Msg == WM_GETMINMAXINFO && !DesignMode)
                LimitMaximizedSizeToScreen(m.HWnd, m.LParam);
        }

        /// <summary>
        /// Gives the borderless window its resize-edges back, and lets the strip above the sub-form drag the window
        /// </summary>
        private int HitTest(Point point)
        {
            var border = LogicalToDeviceUnits(ResizeBorder);
            bool left = point.X <= border, right = point.X >= ClientSize.Width - border;
            bool top = point.Y <= border, bottom = point.Y >= ClientSize.Height - border;

            if (Resizable)
            {
                if (top && left) return HTTOPLEFT;
                if (top && right) return HTTOPRIGHT;
                if (bottom && left) return HTBOTTOMLEFT;
                if (bottom && right) return HTBOTTOMRIGHT;
                if (left) return HTLEFT;
                if (right) return HTRIGHT;
                if (top) return HTTOP;
                if (bottom) return HTBOTTOM;
            }

            return point.Y < CaptionHeight ? HTCAPTION : HTCLIENT;
        }

        private bool Resizable => WindowState != FormWindowState.Maximized;

        /// <summary>
        /// A borderless window maximizes over the taskbar unless it is told the working area of its own monitor.
        /// Without this it is sized for another monitor and parts of it end up outside the visible area.
        /// MaxTrackSize has to be set along with MaxSize: windows fills it in from the primary monitor, and on a
        /// monitor that is larger or scaled differently that value wins over MaxSize and the window grows too big.
        /// </summary>
        private void LimitMaximizedSizeToScreen(IntPtr handle, IntPtr lParam)
        {
            var screen = Screen.FromHandle(handle);
            var workingArea = screen.WorkingArea;
            var info = Marshal.PtrToStructure<MinMaxInfo>(lParam);

            info.MaxSize = new Point32(workingArea.Width, workingArea.Height);
            info.MaxPosition = new Point32(workingArea.Left - screen.Bounds.Left, workingArea.Top - screen.Bounds.Top);
            info.MinTrackSize = new Point32(MinimumSize.Width, MinimumSize.Height);
            info.MaxTrackSize = new Point32(workingArea.Width, workingArea.Height);

            Marshal.StructureToPtr(info, lParam, false);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Point32
        {
            public int X;
            public int Y;
            public Point32(int x, int y) { X = x; Y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MinMaxInfo
        {
            public Point32 Reserved;
            public Point32 MaxSize;
            public Point32 MaxPosition;
            public Point32 MinTrackSize;
            public Point32 MaxTrackSize;
        }
        #endregion

        /// <summary>
        /// One of the three window buttons, drawn the way MetroFramework did it
        /// </summary>
        private sealed class WindowButton : Control
        {
            private static readonly Color NormalForeColor = Color.FromArgb(204, 204, 204);
            private static readonly Color HoverBackColor = Color.FromArgb(34, 34, 34);
            private bool hovered;

            public WindowButton()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.Selectable, false);
                BackColor = BackgroundColor;
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                hovered = true;
                Invalidate();
                base.OnMouseEnter(e);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                hovered = false;
                Invalidate();
                base.OnMouseLeave(e);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.Clear(hovered ? HoverBackColor : BackgroundColor);
                using var font = new Font("Webdings", 9.25f * (DeviceDpi / 96f));
                TextRenderer.DrawText(e.Graphics, Text, font, ClientRectangle, NormalForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
