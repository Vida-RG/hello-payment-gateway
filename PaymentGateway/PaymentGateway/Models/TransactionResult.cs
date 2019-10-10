using PaymentGateway.Domain.Models;

namespace PaymentGateway.Models
{
    public class TransactionResultQuery
    {
        public PaymentStatusCode Code { get; set; }
        public string GatewayId { get; set; }
        Transaction TransactionDetails { get; set; }
    }
}
