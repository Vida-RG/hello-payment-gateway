namespace PaymentGateway.DataStorage.Models
{
    public class CosmosDBOptions
    {
        public string CosmosEndpoint { get; set; }
        public string CosmosAccessKey { get; set; }
        public string CosmosDBName { get; set; }
        public string CosmosCollectionName { get; set; }
        public int CosmosCollectionRUCap { get; set; }
    }
}
