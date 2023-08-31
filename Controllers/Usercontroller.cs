using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authorization
{
    [ApiController]
    [Route("/home")]
    
    public class UserController : ControllerBase
    {
        TokenService _tokenService; 
        UserRepository _userRepository;

        public UserController(TokenService tokenService,UserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        [HttpGet(Name = "Gethelloworld")]
        public ActionResult Get()
        {
            return Ok("Hello World");
        }
        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            var returnedUser =  await _userRepository.GetUserByNameAsync(user.Name);

            if(returnedUser != null) {
                var token = _tokenService.GenerateJwtToken(user);
                return Ok(new { Token = token});
            }
            return Unauthorized();
        }
        [Authorize]
        [HttpGet("GetAuthorized")]
        public ActionResult GetAuthorized()
        {
            return Ok("You are now authorized");
        }
    }
}