using System.Threading.Tasks;
using ekklesia.Models.ViewModels;
using ekklesia.Utils.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email não confirmado.");
                }

                //var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                var succeeded = await signInManager.UserManager.CheckPasswordAsync(user, model.Password);

                if (succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("List", "Member");
                }
                else { ModelState.AddModelError(string.Empty, "Tentativa de login inválida."); }

            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    try
                    {
                        await SendConfirmationEmail(user);
                    }
                    catch (System.Exception ex)
                    {

                        ViewBag.ErrorMessage = ex.Message;
                        return View("Error");
                    }


                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("ListUser", "Account");
                    }
                    return View("ConfirmEmail");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View();
        }

        private async Task SendConfirmationEmail(IdentityUser user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account"
                , new { token, email = user.Email }
                , Request.Scheme);

            var message = $"<p>Esta mensagem foi enviada automaticamente. Por favor não responda esta mensagem.</p>" +
                                $"<br/>" +
                                $"<a href='{confirmationLink}'>Clique aqui para confirmar seu email</a>";

            await emailSender.SendEmailAsync(user.Email, "Link de confirmação", message);
        }

        private async Task SendForgotPasswordEmail(IdentityUser user)
        {
            // Generate the reset password token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Build the password reset link
            var passwordResetLink = Url.Action(nameof(ResetPassword), "Account",
                    new { token, email = user.Email }
                    , Request.Scheme);

            var message = $"<p>Esta mensagem foi enviada automaticamente. Por favor não responda esta mensagem.</p>" +
                                $"<br/>" +
                                $"<a href='{passwordResetLink}'>Clique aqui para redefinir a sua senha</a>";


            await emailSender.SendEmailAsync(user.Email, "Link de redefinição", message);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null || token == null)
            {
                ViewBag.ErrorMessage = $"O email {email} ou token {token} é inválido.";
                return View("Error");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers", "Account");
            }

            ViewBag.ErrorMessage = "Não foi possível confirmar o seu email.";
            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null || token == null)
            {
                ViewBag.ErrorMessage = $"O email {email} ou token {token} é inválido.";
                return View("Error");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers", "Account");
            }

            ViewBag.ErrorMessage = "Não foi possível confirmar o seu email.";
            return View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null || model.Token == null)
                {
                    ViewBag.ErrorMessage = $"O email {model.Email} ou token {model.Token} é inválido.";
                    return View("Error");
                }

                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Account");
                }

                ViewBag.ErrorMessage = "Não foi possível redefinir a sua senha";
                return View("Error");
            }

            return View(model);

        }




        [HttpGet]
        public ViewResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {

                    try
                    {
                        await SendForgotPasswordEmail(user);
                    }
                    catch (System.Exception ex)
                    {

                        ViewBag.ErrorMessage = ex.Message;
                        return View("Error");
                    }


                    // Send the user to Forgot Password Confirmation view
                    return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }
    }
}