using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;
using ProFind_WebService.Lib.Job.Model;

namespace ProFind_WebService.Lib.Job.DataSource;

public class JobDataSource
{
    readonly MySqlConnection connection;
    public JobDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFJob> Get(string id)
    {
        const string query = "SELECT Id_J, Name_J FROM Job WHERE Id_J = ':Id' LIMIT 1;";
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new 
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = result.First() as IDictionary<string,object>;

        return PFJob.FromDictionary(firstRow);
    }

    public async Task <bool> Create(PFJob job)
    {
        const string query = "INSERT INTO Job VALUES (':Id',':Name');";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = job.Id,
            Name = job.Name
        });

        return (await connection.ExecuteAsync(query,dynamicParameters)>0);
    }

    public async Task <bool> Update(PFJob job)
    {
        const string query = "UPDATE Job SET Name_J=':Name' WHERE Id_J = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = job.Id,
            Name = job.Name
        });

        return (await connection.ExecuteAsync(query,dynamicParameters) > 0);

    }

    public async Task <bool> Delete(PFJob job)
    {
        const string query = "DELETE FROM Job WHERE Id_J = ':Id';";

        DynamicParameters dynamicParameters = new DynamicParameters();

        dynamicParameters.AddDynamicParams(new
        {
            Id = job.Id
        });

        return (await connection.ExecuteAsync(query,dynamicParameters) > 0);
    }
}
