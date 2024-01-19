using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MealPlan
    {
        public Guid Id { get; init; }
        public Guid UserId { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public static MealPlan Create(Guid userId, DateOnly starting, DateOnly ending)
        {
            return new MealPlan { 
                Id = userId, 
                StartDate = starting,
                EndDate = ending,
                GeneratedDate= DateTime.UtcNow
            };
        }
    }
}
