using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Controllers
{

    [Route("api/box")]
    public class BoxController : ControllerBase
    {
        private readonly IReserveBoxService _reserveBoxService;

        public BoxController(IReserveBoxService reserveBoxService)
        {
            _reserveBoxService = reserveBoxService;
        }

        [HttpGet]
        public ActionResult<Box> GetBox() => _reserveBoxService.GetBoxInStock();
    }

}
