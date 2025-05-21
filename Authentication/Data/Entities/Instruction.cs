using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Instruction
    {
        public int Id { get; set; }
        [Required] public string Step { get; set; }
        public int Order { get; set; }
        public int RecipeId { get; set; }   // Fk to Recipe
        public Recipe Recipe { get; set; }
    }
}
