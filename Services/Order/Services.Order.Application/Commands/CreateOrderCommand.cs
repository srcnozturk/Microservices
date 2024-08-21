using MediatR;
using Services.Order.Application.Dtos;
using Shared.Dtos;
using System.Collections.Generic;

namespace Services.Order.Application.Commands
{
    class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}
