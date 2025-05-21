
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Data.Entities
{
    public class Recipe
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
      
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }

    }
}
