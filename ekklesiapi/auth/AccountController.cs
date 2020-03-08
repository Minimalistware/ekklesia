using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ekklesiapi.auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email
                    , model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok(GenerateJwt(model.Email));
                }
                return BadRequest("Usuário ou senha inválidos.");
            }

            return BadRequest("Usuário ou senha inválidos.");
        }

        private string GenerateJwt(string email)
        {
            var rights = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var key = Encoding.UTF8.GetBytes("d0bb5f009f2d661495fb32df3a979c04629489d01a143f5e");
            var symetricKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: "Ekklésia",
                audience: "http://localhost",
                claims: rights,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(1)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }

}
