using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Services.FakePayment.API.Models;
using Shared.ControllerBases;
using Shared.Dtos;
using Shared.Messages;
using System;
using System.Threading.Tasks;

namespace Services.FakePayment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand();

            createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessageCommand.Province = paymentDto.Order.Address.Province;
            createOrderMessageCommand.District = paymentDto.Order.Address.District;
            createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
            createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });

            try
            {
                await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Message sending failed: " + ex.Message);
            }

            return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
