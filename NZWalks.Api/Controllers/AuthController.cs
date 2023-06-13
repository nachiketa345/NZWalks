using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Post: api/auth/Register


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                //Add Role sto this User
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("Registration was successful! Please Login.");
                    }
                }
            }
            return BadRequest("Something went wrong.");
        }

        //Post: api/Auth/Login

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginRequest = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(loginRequest!=null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(loginRequest, loginRequestDto.Password);

                if(checkPassword)
                {
                    //Create Token
                    var roles = await userManager.GetRolesAsync(loginRequest);

                    if(roles!=null)
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(loginRequest, roles.ToList());
                        return Ok(jwtToken);

                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }

                    
                }
            }
             
            return BadRequest("Username and Password don't match.");
        }
    }
    
}

