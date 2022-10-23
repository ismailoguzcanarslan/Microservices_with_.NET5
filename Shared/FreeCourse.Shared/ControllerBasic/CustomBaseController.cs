using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared.ControllerBasic
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInsance<T>(Response<T> response) {


        }

    }
}
