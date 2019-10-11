using System.Threading;
using System.Threading.Tasks;
using PaymentGateway.Domain.Infrastructure;

namespace PaymentGateway.BankAccess.Services
{
    public class BankClientService : IBankClientService
    {
        private readonly BankClient _bankClient;

        public BankClientService(BankClient bankClient)
        {
            _bankClient = bankClient;
        }

        public async Task<PaymentState> PostAsync(PaymentRequest paymentRequest, CancellationToken? cancellationToken)
        {
            return await _bankClient.PostAsync(paymentRequest, cancellationToken ?? CancellationToken.None);
        }
    }
}
