using Microsoft.AspNetCore.Mvc;
using Services.FakePayment.API.Models;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Services.FakePayment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
