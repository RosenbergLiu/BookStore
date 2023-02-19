using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _configuration;//Keys
        private readonly UserContext _userContext;//Projections database
        private readonly EventContext _eventContext;//Evnets database



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
            await SaveEvent(id, 1);
            await ApplyEvent(id, 1);

            return RedirectToAction("index");
        }







        //Get current logged user's projection.
        public async Task<UserModel?> GetCurrentUser()
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.Name);
            var userStock = await _userContext.Users.SingleOrDefaultAsync(u => u.id == loggedUser);
            if (userStock == null)
            {
                var newUser = new UserModel()
                {
                    id = loggedUser,
                    Books = new List<BookModel>()
                };
                await _userContext.Users.AddAsync(newUser);
                await _userContext.SaveChangesAsync();
                userStock = await _userContext.Users.SingleOrDefaultAsync(u => u.id == loggedUser);
            }
            return userStock;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task SaveEvent(string BookId, int Quantity)
        {

            //Save event to event database
            var loggedUser = User.FindFirstValue(ClaimTypes.Name);
            var newEvent = new EventModel()
            {
                Id = Guid.NewGuid(),
                BookId = BookId,
                Quantity = Quantity,
                DateTime = DateTime.UtcNow,
                UserId = loggedUser
            };
            await _eventContext.Events.AddAsync(newEvent);
            await _eventContext.SaveChangesAsync();
        }

    }
}