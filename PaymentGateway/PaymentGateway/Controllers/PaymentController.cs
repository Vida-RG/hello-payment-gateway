using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentProvider _paymentProvider;

        public PaymentController(
            ILogger<PaymentController> logger,
            IPaymentProvider paymentProvider)
        {
            _logger = logger;
            _paymentProvider = paymentProvider;

            logger.LogInformation("Payment Controller is called");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionResultQuery), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Get(string id)
        {
            TransactionResultQuery result = await _paymentProvider.GetDetailsOfPayment(id);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 501)]
        public async Task<IActionResult> Post([FromBody] Transaction transaction)
        {
            string resultId = string.Empty;
            try
            {
                resultId = await _paymentProvider.Pay(transaction);
            }
            catch (ApiException error)
            {
                _logger.LogError(error.Message, error);

                if (error.StatusCode == 400)
                {
                    return BadRequest("Invalid data provided");
                }

                throw new ApplicationException("Unexpected exception occured while processing your request");
            }

            return Ok(resultId);
        }
    }
}
