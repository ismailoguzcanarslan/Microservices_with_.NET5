using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Error("Basket Could Not Found", 500);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var isExistedBasket = await _redisService.GetDatabase().StringGetAsync(userId);

            if (string.IsNullOrEmpty(isExistedBasket))
                return Response<BasketDto>.Error("Basket Not Found", 404);
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(isExistedBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Error("Basket Could Not Update Or Save", 500);
        }
    }
}
