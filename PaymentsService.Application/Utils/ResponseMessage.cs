namespace PaymentsService.Application.Utils
{
    public static class ResponseMessage
    {
        public const string PaymentAmountRequired = "The payment amount is required.";
        public const string PaymentAmountInvalid = "The amount must be greater than zero.";
        public const string PaymentAmountExceedsLimit = "The amount cannot exceed 1500 Bs.";
        public const string PaymentCurrencyNotAllowed = "Dollar payments are not allowed. Only Bolivianos (Bs) are accepted.";
        public const string PaymentCustomerNotFound = "Customer not found.";
        public const string PaymentServiceProviderRequired = "The service provider is required.";
        public const string PaymentCreatedSuccess = "Payment registered successfully.";


        public const string PaymentListRetrieved = "Payment list retrieved successfully.";
        public const string PaymentListEmpty = "No payments found for the specified customer.";

        public const string PaymentCustomerIdRequired = "CustomerId is required to retrieve payments.";



    }
}

