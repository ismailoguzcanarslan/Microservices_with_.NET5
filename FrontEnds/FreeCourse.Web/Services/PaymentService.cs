using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _client;

        public PaymentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _client.PostAsJsonAsync<PaymentInfoInput>("payments", paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
