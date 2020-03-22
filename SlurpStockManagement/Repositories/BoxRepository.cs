using MongoDB.Driver;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly IMongoCollection<Box> _boxes;

        public BoxRepository(IBoxDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _boxes = database.GetCollection<Box>(settings.BoxesCollectionName);
        }

        public Box GetBoxes()
        {
            return _boxes.Find(box => true).ToList()[0];
        }

        public Box ReserveBox(Box updatedBox)
        {
            return _boxes.FindOneAndReplace(box => box.Id == updatedBox.Id, updatedBox);
        }
    }
}
