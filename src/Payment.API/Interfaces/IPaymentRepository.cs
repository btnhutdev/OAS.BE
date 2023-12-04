using Core.ViewModel;
using Infrastructure.Contexts;

namespace Payment.API.Interfaces
{
    public interface IPaymentRepository
    {
        Task<bool> Create(PaymentViewModel payment);
    }
}
