using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API
{
  
    public class UsersController : BaseAPIController
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var user = await _usersRepository.GetUserAsync(userId);

            if(user == null)
            {
                return NotFound("No user found");
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
