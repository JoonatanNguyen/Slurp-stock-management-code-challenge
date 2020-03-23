using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SlurpStockManagement.Constants;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class ReserveCoffeeService : IReserveCoffeeService
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IReserveBoxService _reserveBoxServices;
        private readonly IHubContext<RealTimeUpdate> _hubContext;

        public ReserveCoffeeService(ICoffeeRepository coffeeRepository, IReserveBoxService reserveBoxServices, IHubContext<RealTimeUpdate> hubContext)
        {
            _coffeeRepository = coffeeRepository;
            _reserveBoxServices = reserveBoxServices;
            _hubContext = hubContext;
        }

        public ActionResult<List<Coffee>> GetCoffeeInStock() => _coffeeRepository.Get();

        public ActionResult ReserveCoffee(List<CoffeeOrderItem> order)
        {
            try
            {
                var outOfStockOrderSizes = GetOutOfStockOrderSizes(order);
                if (outOfStockOrderSizes.Count > 0)
                {
                    return GetOutOfStockErrorMessage(outOfStockOrderSizes);
                }
                foreach (CoffeeOrderItem orderItem in order)
                {
                    Coffee coffeeInStock = _coffeeRepository.GetCoffeeBySize(orderItem.OrderSize);

                    if (coffeeInStock.Available < orderItem.Quantity)
                    {
                        return new BadRequestObjectResult(Error.CoffeeOutOfStock);
                    }
                    coffeeInStock.Available -= orderItem.Quantity;
                    coffeeInStock.Reserved += orderItem.Quantity;
                    _coffeeRepository.ReserveCoffee(coffeeInStock);
                }
                _reserveBoxServices.ReserveBox(order);
                _hubContext.Clients.All.SendAsync("update");

                return new OkResult();
            }
            catch
            {
                // TODO: Log errors
                return new BadRequestResult();
            }
        }

        private List<int> GetOutOfStockOrderSizes(List<CoffeeOrderItem> order)
        {
            List<int> outOfStockOrderSizes = new List<int>();
            foreach (CoffeeOrderItem orderItem in order)
            {
                Coffee coffeeInStock = _coffeeRepository.GetCoffeeBySize(orderItem.OrderSize);
                if (coffeeInStock.Available < orderItem.Quantity)
                {
                    outOfStockOrderSizes.Add(orderItem.OrderSize);
                }
            }
            return outOfStockOrderSizes;
        }

        private ObjectResult GetOutOfStockErrorMessage(List<int> outOfStockOrderSizes)
        {
            string coffeeBagTypeMessage = "";
            foreach (int outOfStockOrderSize in outOfStockOrderSizes)
            {
                coffeeBagTypeMessage += $"{outOfStockOrderSize.ToString()} grams, ";
            }
            return new BadRequestObjectResult(
                new Error(
                    $"out-of-stock",
                    $"Coffee size {coffeeBagTypeMessage.Remove(coffeeBagTypeMessage.LastIndexOf(", "))} out of stock")
            );
        }
    }
}
