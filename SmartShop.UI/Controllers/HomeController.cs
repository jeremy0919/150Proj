using Microsoft.AspNetCore.Mvc;
using SmartShop.UI.Models;
using System.Diagnostics;

namespace SmartShop.UI.Controllers
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new List<Recipe>()
            {
                new Recipe()
                {
                    Name = "Test1",
                    Description = "Really goood meal"
                },
                new Recipe ()
                {
                    Name = "Test 2",
                    Description = "Kinda Gross"
                },
                new Recipe()
                {
                    Name = "Test 3",
                    Description = "Pick Me!"
                }
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MealPlan()
        {
            return View();
        }

        public IActionResult Recipe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}