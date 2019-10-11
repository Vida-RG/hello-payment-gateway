﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using PaymentGateway.DataStorage.Models;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Domain.Models;
using System.Threading.Tasks;

namespace PaymentGateway.DataStorage.Services
{
    public class TransactionResultCosmosRepository : ITransactionResultRepository
    {
        private const string PRATITION_KEY = "/PaymentId";

        private readonly CosmosClient _cosmosClient;
        private readonly IOptionsMonitor<CosmosDBOptions> _optionsMonitor;

        private Container _container;

        public TransactionResultCosmosRepository(
            CosmosClient cosmosClient,
            IOptionsMonitor<CosmosDBOptions> options)
        {
            _cosmosClient = cosmosClient;
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
                        PRATITION_KEY,
                        options.CosmosCollectionRUCap);

            return containerResponse.Container;
        }

        public async Task<TransactionResult> GetTransactionByID(int transactionId)
        {
            if (_container == null)
            {
                _container = await CreateOrGetContainerAsync(_cosmosClient, _optionsMonitor);
            }

            return await _container.ReadItemAsync<TransactionResult>(
                transactionId.ToString(),
                new PartitionKey(PRATITION_KEY));
        }

        public async Task InsertTransaction(TransactionResult transaction)
        {
            if (_container == null)
            {
                _container = await CreateOrGetContainerAsync(_cosmosClient, _optionsMonitor);
            }

            await _container.CreateItemAsync(transaction);
        }

        public void Dispose()
        {
        }
    }
}
