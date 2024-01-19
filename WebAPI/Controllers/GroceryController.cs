using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GroceryController : ControllerBase
    {
        private readonly ILogger<GroceryController> _logger;
        private readonly SmartShopContext _dbContext;

        public GroceryController(ILogger<GroceryController> logger, SmartShopContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        //[HttpGet(Name = "GetGroceryList")]
        //public GroceryItem[] List()
        //{
        //    var groceryItems = _dbContext.Set<GroceryItem>()
        //        .ToArray();
        //    return groceryItems;
        //}


        //[HttpGet(Name = "GetGroceryItem")]
        //public ActionResult<GroceryItem> Item(Guid id)
        //{
        //    var item = _dbContext.Set<GroceryItem>()
        //        .SingleOrDefault(x => x.Id == id);

        //    if(item == null) { return NotFound(); }

        //    return item;
        //}



        //[HttpPost(Name = "AddItem")]
        //public StatusCodeResult Add(GroceryItem item)
        //{
        //    if (item == null) { return BadRequest(); }

        //    _dbContext.Add(item);
        //    _dbContext.SaveChanges();
        //    return Ok();
        //}
    }


}
