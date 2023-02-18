using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;//for accessing keys
        private readonly UserContext _userContext;//for record the stock states of each users
        private readonly UserManager<IdentityUser> _userManager;



        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            UserContext userContext,
            UserManager<IdentityUser> userManager
            )
        {
            _logger = logger;
            _configuration = configuration;
            _userContext = userContext;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.BookList = new List<BookModel>();
            var store = await _userContext.Users.FirstOrDefaultAsync(u => u.id == "bookstore");
            if (store != null)
            {
                ViewBag.BookList = store.Books;
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Reserved()
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.LoggedUser = loggedUser;
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.id == loggedUser);
            if(user != null)
            {
                return View(user);
            }
            else//if user stock record doesn't exist, create one with empty book list
            {
                UserModel newUser = new UserModel() {
                    id = loggedUser,
                    Books= new List<BookModel>()
                };
                await _userContext.Users.AddAsync(newUser);
                await _userContext.SaveChangesAsync();
                return RedirectToAction("Reserved");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBook()
        {




            return RedirectToAction("index");
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}