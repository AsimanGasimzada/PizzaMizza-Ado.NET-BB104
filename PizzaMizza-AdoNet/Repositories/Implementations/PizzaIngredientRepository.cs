using Dapper;
using Microsoft.Data.SqlClient;
using PizzaMizza_AdoNet.Constants;
using PizzaMizza_AdoNet.Models;
using PizzaMizza_AdoNet.Repositories.Abstractions;

namespace PizzaMizza_AdoNet.Repositories.Implementations;
public class PizzaIngredientRepository : IRepository<PizzaIngredient>
{
    private SqlConnection _connection { get => new(ConnectionStrings.SqlConnectionString); }
    public async Task AddAsync(PizzaIngredient entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("INSERT INTO PizzaIngredients VALUES (@PizzaId,@IngredientId)", entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = _connection;

        await db.ExecuteAsync("DELETE FROM PizzaIngredients WHERE Id=@id", new { id });
    }

    public async Task<List<PizzaIngredient>> GetAllAsync()
    {
        using var db = _connection;

        var list = (await db.QueryAsync<PizzaIngredient>("SELECT*FROM PizzaIngredients")).ToList();

        return list;
    }

    public async Task<PizzaIngredient?> GetByIdAsync(int id)
    {
        using var db = _connection;


        var pizzaIngredient = await db.QuerySingleOrDefaultAsync<PizzaIngredient>("SELECT * FROM PizzaIngredients WHERE Id=@id", new { id });

        return pizzaIngredient;
    }
    public async Task UpdateAsync(PizzaIngredient entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("UPDATE PizzaIngredients SET PizzaId=@PizzaId, IngredientId=@IngredientId WHERE Id=@Id", entity);
    }
}
