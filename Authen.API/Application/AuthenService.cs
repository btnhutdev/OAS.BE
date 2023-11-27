using Authen.API.Interfaces;
using Core.Constant;
using Core.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authen.API.Application
{
    public class AuthenService : IAuthenService
    {
        #region constructor
        private readonly IAuthenRepository _authenRepository;
        public IConfiguration _configuration;

        public AuthenService(IConfiguration configuration, IAuthenRepository authenRepository)
        {
            _authenRepository = authenRepository;
            _configuration = configuration;
        }
        #endregion

        #region Authen
        public async Task<string> Authen(LoginViewModel loginViewModel, string type)
        {
            var result = await _authenRepository.Login(loginViewModel, type).ConfigureAwait(false);

            if (result)
            {
                var user = await _authenRepository.GetUserByUsername(loginViewModel.Username).ConfigureAwait(false);

                //Create Claims for JWT
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Username)
                };

                if (user.PermissionName == PermissionNameConst.Admin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, PermissionNameConst.Admin));
                }
                else if (user.PermissionName == PermissionNameConst.Auctioneer)
                {
                    claims.Add(new Claim(ClaimTypes.Role, PermissionNameConst.Auctioneer));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, PermissionNameConst.Bidder));
                }

                //Create SecurityKey
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:Key"]));

                //Create SigningCredentials
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Create JWT with SigningCredentials
                var token = new JwtSecurityToken(
                    _configuration["JwtToken:Issuer"],
                    _configuration["JwtToken:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                //Return JWT string
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return string.Empty;
        }
        #endregion
    }
}
