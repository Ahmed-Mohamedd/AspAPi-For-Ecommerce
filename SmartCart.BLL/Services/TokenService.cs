using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace SmartCart.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JWT _jwt;

        public TokenService(UserManager<AppUser> userManager , IOptions<JWT> jwt , IConfiguration configuration)
        {
            _userManager=userManager;
            _configuration=configuration;
            _jwt=jwt.Value;
        }
        public async Task<string> GetToken(AppUser user)
        {
            var UserClaims = await _userManager.GetClaimsAsync(user);
            var UserRoles = await _userManager.GetRolesAsync(user);
            var RoleClaims = new List<Claim>();

            foreach (var role in UserRoles)
                RoleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub , user.DisplayName),
                new Claim (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email , user.Email),
                new Claim ("uid" , user.Id),
            }
            .Union(RoleClaims)
            .Union(UserClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredintials = new SigningCredentials(symmetricSecurityKey , SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpiresOn"])),
                signingCredentials: signingCredintials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                
        }
    }
}
