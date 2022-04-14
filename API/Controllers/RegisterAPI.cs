using DataModel.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class RegisterAPI : BaseAPIController
    {
        private readonly IUserManagementService _userManagementService; 

        public RegisterAPI(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost ("register")]
        public async Task<ActionResult<AppUser>> Register (string userName, string password)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = userName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

           await _userManagementService.RegisterUserAsync(user);
           return Ok (user);
        }
    }
}
