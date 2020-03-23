using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class ReserveBoxService : IReserveBoxService
    {
        private readonly IBoxRepository _boxRepository;
        private readonly IBoxSizeSettings _boxSizeSettings;
        private readonly CoffeeBagSize Size200;
        private readonly CoffeeBagSize Size400;
        private readonly CoffeeBagSize Size1000;

        public ReserveBoxService(IBoxRepository boxRepository, ICoffeeBagSettings coffeeBagSizesettings, IBoxSizeSettings boxSizeSettings)
        {
            _boxRepository = boxRepository;
            Size200 = coffeeBagSizesettings.Size200;
            Size400 = coffeeBagSizesettings.Size400;
            Size1000 = coffeeBagSizesettings.Size1000;
            _boxSizeSettings = boxSizeSettings;
        }

        public ActionResult<Box> GetBoxInStock() => _boxRepository.GetBoxes();

        public ActionResult ReserveBox(List<CoffeeOrderItem> order)
        {
            try
            {
                Box boxesInStock = _boxRepository.GetBoxes();
                int boxVolume = _boxSizeSettings.Width * _boxSizeSettings.Height * _boxSizeSettings.Length;
                var totalCoffeeBagsVolume = GetTotalCoffeeBagsVolume(order);
                var r = (double)(totalCoffeeBagsVolume / boxVolume);
                int neededBoxes = (int)Math.Ceiling((double)totalCoffeeBagsVolume / boxVolume);
                bool isNotEnoughBoxesInStock = boxesInStock.Available < neededBoxes;
                int pendingBox = neededBoxes - boxesInStock.Available;

                boxesInStock.Reserved += isNotEnoughBoxesInStock ? boxesInStock.Available : neededBoxes;
                boxesInStock.Available = isNotEnoughBoxesInStock ? 0 : boxesInStock.Available - neededBoxes;
                boxesInStock.Pending = isNotEnoughBoxesInStock ? boxesInStock.Pending + pendingBox : boxesInStock.Pending;
                _boxRepository.ReserveBox(boxesInStock);
                return new OkResult();
            }
            catch
            {
                // TODO: Log errors
                return new BadRequestResult();
            }
        }

        private int GetTotalCoffeeBagsVolume(List<CoffeeOrderItem> order)
        {
            int totalCoffeeBagsVolume = 0;

            foreach (CoffeeOrderItem orderItem in order)
            {
                switch (orderItem.OrderSize)
                {
                    case 200:
                        int coffeeBag200gramsVolume = Size200.Width * Size200.Height * Size200.Length;
                        totalCoffeeBagsVolume += orderItem.Quantity * coffeeBag200gramsVolume;
                        break;
                    case 400:
                        int coffeeBag400gramsVolume = Size400.Width * Size400.Height * Size400.Length;
                        totalCoffeeBagsVolume += orderItem.Quantity * coffeeBag400gramsVolume;
                        break;
                    default:
                        int coffeeBag1000gramsVolume = Size1000.Width * Size1000.Height * Size1000.Length;
                        totalCoffeeBagsVolume += orderItem.Quantity * coffeeBag1000gramsVolume;
                        break;
                }
            }
            return totalCoffeeBagsVolume;
        }
    }
}
