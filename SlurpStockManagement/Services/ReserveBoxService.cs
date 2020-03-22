using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class ReserveBoxService : IReserveBoxServices
    {
        private readonly IBoxRepository _boxRepository;
        private CoffeeBagSize size200;
        private CoffeeBagSize size400;
        private CoffeeBagSize size1000;
        private int boxSizeWidth;
        private int boxSizeHeight;
        private int boxSizeLength;

        public ReserveBoxService(IBoxRepository boxRepository, ICoffeeBagSettings coffeeBagSizesettings, IBoxSizeSettings boxSizeSettings)
        {
            _boxRepository = boxRepository;
            size200 = coffeeBagSizesettings.Size200;
            size400 = coffeeBagSizesettings.Size400;
            size1000 = coffeeBagSizesettings.Size1000;
            boxSizeWidth = boxSizeSettings.Width;
            boxSizeHeight = boxSizeSettings.Height;
            boxSizeLength = boxSizeSettings.Length;
        }

        public ActionResult ReserveBox(List<CoffeeOrderItem> order)
        {
            Box boxesInStock = _boxRepository.GetBoxes();

            int boxVolume = boxSizeWidth * boxSizeHeight * boxSizeLength;
            int coffeeBag200gramsVolume = size200.Width * size200.Height * size200.Length;
            int coffeeBag400gramsVolume = size400.Width * size400.Height * size400.Length;
            int coffeeBag1000gramsVolume = size1000.Width * size1000.Height * size1000.Length;
            int coffeeBagsVolume = 0;

            foreach (CoffeeOrderItem orderItem in order)
            {
                switch(orderItem.OrderSize)
                {
                    case 200:
                        coffeeBagsVolume += orderItem.Quantity * coffeeBag200gramsVolume;
                        break;
                    case 400:
                        coffeeBagsVolume += orderItem.Quantity * coffeeBag400gramsVolume;
                        break;
                    case 1000:
                        coffeeBagsVolume += orderItem.Quantity * coffeeBag1000gramsVolume;
                        break;
                    default:
                        break;
                }
            }
            double neededBoxes = (double)coffeeBagsVolume / boxVolume;
            
            if(neededBoxes < 1 && neededBoxes != 0)
            {
                neededBoxes = 1;
            } else
            {
                neededBoxes = (int)Math.Ceiling(neededBoxes);
            }

            if(boxesInStock.Available < neededBoxes)
            {
                int pendingBox = (int)(neededBoxes - boxesInStock.Available);
                boxesInStock.Reserved += boxesInStock.Available;
                boxesInStock.Available = 0;
                boxesInStock.Pending += pendingBox;
                _boxRepository.ReserveBox(boxesInStock);
                return new OkResult();

            } else
            {
                boxesInStock.Available -= (int)neededBoxes;
                boxesInStock.Reserved += (int)neededBoxes;
                _boxRepository.ReserveBox(boxesInStock);
                return new OkResult();
            }
        }
    }
}
