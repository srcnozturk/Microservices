using Course.Web.Models.FakePayments;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceiverPayment(PaymentInfoInput paymentInfoInput);
    }
}
