using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Model.RequestModels;
using Services.Interfaces;
using Services.Interfaces.Services.Interfaces;

namespace Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        
        protected readonly ApplicationDbContext _context;
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Recipe>> SearchRecipes(RecipeSearchModel search)
        {
            var query = _context.Recipes
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Ingredients)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Title))
                query = query.Where(r => r.Title.Contains(search.Title));

            if (!string.IsNullOrWhiteSpace(search.Category))
                query = query.Where(r => r.Category == search.Category);

            if (!string.IsNullOrWhiteSpace(search.Ingredient))
                query = query.Where(r => r.Ingredients.Any(i => i.Name.Contains(search.Ingredient)));

            //return await query.ToListAsync();
            var totalCount = await query.CountAsync();

            // pagination 
            var items = await query
                .Skip((search.PageNumber -1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return new PagedResult<Recipe>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = search.PageNumber,
                PageSize = search.PageSize
            };
        }

        public async Task<Recipe?> GetRecipeById(int id)
        {
            return await _context.Recipes
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r=>r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Recipe?> AddRecipe(RecipeModel model, string userId)
        {
                var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == userId);

            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                ImageUrl = model.ImageUrl,
                Ingredients = model.Ingredients.Select(i => new Ingredient { Name = i }).ToList(),
                Instructions = model.Instructions.Select((step, index) => new Instruction
                {
                    Step = step,
                    Order = index + 1
                }).ToList(),
                UserId = userId,
                User = user
            };
            

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<bool> UpdateRecipe(int id, RecipeModel model, string userId)
        {
            var existing = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existing == null || existing.UserId != userId)
                return false;

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.Category = model.Category;
            existing.ImageUrl = model.ImageUrl;

            _context.Ingredients.RemoveRange(existing.Ingredients);
            _context.Instructions.RemoveRange(existing.Instructions);

            existing.Ingredients = model.Ingredients.Select(i => new Ingredient { Name = i }).ToList();
            existing.Instructions = model.Instructions.Select((step, index) => new Instruction
            {
                Step = step,
                Order = index + 1
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRecipe(int id, string userId)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null || recipe.UserId != userId)
                return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
