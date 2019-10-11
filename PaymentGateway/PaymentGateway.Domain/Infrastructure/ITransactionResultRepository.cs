using PaymentGateway.Domain.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Infrastructure
{
    public interface ITransactionResultRepository : IDisposable
    {
        Task<TransactionResult> GetTransactionByID(int transactionId);

        Task InsertTransaction(TransactionResult transaction);
    }
}
