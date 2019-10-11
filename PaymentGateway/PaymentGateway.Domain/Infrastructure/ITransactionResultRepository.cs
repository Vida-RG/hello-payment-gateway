using PaymentGateway.Domain.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Infrastructure
{
    public interface ITransactionResultRepository
    {
        Task<TransactionResult> GetTransactionByID(int transactionId);

        Task InsertTransaction(TransactionResult transaction);
    }
}
