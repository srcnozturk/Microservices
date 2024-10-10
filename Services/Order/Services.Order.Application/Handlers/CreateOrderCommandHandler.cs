using MediatR;
using Services.Order.Application.Commands;
using Services.Order.Application.Dtos;
using Services.Order.Domain.OrderAggregate;
using Services.Order.Infrastructure;
using Shared.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext db;

        public CreateOrderCommandHandler(OrderDbContext db)
        {
            this.db = db;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode,
                request.Address.Line);

            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = order.Id },200);
        }
    }
}
