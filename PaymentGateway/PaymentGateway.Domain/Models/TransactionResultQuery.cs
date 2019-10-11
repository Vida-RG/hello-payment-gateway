namespace PaymentGateway.Domain.Models
{
    public class TransactionResultQuery
    { 
        public PaymentStatusCode Code { get; set; }
        public string PaymentId { get; set; }
        public Transaction TransactionDetails { get; set; }
    }
}
