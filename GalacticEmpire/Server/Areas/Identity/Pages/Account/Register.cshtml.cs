using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using GalacticEmpire.Domain.Models.UserModel.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MediatR;
using GalacticEmpire.Application.Features.User.Commands;
using GalacticEmpire.Application.Features.User.Queries;

namespace GalacticEmpire.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMediator mediator;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.mediator = mediator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Felhasználónév:")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Birodalom neve:")]
            public string EmpireName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email:")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A jelszó minimum {2} és maximum {1} karakter hosszú lehet.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Jelszó:")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Jelszó mégegyszer:")]
            [Compare("Password", ErrorMessage = "A két jelszó nem egyezik meg.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new RegisterUserCommand.Command
                {
                    RegisterDto = new Shared.Dto.User.RegisterDto
                    {
                        UserName = Input.UserName,
                        Email = Input.Email,
                        EmpireName = Input.EmpireName,
                        Password = Input.Password,
                        ConfirmPassword = Input.ConfirmPassword
                    }
                });
                if (result.Succeeded)
                {
                    _logger.LogInformation("Új felhasználó létrehozva.");
                    var user = await mediator.Send(new GetUserByEmailAndUsernameQuery.Query { Email = Input.Email, UserName = Input.UserName });

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Email cím megerősítése",
                        $"Kérlek erősítsd meg az email címedet. <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Kattints ide</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
