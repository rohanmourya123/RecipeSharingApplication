using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Model.RequestModels;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        protected readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> getAllUsers()
        {
            var users = await _context.Users
                .Include(r => r.Recipes)
                .Include(r => r.Comments)
                .Include(r => r.Ratings)
                .ToListAsync();

            return users.Select(user => new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.UserName
            }).ToList();
        }

        public async Task<UserModel> getUserByID(string id)
        {
            var res = await _context.Users.FindAsync(id);

            if (res != null)
            {
                return new UserModel
                {
                    Id = res.Id,
                    Name = res.Name,
                    Email = res.Email,
                    Username = res.UserName
                };
            }

            return null;
        }
    }
}
