using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.DTOs.ApplicationUserDto;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace AbySalto.Mid.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
                return BadRequest("Username is taken");

            var applicationUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Gender  = registerDto.Gender,
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok(new RegisterResponseDto
            {
                Username = applicationUser.UserName,
                AccessToken = await _tokenService.GenerateJwtToken(applicationUser),
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Gender = applicationUser.Gender
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (applicationUser is null)
                return Unauthorized("Invalid username!");

            var result = await _userManager.CheckPasswordAsync(applicationUser, loginDto.Password);

            if (!result)
                return Unauthorized("Invalid password!");

            return new LoginResponseDto
            {
                Username = applicationUser.UserName,
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Gender = applicationUser.Gender,
                AccessToken = await _tokenService.GenerateJwtToken(applicationUser),              
            };
        }

        [HttpGet("current-user-info")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
            {
                return Unauthorized();
            }

            var applicationUser = await _userManager.FindByIdAsync(userId);

            if(applicationUser == null)
            {
                return NotFound();
            }

            var currentUserDto = new
            {
                applicationUser.FirstName,
                applicationUser.LastName,
                applicationUser.Email,
                applicationUser.UserName,
                // ...
            };

            return Ok(currentUserDto);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
