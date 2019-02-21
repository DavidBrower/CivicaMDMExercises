using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VisionWare.TechTest.Common.Models;
using VisionWare.TechTest.Data.Repositories;
using VisionWare.TechTest.Web.Models.Account;

namespace VisionWare.TechTest.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository userRepository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            this.userRepository = new UserRepository(); // IoC normally. Overkill for proj purpose...
        }

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await this.userRepository.FindUserByEmail(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email address has not been registered.");
                return View(model);
            }

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

            return Redirect(GetRedirectUrl(model.ReturnUrl));
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpPost]
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns>The view</returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The view.</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.userRepository.AddUser(model.Name, model.Email);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>The redirect url.</returns>
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
    }
}