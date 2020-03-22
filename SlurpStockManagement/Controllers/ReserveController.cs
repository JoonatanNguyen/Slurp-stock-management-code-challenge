using System;
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
        public ReserveController(IReserveCoffeeService reserveCoffeeService)
        {
            _reserveCoffeeService = reserveCoffeeService;
        }

        [HttpPut]
        public ActionResult ReserveCoffee([FromBody] ReserveCoffeeRequest request)
        {
            if(request == null)
            {
                return new BadRequestObjectResult(Error.InvalidInputs);
            }
            return _reserveCoffeeService.ReserveCoffee(request.Order);
        }
    }
}
