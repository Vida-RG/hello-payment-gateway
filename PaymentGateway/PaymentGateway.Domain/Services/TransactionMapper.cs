using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public class TransactionMapper : ITransactionMapper
    {
        public PaymentRequest Map(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException(nameof(transaction));
            }

            return new PaymentRequest
            {
                Amount = (double)transaction.Amount,
                CardHolderName = transaction.CardHolderName,
                CardNumber = transaction.CardNumber,
                CurrencyISO4217Code = transaction.CurrencyISO4217Code,
                CvvNumber = transaction.CVVNumber,
                ExperyDate = transaction.ExperyDate
            };
        }
    }
}
