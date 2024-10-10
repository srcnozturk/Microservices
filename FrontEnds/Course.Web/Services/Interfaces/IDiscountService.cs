using Course.Web.Models.Discount;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
