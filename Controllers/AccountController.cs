using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ITIL.Data;
using ITIL.Data.Domain;
using ITIL.Model;
using ITIL.Sys;
using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{ 
    public class AccountController : Controller
    {
            public ITILDbContext DbContext {get;set;}
            public AccountController(ITILDbContext dbContext)
            {
                DbContext = dbContext;
            }
            
        [HttpPost("/Account/Login")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var email = user.Email?.ToLower();

            var currentUser = DbContext.Users.SingleOrDefault(p => p.Email == email && p.Password == user.Password);

            if (currentUser == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            // Create claims for the authenticated user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                // Add any additional claims as needed
            };

            // Create the identity
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Authenticate the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity));

            return Ok(new
            {
                userId = currentUser.Id
            });
        }


        [HttpPost("/Account/Register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var email = user.Email?.ToLower();

            var currenUser = DbContext.Users.SingleOrDefault(p => p.Email == email);

            if(currenUser != null) 
            {
                throw new BusinessException($"El email {email} ya está registrado.");
            }

            DbContext.Users.Add(new User
            {
                Email = email,
                Password = user.Password
            });

            DbContext.SaveChanges();
                
            return Accepted();
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}