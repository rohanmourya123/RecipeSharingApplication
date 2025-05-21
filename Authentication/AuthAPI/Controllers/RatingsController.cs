using System.Security.Claims;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModels;
using Services.Interfaces;


namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RatingController(IRatingService ratingService, UserManager<ApplicationUser> userManager)
        {
            _ratingService = ratingService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRating([FromBody] RatingModel model)
        {
            if (model == null)
                return BadRequest("Invalid rating data");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token claims");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized("User not found");

            var success = await _ratingService.SubmitRatingAsync(model, userId);
            if (!success)
                return BadRequest("Failed to submit rating or rating out of bounds");

            return Ok("Rating submitted successfully");
        }
    }
}




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
//    [Authorize]
//    public class RatingController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public RatingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [HttpPost]
//        public async Task<IActionResult> SubmitRating([FromBody] RatingModel model)
//        {
//            if (model == null || model.Value < 1 || model.Value > 5)
//                return BadRequest("Rating must be between 1 and 5");

//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized();

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//                return Unauthorized("User not found");

//            var recipe = await _context.Recipes.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == model.RecipeId);
//            if (recipe == null)
//                return NotFound("Recipe not found");

//            var existingRating = await _context.Ratings.FirstOrDefaultAsync(r => r.RecipeId == model.RecipeId && r.UserId == userId);

//            if (existingRating != null)
//            {
//                existingRating.Value = model.Value;
//            }
//            else
//            {
//                var rating = new Rating
//                {
//                    RecipeId = model.RecipeId,
//                    UserId = userId,
//                    Value = model.Value
//                };
//                _context.Ratings.Add(rating);
//            }

//            await _context.SaveChangesAsync();

//            return Ok("Rating submitted");
//        }
//    }
//}











