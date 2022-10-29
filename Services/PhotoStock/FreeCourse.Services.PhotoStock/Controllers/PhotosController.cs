using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBasic;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new () { Url = returnPath };

                return CreateActionResultInsance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInsance(Response<PhotoDto>.Error("Photo is empty", 400));
        }

        [HttpDelete]
        [Route("{photourl}")]
        public IActionResult Delete(string photourl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photourl);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

                return Ok();
            }

            return BadRequest();
        }
    }
}
