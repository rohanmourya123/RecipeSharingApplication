using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Model.RequestModels;
using Services.Interfaces;


namespace Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubmitRatingAsync(RatingModel model, string userId)
        {
            if (model.Value < 1 || model.Value > 5)
                return false;

            var recipe = await _context.Recipes.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == model.RecipeId);
            if (recipe == null)
                return false;

            var existingRating = await _context.Ratings.FirstOrDefaultAsync(r => r.RecipeId == model.RecipeId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Value = model.Value;
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);

                var rating = new Rating
                {
                    RecipeId = model.RecipeId,
                    UserId = userId,
                    User = user,
                    Value = model.Value
                };
                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
