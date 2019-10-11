using PaymentGateway.BankAccess.Services;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Infrastructure
{
    public interface IBankClientService
    {
        Task<PaymentState> PostAsync(PaymentRequest paymentRequest, System.Threading.CancellationToken? cancellationToken);
    }
}
