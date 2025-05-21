

namespace Model.RequestModels
{
    public class RatingModel
    {
        public int RecipeId { get; set; }
        public int Value { get; set; } // e.g., 1-5 stars
    }
}
