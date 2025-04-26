using PizzaMizza_AdoNet.Models.Common;

namespace PizzaMizza_AdoNet.Models;
public class Pizza : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<PizzaIngredient> PizzaIngredients { get; set; } = [];
    public override string ToString()
    {
        string ingredients = "";

        foreach (var item in PizzaIngredients)
        {
            ingredients += item.Ingredient.Name + " ,";
        }

        return $"{Id}.  {Name}  {Price}$    Ingredients: {ingredients}";

    }
}
