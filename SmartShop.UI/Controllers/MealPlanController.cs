using Microsoft.AspNetCore.Mvc;

namespace SmartShop.UI.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MealPlanController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<IActionResult> MealPlanList()
        {
            // Retrieve our local user, if not existing, create a new user profile
            var smartShopClient = _httpClientFactory.CreateClient("SmartShopClient");

            var rawJsonData = await smartShopClient.GetStringAsync($"/api/Grocery/List");

            return View("Index", rawJsonData);
        }

        public IActionResult Index()
        {
            // we want to access the back end api

            var client = _httpClientFactory.CreateClient("SmartShopClient");


            return View();
        }
    }
}
