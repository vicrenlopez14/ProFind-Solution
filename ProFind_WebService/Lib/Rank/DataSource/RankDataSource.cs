using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;
using ProFind_WebService.Lib.Rank.Model;

namespace ProFind_WebService.Lib.Rank.DataSource;

public class RankDataSource
{
    readonly MySqlConnection connection;

    public RankDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFRank> Get(string id)
    {
        const string query = "SELECT Id_R, Name_R FROM `Rank` WHERE Id_R = ':Id' LIMIT 1;";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = result.First() as IDictionary<string,object>;

        return PFRank.FromDictionary(firstRow);

    }

    public async Task<bool> Create(PFRank rank)
    {
        const string query = "INSERT INTO `Rank` VALUES (':Id',':Name');";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = rank.Id,
            Name = rank.Name
        });

        return (await connection.ExecuteAsync(query,dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFRank rank)
    {
        const string query = "UPDATE `Rank` SET Name_R = ':Name' WHERE Id_R = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = rank.Id,
            Name = rank.Name
        });

        return (await connection.ExecuteAsync(query,dynamicParameters) > 0);
    }

    public async Task<bool> Delete (PFRank rank)
    {
        const string query = "DELETE FROM `Rank` WHERE Id_Rank = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = rank.Id
        });

        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);   
    }
}