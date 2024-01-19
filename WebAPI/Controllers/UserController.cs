using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<GroceryController> _logger;
        private readonly SmartShopContext _dbContext;

        public UserController(SmartShopContext dbContext, ILogger<GroceryController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a user profile based on email address.
        /// </summary>
        /// <param name="email">The email address of the user profile to retrieve</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Guid> FindUserId(string email)
        {
            var id = _dbContext.Set<UserProfile>().SingleOrDefault(m => m.EmailAddress == email)?.Id;

            if(id == null) { return NotFound(); }
            return Ok(id);
        }


        /// <summary>
        /// Retrieves a user profile
        /// </summary>
        /// <param name="id">GUID id of the profile to retrieve.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<UserProfile> GetUser(Guid id)
        {
            var user = _dbContext.Find<UserProfile>(id);

            if (user == null) { return NotFound(); }
            return Ok(user);
        }


        /// <summary>
        /// Creates a new user profile, returning the Id of the newly created resource.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Guid> PostUser(UserProfile model)
        {
            if(model.Id == default(Guid))
                model.Id = Guid.NewGuid();

            if (_dbContext.Set<UserProfile>().Any(m => m.Id == model.Id))
                return BadRequest("A user with this id already exists, use Put to update an existing user");

            _dbContext.Set<UserProfile>().Add(model);
            _dbContext.SaveChanges();

            return Ok(model.Id);
        }

        [HttpPut]
        public ActionResult PutUser(UserProfile model)
        {
            var currentProfile = _dbContext.Find<UserProfile>(model.Id);

            if (currentProfile == null) { return NotFound(); }

            if(!string.IsNullOrWhiteSpace(model.FirstName))
                currentProfile.FirstName = model.FirstName;

            if(!string.IsNullOrWhiteSpace(model.LastName))  
                currentProfile.LastName = model.LastName;

            currentProfile.AllergiesJSON = model.AllergiesJSON;
            currentProfile.DietTypesJSON = model.DietTypesJSON;

            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
