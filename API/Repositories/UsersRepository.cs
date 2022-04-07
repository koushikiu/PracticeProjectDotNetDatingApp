using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UsersRepository: IUsersRepository
    {
        private readonly DataContext _dataDbContext;
        public UsersRepository(DataContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async  Task<List<AppUser>> GetAllUsersAsync()
        {
            List<AppUser> users = null;
            users =  await _dataDbContext.Users.ToListAsync();
            return users;
        }
    }
}
public interface IUsersRepository
{
    Task <List<AppUser>> GetAllUsersAsync();
}
