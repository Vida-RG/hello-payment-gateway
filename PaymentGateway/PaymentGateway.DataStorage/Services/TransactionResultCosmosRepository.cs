using Microsoft.Azure.Cosmos;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Models;

namespace PaymentGateway.DataStorage.Services
{
    public class TransactionResultCosmosRepository : ITransactionResultRepository
    {
        private readonly CosmosClient _cosmosClient;

        public TransactionResultCosmosRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public TransactionResult GetTransactionByID(int transactionId)
        {
            throw new System.NotImplementedException();
        }

        public void InsertTransaction(TransactionResult transaction)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
