using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Interfaces;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _payService;

        public PaymentController(IPaymentService payService)
        {
            _payService = payService;
        }

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PaymentViewModel content)
        {
            var result = await _payService.Create(content).ConfigureAwait(false);

            if (result)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
