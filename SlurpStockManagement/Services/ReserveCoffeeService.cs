using System;
using System.Collections.Generic;
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
            foreach (CoffeeOrderItem orderItem in order)
            {
                Coffee coffeeInStock = _coffeeRepository.GetCoffeeBySize(orderItem.OrderSize);

                if (order[0].Quantity == 1)
                {
                    //coffeeInStock.Available < orderItem.Quantity
                    return new BadRequestObjectResult(Error.CoffeeOutOfStock);
                }
                else
                {
                    //coffeeInStock.Available -= orderItem.Quantity;
                    //coffeeInStock.Reserved = orderItem.Quantity;
                    //_coffeeRepository.ReserveCoffee(coffeeInStock);
                    _reserveBoxServices.ReserveBox(order);
                }
            }
            return new OkResult();
        }
    }
}
