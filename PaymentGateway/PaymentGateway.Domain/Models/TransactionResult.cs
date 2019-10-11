namespace PaymentGateway.Domain.Models
{
    public class TransactionResult
    {
        public PaymentStatusCode Code { get; set; }
        public string BankId { get; set; }
        public string PaymentId { get; set; }
        public Transaction TransactionDetails { get; set; }
    }
}
