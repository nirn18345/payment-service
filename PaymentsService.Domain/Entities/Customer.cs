namespace PaymentsService.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación inversa
        public ICollection<Payment>? Payments { get; set; }
    }
}
