using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class DataRetriever : Entity
    {
        public string Name { get; set; }
        /// <summary>
        /// Used to resolve the retriever
        /// </summary>
        public string Type { get; set; }

        public string BaseUrl { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// Order when multiple retrievers are available
        /// 0 or less means not available
        /// </summary>
        public int Priority { get; set; }
        public string SupportedArea { get; set; }
        public string Description { get; set; }

        public DateTime LastRequest { get; set; }
        /// <summary>
        /// Content of the last request
        /// For debugging purposes
        /// </summary>
        public string LastRequestQuery { get; set; }
        /// <summary>
        /// Content of the last response
        /// For debugging purposes
        /// </summary>
        public string LastResponseData { get; set; }

        public ICollection<RetrieverLimitation> RetrieverLimitations { get; set; }
        public ICollection<StockRetrieverCompatibility> StockRetrieverCompatibilities { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// Relation between stocks and retrievers to define the compatibility
    /// </summary>
    public class StockRetrieverCompatibility
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public string StockRef { get; set; }
        public int DataRetrieverId { get; set; }
        public DataRetriever DataRetriever { get; set; }
        /// <summary>
        /// True in case this stock can be retrieved with this retriever
        /// </summary>
        public RetrieverCompatibility Compatibility { get; set; }
    }

    public enum RetrieverCompatibility
    {
        /// <summary>
        /// Not tested yet
        /// </summary>
        Unknown,
        True,
        False,
    }
}