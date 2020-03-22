using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface ICoffeeBagSettings
    {
        CoffeeBagSize Size200 { get; set; }
        CoffeeBagSize Size400 { get; set; }
        CoffeeBagSize Size1000 { get; set; }
    }
}
