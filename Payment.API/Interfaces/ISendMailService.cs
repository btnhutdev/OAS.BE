

using Core.ViewModel;

namespace Payment.API.Interfaces
{
    public interface ISendMailService
    {
        Task SendEmailPaymentSuccessToBidderAsync(MailMessage message, PaymentViewModel detail, string productName);
        Task SendEmailPaymentSuccessToAuctioneerAsync(MailMessage message, MailModel detail);
    }
}
