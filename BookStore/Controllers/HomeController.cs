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


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.books = new List<BookModel>();
            var endpoint = _configuration["CosmosEndpoint"];
            var key = _configuration["CosmosKey"];
            if (endpoint != null && key != null)
            {
                using (var bookContext = new BookContext(endpoint, key))
                {
                    if (bookContext.Books != null)
                    {
                        ViewBag.books = await bookContext.Books.ToListAsync();

                    }
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