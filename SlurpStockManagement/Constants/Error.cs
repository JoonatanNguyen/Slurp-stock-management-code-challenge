namespace SlurpStockManagement.Constants
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static readonly Error CoffeeOutOfStock = new Error("coffee-out-of-stock", "Coffee out of stock");

        public static readonly Error InvalidInputs = new Error("invalid-input", "Invalid inputs");

        public static readonly Error InternalError = new Error("internal-error", "Internal Error");

    }
}
