using LearningAPIs.Model;
using LearningAPIs.Service.UserAccountService;
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
    public class UserAccountController : ControllerBase
    {

        private readonly ILogger<UserAccountController> _logger;
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(ILogger<UserAccountController> logger, 
            IUserAccountService userAccountService)
        {
            _logger = logger;
            _userAccountService = userAccountService;
        }

        [HttpGet(Name = "UserAccount")]
        [ProducesResponseType(typeof(UserAccount), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<UserAccount> Get(string? username, string? emailAddress) 
        {
            var response = _userAccountService.GetUserAccount(username, emailAddress);
            //return JsonConvert.SerializeObject(_userAccountService.GetUserAccount(username),Formatting.Indented);
            //return _userAccountService.Get(username);
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost(Name = "UserAccount")]
        [ProducesResponseType(typeof(UserAccount), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserAccount> Post(UserAccountRequest request)
        {
            var response = _userAccountService.CreateUserAccount(request);
            
            return Ok(response);
        }

        [HttpGet("{userId}")]        
        public ActionResult<UserAccount> GetUserAccountById ([FromRoute] Guid userId)
        {
            var response = _userAccountService.GetUserAccountById(userId);
            return Ok(response);
        }

        [HttpPatch("{userId}")]
        public ActionResult<UserAccount> UpdateUserAccountById ([FromRoute] Guid userId, [FromBody] UserAccountPatchRequest request)
        {
            if (_userAccountService.GetUserAccountById(userId) == null)
            {
                return BadRequest("Issue with userId");
            }

            var response = _userAccountService.UpdateUserAccount(userId, request);

            return Ok();

        }


    }
}
