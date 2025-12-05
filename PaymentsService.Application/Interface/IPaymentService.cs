using PaymentsService.Application.DTOs;
using PaymentsService.Application.DTOs.PaymentDTOs;

namespace PaymentsService.Application.Interface
{
    public interface IPaymentService
    {

        public Task<PaymentResponse> CreatedPayment(PaymentRequest request);

        Task<PageResult<ListPayments>> GetPaymentsByCustomerAsync(Guid customerId, int pageNumber = 1, int pageSize = 10);
    }
}

