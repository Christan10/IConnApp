﻿using System.Threading.Tasks;
using AutoMapper;
using IConnApp.Data.Repositories;
using IConnApp.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace IConnApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _usersRepository.CountAsync();
            return Ok(new { count });
        }

        // eg. /api/users/christos@fou.com
        [HttpGet("{email:minlength(6)}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            return Ok(new
            {
                user = Mapper.Map<UserViewModel>(user)
            });
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersRepository.GetAllUsers();
            return Ok(new
            {
                users = Mapper.Map<UserViewModel[]>(users)
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchString)
        {
            var results = await _usersRepository.Search(searchString);

            return Ok(new
            {
                results = Mapper.Map<UserViewModel[]>(results)
            });
        }

        // eg. /api/users/?email=christos@fou.com
        [HttpGet]
        public async Task<IActionResult> GetByEmailQuery([FromQuery]string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            return Ok(new
            {
                user = Mapper.Map<UserViewModel>(user)
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserEditViewModel model)
        {
            // instead of
            // if (!ModelState.IsValid) return BadRequest(ModelState);
            // use filter
            return Ok(model);
        }
    }
}
