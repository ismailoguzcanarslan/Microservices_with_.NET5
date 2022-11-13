using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _connection.ExecuteAsync("DELETE FROM discount WHERE id=@Id", new { Id = id });

            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Error("Something went wrong", 500);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("SELECT * FROM discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<dynamic>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _connection.QueryAsync("SELECT * FROM discount WHERE userid=@UserId AND code=@Code", new { UserId = userId, Code = code })).FirstOrDefault();

            if (discount == null)
                return Response<dynamic>.Error("Discount Not Found", 404);
            return Response<dynamic>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetDiscount(int id)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
                return Response<Models.Discount>.Error("Discount Not Found", 404);
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var status = await _connection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", new {UserId = discount.UserId, Rate = discount.Rate, Code = discount.Code});

            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Error("Something went wrong", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _connection.ExecuteAsync("UPDATE discount SET userid=@Userid, code=@Code, rate=@Rate WHERE id=@Id", new {Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate});

            if (status > 0)
                return Response<NoContent>.Success(204);
            return Response<NoContent>.Error("Something went wrong", 500);
        }
    }
}
