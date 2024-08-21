using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Order.Application.Dtos;
using Services.Order.Application.Mapping;
using Services.Order.Application.Queries;
using Services.Order.Infrastructure;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers
{
    class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext db;

        public GetOrdersByUserIdQueryHandler(OrderDbContext db)
        {
            this.db = db;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await db.Orders.Include(o=> o.OrderItems).Where(x=> x.BuyerId == request.UserId).ToListAsync();
            if (!orders.Any()) return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
