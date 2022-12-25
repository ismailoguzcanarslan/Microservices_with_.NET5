using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _identityService;

        public OrderService(HttpClient client, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _client = client;
            _paymentService = paymentService;
            _basketService = basketService;
            _identityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var payment = new PaymentInfoInput(){
                CardName = checkoutInfoInput.CardName,
                CardNo = checkoutInfoInput.CardNo,
                CCV = checkoutInfoInput.CCV,
                Expiration = checkoutInfoInput.Expiration,
                Price = basket.TotalPrice
            };

            var result = await _paymentService.ReceivePayment(payment);

            if (!result)
            {
                return new OrderCreatedViewModel()
                {
                    ErrorMessage = "Something went wrong on payment.",
                    IsSuccess = false,
                };
            }

            var address = new AddressCreateInput()
            {
                District = checkoutInfoInput.District,
                Line = checkoutInfoInput.Line,
                Province = checkoutInfoInput.Province,
                Street = checkoutInfoInput.Street,
                ZipCode = checkoutInfoInput.ZipCode
            };

            var order = new OrderCreateInput()
            {
                Address = address,
                BuyerId = _identityService.GetUserId,
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput()
                {
                    ProductId = x.CourseId,
                    Price = x.Price,
                    PictureUrl = "",
                    ProductName = x.CourseName,
                };

                order.OrderItems.Add(orderItem);
            });

            var response = await _client.PostAsJsonAsync<OrderCreateInput>("orders", order);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel()
                {
                    ErrorMessage = "Something went wrong on order create.",
                    IsSuccess = false,
                };
            }

            var responseData = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            await _basketService.Delete();

            return new OrderCreatedViewModel()
            {
                IsSuccess = true,
                Id = responseData.Data.Id
            };
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _client.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
