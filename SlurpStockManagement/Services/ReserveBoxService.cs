using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Constants;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class ReserveBoxService : IReserveBoxServices
    {
        private readonly IBoxRepository _boxRepository;

        public ReserveBoxService(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public ActionResult ReserveBox(List<CoffeeOrderItem> order)
        {
            Box boxesInStock = _boxRepository.GetBoxes();
            int boxVolume = 60 * 60 * 60;
            int coffeeBag200gramsVolume = 16 * 23 * 2;
            int coffeeBag400gramsVolume = 22 * 26 * 2;
            int coffeeBag1000gramsVolume = 14 * 26 * 10;
            int volume = 0;

            foreach (CoffeeOrderItem orderItem in order)
            {
                switch(orderItem.OrderSize)
                {
                    case 200:
                        volume += orderItem.Quantity * coffeeBag200gramsVolume;
                        break;
                    case 400:
                        volume += orderItem.Quantity * coffeeBag400gramsVolume;
                        break;
                    case 1000:
                        volume += orderItem.Quantity * coffeeBag1000gramsVolume;
                        break;
                    default:
                        break;
                }
            }
            double neededBoxes = (double)volume / boxVolume;

            if(neededBoxes < 1 && neededBoxes != 0)
            {
                neededBoxes = 1;
            } else
            {
                neededBoxes = (int)Math.Ceiling(neededBoxes);
            }

            if(boxesInStock.Available < neededBoxes)
            {
                return new BadRequestObjectResult(Error.CoffeeOutOfStock);
            } else
            {
                boxesInStock.Available -= (int)neededBoxes;
                boxesInStock.Reserved = (int)neededBoxes;
                _boxRepository.ReserveBox(boxesInStock);
                return new OkResult();
            }
        }
    }
}
