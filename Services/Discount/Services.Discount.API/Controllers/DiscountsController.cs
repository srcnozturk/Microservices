using Microsoft.AspNetCore.Mvc;
using Services.Discount.API.Services;
using Shared.ControllerBases;
using Shared.Services;
using System.Threading.Tasks;

namespace Services.Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private  readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        { 
            var discount= await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }
        
        [HttpGet]
        // Action method adı 
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;
            var discount= await _discountService.GetByCodeAndUserId(code, userId);

            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount  discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }


    }
}
