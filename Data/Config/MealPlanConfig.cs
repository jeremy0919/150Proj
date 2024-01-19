using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{

    internal class MealPlanConfig : IEntityTypeConfiguration<MealPlan>
    {
        public void Configure(EntityTypeBuilder<MealPlan> builder)
        {
            builder.HasMany(m => m.Recipes).WithMany();

            builder
                .Property(m => m.StartDate)
                .HasConversion<DateOnlyConverter>();

            builder
                .Property(m => m.EndDate)
                .HasConversion<DateOnlyConverter>();

        }
    }
}
