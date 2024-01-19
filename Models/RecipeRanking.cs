using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RecipeRanking
    {
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; } 
        public int Stars { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
