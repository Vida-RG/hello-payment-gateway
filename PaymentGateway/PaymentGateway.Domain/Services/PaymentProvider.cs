using System;
using System.Threading.Tasks;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public class PaymentProvider : IPaymentProvider
    {
        private readonly IBankClientService _bankClientService;
        private readonly ITransactionResultRepository _transactionResultRepository;
        private readonly ITransactionResultMapper _transactionResultMapper;
        private readonly ITransactionMapper _transactionMapper;

        public PaymentProvider(
            IBankClientService bankClientService,
            ITransactionResultRepository transactionResultRepository,
            ITransactionResultMapper transactionResultMapper,
            ITransactionMapper transactionMapper)
        {
            _bankClientService = bankClientService;
            _transactionResultRepository = transactionResultRepository;
            _transactionResultMapper = transactionResultMapper;
            _transactionMapper = transactionMapper;
        }

        public async Task<TransactionResultQuery> GetDetailsOfPayment(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Guid parsed = Guid.Empty;
            if (!Guid.TryParse(id, out parsed))
            {
                throw new ArgumentException("Id has to be a valid guid", nameof(id));
            }

            TransactionResult transactionResult =
                await _transactionResultRepository.GetTransactionByID(id);

            TransactionResultQuery result = null;
            if (transactionResult != null)
            {
                result = _transactionResultMapper.Map(transactionResult);
            }

            return result;
        }

        public async Task<string> Pay(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            PaymentState paymentResult =
                await _bankClientService.PostAsync(_transactionMapper.Map(transaction));

            throw new NotImplementedException();
        }
    }
}
