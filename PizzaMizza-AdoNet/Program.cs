using PizzaMizza_AdoNet.Models;
using PizzaMizza_AdoNet.Repositories.Abstractions;
using PizzaMizza_AdoNet.Repositories.Implementations;
using System.Threading.Tasks;

namespace PizzaMizza_AdoNet;

internal class Program
{
    static async Task Main(string[] args)
    {
        IRepository<Pizza> pizzaRepository = new PizzaRepository();
        IRepository<Ingredient> ingredientRepository = new IngredientRepository();
        IRepository<PizzaIngredient> pizzaIngredientRepository = new PizzaIngredientRepository();

        //await pizzaRepository.AddAsync(new() { Name = "Chicken pizza", Price = 28 });
        //await pizzaRepository.DeleteAsync(1);
        //await pizzaRepository.DeleteAsync(2);


        //await pizzaRepository.UpdateAsync(new() { Id = 3, Name = "Updated pizza", Price = 1 });


        //pizzas.ForEach(pizza => Console.WriteLine(pizza));


        //await ingredientRepository.AddAsync(new() { Name = "Davuk" });
        //await ingredientRepository.AddAsync(new() { Name = "Gobelek" });
        //await ingredientRepository.AddAsync(new() { Name = "Pendir" });


        //ingredients.ForEach(ingredient => Console.WriteLine(ingredient));

        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 3, IngredientId = 1 });
        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 3, IngredientId = 2 });
        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 3, IngredientId = 3 });
        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 4, IngredientId = 1 });
        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 4, IngredientId = 2 });
        //await pizzaIngredientRepository.AddAsync(new() { PizzaId = 5, IngredientId = 3 });

        var ingredients = await ingredientRepository.GetAllAsync();
        var pizzas = await pizzaRepository.GetAllAsync();
        var pizzaIngredients=await pizzaIngredientRepository.GetAllAsync();

        foreach (var pizza in pizzas)
        {
            var list = pizzaIngredients.Where(x => x.PizzaId == pizza.Id).ToList();

            foreach (var item in list)
            {
                item.Ingredient = ingredients.FirstOrDefault(x => x.Id == item.IngredientId)!;
            }

            pizza.PizzaIngredients = list;
        }

        pizzas.ForEach(pizza => Console.WriteLine(pizza));


    }
}
