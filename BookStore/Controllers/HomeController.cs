using BookStore.Data;
using BookStore.Models;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly BookContext _bookContext;
        private readonly Dictionary<string, IList<IEvent>> _eventContext;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, BookContext bookContext, Dictionary<string, IList<Event>> eventContext)
        {
            _logger = logger;
            _configuration = configuration;
            _bookContext = bookContext;
            _eventContext = eventContext;
        }



        public async Task<IActionResult> Index()
        {
            ViewBag.books = new List<BookModel>();
            if (_bookContext.Books != null)
            {
                ViewBag.books = await _bookContext.Books.ToListAsync();
            }

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