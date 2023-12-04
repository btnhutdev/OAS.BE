using Core.ViewModel;
using Payment.API.Interfaces;

namespace Payment.API.Application
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> Create(PaymentViewModel payment)
        {
            return await _paymentRepository.Create(payment).ConfigureAwait(false);
        }
    }
}
