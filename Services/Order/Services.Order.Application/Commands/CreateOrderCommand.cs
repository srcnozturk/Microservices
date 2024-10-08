﻿using MediatR;
using Services.Order.Application.Dtos;
using Shared.Dtos;
using System.Collections.Generic;

namespace Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
