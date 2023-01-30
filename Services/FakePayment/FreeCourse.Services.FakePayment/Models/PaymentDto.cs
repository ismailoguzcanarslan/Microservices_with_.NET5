namespace FreeCourse.Services.FakePayment.Models
{
    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNo { get; set; }
        public string Expiration { get; set; }
        public string CCV { get; set; }
        public decimal Price { get; set; }
        public OrderDto Order { get; set; }
    }
}
