//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Data.Entities;

//namespace Data.Configurations
//{
//    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
//    {
//        public void Configure(EntityTypeBuilder<Comment> builder)
//        {
//            builder.HasKey(c => c.CommentId);

//            builder.Property(c => c.CommentText)
//                .IsRequired()
//                .HasMaxLength(1000); // Optional length constraint

//            builder.Property(c => c.CreatedAt)
//                .HasDefaultValueSql("GETDATE()");

//            // Comment → User
//            builder.HasOne(c => c.User)
//                .WithMany(u => u.Comments)
//                .HasForeignKey(c => c.UserId)
//                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete and no conflict

//            // Comment → Recipe
//            builder.HasOne(c => c.Recipe)
//                .WithMany(r => r.Comments)
//                .HasForeignKey(c => c.RecipeId)
//                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete and no conflict
//        }
//    }
//}
