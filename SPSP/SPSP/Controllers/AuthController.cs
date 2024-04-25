using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPSP.Models.Request.Auth;
using SPSP.Services.UserAccount;

namespace SPSP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController 
    {
        readonly IUserAccountService userAccountService;
        readonly ILogger<AuthController> logger;

        public AuthController(ILogger<AuthController> logger, IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        [HttpPost("login")]
        public async Task<Models.UserAuthInfo> Login([FromBody] LoginRequest loginRequest)
        {
            return await userAccountService.Login(loginRequest.Username, loginRequest.Password);
        }

    }
}
