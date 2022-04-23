using API.DTOs;
using DataModel.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly ITockenService _tockenService;

        public AccountController(IUserManagementService userManagementService, ITockenService tockenService)
        {
            _userManagementService = userManagementService;
            _tockenService = tockenService;
        }

        [HttpPost("Login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManagementService.FindUser(loginDTO.UserName);

            if(user == null)
            {
                return Unauthorized("Not a valid user");
            }

           using var  hmac = new HMACSHA512(user.PasswordSalt);
           var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0;i< computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid User");
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Tocken = _tockenService.CreateTocken(user)
            };

        }

        [HttpPost ("register")]
        public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
        {
            if(await UserExitAsync(registerDTO.UserName))
            {
                return BadRequest("This user taken");
            }

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTO.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

           await _userManagementService.RegisterUserAsync(user);

            return new UserDTO
            {
                UserName = user.UserName,
                Tocken = _tockenService.CreateTocken(user)
            };
        }

        private async Task<bool> UserExitAsync(string userName)
        {
            return (await _userManagementService.UserExit(userName));
        }
    }
}
