
using System.Text.Json.Serialization;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name {  get; set; }
        // Navigation Properties
        //public ICollection<Recipe>? Recipes { get; set; }

        [JsonIgnore]
        public ICollection<Rating> Ratings { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        public ICollection<Recipe> Recipes { get; set; }
    }
}
