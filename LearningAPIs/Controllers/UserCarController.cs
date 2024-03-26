using LearningAPIs.Model;
using LearningAPIs.Service.UserCarService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace LearningAPIs.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class UserCarController : Controller
    {
        private readonly ILogger<UserCarController> _logger;
        private readonly IUserCarService _userCarService;

        public UserCarController(ILogger<UserCarController> logger, IUserCarService userCarService)
        {
            _logger = logger;
            _userCarService = userCarService;
        }

        [HttpPost(Name = "UserCar")]
        [ProducesResponseType(typeof(UserCar), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> CreateUserCar (UserCarRequest request)
        {
            return Ok();
        }

        [HttpDelete(Name = "{carId}")]
        public ActionResult<bool> DeleteUserCar ([FromRoute] string carId)
        {
            return Ok();
        }

        [HttpGet(Name = "UserCar")]
        public ActionResult<List<UserCar>> GetUserCars(Guid userId)
        {
            return Ok();
        }

        [HttpGet("{carId}")]
        public ActionResult<UserCar> GetUserCarById([FromRoute] string carId) 
        {
            return Ok();
        }

    }
}
