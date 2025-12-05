using Microsoft.AspNetCore.Mvc;
using PaymentsService.Api.Utils;
using PaymentsService.Application.DTOs.Erros;
using PaymentsService.Application.DTOs.PaymentDTOs;
using PaymentsService.Application.Interface;
using PaymentsService.Application.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentsService.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        public readonly IPaymentService _PaymentService;


        public PaymentsController(IPaymentService PaymentService)
        {
            _PaymentService = PaymentService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(MsDtoResponse<PaymentResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        public async Task<ActionResult> CreatedPayment([FromBody] PaymentRequest request)
        {
            var result = await _PaymentService.CreatedPayment(request);

            return Ok(ApiResponseBuilder.Success(result, HttpContext, ResponseMessage.PaymentCreatedSuccess));
        }


        [HttpGet]
        [ProducesResponseType(typeof(MsDtoResponse<PaymentResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        public async Task<IActionResult> GetByCustomer([FromQuery] Guid customerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _PaymentService.GetPaymentsByCustomerAsync(customerId, pageNumber, pageSize);
            return Ok(ApiResponseBuilder.Success(result, HttpContext, ResponseMessage.PaymentListRetrieved));
        }
    }
}

