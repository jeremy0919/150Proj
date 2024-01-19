using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EmailAddress { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string AllergiesJSON { get; set; } = "[]";
        public string DietTypesJSON { get; set; } = "[]"; 
        public ICollection<RecipeRanking> RankedRecipes { get; set; } = new List<RecipeRanking>();
    }

    public class PlanPreferences
    {
        public int Days { get; set; } = 3;
        public string MealTypesJSON { get; set; } = "[]";
    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
}
