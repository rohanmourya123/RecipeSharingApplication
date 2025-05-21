

using Microsoft.AspNetCore.Http;

namespace Model.RequestModels
{
    public class RecipeModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

     //   public List<IFormFile> ImageData { get; set; }  

        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
    }
}
