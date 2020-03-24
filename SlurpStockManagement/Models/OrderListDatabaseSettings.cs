using System;
using SlurpStockManagement.Interfaces;

namespace SlurpStockManagement.Models
{
    public class OrderListDatabaseSettings : IOrderListDatabaseSettings
    {
        public string OrderListCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
