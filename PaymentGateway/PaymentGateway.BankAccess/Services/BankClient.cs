using Microsoft.Extensions.Options;
using PaymentGateway.BankAccess.Models;

namespace PaymentGateway.BankAccess.Services
{
    public partial class BankClient
    {
        public BankClient(
            System.Net.Http.HttpClient httpClient,
            IOptionsMonitor<BankClientOptions> optionsMonitor)
            : this(optionsMonitor.CurrentValue.BankClinetUrl, httpClient)
        {
        }
    }
}
