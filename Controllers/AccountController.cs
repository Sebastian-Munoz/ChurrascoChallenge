using Microsoft.AspNetCore.Mvc;
using ChurrascoChallenge.Data;
using ChurrascoChallenge.Models;
using Microsoft.EntityFrameworkCore;
using ChurrascoChallenge.ViewModels;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            User? user = await _appDbContext.Users.Where(u => (u.EMail == model.Username || u.UserName == model.Username) && u.Password == model.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                ViewData["Message"] = "Invalid Credentials";   
                return View(); 
            }
            else
            {
                if (user.Role == "ADMIN" && user.Active == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Message"] = "You don't have permissions"; 
                    return View();
                }
            }
        }
    }
}