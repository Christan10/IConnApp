using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IConnApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IConnApp.Data.Repositories
{
    public enum SortDirection
    {
        Asc = 0,
        Dsc = 1
    }
    public interface IUsersRepository
    {
        Task<int> CountAsync();
        Task<ApplicationUser> GetByEmail(string email);

        Task<List<ApplicationUser>> GetAllUsers();

        Task<List<ApplicationUser>> GetUsersByPagination(int page, int pageSize);

        Task<IEnumerable<ApplicationUser>> GetUsersBySortingDirection(string direction);

        Task<IEnumerable<ApplicationUser>> Search(string searchString);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Task<int> CountAsync()
        {
            return _db.Users.CountAsync();
        }

        public Task<ApplicationUser> GetByEmail(string email)
        {
            return _db.Users.Where(c => c.Email == email).FirstOrDefaultAsync();
        }

        public Task<List<ApplicationUser>> GetAllUsers()
        {
            return _db.Users.OrderBy(c => c.Id).ToListAsync();
        }

        public Task<List<ApplicationUser>> GetUsersByPagination(int page, int pageSize)
        {
            return _db.Users.OrderBy(c => c.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersBySortingDirection(string direction)
        {
            if (direction == SortDirection.Dsc.ToString())
            {
                return await _db.Users.OrderByDescending(c => c.Id).ToListAsync();
            }

            return await _db.Users.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> Search(string searchString)
        {
            return await _db.Users.Where(c => c.Name.ToLower().Contains(searchString.ToLower())
                                              || c.Surname.ToLower().Contains(searchString.ToLower())
                                              || c.Email.ToLower().Contains(searchString.ToLower())).ToListAsync();
        }
    }
}
