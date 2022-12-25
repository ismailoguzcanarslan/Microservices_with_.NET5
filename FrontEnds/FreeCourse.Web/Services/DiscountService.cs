using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _client;

        public DiscountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<DiscountViewModel> Get(string code)
        {
            var response = await _client.GetAsync($"discounts/GetByCode/{code}");

            if (!response.IsSuccessStatusCode)
                return null;

            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

            return discount.Data;
        }
    }
}
