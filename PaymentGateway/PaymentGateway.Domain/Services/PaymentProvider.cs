using System.Threading.Tasks;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public class PaymentProvider : IPaymentProvider
    {
        private readonly IBankClientService _bankClientService;
        private readonly ITransactionResultRepository _transactionResultRepository;

        public PaymentProvider(
            IBankClientService bankClientService,
            ITransactionResultRepository transactionResultRepository)
        {
            _bankClientService = bankClientService;
            _transactionResultRepository = transactionResultRepository;
        }

        public Task<TransactionResultQuery> GetDetailsOfPayment(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> Pay(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}
