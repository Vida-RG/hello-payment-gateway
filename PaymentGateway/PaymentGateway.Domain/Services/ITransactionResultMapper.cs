using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public interface ITransactionResultMapper
    {
        TransactionResultQuery Map(TransactionResult result);

        TransactionResult Map(PaymentState paymentResult, Transaction transaction);
    }
}
