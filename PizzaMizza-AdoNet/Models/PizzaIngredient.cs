using PizzaMizza_AdoNet.Models.Common;

namespace PizzaMizza_AdoNet.Models;

public class PizzaIngredient : BaseEntity
{
    public int PizzaId { get; set; }
    public int IngredientId { get; set; }
    public Pizza Pizza { get; set; }
    public Ingredient Ingredient { get; set; }
}
