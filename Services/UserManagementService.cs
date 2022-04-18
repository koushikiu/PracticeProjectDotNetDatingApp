using DataModel.Entites;


namespace Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUsersRepository _usersRepository;
        

        public UserManagementService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<AppUser> FindUser(string userName)
        {
            return await _usersRepository.FindUser(userName);
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();
            return users;
        }

        public async Task<AppUser> RegisterUserAsync(AppUser user)
        {
            await _usersRepository.AddUserAsync(user);
            return user;    
        }

        public async Task<bool> UserExit(string userName)
        {
            return (await _usersRepository.UserExit(userName));
        }
    }
}
public interface IUserManagementService
{
    Task<List<AppUser>> GetUsersAsync();
    Task<AppUser> RegisterUserAsync(AppUser user);
    Task<bool> UserExit(string userName);
    Task<AppUser>FindUser(string userName);
}
