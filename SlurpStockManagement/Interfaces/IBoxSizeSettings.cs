using System;
namespace SlurpStockManagement.Interfaces
{
    public interface IBoxSizeSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
    }
}
