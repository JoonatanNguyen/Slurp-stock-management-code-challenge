using System;
using SlurpStockManagement.Interfaces;

namespace SlurpStockManagement.Models
{
    public class BoxSizeSettings : IBoxSizeSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
    }
}
