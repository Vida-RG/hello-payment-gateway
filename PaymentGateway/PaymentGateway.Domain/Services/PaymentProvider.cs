using PaymentGateway.Domain.Infrastructure;

namespace PaymentGateway.Domain.Services
{
    public class PaymentProvider : IPamentProvider
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
    }
}
