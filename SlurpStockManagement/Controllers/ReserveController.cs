using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Constants;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;
using SlurpStockManagement.ViewModels;

namespace SlurpStockManagement.Controllers
{
    [Route("api/order")]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveCoffeeService _reserveCoffeeService;
        private readonly ICoffeeBagSettings _coffeeBagSettings;

        public ReserveController(IReserveCoffeeService reserveCoffeeService, ICoffeeBagSettings coffeeBagSettings)
        {
            _reserveCoffeeService = reserveCoffeeService;
            _coffeeBagSettings = coffeeBagSettings;
        }

        [HttpPut]
        public ActionResult ReserveCoffee([FromBody] ReserveCoffeeRequest request)
        {
            if (request == null)
            {
                return new BadRequestObjectResult(Error.InvalidInputs);
            }
            var coffeeBagWeightList = new List<int> { _coffeeBagSettings.Size200.Weight, _coffeeBagSettings.Size400.Weight, _coffeeBagSettings.Size1000.Weight };

            foreach (CoffeeOrderItem orderItem in request.Order)
            {
                if (!coffeeBagWeightList.Contains(orderItem.OrderSize))
                {
                    return new BadRequestObjectResult(Error.InvalidInputs);
                }
            }
            return _reserveCoffeeService.ReserveCoffee(request.Order);
        }
    }
}
