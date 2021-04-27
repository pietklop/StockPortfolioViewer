using System;

namespace DAL.Entities
{
    public class RetrieverLimitation : Entity
    {
        public int DataRetrieverId { get; set; }
        public DataRetriever DataRetriever { get; set; }
        public RetrieverLimitTimespanType TimespanType { get; set; }
        public int Limit { get; set; }
        /// <summary>
        /// Requests done during this timespan
        /// </summary>
        public int RequestsDone { get; set; }
    }

    public enum RetrieverLimitTimespanType
    {
        None,
        Second,
        Minute,
        Hour,
        Day,
        Month,
    }

    public static class RetrieverLimitationHelper
    {
        public static bool CanCall(this RetrieverLimitation limitation, DateTime lastRequest) =>
            limitation.RequestsDone < limitation.Limit || limitation.CanResetLimit(lastRequest);

        public static bool CanResetLimit(this RetrieverLimitation limitation, DateTime lastRequest)
        {
            var now = DateTime.Now;

            switch (limitation.TimespanType)
            {
                case RetrieverLimitTimespanType.Second:
                    return (now - lastRequest).TotalSeconds > 1;
                case RetrieverLimitTimespanType.Minute:
                    return (now - lastRequest).TotalSeconds > 60;
                case RetrieverLimitTimespanType.Hour:
                    return now.Date != lastRequest.Date || now.Hour != lastRequest.Hour;
                case RetrieverLimitTimespanType.Day:
                    return now.Date != lastRequest.Date;
                case RetrieverLimitTimespanType.Month:
                    return now.Year != lastRequest.Year || now.Month != lastRequest.Month;
                default:
                    throw new ArgumentOutOfRangeException($"{limitation.TimespanType} is not supported");
            }
        }

        public static void UpdateAfterRequest(this RetrieverLimitation limitation, DateTime lastRequest, int weight = 1)
        {
            if (limitation.CanResetLimit(lastRequest))
                limitation.RequestsDone = weight;
            else
                limitation.RequestsDone += weight;
        }
    }
}