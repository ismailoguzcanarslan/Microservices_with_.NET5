using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();

        Task<List<CourseViewModel>> GetAllByUserAsync(string userId);

        Task<CourseViewModel> GetCourseByIdAsync(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);

        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);

        Task<bool> DeleteCourseAsync(string courseId);

        Task<List<CategoryViewModel>> GetCategoriesAsync();
    }
}
