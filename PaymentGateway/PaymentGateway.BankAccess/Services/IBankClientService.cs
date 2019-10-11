using System.Threading.Tasks;

namespace PaymentGateway.BankAccess.Services
{
    public interface IBankClientService
    {
        Task<PaymentState> PostAsync(PaymentRequest paymentRequest, System.Threading.CancellationToken? cancellationToken);
    }
}
