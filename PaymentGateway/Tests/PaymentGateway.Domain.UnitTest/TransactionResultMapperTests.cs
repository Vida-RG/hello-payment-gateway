using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Services;
using System;

namespace PaymentGateway.Domain.UnitTest
{
    [TestClass]
    public class TransactionResultMapperTests
    {
        [TestMethod]
        public void MapTQR_NullTransactionResult_ThrowsArgumentNullException()
        {
            var mapper = new TransactionResultMapper();

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

            var mapper = new TransactionResultMapper();
            var input = new Models.TransactionResult
            {
                Code = (PaymentStatusCode)1,
                BankId = string.Empty,
                PaymentId = string.Empty,
                TransactionDetails = new Models.Transaction
                {
                    Amount = 1.0m,
                    CardHolderName = string.Empty,
                    CardNumber = $"0000-1111-2222-{lastFourDigit}",
                    CurrencyISO4217Code = "EUR",
                    CVVNumber = 123,
                    ExperyDate = new DateTime(1, 1, 1)
                }
            };

            var result = mapper.Map(input);

            Models.Transaction resultTransaction = result.TransactionDetails;
            Models.Transaction inputTransaction = input.TransactionDetails;

            Assert.AreEqual(result.Code, input.Code);
            Assert.AreEqual(result.PaymentId, input.PaymentId);
            Assert.AreEqual(resultTransaction.Amount, inputTransaction.Amount);
            Assert.AreEqual(resultTransaction.CardHolderName, inputTransaction.CardHolderName);
            Assert.AreEqual(resultTransaction.CurrencyISO4217Code, inputTransaction.CurrencyISO4217Code);
            Assert.IsTrue(resultTransaction.ExperyDate.CompareTo(inputTransaction.ExperyDate) == 0);
            Assert.IsNull(resultTransaction.CVVNumber);

            Assert.IsTrue(resultTransaction.CardNumber.StartsWith(TransactionResultMapper.CARD_MASK));
            Assert.IsTrue(resultTransaction.CardNumber.EndsWith(lastFourDigit));
        }

        [TestMethod]
        public void MapTransactionResult_NullTransaction_ThrowsArgumentNullException()
        {
            var mapper = new TransactionResultMapper();

            bool thrown = false;
            try
            {
                var result = mapper.Map(new PaymentState(), null);
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void MapTransactionResult_NullPaymentState_ThrowsArgumentNullException()
        {
            var mapper = new TransactionResultMapper();

            bool thrown = false;
            try
            {
                var result = mapper.Map(null, new Transaction());
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }


        [TestMethod]
        public void MapTransactionResult_NormalTransaction_MapsAndMaskData()
        {
            var mapper = new TransactionResultMapper();
            var tansaction = new Models.Transaction
            {
                Amount = 1.0m,
                CardHolderName = string.Empty,
                CardNumber = "0000-1111-2222-3333",
                CurrencyISO4217Code = "EUR",
                CVVNumber = 123,
                ExperyDate = new DateTime(1, 1, 1)
            };
            var paymentState = new PaymentState
            {
                Code = (PaymentStateCode)1,
                Id = string.Empty
            };

            var result = mapper.Map(paymentState, tansaction);

            Assert.AreEqual(result.Code, PaymentStatusCode.Success);
            Assert.AreEqual(result.BankId, string.Empty);
            Assert.IsNull(result.PaymentId);
            Assert.IsNotNull(result.TransactionDetails);
        }
    }
}
