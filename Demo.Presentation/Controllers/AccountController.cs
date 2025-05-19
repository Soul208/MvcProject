using Demo.DataAccess.Moodels.IdentityModel;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Demo.Presentation.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;




namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicatonUser> _userManager 
        , SignInManager<ApplicatonUser> _signInManager ,
        IMailService _mailService,
        ISmsService _smsService
        ) : Controller
    {

        //register
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);


            var User = new ApplicatonUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,

            };
          var Result = _userManager.CreateAsync(User , viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }
        }

        #endregion

        #region Login

        [HttpGet]

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if(User is not null)
            {
               bool Flag =  _userManager.CheckPasswordAsync(User, viewModel.Password).Result;
                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Loced Out");
                    if(Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
                return View(viewModel);
        }

        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(prop , GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return RedirectToAction("Index", "Home");
        }

        #endregion


        [HttpGet]
        public IActionResult Signout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
               if (User is not null)
               {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("RestPassword", "Account", new { email = viewModel.Email , Token } , Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink
                    };
                    //EmailsSetting.SendEmail(email);
                    _mailService.Send(email);
                    return RedirectToAction(nameof(CheckYourInbox));
               }

            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);


        }

        [HttpPost]
        public IActionResult SendResetPasswordLinkSms(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("RestPassword", "Account", new { email = viewModel.Email, Token }, Request.Scheme);
                    //var email = new Email()
                    //{
                    //    To = viewModel.Email,
                    //    Subject = "Reset Password",
                    //    Body = ResetPasswordLink
                    //};
                    //EmailsSetting.SendEmail(email);
                    //_mailService.Send(email);

                    var sms = new SmsMessage
                    {
                        Body = ResetPasswordLink,
                        PhoneNumber = User.PhoneNumber
                    };

                    _smsService.SendSms(sms);
                    return Ok("Check Your SMS");
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);


        }

        public IActionResult CheckYourInbox() => View();

        [HttpGet]
        public IActionResult RestPassword(string email , string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public IActionResult RestPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            string email = TempData["email"] as string ?? string.Empty;
            string Token = TempData["Token"] as string ?? string.Empty;

            var User =_userManager.FindByEmailAsync(email).Result;
            if (User != null)
            { 
               var Result = _userManager.ResetPasswordAsync(User, Token , viewModel.Password).Result;
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty , error.Description);
                }

            }
            return View(nameof(RestPassword) , viewModel);

        }
    }
}
