using FreeCourse.Web.Models;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> Get(string code);
    }
}
