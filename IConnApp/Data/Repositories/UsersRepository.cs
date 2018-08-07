﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IConnApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IConnApp.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CountAsync();
        Task<ApplicationUser> GetByEmail(string email);

        Task<List<ApplicationUser>> GetAllUsers();

        Task<List<ApplicationUser>> GetUsersByPagenation(int page, int pageSize);

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

        public Task<List<ApplicationUser>> GetUsersByPagenation(int page, int pageSize)
        {
            return _db.Users.OrderBy(c => c.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> Search(string searchString)
        {
            return await _db.Users.Where(c => c.Name.ToLower().Contains(searchString.ToLower()) 
                                              || c.Surname.ToLower().Contains(searchString.ToLower())
                                              || c.Email.ToLower().Contains(searchString.ToLower())).ToListAsync();
        }
    }
}
