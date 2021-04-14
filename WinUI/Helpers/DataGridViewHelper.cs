﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Messages.UI;

namespace Dashboard.Helpers
{
    public static class DataGridViewHelper
    {
        public static void ApplyColumnDisplayFormatAttributes(this DataGridView dgv)
        {
            var type = ListBindingHelper.GetListItemType(dgv.DataSource);
            var properties = TypeDescriptor.GetProperties(type);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                var p = properties[column.DataPropertyName];
                if (p != null)
                {
                    var format = (DisplayFormatAttribute)p.Attributes[typeof(DisplayFormatAttribute)];
                    column.ToolTipText = p.Description;
                    column.DefaultCellStyle.Format = format?.Format;
                }
            }
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
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
    }
}