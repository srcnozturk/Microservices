using Course.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetByCourseId(string courseId);
        Task<bool> AddCourseAsync(CourseCreate courseCreate);
        Task<bool> UpdateCourseAsync(CourseUpdate courseUpdate);
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
