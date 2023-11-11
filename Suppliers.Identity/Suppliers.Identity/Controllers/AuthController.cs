using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Suppliers.Identity.Model;
using Suppliers.Identity.Services;

namespace Suppliers.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService, EmailService emailService, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _emailService = emailService;
            _emailService = emailService;
            _configuration = configuration;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel 
            {
                ReturnUrl = returnUrl 
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if(user == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl!);
            }
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(await _userManager.FindByNameAsync(model.Username) != null)
            {
                ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Email = model.Email,
                Organization = model.Organization,
                IsLicenseLoaded = false,
                IsLicensed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Supplier");
            if(result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(model.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                return View("ConfirmRegister", "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            }

            ModelState.AddModelError("", "Error occured");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Redirect(_configuration.GetSection("ClientAddress").Value!);
            else
                return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
