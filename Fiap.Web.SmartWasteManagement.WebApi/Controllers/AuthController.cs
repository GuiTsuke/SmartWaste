using Fiap.Web.SmartWasteManagement.Models;
using Fiap.Web.SmartWasteManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Web.SmartWasteManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController()
        {
            _authService = new AuthService(); // Em um cenário real, isso deve ser injetado via DI
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            var authenticatedUser = _authService.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(authenticatedUser);
            return Ok(new { Token = token });
        }
        private string GenerateJwtToken(UserModel user)
        {
            byte[] secret = Encoding.UTF8.GetBytes("RmlhcFNtYXJ0V2FzdGVNYW5hZ2VtZW50");
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "fiap",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}