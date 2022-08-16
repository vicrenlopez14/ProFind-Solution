﻿using Application.Models;
using Dapper;
using MySql.Data.MySqlClient;
using WebService.Lib.DataSource;

namespace WebService.Lib.Project.DataSource;

public class ProjectDataSource
{
    readonly MySqlConnection _connection;

    public ProjectDataSource()
    {
        _connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFProject> Get(string id)
    {
        // NOTE: Project is supposed to have already an Id at this point.

        const string query =
            "SELECT * FROM Project WHERE IdPJ = @id";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["id"] = id
        });

        var result = await _connection.QueryAsync<PFProject>(query, dynamicParameters);

        return result.ToList()[0];
    }

    public async Task<IEnumerable<PFProject>> List()
    {
        const string query = "SELECT * FROM Project;";

        var result = await _connection.QueryAsync<PFProject>(query);

        return result.ToList();
    }

    public async Task<bool> Create(PFProject Project)
    {
        const string query =
            "INSERT INTO Project VALUES(@IdPJ, @TitlePJ, @DescriptionPJ, @PicturePJ, @TotalPricePJ, @IdPS1, @IdP1, @IdC1);";

        DynamicParameters dynamicParameters = new DynamicParameters();


        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["IdPJ"] = Project.IdPJ,
            ["TitlePJ"] = Project.TitlePJ,
            ["DescriptionPJ"] = Project.DescriptionPJ,
            ["PicturePJ"] = Project.PicturePJ,
            ["TotalPricePJ"] = Project.TotalPricePJ,
            ["IdPS1"] = Project.IdPS1,
            ["IdP1"] = Project.IdP1,
            ["IdC1"] = Project.IdC1
        });

        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(string id, PFProject Project)
    {
        const string query =
            "UPDATE Project SET TitlePJ=@Title, DescriptionPJ=@Description, PicturePJ=@Picture, TotalPricePJ=@TotalPrice, IdPS1=@IdPS1, IdP1=@IdP1, IdC1=@IdC1 WHERE IdPJ=@Id;";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
        {
            ["Id"] = id,
            ["Description"] = Project.DescriptionPJ,
            ["Title"] = Project.TitlePJ,
            ["Picture"] = Project.PicturePJ,
            ["TotalPrice"] = Project.TotalPricePJ,
            ["IdPS1"] = Project.IdPS1,
            ["IdP1"] = Project.IdP1,
            ["IdC1"] = Project.IdC1
        });

        // If more than 0 rows modified, was successful
        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(string id)
    {
        const string query =
            "DELETE FROM Project WHERE IdPJ=@Id;";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new Dictionary<string, object>()
            {
                ["Id"] = id
            }
        );

        // If more than 0 rows modified, was successful
        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }
}