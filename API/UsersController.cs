using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
