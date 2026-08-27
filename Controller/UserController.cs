using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeekStudy.API.DTOs.UserDTOs;
using PeekStudy.API.Services.IServices;

namespace PeekStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;


        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await userService.RegisterAsync(dto);

            if (result == null)
            {
                return BadRequest("Email already exists.");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await userService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            if (id != userId)
            {
                return Forbid();
            }

            var result = await userService.GetProfileAsync(id);

            if (result == null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            var userId = int.Parse(
     User.FindFirstValue(ClaimTypes.NameIdentifier)!
 );

            if (id != userId)
            {
                return Forbid();
            }

            var result = await userService.UpdateUserAsync(id, dto);

            if (result == null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(
       User.FindFirstValue(ClaimTypes.NameIdentifier)!
   );

            if (id != userId)
            {
                return Forbid();
            }
            var result = await userService.DeleteUserAsync(id);



            if (!result)
            {
                return NotFound("User not found");
            }

            return Ok("User deleted successfully");
        }
    }

}
