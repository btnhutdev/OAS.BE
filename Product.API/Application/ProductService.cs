using Core.ViewModel;
using Product.API.Interfaces;

namespace Product.API.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public bool Approve(Guid id)
        {
            return _productRepository.Approve(id);
        }

        public bool Reject(Guid id, string message)
        {
            return _productRepository.Reject(id, message);
        }

        public async Task<ProductViewModel> GetDetail(Guid? id)
        {
            return await _productRepository.GetDetail(id).ConfigureAwait(false);
        }

        public async Task<ProductViewModel> GetDetailProductForAuctioneer(Guid? id)
        {
            return await _productRepository.GetDetailProductForAuctioneer(id).ConfigureAwait(false);
        }

        public async Task<DetailProductBidderViewModel> GetDetailProductForBidder(Guid? id)
        {
            return await _productRepository.GetDetailProductForBidder(id).ConfigureAwait(false);
        }

        public async Task<IList<ProductViewModel>> GetListProductAuctioning()
        {
            return await _productRepository.GetListProductAuctioning().ConfigureAwait(false);
        }

        public async Task<IList<ProductViewModel>> GetListProductAllStatusByAuctioneerID(Guid? userId)
        {
            return await _productRepository.GetListProductAllStatusByAuctioneerID(userId).ConfigureAwait(false);
        }

        public async Task<IList<ProductViewModel>> GetListProductByAdmin(string type)
        {
            return await _productRepository.GetListProductByAdmin(type).ConfigureAwait(false);
        }

        public async Task<IList<DetailProductBidderViewModel>> GetListWinnerProductByBidder(Guid? bidderId)
        {
            return await _productRepository.GetListWinnerProductByBidder(bidderId).ConfigureAwait(false);
        }

        public async Task<bool> IsProductAuctioning(Guid? id)
        {
            return await _productRepository.IsProductAuctioning(id).ConfigureAwait(false);
        }

        public async Task<bool> Create(ProductViewModel productViewModel)
        {
            return await _productRepository.Create(productViewModel).ConfigureAwait(false);
        }

        public async Task<bool> Update(ProductViewModel productViewModel)
        {
            return await _productRepository.Update(productViewModel).ConfigureAwait(false);
        }

        public async Task<DetailProductBidderViewModel> GetDetailWinnerProductByBidder(Guid? bidderId, Guid? productId)
        {
            return await _productRepository.GetDetailWinnerProductByBidder(bidderId, productId).ConfigureAwait(false);
        }

        public async Task<IList<HistoryAuctionViewModel>> GetDetaiHistoryAuctionByAdmin(Guid? productId)
        {
            return await _productRepository.GetDetaiHistoryAuctionByAdmin(productId).ConfigureAwait(false);
        }

        public async Task<bool> SendMailNotHighestPrice(string email, Guid productId, float currentPrice, float yourPrice)
        {
            return await _productRepository.SendMailNotHighestPrice(email, productId, currentPrice, yourPrice).ConfigureAwait(false);
        }
    }
}
