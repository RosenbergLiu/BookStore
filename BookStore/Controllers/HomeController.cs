using BookStore.Data;
using BookStore.Models;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, BookContext bookContext)
        {
            _logger = logger;
            _configuration = configuration;
            _bookContext = bookContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.books = new List<BookModel>();
                {
                    if (_bookContext.Books != null)
                    {
                        ViewBag.books = await _bookContext.Books.ToListAsync();

                    }
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