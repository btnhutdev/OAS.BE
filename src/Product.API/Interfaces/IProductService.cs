using Core.ViewModel;
using Domain.Entities;
using System;

namespace Product.API.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetDetail(Guid? id);
        Task<ProductViewModel> GetDetailProductForAuctioneer(Guid? id);
        Task<DetailProductBidderViewModel> GetDetailProductForBidder(Guid? id);
        Task<IList<ProductViewModel>> GetListProductAllStatusByAuctioneerID(Guid? userId);
        Task<IList<ProductViewModel>> GetListProductAuctioning();
        Task<IList<ProductViewModel>> GetListProductByAdmin(string type);
        Task<IList<DetailProductBidderViewModel>> GetListWinnerProductByBidder(Guid? bidderId);
        Task<IList<HistoryAuctionViewModel>> GetDetaiHistoryAuctionByAdmin(Guid? productId);
        Task<DetailProductBidderViewModel> GetDetailWinnerProductByBidder(Guid? bidderId, Guid? productId);
        Task<bool> SendMailNotHighestPrice(string email, Guid productId, float currentPrice, float yourPrice);
        Task<bool> IsProductAuctioning(Guid? id);
        bool Approve(Guid id);
        bool Reject(Guid id, string message);
        Task<bool> Create(ProductViewModel productViewModel);
        Task<bool> Update(ProductViewModel productViewModel);
    }
}
