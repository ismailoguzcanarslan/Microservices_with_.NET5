namespace FreeCourse.Web.Models
{
    public class PaymentInfoInput
    {
        public string CardName { get; set; }
        public string CardNo { get; set; }
        public string Expiration { get; set; }
        public string CCV { get; set; }
        public decimal Price { get; set; }
    }
}
