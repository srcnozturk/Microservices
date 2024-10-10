using Services.Basket.API.Dtos;
using Shared.Dtos;
using System.Threading.Tasks;

namespace Services.Basket.API.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> DeleteBasket(string userId);
    }
}
