using Course.Web.Models.Catalog;
using Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Services;
using System.Threading.Tasks;

namespace Course.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreate courseCreate)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            if (!ModelState.IsValid) return View();

            courseCreate.UserId = _sharedIdentityService.GetUserId;
            await _catalogService.AddCourseAsync(courseCreate);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategoriesAsync();
            if (course == null) RedirectToAction(nameof(Index));

            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            CourseUpdate courseUpdate = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description= course.Description,
                Price = course.Price,
                Feature= course.Feature,
                CategoryId = course.CategoryId,
                UserId= course.UserId,
                Picture= course.Picture,
            };
            return View(courseUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdate courseUpdate)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdate.Id);
            if (!ModelState.IsValid) return View();
            await _catalogService.UpdateCourseAsync(courseUpdate);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
