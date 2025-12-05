namespace PaymentsService.Application.DTOs.PaymentDTOs
{
    public class PaymentRequest
    {

        public Guid CustomerId { get; set; }
        public string ServiceProvider { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BS"; // Default BS



    }
}
