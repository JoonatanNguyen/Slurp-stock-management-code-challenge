using System;
using SlurpStockManagement.Interfaces;

namespace SlurpStockManagement.Models
{
    public class CoffeeBagSizeSettings : ICoffeeBagSettings
    {
        public CoffeeBagSize Size200 { get; set; } = new CoffeeBagSize();
        public CoffeeBagSize Size400 { get; set; } = new CoffeeBagSize();
        public CoffeeBagSize Size1000 { get; set; } = new CoffeeBagSize();
    }

    public class CoffeeBagSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
    }
}
