using Jwt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Jwt.Controller
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager, IJWTManagerRepository accountRepository)
        {
            _jwtManagerRepository = accountRepository;
            _userManager = userManager;

            _config = config;
        }
        [HttpPost("signup")]
        public IActionResult SignUp(Register model)
        {
            if (ModelState.IsValid)
            {
                var result = _jwtManagerRepository.SignUp(model);
                if (result.Result != null)
                {
                    if (result.Result.IsSuccess) 
                    {
                        return Ok();
                    }
                    return NotFound();
                }
            }
            return NotFound();
        }






        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _jwtManagerRepository.Login(model);
            if (result != null)
            {
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return Unauthorized();
            }
            return Unauthorized();
        }











    }

}

