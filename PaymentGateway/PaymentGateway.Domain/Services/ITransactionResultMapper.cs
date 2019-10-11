using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public interface ITransactionResultMapper
    {
        TransactionResultQuery Map(TransactionResult result);
    }
}
