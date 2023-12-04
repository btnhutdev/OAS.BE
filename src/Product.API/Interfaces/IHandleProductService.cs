using Core.ViewModel;
using ProductEntity = Domain.Entities.Product;
namespace Product.API.Interfaces
{
    public interface IHandleProductService
    {
        void StartAuctionTask(ProductEntity product);
        void EndAuctionTask(Guid? IdAuction);
        void SendMailRejectTask(MailModel detail, string reason);
        Task SendMailNotHighestPrice(string email, string productName, string categoryName, float currentPrice, float yourPrice);
    }
}
