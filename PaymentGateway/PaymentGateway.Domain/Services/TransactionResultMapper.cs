using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
    public class TransactionResultMapper : ITransactionResultMapper
    {
        public const string CARD_MASK = "****-****-****-";

        public TransactionResultQuery Map(TransactionResult result)
        {
            if (result == null)
            {
                throw new System.ArgumentNullException(nameof(result));
            }

            var cardNumber = result.TransactionDetails.CardNumber;
            var lastFourDigit =
                cardNumber.Substring(cardNumber.Length - 5);

            return new TransactionResultQuery
            {
                Code = result.Code,
                PaymentId = result.PaymentId,
                TransactionDetails = new Transaction
                {
                    Amount = result.TransactionDetails.Amount,
                    CardHolderName = result.TransactionDetails.CardHolderName,
                    CardNumber = $"{CARD_MASK}{lastFourDigit}",
                    CurrencyISO4217Code = result.TransactionDetails.CurrencyISO4217Code,
                    CVVNumber = null,
                    ExperyDate = result.TransactionDetails.ExperyDate
                }
            };
        }

        public TransactionResult Map(PaymentState paymentResult, Transaction transaction)
        {
            if (paymentResult == null)
            {
                throw new System.ArgumentNullException(nameof(paymentResult));
            }

            if (transaction == null)
            {
                throw new System.ArgumentNullException(nameof(transaction));
            }

            return new TransactionResult
            {
                BankId = paymentResult.Id,
                Code = (PaymentStatusCode)paymentResult.Code,
                TransactionDetails = transaction
            };
        }
    }
}
