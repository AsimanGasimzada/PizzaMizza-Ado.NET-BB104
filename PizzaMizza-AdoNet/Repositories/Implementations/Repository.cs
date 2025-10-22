using Dapper;
using Microsoft.Data.SqlClient;
using PizzaMizza_AdoNet.Constants;
using PizzaMizza_AdoNet.Models;
using PizzaMizza_AdoNet.Models.Common;
using PizzaMizza_AdoNet.Repositories.Abstractions;
using System;

namespace PizzaMizza_AdoNet.Repositories.Implementations;

internal class Repository<T> : IRepository<T> where T : BaseEntity
{
    private SqlConnection _connection { get => new(ConnectionStrings.SqlConnectionString); }
    private readonly string _tableName = Pluralize(typeof(T).Name);
    private readonly string _columnNames = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name).Where(x => x != "Id"));

    public Repository(string? tableName = null, string? columnNames = null)
    {
        if (!string.IsNullOrEmpty(tableName))
            _tableName = tableName;

        if (!string.IsNullOrEmpty(columnNames))
            _columnNames = columnNames;
    }
    public async Task AddAsync(T entity)
    {
        using var db = _connection;

        await db.ExecuteAsync($"INSERT INTO {_tableName} VALUES (@{_columnNames})", entity);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = _connection;

        await db.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@id", new { id });
    }

    public async Task<List<T>> GetAllAsync()
    {
        using var db = _connection;

        var list = (await db.QueryAsync<T>($"SELECT * FROM {_tableName}")).ToList();

        return list ?? new();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        using var db = _connection;

        var entity = await db.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@id", new { id });

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        using var db = _connection;

        string updateQuery = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name).Where(x => x != "Id").Select(x => $"{x}=@{x}"));

        await db.ExecuteAsync($"UPDATE {_tableName} SET {updateQuery} WHERE Id=@Id", entity);
    }

    private static string Pluralize(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name ?? string.Empty;

        // If name ends with 'y' and previous char is not a vowel -> replace 'y' with 'ies'
        if (name.EndsWith("y", StringComparison.OrdinalIgnoreCase) && name.Length > 1)
        {
            char prev = name[name.Length - 2];
            const string vowels = "aeiouAEIOU";
            if (!vowels.Contains(prev))
            {
                return name.Substring(0, name.Length - 1) + "ies";
            }
        }

        return name + "s";
    }
}
