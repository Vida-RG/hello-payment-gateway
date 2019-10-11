using PaymentGateway.Domain.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Services
{
    public interface IPaymentProvider
    {
        Task<TransactionResultQuery> GetDetailsOfPayment(string id);

        Task<string> Pay(Transaction transaction);
    }
}
