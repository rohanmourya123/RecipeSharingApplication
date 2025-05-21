using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RequestModels
{
    public class RecipeSearchModel
    {
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Ingredient { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }

}
