using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(m => m.FirstName).HasMaxLength(25);
            builder.Property(m => m.LastName).HasMaxLength(50);

            builder.Property(m => m.DietTypesJSON).HasMaxLength(512);
            builder.Property(m => m.AllergiesJSON).HasMaxLength(512);
            builder.Property(m => m.EmailAddress).HasMaxLength(128);

            builder.HasMany(m => m.RankedRecipes).WithOne().HasPrincipalKey(k => k.Id).HasForeignKey(f => f.UserId);
        }
    }


    internal class RankedRecipieConfig : IEntityTypeConfiguration<RecipeRanking>
    {
        public void Configure(EntityTypeBuilder<RecipeRanking> builder)
        {
            builder.HasKey(m => new { m.UserId, m.RecipeId });
            builder.Property(m => m.Notes).HasMaxLength(1000);
        }
    }
}
