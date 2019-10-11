using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public interface ITransactionMapper
    {
        PaymentRequest Map(Transaction transaction);
    }
}
