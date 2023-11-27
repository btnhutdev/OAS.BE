using Core.ViewModel;
using Search.API.Interfaces;

namespace Search.API.Application
{

    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<IList<CategoryViewModel>> GetListCategory()
        {
            return await _searchRepository.GetListCategory();
        }

        public async Task<IList<ProductViewModel>> GetListProductAuctioningByCategory(Guid categoryId)
        {
            return await _searchRepository.GetListProductAuctioningByCategory(categoryId);
        }

        public async Task<IList<ProductViewModel>> SearchByProductNameAndCategory(Guid categoryId, List<string> optionsList)
        {
            return await _searchRepository.SearchByProductNameAndCategory(categoryId, optionsList);
        }

        public async Task<IList<string>> SearchNameProduct()
        {
            return await _searchRepository.SearchNameProduct();
        }

        public async Task<IList<ProductViewModel>> SearchProductByProductName(string productName)
        {
            return await _searchRepository.SearchProductByProductName(productName);
        }
    }
}
