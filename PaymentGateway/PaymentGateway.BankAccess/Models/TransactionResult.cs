namespace PaymentGateway.BankAccess.Models
{
    public class TransactionResult
    {
        public PaymentStatusCode Code { get; set; }
        public string Id { get; set; }
    }
}
