using System.ComponentModel;
using Core;

namespace Messages.UI.Overview
{
    public class RetrieverLimitationViewModel
    {
        [DisplayName("Timespan")]
        public RetrieverLimitTimespanType TimespanType { get; set; }
        public int Limit { get; set; }
        /// <summary>
        /// Requests done during this timespan
        /// </summary>
        [DisplayName("Requests")]
        [Description("Requests done during this timespan")]
        public int RequestsDone { get; set; }
    }
}