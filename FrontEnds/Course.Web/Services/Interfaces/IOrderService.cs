using Course.Web.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron İletişim
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        
        /// <summary>
        /// Asenkron iletişim - sipariş bilgileri rabbitMQ'ya gönderilecek
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
