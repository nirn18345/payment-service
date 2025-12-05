namespace PaymentsService.Application.DTOs.PaymentDTOs
{
    public class ListPayments
    {
        public Guid PaymentId { get; set; }
        public string ServiceProvider { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BS";
        public string Status { get; set; } = "pendiente";
        public DateTime CreatedAt { get; set; }
    }
}
