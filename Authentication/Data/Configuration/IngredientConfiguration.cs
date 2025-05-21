//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Data.Entities;

//namespace Data.Configurations
//{
//    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
//    {
//        public void Configure(EntityTypeBuilder<Ingredient> builder)
//        {
//            // Define the primary key
//            builder.HasKey(i => i.Id);

//            // Configure the Name property with a max length
//            builder.Property(i => i.Name)
//                .IsRequired()
//                .HasMaxLength(255); // Optional max length

//            // Configure the relationship with Recipe
//            builder.HasOne(i => i.Recipe)
//                .WithMany(r => r.Ingredients)
//                .HasForeignKey(i => i.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // Set RecipeId to null if the Recipe is deleted

//            // Ensure that the foreign key column `RecipeId` is nullable
//            builder.Property(i => i.RecipeId)
//                .IsRequired(false); // RecipeId is optional
//        }
//    }
//}
