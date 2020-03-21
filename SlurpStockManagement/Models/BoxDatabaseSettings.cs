using System;
using SlurpStockManagement.Interfaces;

namespace SlurpStockManagement.Models
{
    public class BoxDatabaseSettings : IBoxDatabaseSettings
    {
        public string BoxesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
