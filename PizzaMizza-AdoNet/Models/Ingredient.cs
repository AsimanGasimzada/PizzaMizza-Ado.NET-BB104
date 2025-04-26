using PizzaMizza_AdoNet.Models.Common;

namespace PizzaMizza_AdoNet.Models;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public List<PizzaIngredient> PizzaIngredients { get; set; } = [];


    public override string ToString()
    {
        return $"{Id}. {Name}";
    }
}
