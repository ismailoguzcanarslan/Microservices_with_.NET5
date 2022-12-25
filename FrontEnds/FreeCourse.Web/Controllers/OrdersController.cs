using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderServcie;
        public OrdersController(IBasketService basketService, IOrderService orderServcie)
        {
            _basketService = basketService;
            _orderServcie = orderServcie;
        }

        public async Task<IActionResult> CheckOut()
        {
            var basket = await _basketService.Get();

            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckoutInfoInput checkoutInfoInput)
        {
            var orderStatus = await _orderServcie.CreateOrder(checkoutInfoInput);

            if (!orderStatus.IsSuccess)
            {
                ViewBag["error"] = orderStatus.ErrorMessage;
                return RedirectToAction(nameof(CheckOut));
            }
            else
            {
                return RedirectToAction(nameof(SuccecfullCheckOut), new {orderId = orderStatus.Id});
            }
        }

        public IActionResult SuccecfullCheckOut(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
