using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentsService.Application.DTOs;
using PaymentsService.Application.DTOs.PaymentDTOs;
using PaymentsService.Application.Exceptions;
using PaymentsService.Application.Interface;
using PaymentsService.Application.Utils;
using PaymentsService.Domain.Entities;
using PaymentsService.Infraestructure.Data;

namespace PaymentsService.Infraestructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly IEventProducer _eventProducer;

        private const string STATUS_PENDING = "pending";

        public PaymentService(ApiDbContext context, IConfiguration configuration, ILogger<PaymentService> logger, IEventProducer eventProducer)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _eventProducer = eventProducer;
        }

        public async Task<PaymentResponse> CreatedPayment(PaymentRequest request)
        {
            try
            {
                // Validar Cliente
                var customer = await _context.Customer.FindAsync(request.CustomerId);
                if (customer == null)
                    throw new RequestException(ResponseMessage.PaymentCustomerNotFound);

                // Validación Provider
                if (string.IsNullOrWhiteSpace(request.ServiceProvider))
                    throw new RequestException(ResponseMessage.PaymentServiceProviderRequired);

                // Validación monto requerido
                if (request.Amount <= 0)
                    throw new RequestException(ResponseMessage.PaymentAmountInvalid);

                // Validación límite
                if (request.Amount > 1500)
                    throw new RequestException(ResponseMessage.PaymentAmountExceedsLimit);

                // Validación moneda
                var currency = request.Currency?.ToUpper();
                if (currency != "BS")
                    throw new RequestException(ResponseMessage.PaymentCurrencyNotAllowed);

                // Crear entidad Payment
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    ServiceProvider = request.ServiceProvider,
                    Amount = request.Amount,
                    Currency = currency!,
                    Status = STATUS_PENDING,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Payment.Add(payment);
                await _context.SaveChangesAsync();

                // Publicar evento
                await _eventProducer.PublishAsync("payment.created", payment);
                _logger.LogInformation("Payment created and event published: {PaymentId}", payment.PaymentId);

                return new PaymentResponse
                {
                    PaymentId = payment.PaymentId,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment for customer {CustomerId}", request.CustomerId);
                throw;
            }
        }

        public async Task<PageResult<ListPayments>> GetPaymentsByCustomerAsync(Guid customerId, int pageNumber = 1, int pageSize = 10)
        {
            if (customerId == null || customerId == Guid.Empty)
                throw new RequestException(ResponseMessage.PaymentCustomerIdRequired);




            var query = _context.Payment
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.CreatedAt);

            var totalRecords = await query.CountAsync();

            var payments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ListPayments
                {
                    PaymentId = p.PaymentId,
                    ServiceProvider = p.ServiceProvider,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return new PageResult<ListPayments>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = payments
            };
        }
    }

}

