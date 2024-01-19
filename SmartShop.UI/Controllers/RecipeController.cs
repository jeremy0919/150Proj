using Microsoft.AspNetCore.Mvc;
using Models;

namespace SmartShop.UI.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Suggestion()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=5");


            return View("Recipe", rawJsonData);
        }

        public async Task<IActionResult> Catalogue()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=10");


            return View("Index", rawJsonData);
        }

        public async Task<IActionResult> Search()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Recipe/Random?count=10");


            return View("Index", rawJsonData);
        }

        public async Task<IActionResult> ViewRecipe(string id)
        {
            try
            {
                var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");
                ///                                                          Recipe/GetRecipe?id=641730
                var recipeData = await smartShopClient.GetStringAsync($"/api/Recipe/GetRecipe?id={id}");

                // This is a brutal sloppy last second hack, we really should have a page that accepts a single recipie without being in a json list
                recipeData = "{\"recipes\":[" + recipeData + "]}";
                //return PartialView("RecipePartial", recipeData);
                return Json(recipeData);
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or redirect to an error page
                Console.WriteLine("Error: " + ex.Message);
                return RedirectToAction("Error");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recipe()
        {
            return View();
        }
    }
}
