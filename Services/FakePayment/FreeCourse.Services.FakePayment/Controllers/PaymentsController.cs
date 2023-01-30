using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.ControllerBasic;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _provider;

        public PaymentsController(ISendEndpointProvider provider)
        {
            _provider = provider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var send = await _provider.GetSendEndpoint(new System.Uri("queue:order-service"));

            var createOrderMessage = new CreateOrderMessageCommand();

            createOrderMessage.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessage.Street = paymentDto.Order.Address.Street;
            createOrderMessage.District = paymentDto.Order.Address.District;
            createOrderMessage.Province = paymentDto.Order.Address.Province;
            createOrderMessage.Line = paymentDto.Order.Address.Line;

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessage.OrderItems.Add(new OrderItem()
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                });
            });

            await send.Send<CreateOrderMessageCommand>(createOrderMessage);

            return CreateActionResultInsance(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
