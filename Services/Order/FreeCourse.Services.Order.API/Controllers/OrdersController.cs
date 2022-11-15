using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared.ControllerBasic;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _service;

        public OrdersController(IMediator mediator, ISharedIdentityService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {

            var response = await _mediator.Send(new GetOrdersByUserIdQuery() { UserId = _service.GetUserId});
        
            return CreateActionResultInsance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);

            return CreateActionResultInsance(response);
        }
    }
}
