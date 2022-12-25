using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetCourseByIdAsync(courseId);

            var basketItemViewModel = new BasketItemViewModel()
            {
                CourseId = courseId,
                CourseName = course.Name,
                Price = course.Price,
                Quantity = 1
            };

            await _basketService.AddBasketItem(basketItemViewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AppliedDiscount(DiscountApplyInput discountApplyInput)
        {
            var discountStatus = await _basketService.AppliedDiscount(discountApplyInput.Code);

            TempData["discountStatus"] = discountStatus;
        
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelAppliedDiscount()
        {
            await _basketService.CancelDiscount();

            return RedirectToAction(nameof(Index));
        }
    }
}
