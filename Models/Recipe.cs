using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Recipe
    {
        [JsonIgnore]
        public Guid Id { get; init; }

        [JsonPropertyName("sourceUrl")]
        public string ApiUri { get; init; } = "";

        [JsonPropertyName("id")]
        public string ApiId { get; init; } = "";

        [JsonPropertyName("title")]
        public string Title { get; init; } = "";

        [JsonPropertyName("image")]
        public string ImageUrl { get; init; } = "";

        [JsonPropertyName("summary")]
        public string Description { get; init; } = "";


        [JsonPropertyName("extendedIngredients")]

        public List<ExtendedIngredient> Ingredients = new List<ExtendedIngredient>();

        public static Recipe Create(Guid mealPlanId, string apiId, string apiUri, string title, string description, string imageUrl)
        {
            return new Recipe()
            {
                Id = Guid.NewGuid(),
                ApiUri= apiUri,
                ApiId = apiId,
                Title = title,
                Description = description,
                ImageUrl = imageUrl
            };
        }

        public void AddIngredient(ExtendedIngredient ingredient) => this.Ingredients.Add(ingredient);
    }
}
