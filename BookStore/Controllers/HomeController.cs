using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
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
        private readonly UserContext _userContext;//Projections
        private readonly EventContext _eventContext;

        

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            UserContext userContext,
            EventContext eventContext
            
            )
        {
            _logger = logger;
            _configuration = configuration;
            _userContext = userContext;
            _eventContext = eventContext;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.BookList = new List<BookModel>();
            var store = await _userContext.Users.SingleOrDefaultAsync(u => u.id == "bookstore");
            if (store != null)
            {
                ViewBag.BookList = store.Books;
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Reserved()
        {
            var user = await GetCurrentUser();
            return View(user);
        }

        public async Task<IActionResult> ReserveBook(string id)
        {
            
            return RedirectToAction("index");
        }





        ///Get current logged user's stock.
        public async Task<UserModel> GetCurrentUser() {
            var loggedUser = User.FindFirstValue(ClaimTypes.Name);
            var userStock = await _userContext.Users.SingleOrDefaultAsync(u => u.id == loggedUser);
            if (userStock != null)
            {
                return userStock;
            }
            else
            {
                var newUser = new UserModel()
                {
                    id = loggedUser,
                    Books = new List<BookModel>()
                };
                await _userContext.Users.AddAsync(newUser);
                await _userContext.SaveChangesAsync();
                userStock = await _userContext.Users.SingleOrDefaultAsync(u => u.id == loggedUser);
                return newUser;
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}