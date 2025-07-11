﻿using System;
using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class TransactionViewModel
    {
        public const string SumOf = "Sum of";
        public const string AnnualSumOf = "Annual sum of";
        public string Name { get; set; }
        public string Date { get; set; }
        [DisplayName("N")]
        public double Quantity { get; set; }
        public string Price { get; set; }
        [DisplayName("Current")]
        public string Performance { get; set; }
        public string NativeValue { get; set; }
        public string UserValue { get; set; }
        public string Costs { get; set; }
        public string CurrRatio { get; set; }
        [ColumnHide]
        public double HiddenPrice { get; set; }
    }
}