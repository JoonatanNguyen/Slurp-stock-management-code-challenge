using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Constants;

namespace SlurpStockManagement.Responses
{
    public class ErrorResponse : IActionResult
    {
        public string Message { get; }
        public string Code { get; }
        private int StatusCode { get; }

        public ErrorResponse() : this(null) { }

        public ErrorResponse(Error error = null, int statusCode = 400)
        {
            Message = error.Message;
            Code = error.Code;
            StatusCode = statusCode;
        }

        Task IActionResult.ExecuteResultAsync(ActionContext context) => new ObjectResult(this)
        {
            StatusCode = StatusCode
        }.ExecuteResultAsync(context);
    }
}
