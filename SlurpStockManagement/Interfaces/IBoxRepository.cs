using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IBoxRepository
    {
        Box GetBoxes();
        Box ReserveBox(Box updatedBox);
    }
}
