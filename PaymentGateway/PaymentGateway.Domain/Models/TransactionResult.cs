using PaymentGateway.Domain.Models;

namespace PaymentGateway.Models
{
    public class TransactionResult
    {
        public PaymentStatusCode Code { get; set; }
        public string BankId { get; set; }
        public string PaymentId { get; set; }
        Transaction TransactionDetails { get; set; }
    }
}
