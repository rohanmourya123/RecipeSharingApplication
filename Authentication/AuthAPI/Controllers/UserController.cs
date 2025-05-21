using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> FetchAllUsers()
        {
            var res = await _userService.getAllUsers();

            if (res == null || !res.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(res);
        }

   
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> FetchUserById(string id)
        {
            var user = await _userService.getUserByID(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }
    }
}
