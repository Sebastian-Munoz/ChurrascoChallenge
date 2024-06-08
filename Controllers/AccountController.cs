using Microsoft.AspNetCore.Mvc;
using ChurrascoChallenge.Data;
using ChurrascoChallenge.Models;
using Microsoft.EntityFrameworkCore;
using ChurrascoChallenge.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Cryptography;
using System.Text;

namespace ChurrascoChallenge.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;    
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)      
            {
                return RedirectToAction("Index", "Products");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var hashedPassword = HashValue(model.Password); 
            User? user = await _appDbContext.Users.Where(u => (u.email == model.Username || u.username == model.Username) && u.password == hashedPassword).FirstOrDefaultAsync();

            if (user == null)
            {
                ViewData["Message"] = "Invalid Credentials";   
                return View(); 
            }
            else
            {
                if (user.role == "ADMIN" && user.active == 1)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.email)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties
                    );

                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ViewData["Message"] = "You don't have permissions"; 
                    return View();
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private static string HashValue(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}