//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Data.Entities;

//namespace Data.Configurations
//{
//    public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
//    {
//        public void Configure(EntityTypeBuilder<Instruction> builder)
//        {
//            // Define the primary key for Instruction
//            builder.HasKey(i => i.Id);

//            // Configure the Step property
//            builder.Property(i => i.Step)
//                .IsRequired()
//                .HasMaxLength(1000); // Optional: limit text length

//            // Configure the Order property (if this is required)
//            builder.Property(i => i.Order)
//                .IsRequired();

//            // Configure the relationship with Recipe
//            builder.HasOne(i => i.Recipe)
//                .WithMany(r => r.Instructions)
//                .HasForeignKey(i => i.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // Set RecipeId to null when the Recipe is deleted

//            // Make sure the RecipeId is nullable
//            builder.Property(i => i.RecipeId)
//                .IsRequired(false); // RecipeId is optional (nullable)
//        }
//    }
//}
