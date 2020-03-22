﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Constants;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class ReserveCoffeeService : IReserveCoffeeService
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IReserveBoxServices _reserveBoxServices;

        public ReserveCoffeeService(ICoffeeRepository coffeeRepository, IReserveBoxServices reserveBoxServices)
        {
            _coffeeRepository = coffeeRepository;
            _reserveBoxServices = reserveBoxServices;
        }

        public ActionResult ReserveCoffee(List<CoffeeOrderItem> order)
        {
            if (IsCoffeeAvailable(order).Count > 0)
            {
                List<int> bagType = IsCoffeeAvailable(order);
                string coffeeBagTypeCode= "";
                string coffeeBagTypeMessage = "";

                foreach (int type in bagType)
                {
                    coffeeBagTypeCode += $"{type.ToString()}-grams-";
                    coffeeBagTypeMessage += $"{type.ToString()} grams, ";
                }
                return new BadRequestObjectResult(
                    new Error(
                        $"coffee-size-{coffeeBagTypeCode}out-of-stock",
                        $"Coffee size {coffeeBagTypeMessage.Remove(coffeeBagTypeMessage.LastIndexOf(", "))} out of stock")
                );
            }

            foreach (CoffeeOrderItem orderItem in order)
            {
                Coffee coffeeInStock = _coffeeRepository.GetCoffeeBySize(orderItem.OrderSize);

                if (coffeeInStock.Available < orderItem.Quantity)
                {
                    return new BadRequestObjectResult(Error.CoffeeOutOfStock);
                }
                else
                {
                    coffeeInStock.Available -= orderItem.Quantity;
                    coffeeInStock.Reserved += orderItem.Quantity;
                    _coffeeRepository.ReserveCoffee(coffeeInStock);
                }
            }
            _reserveBoxServices.ReserveBox(order);
            return new OkResult();
        }

        public List<int> IsCoffeeAvailable(List<CoffeeOrderItem> order)
        {
            List<int> outOfStockOrderSizes = new List<int>();
            foreach (CoffeeOrderItem orderItem in order)
            {
                Coffee coffeeInStock = _coffeeRepository.GetCoffeeBySize(orderItem.OrderSize);
                if (coffeeInStock.Available < orderItem.Quantity)
                {
                    outOfStockOrderSizes.Add(orderItem.OrderSize);
                }
                continue;
            }
            return outOfStockOrderSizes;
        }
    }
}
