using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient client, IDiscountService discountService)
        {
            _client = client;
            _discountService = discountService;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get();
            
            if(basket != null)
            {
                if(!basket.BasketItems.Any(x=>x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel()
                {
                    BasketItems = new List<BasketItemViewModel>() { basketItemViewModel },

                };
            }

            await SaveOrUpdate(basket);
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        /// <param name="discountCode"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<bool> AppliedDiscount(string discountCode)
        {
            await CancelDiscount();

            var basket = await Get();

            if(basket == null || basket.DiscountCode == null)
                return false;

            var hasDiscount = await _discountService.Get(discountCode);

            if (hasDiscount == null)
                return false;

            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode = discountCode;
            
            await SaveOrUpdate(basket);

            return true;
        }
        
        public async Task<bool> CancelDiscount()
        {
            var basket = await Get();

            if (basket == null || basket.DiscountCode == null)
                return false;

            basket.DiscountCode = null;

            await SaveOrUpdate(basket);

            return true;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();

            if(basket == null)
                return false;

            var deleteResult = basket.BasketItems.Remove(basket.BasketItems.First(x => x.CourseId == courseId));

            if (!deleteResult)
                return false;

            if (!basket.BasketItems.Any())
                basket.DiscountCode = null;

            await SaveOrUpdate(basket);

            return true;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _client.GetAsync("baskets");

            if (response.IsSuccessStatusCode)
            {
                var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
                return basketViewModel.Data;
            }

            return null;
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var response = await _client.PostAsJsonAsync<BasketViewModel>("baskets", basketViewModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete()
        {
            var result = await _client.DeleteAsync("baskets");

            return result.IsSuccessStatusCode;
        }
    }
}
