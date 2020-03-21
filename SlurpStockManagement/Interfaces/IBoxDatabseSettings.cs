namespace SlurpStockManagement.Interfaces
{
    public interface IBoxDatabaseSettings
    {
        string BoxesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
