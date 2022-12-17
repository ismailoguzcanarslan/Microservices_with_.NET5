using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {

        private readonly ICatalogService _catalogService;

        private readonly ISharedIdentityService _identityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService identityService)
        {
            _catalogService = catalogService;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllByUserAsync(_identityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetCategoriesAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput input)
        {
            var categories = await _catalogService.GetCategoriesAsync();
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            input.UserId = _identityService.GetUserId;

            await _catalogService.CreateCourseAsync(input);

            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetCourseByIdAsync(id);
            var categories = await _catalogService.GetCategoriesAsync();

            if(course != null)
            {
                CourseUpdateInput courseUpdateInput = new CourseUpdateInput
                {
                    UserId = course.UserId,
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Feature = course.Feature,
                    CategoryId = course.CategoryId,
                    Description = course.Description,
                    Picture = course.Picture,
                };

                ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);

                return View(courseUpdateInput);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput input)
        {
            var categories = await _catalogService.GetCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", input.Id);
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(input);

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction("Index", "Course");
        }
    }
}
