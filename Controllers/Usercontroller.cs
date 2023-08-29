using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authorization
{
    [ApiController]
    [Route("/home")]
    
    public class UserController : ControllerBase
    {
        TokenService _tokenService;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpGet(Name = "Get hello world")]
        public ActionResult Get()
        {
            return Ok("Hello World");
        }
        [HttpPost("Login")]
        public ActionResult Login([FromBody] User user)
        {
            if(user.Name == "John") {
                var token = _tokenService.GenerateJwtToken(user);
                return Ok(new { Token = token});
            }

            return Unauthorized();
        }
        [Authorize]
        [HttpGet("GetAuthorized")]
        public ActionResult GetAuthorized() {
            return Ok("You are now authorized");
        }
    }
}