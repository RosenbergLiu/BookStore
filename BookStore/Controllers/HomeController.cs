using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookStore.Controllers
{
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



        public async Task<IActionResult> Index()
        {

            var bookList = new List<BookModel>();
            var store = await _userContext.Users.FirstOrDefaultAsync(u => u.id == "bookstore");
            if (store != null)
            {
                var books = store.Books;
                if (books != null)
                {
                    foreach (var book in books)
                    {
                        bookList.Add(book);
                    }
                }
            }
            ViewBag.BookList = bookList;

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Reserve()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}