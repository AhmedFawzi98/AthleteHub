using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Infrastructure.Authorization.Services.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("Payment/create-checkout-session")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var sessionUrl = await _paymentService.CreateCheckoutSessionAsync(request.Price, request.SubscriptionName, request.AthleteId, request.SubscriptionId);
        return Ok(new { SessionUrl = sessionUrl });
    }

    [HttpGet("Payment/Success")]
    public async Task<IActionResult> Success([FromQuery] string sessionId)
    {
        var successResult = await _paymentService.Success(sessionId);
        return Ok();
    }
    }
}
