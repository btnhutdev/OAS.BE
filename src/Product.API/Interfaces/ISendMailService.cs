using Core.ViewModel;
using Product.API.Application;
using Product.API.Utilities;

namespace Product.API.Interfaces
{
    public interface ISendMailService
    {
        Task SendMailSuccessAsync(MailMessage message, MailModel detail);
        Task SendMailFailedAsync(MailMessage message, MailModel detail);
        Task SendMailApproveTask(MailMessage message, MailModel detail);
        Task SendMailRejectTask(MailMessage message, MailModel detail, string reason);
        Task SendMailNotHighestPrice(MailMessage message, string productName, string categoryName, float currentPrice, float yourPrice);
    }
}
