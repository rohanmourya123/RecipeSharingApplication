using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModels;
using Services.Interfaces.Services.Interfaces;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<Recipe>>> GetRecipes(
            //[FromQuery] string? title,
            //[FromQuery] string? category,
            //[FromQuery] string? ingredient)
            [FromQuery] RecipeSearchModel searchModel)
        {
            var pagedRecipes = await _recipeService.SearchRecipes(searchModel);
            return Ok(pagedRecipes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeById(id);
            if (recipe == null)
                return NotFound("Recipe not found");

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe([FromBody] RecipeModel recipe)
        {
            if (recipe == null || !ModelState.IsValid)
                return BadRequest("Invalid recipe data");

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token claims");

            var created = await _recipeService.AddRecipe(recipe, userId);
            if (created == null)
                return BadRequest("Failed to create recipe");

            return CreatedAtAction(nameof(GetRecipe), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, [FromBody] RecipeModel recipe)
        {
            if (recipe == null || !ModelState.IsValid)
                return BadRequest("Invalid recipe data");

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token claims");

            var updated = await _recipeService.UpdateRecipe(id, recipe, userId);
            if (!updated)
                return Forbid("You are not authorized or recipe does not exist");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token claims");

            var deleted = await _recipeService.DeleteRecipe(id, userId);
            if (!deleted)
                return Unauthorized("You are not authorized or recipe does not exist");

            return NoContent();
        }
    }
}





























//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Data;
//using Data.Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Model.RequestModels;

//namespace AuthAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize] // Require authentication for all unless overridden
//    public class RecipeController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public RecipeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: api/recipe?title=...&category=...&ingredient=...
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes(
//            [FromQuery] string? title,
//            [FromQuery] string? category,
//            [FromQuery] string? ingredient)
//        {
//            var query = _context.Recipes
//                .Include(r => r.Ratings)
//                .Include(r => r.Comments)
//                .Include(r => r.Ingredients)
//                .AsQueryable();

//            if (!string.IsNullOrWhiteSpace(title))
//                query = query.Where(r => r.Title.Contains(title));

//            if (!string.IsNullOrWhiteSpace(category))
//                query = query.Where(r => r.Category == category);

//            if (!string.IsNullOrWhiteSpace(ingredient))
//                query = query.Where(r => r.Ingredients.Any(i => i.Name.Contains(ingredient)));

//            return await query.ToListAsync();
//        }

//        // GET: api/recipe/5
//        [HttpGet("{id}")]
//        [AllowAnonymous]
//        public async Task<ActionResult<Recipe>> GetRecipe(int id)
//        {
//            var recipe = await _context.Recipes
//                .Include(r => r.Ratings)
//                .Include(r => r.Comments)
//                .Include(r => r.Ingredients)
//                .Include(r => r.Instructions)
//                .FirstOrDefaultAsync(r => r.Id == id);

//            return recipe == null ? NotFound() : recipe;
//        }

//        // POST: api/recipe
//        [HttpPost]
//        public async Task<ActionResult<Recipe>> PostRecipe([FromBody] RecipeModel recipe)
//        {
//            if (recipe == null || !ModelState.IsValid)
//                return BadRequest("Invalid recipe data");

//            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
//                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized("Invalid token claims");

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//                return Unauthorized("User not found");

//            var newRecipe = new Recipe
//            {
//                Title = recipe.Title,
//                Description = recipe.Description,
//                Category = recipe.Category,
//                ImageUrl = recipe.ImageUrl,
//                Ingredients = recipe.Ingredients.Select(i => new Ingredient { Name = i }).ToList(),
//                Instructions = recipe.Instructions.Select((step, index) => new Instruction
//                {
//                    Step = step,
//                    Order = index + 1
//                }).ToList(),
//                UserId = user.Id
//            };

//            _context.Recipes.Add(newRecipe);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetRecipe), new { id = newRecipe.Id }, newRecipe);
//        }

//        // PUT: api/recipe/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRecipe(int id, [FromBody] RecipeModel recipe)
//        {
//            if (recipe == null)
//                return BadRequest("Invalid recipe data");

//            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
//                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized("Invalid token claims");

//            var existing = await _context.Recipes
//                .Include(r => r.Ingredients)
//                .Include(r => r.Instructions)
//                .FirstOrDefaultAsync(r => r.Id == id);

//            if (existing == null)
//                return NotFound("Recipe not found");

//            if (existing.UserId != userId)
//                return Forbid("You are not authorized to update this recipe");

//            // Update fields
//            existing.Title = recipe.Title;
//            existing.Description = recipe.Description;
//            existing.Category = recipe.Category;
//            existing.ImageUrl = recipe.ImageUrl;

//            // Replace ingredients and instructions
//            _context.Ingredients.RemoveRange(existing.Ingredients);
//            _context.Instructions.RemoveRange(existing.Instructions);

//            existing.Ingredients = recipe.Ingredients.Select(i => new Ingredient { Name = i }).ToList();
//            existing.Instructions = recipe.Instructions.Select((step, index) => new Instruction
//            {
//                Step = step,
//                Order = index + 1
//            }).ToList();

//            await _context.SaveChangesAsync();
//            return NoContent();
//        }

//        // DELETE: api/recipe/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRecipe(int id)
//        {
//            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
//                      ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized("Invalid token claims");

//            var recipe = await _context.Recipes.FindAsync(id);
//            if (recipe == null)
//                return NotFound("Recipe not found");

//            if (recipe.UserId != userId)
//                return Forbid("You are not authorized to delete this recipe");

//            _context.Recipes.Remove(recipe);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}



































