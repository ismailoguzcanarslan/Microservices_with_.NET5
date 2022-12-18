using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> Upload(IFormFile photo);
        Task<bool> Delete(string photoUrl);
    }
}
