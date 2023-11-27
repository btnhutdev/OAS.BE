using Microsoft.AspNetCore.Mvc;
using Search.API.Interfaces;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        #region Constructor
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        #endregion

        #region GetListCategory
        [HttpGet("GetListCategory")]
        public async Task<IActionResult> GetListCategory()
        {
            var result = await _searchService.GetListCategory().ConfigureAwait(false);

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region GetListProductAuctioningByCategory
        [HttpGet("GetListProductAuctioningByCategory/{id}")]
        public async Task<IActionResult> GetListProductAuctioningByCategory(Guid id)
        {
            var result = await _searchService.GetListProductAuctioningByCategory(id).ConfigureAwait(false);

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region SearchNameProduct
        [HttpGet("SearchNameProduct")]
        public async Task<IActionResult> SearchNameProduct()
        {
            var result = await _searchService.SearchNameProduct();

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region SearchProductByProductName
        [HttpGet("SearchProductByProductName/{productName}")]
        public async Task<IActionResult> SearchProductByProductName(string productName)
        {
            var result = await _searchService.SearchProductByProductName(productName);

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region SearchProductByProductName
        [HttpGet("SearchByProductNameAndCategory/{categoryId}/{selectedOptions}")]
        public async Task<IActionResult> SearchByProductNameAndCategory(Guid categoryId, string selectedOptions)
        {
            List<string> optionsList = new List<string>();

            if (selectedOptions == Core.Constant.SearchNameConst.All)
            {
                optionsList.Add(Core.Constant.SearchNameConst.All);
            }
            else
            {
                optionsList.AddRange(selectedOptions.Split(new[] { "&option=" }, StringSplitOptions.RemoveEmptyEntries));
            }

            var result = await _searchService.SearchByProductNameAndCategory(categoryId, optionsList);

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
