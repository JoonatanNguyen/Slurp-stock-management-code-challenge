using System;
namespace SlurpStockManagement.Interfaces
{
    public interface ICoffeeDatabaseSettings
    {
        string CoffeesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
