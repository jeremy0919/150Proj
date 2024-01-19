using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Serialization;
using Data;
using Models;
namespace WebAPI.Controllers
{
    public class RecpieListResult
    {
        [JsonPropertyName("recipes")]
        public List<Recipe> Recipes { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SmartShopContext _dbContext;



        public RecipeController(IHttpClientFactory httpClientFactory, SmartShopContext dbContext)
        {
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a specific Recipe
        /// </summary>
        /// <param name="id">GUID id of the profile to retrieve.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetRecipe(string id)
        {
            var client = _httpClientFactory.CreateClient("SpoonacularClient");
            HttpResponseMessage response = await client.GetAsync($"/recipes/{id}/information");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                await response.Content.ReadFromJsonAsync<RecpieListResult>();
                return responseBody;
            }
            else
            {
                // Handle error if the request is not successful
                return $"Error: {response.StatusCode}";
            }
        }


        [HttpGet]
        public async Task<string> Random(int count = 10)
        {
            var client = _httpClientFactory.CreateClient("SpoonacularClient");
            HttpResponseMessage response = await client.GetAsync($"/recipes/random?limitLicense=true&number={count}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                //var test = await response.Content.ReadFromJsonAsync<RecpieListResult>();



                return responseBody;
            }
            else
            {
                // Handle error if the request is not successful
                return $"Error: {response.StatusCode}";
            }
        }


        [HttpGet]
        public async Task<string> IngredientMatching(string ingredientsCsv, int count = 10)
        {
            // TODO: If we use the complex search we can include diet along with ingredient matching

            var client = _httpClientFactory.CreateClient("SpoonacularClient");
            HttpResponseMessage response = await client.GetAsync($"/recipes/findByIngredients?ingredients={ingredientsCsv}&limitLicense=true&ranking=1&ignorePantry=false&number={count}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                // Handle error if the request is not successful
                return $"Error: {response.StatusCode}";
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>> ByDiet(string dietcsv, int count = 10)
        {
            var client = _httpClientFactory.CreateClient("SpoonacularClient");
            try
            {
                //{{baseUrl}}/recipes/complexSearch?query=burger&cuisine=italian&excludeCuisine=greek&diet=vegetarian&intolerances=gluten&equipment=pan&includeIngredients=tomato,cheese&excludeIngredients=eggs&type=main course&instructionsRequired=true&fillIngredients=false&addRecipeInformation=false&addRecipeNutrition=false&author=coffeebean&tags=ipsum ea proident amet occaecat&recipeBoxId=2468&titleMatch=Crock Pot&maxReadyTime=20&ignorePantry=false&sort=calories&sortDirection=asc&minCarbs=10&maxCarbs=100&minProtein=10&maxProtein=100&minCalories=50&maxCalories=800&minFat=1&maxFat=100&minAlcohol=0&maxAlcohol=100&minCaffeine=0&maxCaffeine=100&minCopper=0&maxCopper=100&minCalcium=0&maxCalcium=100&minCholine=0&maxCholine=100&minCholesterol=0&maxCholesterol=100&minFluoride=0&maxFluoride=100&minSaturatedFat=0&maxSaturatedFat=100&minVitaminA=0&maxVitaminA=100&minVitaminC=0&maxVitaminC=100&minVitaminD=0&maxVitaminD=100&minVitaminE=0&maxVitaminE=100&minVitaminK=0&maxVitaminK=100&minVitaminB1=0&maxVitaminB1=100&minVitaminB2=0&maxVitaminB2=100&minVitaminB5=0&maxVitaminB5=100&minVitaminB3=0&maxVitaminB3=100&minVitaminB6=0&maxVitaminB6=100&minVitaminB12=0&maxVitaminB12=100&minFiber=0&maxFiber=100&minFolate=0&maxFolate=100&minFolicAcid=0&maxFolicAcid=100&minIodine=0&maxIodine=100&minIron=0&maxIron=100&minMagnesium=0&maxMagnesium=100&minManganese=0&maxManganese=100&minPhosphorus=0&maxPhosphorus=100&minPotassium=0&maxPotassium=100&minSelenium=0&maxSelenium=100&minSodium=0&maxSodium=100&minSugar=0&maxSugar=100&minZinc=0&maxZinc=100&offset=606&number=10&limitLicense=true
               HttpResponseMessage response = await client.GetAsync($"/recipes/complexSearch?diet={dietcsv}&limitLicense=true&ranking=1&ignorePantry=false&number={count}");

            
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    // Handle error if the request is not successful
                    return $"Error: {response.StatusCode}, Message: {response.Content}";
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>> DietInfo(Guid userId, int count = 10)
        {   
            try
            {
                UriBuilder uriBuilder = new UriBuilder("https://api.spoonacular.com/recipes/complexSearch");
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty); // Initialize the query string

                // Set common query parameters
                query["limitLicense"] = "true";
                query["ranking"] = "1";
                query["ignorePantry"] = "false";
                query["number"] = count.ToString();

                var userInfo = _dbContext.Set<UserProfile>().Find(userId);
                // Dummy User for testing
               /* var userInfo = new UserProfile() {
                    DietTypesJSON = JsonSerializer.Serialize(new string[] { "Vegitarian" })
                };*/
                
                if (userInfo == null)
                {
                    return NotFound(); // Return a 404 Not Found response if the user is not found.
                }

                // Check if the user has dietary restrictions and add them to the query
                if (!string.IsNullOrWhiteSpace(userInfo.DietTypesJSON))
                {
                    var diets = JsonSerializer.Deserialize<string[]>(userInfo.DietTypesJSON);
                    if(diets != null && diets.Any()){
                        query["diet"] = string.Join(",", diets);
                    }
                }

                uriBuilder.Query = query.ToString();
                string apiUrl = uriBuilder.Uri.ToString();

                var client = _httpClientFactory.CreateClient("SpoonacularClient");
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody); // Return a 200 OK response with the response body.
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }


        // public async Task<ActionResult<string>> Allergy(Guid userId, int count = 10)
        //{   
        //    try
        //    {
        //        UriBuilder uriBuilder = new UriBuilder("https://api.spoonacular.com/recipes/complexSearch");
        //        var query = System.Web.HttpUtility.ParseQueryString(string.Empty); // Initialize the query string

        //        // Set common query parameters
        //        query["limitLicense"] = "true";
        //        query["ranking"] = "1";
        //        query["ignorePantry"] = "false";
        //        query["number"] = count.ToString();

        //        var userInfo = _dbContext.Set<UserProfile>().Find(userId);
        //        // Dummy User for testing
        //       /* var userInfo = new UserProfile() {
        //            DietTypesJSON = JsonSerializer.Serialize(new string[] { "Vegitarian" })
        //        };*/
                
        //        if (userInfo == null)
        //        {
        //            return NotFound(); // Return a 404 Not Found response if the user is not found.
        //        }

        //        // Check if the user has dietary restrictions and add them to the query
        //        if (!string.IsNullOrWhiteSpace(userInfo.AllergiesJSON))
        //        {
        //            var allergy = JsonSerializer.Deserialize<string[]>(userInfo.AllergiesJSON);
        //            if(allergy != null && allergy.Any()){
        //                query["allergy"] = string.Join(",", allergy);
        //            }
        //        }

        //        uriBuilder.Query = query.ToString();
        //        string apiUrl = uriBuilder.Uri.ToString();

        //        var client = _httpClientFactory.CreateClient("SpoonacularClient");
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            return Ok(responseBody); // Return a 200 OK response with the response body.
        //        }
        //        else
        //        {
        //            return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
    }
}
