using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using PersonalNotesAPI.Data;
using PersonalNotesAPI.Models;
using System.Threading.Tasks;

namespace PersonalNotesAPI.Auth
{
    public class AuthTokenUtil
    {
        public static JwtSecurityToken GetJwtToken(string userName, string roleName, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTAuthentication:SecretKey"]));
            
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(JwtRegisteredClaimNames.Sub,userName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                //new Claim("role",roleName)
            };

            var token = new JwtSecurityToken(
                issuer: config["JWTAuthentication:Issuer"],
                audience: config["JWTAuthentication:Audience"],
                claims: claims,             
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );   

            return token;
        }

        public async static Task<TokenVM> GetJwtTokenString(string userName, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            var user = await userManager.FindByNameAsync(userName);
            //var userRole = await userManager.GetRolesAsync(user);
            var token = GetJwtToken(user.UserName, null, config);
            
            return new TokenVM
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                UserEmail = user.Email
            };
        }
    }
}
