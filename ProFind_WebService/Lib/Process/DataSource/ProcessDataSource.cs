using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;
using ProFind_WebService.Lib.Process.Model;

namespace ProFind_WebService.Lib.Process.DataSource;

public class ProcessDataSource
{
    readonly MySqlConnection _connection;

    public ProcessDataSource()
    {
        _connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFProcess> Get(string id)
    {
        // NOTE: Process is supposed to have already an Id at this point.

        const string query =
            "SELECT Process.Id_PR," +
            "Process.Title_PR," +
            "Process.Description_PR," +
            "Process.Begin_Date_PR," +
            "Process.End_Date_PR," +
            "Process.Id_T1," +
            "Name_T," +
            "Id_PJ1," +
            "Title_PJ," +
            "Description_PJ FROM Process " +
            "INNER JOIN Tag T on Process.Id_T1 = T.Id_T " +
            "INNER JOIN Project P on T.Id_PJ1 = P.Id_PJ " +
            "WHERE Process.Id_PR = '@Id' " +
            "LIMIT 1;";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await _connection.QueryAsync<PFProcess>(query, dynamicParameters);
        var firstRow = result.First();

        return firstRow;
    }

    public async Task<bool> Create(PFProcess process)
    {
        // NOTE@ Process is supposed to have already an Id at this point.

        const string query =
            "INSERT INTO Process (Id_PR, Title_PR, Description_PR, Begin_Date_PR, Id_T1, End_Date_PR) VALUES ('@Id', '@Title', '@Description', '@BeginDate', '@IdTag', '@EndDate');";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            process.Id,
            process.Title,
            process.Description,
            process.BeginDate,
            process.IdTag,
            process.EndDate
        });

        // If more than 0 rows modified, was successful
        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFProcess process)
    {
        const string query =
            "UPDATE Process SET Title_PR='@Title', Description_PR='@Description', Begin_Date_PR='@BeginDate', Id_T1='@IdTag', End_Date_PR='@EndDate' WHERE Id_PR='@Id';";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            process.Id,
            process.Title,
            process.Description,
            process.BeginDate,
            process.IdTag,
            process.EndDate
        });

        // If more than 0 rows modified, was successful
        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(PFProcess process)
    {
        const string query =
            "DELETE FROM Process WHERE Id_PR='@Id';";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(process.Id);

        // If more than 0 rows modified, was successful
        return (await _connection.ExecuteAsync(query, dynamicParameters) > 0);
    }
}