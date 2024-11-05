using Course.Web.Models.FakePayments;
using Course.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ReceiverPayment(PaymentInfoInput paymentInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
