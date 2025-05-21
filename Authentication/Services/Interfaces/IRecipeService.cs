

using Data.Entities;
using Model.RequestModels;

namespace Services.Interfaces
{


    namespace Services.Interfaces
    {
        public interface IRecipeService
        {
            Task<PagedResult<Recipe>> SearchRecipes(RecipeSearchModel search);
            Task<Recipe?> GetRecipeById(int id);
            Task<Recipe?> AddRecipe(RecipeModel model, string userId);
            Task<bool> UpdateRecipe(int id, RecipeModel model, string userId);
            Task<bool> DeleteRecipe(int id, string userId);
        }
    }

}
