using Course.Web.Models.FakePayments;
using Course.Web.Models.Orders;
using Course.Web.Services.Interfaces;
using Shared.Dtos;
using Shared.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly HttpClient _httpclient;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpclient, IBasketService basketService,
            IPaymentService paymentService, ISharedIdentityService sharedIdentityService)
        {
            _httpclient = httpclient;
            _basketService = basketService;
            _paymentService = paymentService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceiverPayment(paymentInfoInput);
            if (!responsePayment) return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccessfull = false };

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode,
                }
            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput
                {
                    ProductId = x.CourseId,
                    Price = x.GetCurrentPrice,
                    PictureUrl = "",
                    ProductName = x.CourseName,
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });
            var response = await _httpclient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if(!response.IsSuccessStatusCode) return new OrderCreatedViewModel() { Error = "Sipariş oluşturalamadı!", IsSuccessfull = false };

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessfull = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpclient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
