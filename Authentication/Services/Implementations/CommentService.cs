using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Model.RequestModels;
using Services.Interfaces;
using Services.Interfaces.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PostCommentAsync(CommentModel model, string userId)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Content))
                return false;

            var recipe = await _context.Recipes.FindAsync(model.RecipeId);
            if (recipe == null)
                return false;

             var user = await _context.Users.FindAsync(userId);

            var comment = new Comment
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                User = user,
                RecipeId = model.RecipeId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
