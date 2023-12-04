using Core.ViewModel;

namespace Payment.API.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> Create(PaymentViewModel payment);
    }
}
