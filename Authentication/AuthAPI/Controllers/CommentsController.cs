using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RequestModels;
using Services.Interfaces;
using Services.Interfaces.Services.Interfaces;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CommentModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Content))
                return BadRequest("Comment content is required.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var result = await _commentService.PostCommentAsync(model, userId);
            if (result == null)
                return NotFound("Recipe not found.");

            return Ok(result);
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
//    public class CommentsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [HttpPost]
//        public async Task<IActionResult> PostComment([FromBody] CommentModel model)
//        {
//            if (model == null || string.IsNullOrWhiteSpace(model.Content))
//                return BadRequest("Comment content is required.");

//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized("User not authenticated.");

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//                return Unauthorized("User not found.");

//            var recipe = await _context.Recipes.FindAsync(model.RecipeId);
//            if (recipe == null)
//                return NotFound("Recipe not found.");

//            var comment = new Comment
//            {
//                Content = model.Content,
//                CreatedAt = DateTime.UtcNow,
//                UserId = userId,
//                RecipeId = model.RecipeId
//            };

//            _context.Comments.Add(comment);
//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                comment.Id,
//                comment.Content,
//                comment.CreatedAt,
//                comment.UserId,
//                comment.RecipeId
//            });
//        }
//    }
//}

