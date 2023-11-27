using Core.ViewModel;

namespace Search.API.Interfaces
{
    public interface ISearchRepository
    {
        Task<IList<CategoryViewModel>> GetListCategory();
        Task<IList<ProductViewModel>> GetListProductAuctioningByCategory(Guid categoryId);
        Task<IList<string>> SearchNameProduct();
        Task<IList<ProductViewModel>> SearchProductByProductName(string productName);
        Task<IList<ProductViewModel>> SearchByProductNameAndCategory(Guid categoryId, List<string> optionsList);
    }
}
