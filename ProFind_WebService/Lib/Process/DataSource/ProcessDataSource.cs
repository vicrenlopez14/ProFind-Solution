using Dapper;
using MySql.Data.MySqlClient;
using ProFind_WebService.Lib.DataSource;
using ProFind_WebService.Lib.Process.Model;

namespace ProFind_WebService.Lib.Process.DataSource;

public class ProcessDataSource
{
    readonly MySqlConnection connection;

    public ProcessDataSource()
    {
        connection = new MySqlDataSourceLink().getConnection();
    }

    public async Task<PFProcess> Get(string id)
    {
        // NOTE: Process is supposed to have already an Id at this point.

        const string query =
            "SELECT Process.Id_P," +
            "Process.Title_P," +
            "Process.Description_P," +
            "Process.Begin_Date_P," +
            "Process.End_Date_P," +
            "Process.Id_T1," +
            "Name_T," +
            "Id_PJ1," +
            "Title_PJ," +
            "Description_PJ FROM Process " +
            "INNER JOIN Tag T on Process.Id_T1 = T.Id_T " +
            "INNER JOIN Project P on T.Id_PJ1 = P.Id_PJ" +
            "WHERE Process.Id_P = ':Id' " +
            "LIMIT 1;";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            Id = id
        });

        var result = await connection.QueryAsync(query, dynamicParameters);
        var firstRow = result.First() as IDictionary<string, object>;

        return PFProcess.FromDictionary(firstRow);
    }

    public async Task<bool> Create(PFProcess process)
    {
        // NOTE: Process is supposed to have already an Id at this point.

        const string query =
            "INSERT INTO Process (Id_P, Title_P, Description_P, Begin_Date_P, Id_T1, End_Date_P) VALUES (':Id', ':Title', ':Description', ':BeginDate', ':IdTag', ':EndDate');";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            process.Id,
            process.Title,
            process.Description,
            process.BeginDate,
            IdTag = process.PfTag.Id,
            process.EndDate
        });

        // If more than 0 rows modified, was successful
        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Update(PFProcess process)
    {
        const string query =
            "UPDATE Process SET Title_P=':Title', Description_P=':Description', Begin_Date_P=':BeginDate', Id_T1=':IdTag', End_Date_P=':EndDate' WHERE Id_P=':Id';";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.AddDynamicParams(new
        {
            Id = process.Id,
            Title = process.Title,
            Description = process.Description,
            BeginDate = process.BeginDate,
            IdTag = process.PfTag.Id,
            EndDate = process.EndDate
        });

        // If more than 0 rows modified, was successful
        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }

    public async Task<bool> Delete(PFProcess process)
    {
        const string query =
            "DELETE FROM Process WHERE Id_P=':Id';";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(process.Id);

        // If more than 0 rows modified, was successful
        return (await connection.ExecuteAsync(query, dynamicParameters) > 0);
    }
}