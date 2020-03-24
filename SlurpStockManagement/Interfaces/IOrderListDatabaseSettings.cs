using System;
namespace SlurpStockManagement.Interfaces
{
    public interface IOrderListDatabaseSettings
    {
        string OrderListCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
