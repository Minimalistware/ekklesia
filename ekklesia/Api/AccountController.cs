using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;

namespace ekklesia.Api
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email
                    , model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var token = "";
                    return Ok(token);
                }
                return BadRequest("Usuário ou senha inválidos.");
            }

            return BadRequest("Usuário ou senha inválidos.");
        }

        private async Task<string> GenerateJwt(string email)
        {

            var user = await userManager.FindByEmailAsync(email);
            var key = Encoding
                .ASCII
                .GetBytes("d0bb5f009f2d661495fb32df3a979c04629489d01a143f5e");

        }
    }


}
