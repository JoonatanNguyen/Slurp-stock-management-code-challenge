using SlurpStockManagement.Interfaces;

namespace SlurpStockManagement.Models
{
    public class CoffeeDatabaseSettings : ICoffeeDatabaseSettings
    {
        public string CoffeesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
