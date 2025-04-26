using Dapper;
using Microsoft.Data.SqlClient;
using PizzaMizza_AdoNet.Constants;
using PizzaMizza_AdoNet.Models;
using PizzaMizza_AdoNet.Repositories.Abstractions;

namespace PizzaMizza_AdoNet.Repositories.Implementations;
public class PizzaRepository : IRepository<Pizza>
{
    private SqlConnection _connection { get => new(ConnectionStrings.SqlConnectionString); }

    public PizzaRepository()
    {
    }
    public async Task AddAsync(Pizza entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("INSERT INTO Pizzas VALUES (@Name,@Price)", entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = _connection;

        await db.ExecuteAsync("DELETE FROM Pizzas WHERE Id=@id", new { id });
    }

    public async Task<List<Pizza>> GetAllAsync()
    {
        using var db = _connection;

        var list = (await db.QueryAsync<Pizza>("SELECT*FROM Pizzas")).ToList();

        return list ?? new();
    }

    public async Task<Pizza?> GetByIdAsync(int id)
    {
        using var db = _connection;


        var pizza = await db.QuerySingleOrDefaultAsync<Pizza>("SELECT * FROM Pizzas WHERE Id=@id", new { id });

        return pizza;
    }

    public async Task UpdateAsync(Pizza entity)
    {
        using var db = _connection;

        await db.ExecuteAsync("UPDATE Pizzas SET Name=@Name,Price=@Price WHERE Id=@Id", entity);
    }
}
