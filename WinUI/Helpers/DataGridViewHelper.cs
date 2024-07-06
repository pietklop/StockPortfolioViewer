using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Castle.Core.Internal;
using Messages.UI;

namespace Dashboard.Helpers
{
    public static class DataGridViewHelper
    {
        public static void ApplyColumnDisplayFormatAttributes(this DataGridView dgv)
        {
            var type = ListBindingHelper.GetListItemType(dgv.DataSource);
            var properties = TypeDescriptor.GetProperties(type);

            bool anyHeaderTooltip = false;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                var p = properties[column.DataPropertyName];
                if (p != null)
                {
                    var hide = (ColumnHideAttribute)p.Attributes[typeof(ColumnHideAttribute)];
                    if (hide != null)
                    {
                        column.Visible = false;
                        continue;
                    }
                    var format = (DisplayFormatAttribute)p.Attributes[typeof(DisplayFormatAttribute)];
                    column.ToolTipText = p.Description;
                    if (!p.Description.IsNullOrEmpty()) anyHeaderTooltip = true;
                    column.DefaultCellStyle.Format = format?.Format;
                    var underline = (ColumnCellsUnderlineAttribute)p.Attributes[typeof(ColumnCellsUnderlineAttribute)];
                    if (underline != null)
                        column.DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Underline);
                }
            }

            if (!anyHeaderTooltip)
                dgv.ShowCellToolTips = false;
        }

        public static DataGridViewColumn GetColumn(this DataGridView dgv, string columnName)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
                if (column.Name == columnName)
                    return column;

            throw new Exception($"Could not find column: '{columnName}'");
        }

        public static void SetReadOnly(this DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.AllowUserToResizeColumns = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        public static void SetVisualStyling(this DataGridView dgv)
        {
            dgv.RowHeadersVisible = false; // hides most left 'column'

            dgv.BackgroundColor = Color.FromArgb(46, 51, 73);
            dgv.ForeColor = Color.Gainsboro;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ShowCellToolTips = false;

            // Set the selection background color for all the cells.
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Set the background color for all rows and for alternating rows.
            // The value for alternating rows overrides the value for all rows.
            dgv.RowsDefaultCellStyle.BackColor = Color.FromArgb(24, 30, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Black;

            // column header color
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 126, 249);
            dgv.EnableHeadersVisualStyles = false;
        }

        public static void DoColumnOrdering<T>(this DataGridView dgv, List<T> dataSourceList, int columnIndex, SortOrder sortOrder = SortOrder.None)
        {
            var column = dgv.Columns[columnIndex];
            var colName = column.Name;
            if (column.Tag == null)
                column.Tag = SortOrder.Descending;
            else if (column.Tag.GetType() != typeof(SortOrder))
                throw new Exception($"Column.Tag is already used, so can not be (ab)used for column ordering state");

            switch (sortOrder)
            {
                case SortOrder.None:
                    column.Tag = (SortOrder)column.Tag == SortOrder.Descending ? SortOrder.Ascending : SortOrder.Descending;
                    break;
                case SortOrder.Ascending:
                    column.Tag = SortOrder.Ascending;
                    break;
                case SortOrder.Descending:
                    column.Tag = SortOrder.Descending;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }

            dgv.DataSource = (SortOrder)column.Tag == SortOrder.Ascending
                ? dataSourceList.OrderBy(x => x.GetType().GetProperty(colName).GetValue(x)).ToList()
                : dataSourceList.OrderByDescending(x => x.GetType().GetProperty(colName).GetValue(x)).ToList();
        }

        public static void ShowRedAtNegativeValue(this DataGridViewCell cell, bool showGreenAsPositiveValue = true)
        {
            int sign = 0;
            if (cell.Value is string s)
            {
                if (s?.IndexOf('-') >= 0) sign = -1;
                else sign = 1;
            }
            else if (cell.Value is double d)
            {
                if (d < 0) sign = -1;
                if (d > 0) sign = 1;
            }
            else if (cell.Value is float f)
            {
                if (f < 0) sign = -1;
                if (f > 0) sign = 1;
            }
            else if (cell.Value is int i)
            {
                if (i < 0) sign = -1;
                if (i > 0) sign = 1;
            }

            if (sign < 0)
                cell.Style.ForeColor = Color.Red;
            else if (showGreenAsPositiveValue && sign > 0)
                cell.Style.ForeColor = Color.LawnGreen;
        }
    }
}