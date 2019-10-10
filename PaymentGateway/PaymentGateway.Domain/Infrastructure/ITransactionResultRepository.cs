using System;

namespace PaymentGateway.Domain.Infrastructure
{
    public interface ITransactionResultRepository : IDisposable
    {
        PaymentGateway.Models.TransactionResult GetTransactionByID(int transactionId);

        void InsertTransaction(PaymentGateway.Models.TransactionResult transaction);
    }
}
