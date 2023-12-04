using Authen.API.Interfaces;
using AutoMapper;
using Core.Constant;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : Controller
    {
        #region constructor
        private readonly IAuthenService _authenService;

        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }
        #endregion

        #region AdminAuthen
        [HttpPost("AdminAuthen")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminAuthen([FromBody] LoginViewModel user)
        {
            string result = await _authenService.Authen(user, PermissionTypeConst.Admin).ConfigureAwait(false);

            if (!result.IsNullOrEmpty())
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region AuctioneerAuthen
        [HttpPost("AuctioneerAuthen")]
        [AllowAnonymous]
        public async Task<IActionResult> AuctioneerAuthen([FromBody] LoginViewModel user)
        {
            string result = await _authenService.Authen(user, PermissionTypeConst.Auctioneer).ConfigureAwait(false);

            if (!result.IsNullOrEmpty())
            {
                return new OkObjectResult(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region BidderAuthen
        [HttpPost("BidderAuthen")]
        [AllowAnonymous]
        public async Task<IActionResult> BidderAuthen([FromBody] LoginViewModel user)
        {
            string result = await _authenService.Authen(user, PermissionTypeConst.Bidder).ConfigureAwait(false);

            if (!result.IsNullOrEmpty())
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
