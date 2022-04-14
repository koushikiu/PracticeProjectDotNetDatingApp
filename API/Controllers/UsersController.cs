using API.Controllers;
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

        [HttpGet]

        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
