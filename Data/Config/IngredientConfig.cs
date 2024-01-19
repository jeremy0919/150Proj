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
    internal class IngredientConfig : IEntityTypeConfiguration<ExtendedIngredient>
    {
        public void Configure(EntityTypeBuilder<ExtendedIngredient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Ingredients");
            builder
                .OwnsOne(x => x.Measures, measures =>
                {
                    measures.OwnsOne(m => m.Us);
                });
        }
    }
}
