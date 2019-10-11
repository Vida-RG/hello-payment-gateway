using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Services;
using System;

namespace PaymentGateway.Domain.UnitTest
{
    [TestClass]
    public class TransactionMapperTest
    {
        [TestMethod]
        public void MapTQR_NullTransactionResult_ThrowsArgumentNullException()
        {
            var mapper = new TransactionMapper();

            bool thrown = false;
            try
            {
                var result = mapper.Map(null);
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void MapTQR_NormalTransaction_MapsAndMaskData()
        {
            const string lastFourDigit = "3333";

            var mapper = new TransactionMapper();
            Models.Transaction inputTransaction = new Models.Transaction
            {
                Amount = 1.0m,
                CardHolderName = string.Empty,
                CardNumber = $"0000-1111-2222-{lastFourDigit}",
                CurrencyISO4217Code = "EUR",
                CVVNumber = 123,
                ExperyDate = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Local)
            };

            PaymentRequest paymentRequest = mapper.Map(inputTransaction);

            Assert.AreEqual((decimal)paymentRequest.Amount, inputTransaction.Amount);
            Assert.AreEqual(paymentRequest.CardHolderName, inputTransaction.CardHolderName);
            Assert.AreEqual(paymentRequest.CurrencyISO4217Code, inputTransaction.CurrencyISO4217Code);
            Assert.IsNotNull(paymentRequest.ExperyDate);
            Assert.AreEqual(paymentRequest.CvvNumber, inputTransaction.CVVNumber);
            Assert.AreEqual(paymentRequest.CardNumber, inputTransaction.CardNumber);
        }
    }
}
