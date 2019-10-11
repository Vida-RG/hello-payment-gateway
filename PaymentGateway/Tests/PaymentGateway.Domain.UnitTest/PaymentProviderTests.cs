using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.UnitTest
{
    [TestClass]
    public class PaymentProviderTests
    {

        [TestMethod]
        public void GetDetailsOfPayment_NullId_ThrowsNullArgumentException()
        {
            var bankClientMoq = new Mock<IBankClientService>();
            var repositoryMoq = new Mock<ITransactionResultRepository>();
            var mapperMoq = new Mock<ITransactionResultMapper>();

            var provider = new PaymentProvider(
                bankClientMoq.Object,
                repositoryMoq.Object,
                mapperMoq.Object);

            bool thrown = false;
            try
            {
                var result = provider.GetDetailsOfPayment(null).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ArgumentNullException)
                {
                    thrown = true; 
                }
            }

            Assert.IsTrue(thrown);
        }


        [TestMethod]
        public void GetDetailsOfPayment_IdNotGuid_ThrowsArgumentException()
        {
            var bankClientMoq = new Mock<IBankClientService>();
            var repositoryMoq = new Mock<ITransactionResultRepository>();
            var mapperMoq = new Mock<ITransactionResultMapper>();

            var provider = new PaymentProvider(
                bankClientMoq.Object,
                repositoryMoq.Object,
                mapperMoq.Object);

            bool thrown = false;
            try
            {
                var result = provider.GetDetailsOfPayment(null).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ArgumentException)
                {
                    thrown = true; 
                }
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void GetDetailsOfPayment_IdNotFound_ReturnsNull()
        {
            var bankClientMoq = new Mock<IBankClientService>();
            var repositoryMoq = new Mock<ITransactionResultRepository>();
            repositoryMoq
                .Setup(moq => moq.GetTransactionByID(It.IsAny<string>()))
                .Returns(Task.FromResult<TransactionResult>(null));

            var mapperMoq = new Mock<ITransactionResultMapper>();

            var provider = new PaymentProvider(
                bankClientMoq.Object,
                repositoryMoq.Object,
                mapperMoq.Object);

            var result = provider.GetDetailsOfPayment(Guid.Empty.ToString()).Result;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDetailsOfPayment_IdFound_ReturnsTransactionQueryResult()
        {
            var bankClientMoq = new Mock<IBankClientService>();
            var repositoryMoq = new Mock<ITransactionResultRepository>();
            repositoryMoq
                .Setup(moq => moq.GetTransactionByID(It.IsAny<string>()))
                .Returns(Task.FromResult<TransactionResult>(new TransactionResult()));

            var mapperMoq = new Mock<ITransactionResultMapper>();
            mapperMoq
                .Setup(moq => moq.Map(It.IsAny<TransactionResult>()))
                .Returns(new TransactionResultQuery());

            var provider = new PaymentProvider(
                bankClientMoq.Object,
                repositoryMoq.Object,
                mapperMoq.Object);

            var result = provider.GetDetailsOfPayment(Guid.Empty.ToString()).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Pay_NullTransaction_ThrowsNullArgumentException()
        {
            var bankClientMoq = new Mock<IBankClientService>();
            var repositoryMoq = new Mock<ITransactionResultRepository>();
            var mapperMoq = new Mock<ITransactionResultMapper>();

            var provider = new PaymentProvider(
                bankClientMoq.Object,
                repositoryMoq.Object,
                mapperMoq.Object);

            bool thrown = false;
            try
            {
                var result = provider.Pay(null).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ArgumentNullException)
                { 
                    thrown = true;
                }
            }

            Assert.IsTrue(thrown);
        }

    }
}
