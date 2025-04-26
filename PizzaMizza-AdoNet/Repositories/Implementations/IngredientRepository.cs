using Dapper;
using Microsoft.Data.SqlClient;
using PizzaMizza_AdoNet.Constants;
using PizzaMizza_AdoNet.Models;
using PizzaMizza_AdoNet.Repositories.Abstractions;

namespace PizzaMizza_AdoNet.Repositories.Implementations;
public class IngredientRepository : IRepository<Ingredient>
{
    private SqlConnection _connection { get => new(ConnectionStrings.SqlConnectionString); }


    public async Task AddAsync(Ingredient entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("INSERT INTO Ingredients VALUES (@Name)", entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = _connection;

        await db.ExecuteAsync("DELETE FROM Ingredients WHERE Id=@id", new { id });
    }

    public async Task<List<Ingredient>> GetAllAsync()
    {
        using var db = _connection;

        var list = (await db.QueryAsync<Ingredient>("SELECT*FROM Ingredients")).ToList();

        return list ?? new();
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
    {
        using var db = _connection;


        var Ingredient = await db.QuerySingleOrDefaultAsync<Ingredient>("SELECT * FROM Ingredients WHERE Id=@id", new { id });

        return Ingredient;
    }

    public async Task UpdateAsync(Ingredient entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("UPDATE Ingredients SET Name=@Name WHERE Id=@Id", entity);
    }
}
