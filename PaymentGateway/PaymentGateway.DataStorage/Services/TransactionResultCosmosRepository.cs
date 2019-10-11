using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using PaymentGateway.DataStorage.Models;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.DataStorage.Services
{
    public class TransactionResultCosmosRepository : ITransactionResultRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Lazy<Task<Container>> _container;

        public TransactionResultCosmosRepository(
            CosmosClient cosmosClient,
            IOptionsMonitor<CosmosDBOptions> options)
        {
            _cosmosClient = cosmosClient;

            _container = new Lazy<Task<Container>>(async () => { return await CreateOrGetContainerAsync(_cosmosClient, options); });
        }

        public static async Task<Container> CreateOrGetContainerAsync(
            CosmosClient cosmosClient,
            IOptionsMonitor<CosmosDBOptions> monitoringOptions)
        {
            CosmosDBOptions options = monitoringOptions.CurrentValue;

            var databaseResponse =
                   await cosmosClient.CreateDatabaseIfNotExistsAsync(options.CosmosDBName);

            var containerResponse =
                await databaseResponse
                    .Database
                    .CreateContainerIfNotExistsAsync(
                        options.CosmosCollectionName,
                        "/PaymentId",
                        options.CosmosCollectionRUCap);

            return containerResponse.Container;
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
