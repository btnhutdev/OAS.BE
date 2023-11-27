using Core.Constant;
using Core.ViewModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Product.API.Interfaces;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        #region Constructor
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Approve
        [HttpGet("Approve/{id}")]
        public IActionResult Approve(Guid id)
        {
            var result = _productService.Approve(id);

            if (result != null && result == true)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Reject
        [HttpGet("Reject/{id}/{message}")]
        public IActionResult Reject(Guid id, string message)
        {
            var result = _productService.Reject(id, message);

            if (result != null && result == true)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region GetDetail
        [HttpGet("GetDetail/{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var result = await _productService.GetDetail(id).ConfigureAwait(false);

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

        #region GetDetailProductForAuctioneer
        [HttpGet("GetDetailProductForAuctioneer/{id}")]
        public async Task<IActionResult> GetDetailProductForAuctioneer(Guid id)
        {
            var result = await _productService.GetDetailProductForAuctioneer(id).ConfigureAwait(false);

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

        #region GetDetailProductForBidder
        [HttpGet("GetDetailProductForBidder/{id}")]
        public async Task<IActionResult> GetDetailProductForBidder(Guid id)
        {
            var result = await _productService.GetDetailProductForBidder(id).ConfigureAwait(false);

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

        #region GetListProcessingProductByAdmin
        [HttpGet("GetListProcessingProductByAdmin")]
        public async Task<IActionResult> GetListProcessingProductByAdmin()
        {
            var result = await _productService.GetListProductByAdmin(StatusNameConst.Processing).ConfigureAwait(false);

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

        #region GetListHistoryAuctionByAdmin
        [HttpGet("GetListHistoryAuctionByAdmin")]
        public async Task<IActionResult> GetListHistoryAuctionByAdmin()
        {
            var result = await _productService.GetListProductByAdmin(StatusNameConst.Sold).ConfigureAwait(false);

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

        #region GetDetaiHistoryAuctionByAdmin
        [HttpGet("GetDetaiHistoryAuctionByAdmin/{id}")]
        public async Task<IActionResult> GetDetaiHistoryAuctionByAdmin(Guid id)
        {
            var result = await _productService.GetDetaiHistoryAuctionByAdmin(id).ConfigureAwait(false);

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

        #region GetListProductAllStatusByAuctioneerID
        [HttpGet("GetListProductAllStatusByAuctioneerID/{id}")]
        public async Task<IActionResult> GetListProductAllStatusByAuctioneerID(Guid id)
        {
            var result = await _productService.GetListProductAllStatusByAuctioneerID(id).ConfigureAwait(false);

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

        #region GetListWinnerProductByBidder
        [HttpGet("GetListWinnerProductByBidder/{id}")]
        public async Task<IActionResult> GetListWinnerProductByBidder(Guid id)
        {
            var result = await _productService.GetListWinnerProductByBidder(id).ConfigureAwait(false);

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

        #region GetDetailWinnerProductByBidder
        [HttpGet("GetDetailWinnerProductByBidder/{bidderId}/{productId}")]
        public async Task<IActionResult> GetDetailWinnerProductByBidder(Guid bidderId, Guid productId)
        {
            var result = await _productService.GetDetailWinnerProductByBidder(bidderId, productId).ConfigureAwait(false);

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

        #region GetListProductAuctioning
        [HttpGet("GetListProductAuctioning")]
        public async Task<IActionResult> GetListAuctioning()
        {
            var result = await _productService.GetListProductAuctioning().ConfigureAwait(false);

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

        #region IsProductAuctioning
        [HttpGet("IsProductAuctioning/{auctionId}")]
        public async Task<IActionResult> IsProductAuctioning(Guid auctionId)
        {
            var result = await _productService.IsProductAuctioning(auctionId).ConfigureAwait(false);

            if (result)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] ProductViewModel content)
        {
            var result = await _productService.Create(content).ConfigureAwait(false);

            if (result)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromForm] ProductViewModel content)
        {
            var result = await _productService.Update(content).ConfigureAwait(false);

            if (result)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region SendMailNotHighestPrice
        [HttpGet("SendMailNotHighestPrice/{email}/{productId}/{currentPrice}/{yourPrice}")]
        public async Task<IActionResult> SendMailNotHighestPrice(string email, Guid productId, float currentPrice, float yourPrice)
        {
            var result = await _productService.SendMailNotHighestPrice(email, productId, currentPrice, yourPrice).ConfigureAwait(false);

            if (result)
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
