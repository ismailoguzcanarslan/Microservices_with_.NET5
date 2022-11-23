using FreeCourse.Shared.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
